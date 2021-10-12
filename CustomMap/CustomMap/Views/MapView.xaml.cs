using CustomMap.Map;
using CustomMap.ViewModels;
using Xamarin.Forms;
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
            var mapSpan = MapSpan.FromCenterAndRadius(new Position(52.75305, 4.79815), Distance.FromKilometers(1));
            MyCustomMap.MoveToRegion(mapSpan);
        }
    }
}