using CustomMap.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationListView : ContentPage
    {
        public LocationListView()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<LocationListViewModel>();
        }
    }
}