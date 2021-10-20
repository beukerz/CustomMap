using System.Collections.Generic;
using CustomMap.Models;
using CustomMap.Services;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.BetterMaps;

namespace CustomMap.ViewModels
{
    public class AddLocationPopupViewModel
    {
        private readonly Position _position;
        private readonly ILocationService _locationService;
        private readonly Popup _popup;
        
        public bool IsCommercial { get; set; }
        public string Address { get; set; }

        public Command SaveLocationCommand => new Command(AddLocation);

        public AddLocationPopupViewModel(Position position, ILocationService locationService, Popup popup)
        {
            _position = position;
            _locationService = locationService;
            _popup = popup;
        }

        private void AddLocation()
        {
            var location = new Location(_position, Address, string.Empty, IsCommercial, PinColor.Blue, new Dictionary<string, string>());
            _locationService.AddLocation(location);
            _popup.Dismiss("Popup closed.");
        }
    }
}