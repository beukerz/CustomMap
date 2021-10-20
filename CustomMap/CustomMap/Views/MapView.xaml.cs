using System;
using CustomMap.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.BetterMaps;
using Xamarin.Forms.Xaml;

namespace CustomMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapView : ContentPage
    {
        public MapView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<MapViewModel>();
            var mapSpan = MapSpan.FromCenterAndRadius(new Position(52.75305, 4.79815), Distance.FromKilometers(10));
            MyCustomMap.MoveToRegion(mapSpan);
        }

        private void MyCustomMap_OnPinClicked(object sender, PinClickedEventArgs e)
        {
            Console.WriteLine("Pin clicked.");
        }
    }
}