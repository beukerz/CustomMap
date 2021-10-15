using System;

namespace CustomMap.Map
{
    public class PinClickedEventArgs : EventArgs
    {
        public CustomPin CustomPin { get; }
        public bool HideInfoWindow { get; set; }

        public PinClickedEventArgs(CustomPin customPin)
        {
            CustomPin = customPin;
            HideInfoWindow = false;
        }
    }
}