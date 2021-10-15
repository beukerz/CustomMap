using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace CustomMap.Services
{
    public interface IPageService
    {
        Task PushAsync(Page page);
        Task<object> ShowPopupAsync(Popup popup);
        Task<bool> DisplayAlert(string title, string message, string ok, string cancel);
    }

    public class PageService : IPageService
    {
        private Page MainPage => Application.Current.MainPage;

        public async Task PushAsync(Page page)
        {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<object> ShowPopupAsync(Popup popup)
        {
            return await MainPage.ShowPopupAsync(popup);
        }

        public async Task<bool> DisplayAlert(string title, string message, string ok, string cancel)
        {
            return await MainPage.DisplayAlert(title, message, ok, cancel);
        }
    }
}