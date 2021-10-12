using System.Collections.ObjectModel;
using CustomMap.Models;
using CustomMap.Services;
using Xamarin.Forms;

namespace CustomMap.ViewModels
{
    public class LocationListViewModel
    {
        public ObservableCollection<Location> Locations { get; }

        private readonly ILocationService _locationService;
        
        public Command ChangeColorBlueCommand => new Command<Location>(location => ChangeToColor(location, PinColor.Blue));
        public Command ChangeColorRedCommand => new Command<Location>(location => ChangeToColor(location, PinColor.Red));
        public Command ChangeColorBlackCommand => new Command<Location>(location => ChangeToColor(location, PinColor.Black));
        public Command ChangeColorGreenCommand => new Command<Location>(location => ChangeToColor(location, PinColor.Green));

        private void ChangeToColor(Location location, PinColor pinColor)
        {
            location.SetPinIcon(pinColor);
        }

        public LocationListViewModel(ILocationService locationService)
        {
            _locationService = locationService; 
            Locations = _locationService.LocationList;
        }
    }
}