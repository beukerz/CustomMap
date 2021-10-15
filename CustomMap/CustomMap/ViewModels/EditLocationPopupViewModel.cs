using CustomMap.Models;
using Xamarin.Forms;

namespace CustomMap.ViewModels
{
    public class EditLocationPopupViewModel
    {
        private readonly Location _location;
        public string Title { get; } = "Change pin color";
        public string Address { get; }

        public Command ChangeColorBlueCommand => new Command(location => ChangeColor(PinColor.Blue));
        public Command ChangeColorRedCommand => new Command(location => ChangeColor(PinColor.Red));
        public Command ChangeColorBlackCommand => new Command(location => ChangeColor(PinColor.Black));
        public Command ChangeColorGreenCommand => new Command(location => ChangeColor(PinColor.Green));

        private void ChangeColor(PinColor pinColor)
        {
            _location.SetPinIcon(pinColor);
        }

        public EditLocationPopupViewModel(Location location)
        {
            _location = location;
            Address = location.Address;
        }
    }
}