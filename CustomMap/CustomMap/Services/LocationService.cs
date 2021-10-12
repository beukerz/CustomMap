using CustomMap.Map;
using CustomMap.Models;

namespace CustomMap.Services
{
    public interface ILocationService
    {
        ObservableRangeCollection<Location> LocationList { get; }
    }
    
    public class LocationService : ILocationService
    {
        private readonly ObservableRangeCollection<Location> _locations;

        public LocationService()
        {
            _locations = new ObservableRangeCollection<Location>();
            Init();
        }
        private void Init()
        {
            _locations.Add(new Location(new Position(52.75305, 4.79815), "Middenweg 26, 1756 EA, Dirkshorn", "Mom",
                false, PinColor.Blue));
            _locations.Add(new Location(new Position(52.75399, 4.79825), "Middenweg 30, 1756 EA, Dirkshorn", "Neighbour", 
                false, PinColor.Green));
            _locations.Add(new Location(new Position(52.75500, 4.79500), "Middenweg 67, 1756 EA, Dirkshorn", "Piet",
                false, PinColor.Red));
            _locations.Add(new Location(new Position(52.75600, 4.79600), "Middenweg 89, 1756 EA, Dirkshorn", "Jan",
                true, PinColor.Blue)); 
            _locations.Add(new Location(new Position(52.75100, 4.79100), "Middenweg 11, 1756 EA, Dirkshorn", "Eva",
                false, PinColor.Green));
            _locations.Add(new Location(new Position(52.75300, 4.79300), "Middenweg 2, 1756 EA, Dirkshorn", "Marleen",
                true, PinColor.Green));
            _locations.Add(new Location(new Position(52.75200, 4.79200), "Middenweg 4, 1756 EA, Dirkshorn", "Seth",
                false, PinColor.Black));
        }

        public ObservableRangeCollection<Location> LocationList => _locations;
    }
}