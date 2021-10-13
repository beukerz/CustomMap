using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CustomMap.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task ShowPopup(Popup popup);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
    }

    public class PageService : IPageService
    {
        public Task PushAsync(Page page)
        {
            throw new System.NotImplementedException();
        }

        public Task ShowPopup(Popup popup)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            throw new System.NotImplementedException();
        }
    }
}