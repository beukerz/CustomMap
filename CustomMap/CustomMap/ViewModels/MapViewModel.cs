using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CustomMap.Annotations;
using CustomMap.Map;
using CustomMap.Models;
using CustomMap.Services;
using CustomMap.Views;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using PinClickedEventArgs = CustomMap.Map.PinClickedEventArgs;

namespace CustomMap.ViewModels
{
    public class MapViewModel : INotifyPropertyChanged
    {
        // Services
        private readonly ILocationService _locationService;
        private readonly IPageService _pageService;
        
        public ReadOnlyObservableCollection<Location> Locations { get; }
        public string Title { get; set; } = "Welcome to the custom map page";
        
        
        // Color selection
        private Color _redButtonBorderColor;
        public Color RedButtonBorderColor
        {
            get => _redButtonBorderColor;
            set
            {
                if (value != _redButtonBorderColor)
                {
                    _redButtonBorderColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool RedSelected { get; set; }

        private Color _blueButtonBorderColor;
        public Color BlueButtonBorderColor
        {
            get => _blueButtonBorderColor;
            set
            {
                if (value != _blueButtonBorderColor)
                {
                    _blueButtonBorderColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool BlueSelected { get; set; }    
        
        private Color _greenButtonBorderColor;
        public Color GreenButtonBorderColor
        {
            get => _greenButtonBorderColor;
            set
            {
                if (value != _greenButtonBorderColor)
                {
                    _greenButtonBorderColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool GreenSelected { get; set; }  
        
        private Color _blackButtonBorderColor;
        public Color BlackButtonBorderColor
        {
            get => _blackButtonBorderColor;
            set
            {
                if (value != _blackButtonBorderColor)
                {
                    _blackButtonBorderColor = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool BlackSelected { get; set; }  
        
        private Color SelectedBorderColor => Color.Black;
        private Color DeSelectedBorderColor => Color.White;
        
        
        // Commands
        public Command PinClickedCommand => new Command<PinClickedEventArgs>(async (args) => await PinClicked(args));
        public Command SelectedPinChangedCommand => new Command<MapSelectedPinChangedArgs>(SelectedPinChanged);
        public Command MapClickedCommand => new Command<MapClickedEventArgs>(async args => await MapClicked(args));
        public Command FilterCommand => new Command<string>(FilterColors);

        
        public MapViewModel(ILocationService locationService, IPageService pageService)
        {
            _locationService = locationService;
            _pageService = pageService;
            Locations = _locationService.LocationList;
            RedButtonBorderColor = DeSelectedBorderColor;
            BlueButtonBorderColor = DeSelectedBorderColor;
            GreenButtonBorderColor = DeSelectedBorderColor;
            BlackButtonBorderColor = DeSelectedBorderColor;
        }

        private void FilterColors(string color)
        {
            switch (color)
            {
                case "Red":
                    RedButtonBorderColor = RedSelected ? DeSelectedBorderColor : SelectedBorderColor;
                    RedSelected = !RedSelected;
                    break;
                case "Green":
                    GreenButtonBorderColor = GreenSelected ? DeSelectedBorderColor : SelectedBorderColor;
                    GreenSelected = !GreenSelected;
                    break;
                case "Blue":
                    BlueButtonBorderColor = BlueSelected ? DeSelectedBorderColor : SelectedBorderColor;
                    BlueSelected = !BlueSelected;
                    break;
                case "Black":
                    BlackButtonBorderColor = BlackSelected ? DeSelectedBorderColor : SelectedBorderColor;
                    BlackSelected = !BlackSelected;
                    break;
            }
        }

        private async Task MapClicked(MapClickedEventArgs args)
        {
            var popup = new AddLocationPopupView(args.Position);
            await _pageService.ShowPopupAsync(popup);
        }

        private async Task PinClicked(PinClickedEventArgs args)
        {
            if (args.CustomPin.BindingContext is Location location)
            {
                location.MarkSelected();
                var popup = new EditLocationPopupView(location);
                await _pageService.ShowPopupAsync(popup);
                location?.MarkDeselected();
            }
        }

        private void OnPopupDismissed(object sender, PopupDismissedEventArgs e)
        {
            foreach (var location in Locations.Where(x => x.IsSelected))
            {
                location.MarkDeselected();
            }

            if (sender is EditLocationPopupView popup) popup.Dismissed -= OnPopupDismissed;
        }

        private void SelectedPinChanged(MapSelectedPinChangedArgs args)
        {
            if (args.OldValue == null) return;
            var oldLocation = args.OldValue.BindingContext as Location;
            oldLocation?.MarkDeselected();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}