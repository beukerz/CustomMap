using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace CustomMap.Map
{
    public class CustomMap : View, IEnumerable<CustomPin>
    {
        public const string MoveToRegionMessageName = "MapMoveToRegion";

        public static readonly BindableProperty MapThemeProperty =
            BindableProperty.Create(nameof(MapTheme), typeof(MapTheme), typeof(CustomMap), MapTheme.System);

        public static readonly BindableProperty MapTypeProperty =
            BindableProperty.Create(nameof(MapType), typeof(MapType), typeof(CustomMap), default(MapType));

        public static readonly BindableProperty IsShowingUserProperty =
            BindableProperty.Create(nameof(IsShowingUser), typeof(bool), typeof(CustomMap), default(bool));

        public static readonly BindableProperty ShowUserLocationButtonProperty =
            BindableProperty.Create(nameof(ShowUserLocationButton), typeof(bool), typeof(CustomMap), default(bool));

        public static readonly BindableProperty ShowCompassProperty =
            BindableProperty.Create(nameof(ShowCompass), typeof(bool), typeof(CustomMap), true);

        public static readonly BindableProperty SelectedPinProperty =
            BindableProperty.Create(nameof(SelectedCustomPin), typeof(CustomPin), typeof(CustomMap), default(CustomPin),
                propertyChanged: (b, o, n) => OnSelectedPinChanged((CustomMap)b, o as CustomPin, n as CustomPin));

        public static readonly BindableProperty TrafficEnabledProperty =
            BindableProperty.Create(nameof(TrafficEnabled), typeof(bool), typeof(CustomMap), default(bool));

        public static readonly BindableProperty HasScrollEnabledProperty =
            BindableProperty.Create(nameof(HasScrollEnabled), typeof(bool), typeof(CustomMap), true);

        public static readonly BindableProperty HasZoomEnabledProperty =
            BindableProperty.Create(nameof(HasZoomEnabled), typeof(bool), typeof(CustomMap), true);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(CustomMap), default(IEnumerable),
                propertyChanged: (b, o, n) => ((CustomMap)b).OnItemsSourcePropertyChanged((IEnumerable)o, (IEnumerable)n));

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(CustomMap), default(DataTemplate),
                propertyChanged: (b, o, n) => ((CustomMap)b).OnItemTemplatePropertyChanged((DataTemplate)o, (DataTemplate)n));

        public static readonly BindableProperty ItemTemplateSelectorProperty =
            BindableProperty.Create(nameof(ItemTemplateSelector), typeof(DataTemplateSelector), typeof(CustomMap), default(DataTemplateSelector),
                propertyChanged: (b, o, n) => ((CustomMap)b).OnItemTemplateSelectorPropertyChanged());

        public static readonly BindableProperty MoveToLastRegionOnLayoutChangeProperty =
            BindableProperty.Create(nameof(MoveToLastRegionOnLayoutChange), typeof(bool), typeof(CustomMap), defaultValue: false);

        private readonly ObservableRangeCollection<CustomPin> _pins = new ObservableRangeCollection<CustomPin>();
        private readonly ObservableRangeCollection<MapElement> _mapElements = new ObservableRangeCollection<MapElement>();
        private MapSpan _visibleRegion;

        public CustomMap(MapSpan region)
        {
            LastMoveToRegion = region;
            VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        // center on Hoekenrode by default
        public CustomMap() : this(new MapSpan(new Position(52.31263774649747, 4.949810556257354), 0.1, 0.1))
        {
        }

        public bool HasScrollEnabled
        {
            get => (bool)GetValue(HasScrollEnabledProperty);
            set => SetValue(HasScrollEnabledProperty, value);
        }

        public bool HasZoomEnabled
        {
            get => (bool)GetValue(HasZoomEnabledProperty);
            set => SetValue(HasZoomEnabledProperty, value);
        }
        public bool IsShowingUser
        {
            get => (bool)GetValue(IsShowingUserProperty);
            set => SetValue(IsShowingUserProperty, value);
        }

        public bool ShowUserLocationButton
        {
            get => (bool)GetValue(ShowUserLocationButtonProperty);
            set => SetValue(ShowUserLocationButtonProperty, value);
        }

        public bool ShowCompass
        {
            get => (bool)GetValue(ShowCompassProperty);
            set => SetValue(ShowCompassProperty, value);
        }

        public CustomPin SelectedCustomPin
        {
            get => (CustomPin)GetValue(SelectedPinProperty);
            set => SetValue(SelectedPinProperty, value);
        }

        public bool TrafficEnabled
        {
            get => (bool)GetValue(TrafficEnabledProperty);
            set => SetValue(TrafficEnabledProperty, value);
        }

        public MapTheme MapTheme
        {
            get => (MapTheme)GetValue(MapThemeProperty);
            set => SetValue(MapThemeProperty, value);
        }

        public MapType MapType
        {
            get => (MapType)GetValue(MapTypeProperty);
            set => SetValue(MapTypeProperty, value);
        }

        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public DataTemplateSelector ItemTemplateSelector
        {
            get => (DataTemplateSelector)GetValue(ItemTemplateSelectorProperty);
            set => SetValue(ItemTemplateSelectorProperty, value);
        }

        public bool MoveToLastRegionOnLayoutChange
        {
            get => (bool)GetValue(MoveToLastRegionOnLayoutChangeProperty);
            set => SetValue(MoveToLastRegionOnLayoutChangeProperty, value);
        }

        public ObservableCollection<CustomPin> Pins => _pins;
        public ObservableCollection<MapElement> MapElements => _mapElements;

        public event EventHandler<MapClickedEventArgs> MapClicked;
        public event EventHandler<MapSelectedPinChangedArgs> SelectedPinChanged;
        public event EventHandler<PinClickedEventArgs> PinClicked;
        public event EventHandler<PinClickedEventArgs> InfoWindowClicked;
        public event EventHandler<PinClickedEventArgs> InfoWindowLongClicked;

        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool CanSendMapClicked() => MapClicked != null;
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SendMapClicked(Position position) => MapClicked?.Invoke(this, new MapClickedEventArgs(position));

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SendPinClick(CustomPin customPin)
        {
            var args = new PinClickedEventArgs(customPin);
            PinClicked?.Invoke(this, args);
            return args.HideInfoWindow;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SendInfoWindowClick(CustomPin customPin)
        {
            var args = new PinClickedEventArgs(customPin);
            InfoWindowClicked?.Invoke(this, args);
            return args.HideInfoWindow;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool SendInfoWindowLongClick(CustomPin customPin)
        {
            var args = new PinClickedEventArgs(customPin);
            InfoWindowLongClicked?.Invoke(this, args);
            return args.HideInfoWindow;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetVisibleRegion(MapSpan value) => VisibleRegion = value;

        public MapSpan VisibleRegion
        {
            get => _visibleRegion;
            internal set
            {
                if (_visibleRegion == value)
                    return;
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                OnPropertyChanging();
                _visibleRegion = value;
                OnPropertyChanged();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public MapSpan LastMoveToRegion { get; private set; }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<CustomPin> GetEnumerator() => _pins.GetEnumerator();

        public void MoveToRegion(MapSpan mapSpan)
        {
            LastMoveToRegion = mapSpan ?? throw new ArgumentNullException(nameof(mapSpan));
            MessagingCenter.Send(this, MoveToRegionMessageName, mapSpan);
        }

        private void OnItemsSourcePropertyChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            if (oldItemsSource is INotifyCollectionChanged ncc)
                ncc.CollectionChanged -= OnItemsSourceCollectionChanged;
            if (newItemsSource is INotifyCollectionChanged ncc1)
                ncc1.CollectionChanged += OnItemsSourceCollectionChanged;

            _pins.Clear();
            CreatePinItems();
        }

        private void OnItemTemplatePropertyChanged(DataTemplate oldItemTemplate, DataTemplate newItemTemplate)
        {
            if (newItemTemplate is DataTemplateSelector)
            {
                throw new NotSupportedException(
                    $"The {nameof(CustomMap)}.{ItemTemplateProperty.PropertyName} property only supports {nameof(DataTemplate)}." +
                    $" Set the {nameof(CustomMap)}.{ItemTemplateSelectorProperty.PropertyName} property instead to use a {nameof(DataTemplateSelector)}");
            }

            _pins.Clear();
            CreatePinItems();
        }

        private void OnItemTemplateSelectorPropertyChanged()
        {
            _pins.Clear();
            CreatePinItems();
        }

        private void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var itemsToAdd = e.NewItems?.Cast<object>()?.Select(o => CreatePin(o))?.ToList() ?? new List<CustomPin>(0);
            var itemsToRemove = (from i in e.OldItems?.Cast<object>() ?? Enumerable.Empty<object>()
                                 join p in _pins on i equals p.BindingContext
                                 select p).ToList();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _pins.AddRange(itemsToAdd);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    _pins.RemoveRange(itemsToRemove, NotifyCollectionChangedAction.Remove);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    _pins.RemoveRange(itemsToRemove, NotifyCollectionChangedAction.Remove);
                    _pins.AddRange(itemsToAdd);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _pins.Clear();

                    var newItems = ItemsSource?.Cast<object>()?.Select(o => CreatePin(o))?.ToList() ?? new List<CustomPin>(0);
                    _pins.AddRange(newItems);
                    break;
                default:
                    break;
            }
        }

        private void CreatePinItems()
        {
            if (ItemsSource == null || (ItemTemplate == null && ItemTemplateSelector == null)) return;
            _pins.AddRange(ItemsSource.Cast<object>().Select(o => CreatePin(o)));
        }

        private CustomPin CreatePin(object newItem)
        {
            var itemTemplate = ItemTemplate;

            if (itemTemplate == null)
                itemTemplate = ItemTemplateSelector?.SelectTemplate(newItem, this);

            if (itemTemplate == null)
                return null;

            var pin = (CustomPin)itemTemplate.CreateContent();
            pin.BindingContext = newItem;

            if (pin.Label == null)
                throw new ArgumentException("Pin must have a Label to be added to a map");

            return pin;
        }

        private static void OnSelectedPinChanged(CustomMap customMap, CustomPin oldValue, CustomPin newValue)
            => customMap.SelectedPinChanged?.Invoke(customMap, new MapSelectedPinChangedArgs(oldValue, newValue));
    }
}