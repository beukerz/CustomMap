using CustomMap.Services;
using CustomMap.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace CustomMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLocationPopupView : Popup
    {
        public AddLocationPopupView(Position position)
        {
            BindingContext = new AddLocationPopupViewModel(position, Startup.ServiceProvider.GetService<ILocationService>(), this);
            InitializeComponent();
        }
    }
}