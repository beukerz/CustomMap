using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CustomMap.Models;
using CustomMap.Services;
using CustomMap.Views;
using Xamarin.Forms;

namespace CustomMap.ViewModels
{
    public class LocationListViewModel
    {
        public ReadOnlyObservableCollection<Location> Locations { get; }

        private readonly ILocationService _locationService;
        private readonly IPageService _pageService;
        
        public Command RemoveLocationCommand => new Command<Location>(RemoveLocation);
        public Command ItemSelectedCommand => new Command<Location>(async location => await ItemSelected(location));

        public LocationListViewModel(ILocationService locationService, IPageService pageService)
        {
            _locationService = locationService;
            _pageService = pageService;
            Locations = _locationService.LocationList;
        }
        
        private async Task ItemSelected(Location location)
        {
            await _pageService.ShowPopupAsync(new EditLocationPopupView(location));            
        }

        private void RemoveLocation(Location location)
        {
            _locationService.RemoveLocation(location);
        }
    }
}