using System;
using CustomMap.Map;
using CustomMap.Models;
using CustomMap.Services;
using Xamarin.Forms;

namespace CustomMap.ViewModels
{
    public class MapViewModel
    {
        public ObservableRangeCollection<Location> Locations { get; }
    
        public string Title { get; set; } = "Welcome to the custom map page";
        public Command PinClickedCommand => new Command<PinClickedEventArgs>(PinClicked);

        private void PinClicked(PinClickedEventArgs args)
        {
            Console.WriteLine($"Pin clicked");
        }

        private readonly ILocationService _locationService;
        
        public MapViewModel(ILocationService locationService)
        {
            _locationService = locationService;
            Locations = _locationService.LocationList;
        }
    }
}