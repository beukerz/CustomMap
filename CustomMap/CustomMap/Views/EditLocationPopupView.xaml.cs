using CustomMap.Models;
using CustomMap.ViewModels;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace CustomMap.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinClickedPopupView : Popup
    {
        public PinClickedPopupView(Location location)
        {
            var viewModel = new EditLocationPopupViewModel(location);
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}