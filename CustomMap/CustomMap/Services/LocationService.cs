using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CustomMap.Models;
using Xamarin.Forms.BetterMaps;

namespace CustomMap.Services
{
    public interface ILocationService
    {
        ReadOnlyObservableCollection<Location> LocationList { get; }
        void AddLocation(Location location);
        void RemoveLocation(Location location);
        void ColorFilter(PinColor pinColor);
        void RemoveFilter(PinColor pinColor);
    }
    
    public class LocationService : ILocationService
    {
        private readonly ReadOnlyObservableCollection<Location> _locationsReadOnly;
        private readonly ObservableCollection<Location> _locations;

        public ReadOnlyObservableCollection<Location> LocationList => _locationsReadOnly;

        public LocationService()
        {
            _locations = new ObservableCollection<Location>();
            _locationsReadOnly = new ReadOnlyObservableCollection<Location>(_locations);
            Init();
        }
        private void Init()
        {
            _locations.Add(new Location(new Position(52.75305, 4.79815), "Middenweg 26, 1756 EA, Dirkshorn", "Mom",
                false, PinColor.Blue, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75399, 4.79825), "Middenweg 30, 1756 EA, Dirkshorn", "Neighbour", 
                false, PinColor.Green, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75500, 4.79500), "Middenweg 67, 1756 EA, Dirkshorn", "Piet",
                false, PinColor.Red, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75600, 4.79600), "Middenweg 89, 1756 EA, Dirkshorn", "Jan",
                true, PinColor.Blue, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75100, 4.79100), "Middenweg 11, 1756 EA, Dirkshorn", "Eva",
                false, PinColor.Green, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75300, 4.79300), "Middenweg 2, 1756 EA, Dirkshorn", "Marleen",
                true, PinColor.Green, new Dictionary<string, string>{{"someKey", "someValue"}}));
            _locations.Add(new Location(new Position(52.75200, 4.79200), "Middenweg 4, 1756 EA, Dirkshorn", "Seth",
                false, PinColor.Black, new Dictionary<string, string>{{"someKey", "someValue"}}));
        }

        public void AddLocation(Location location)
        {
            _locations.Add(location);
        }

        public void RemoveLocation(Location location)
        {
            _locations.Remove(location);
        }

        public void ColorFilter(PinColor pinColor)
        {
            _locations.Where(x => x.DisplayedPinIcon == "cs_red.png" || x.DisplayedPinIcon == "kzm_red.png");
        }

        public void RemoveFilter(PinColor pinColor)
        {
            // _locations = allLocations;
        }
    }
}