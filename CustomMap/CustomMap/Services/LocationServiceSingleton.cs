// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Collections.ObjectModel;
// using CustomMap.Models;
// using Xamarin.Forms.Maps;
//
// namespace CustomMap.Services
// {
//     public sealed class LocationServiceSingleton
//     {
//         private static readonly LocationServiceSingleton _instance = new LocationServiceSingleton();
//         private static readonly object padlock = new object();
//
//         static LocationServiceSingleton()
//         {
//             
//         }
//         
//         public static LocationServiceSingleton Instance
//         {
//             get
//             {
//                 Console.WriteLine("Instance called.");
//                 if (_instance == null)
//                 {
//                     lock (padlock)
//                     {
//                         if (_instance == null)
//                         {
//                             _instance = new LocationServiceSingleton();
//                         }
//                     }
//                 }
//                 return _instance;
//             }
//         }
//
//         private LocationServiceSingleton()
//         {
//             Console.WriteLine("Constructor was called.");
//         }
//         
//         readonly ObservableCollection<Location> _locations;
//         public IEnumerable Locations => _locations;
//     
//         public List<Location> GetLocations()
//         {
//             return new List<Location>
//             {
//                 new Location(new Position(52.75305, 4.79815), "Middenweg 26, 1756 EA, Dirkshorn", "Mom", false, PinColor.Blue),
//                 new Location(new Position(52.75399, 4.79825), "Middenweg 30, 1756 EA, Dirkshorn", "Neighbour", false, PinColor.Green),
//                 new Location(new Position(52.75500, 4.79500), "Middenweg 67, 1756 EA, Dirkshorn", "Piet", false, PinColor.Red),
//                 new Location(new Position(52.75600, 4.79600), "Middenweg 89, 1756 EA, Dirkshorn", "Jan", true, PinColor.Blue),
//                 new Location(new Position(52.75100, 4.79100), "Middenweg 11, 1756 EA, Dirkshorn", "Eva", false, PinColor.Green),
//                 new Location(new Position(52.75300, 4.79300), "Middenweg 2, 1756 EA, Dirkshorn", "Marleen", true, PinColor.Green),
//                 new Location(new Position(52.75200, 4.79200), "Middenweg 4, 1756 EA, Dirkshorn", "Seth", false, PinColor.Black)
//             };
//         }
//     
//     
//     }
// }
