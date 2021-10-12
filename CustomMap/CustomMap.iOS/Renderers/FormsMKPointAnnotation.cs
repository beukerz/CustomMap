using CoreGraphics;
using CoreLocation;
using CustomMap.Map;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CustomMap.iOS.Renderers
{
    internal class FormsMKPointAnnotation : MKPointAnnotation
    {
        public readonly Pin Pin;

        public FormsMKPointAnnotation(Pin pin) : base()
        {
            Pin = pin;

            Title = pin.Label;
            Subtitle = pin.Address ?? string.Empty;
            Coordinate = new CLLocationCoordinate2D(pin.Position.Latitude, pin.Position.Longitude);
        }

        public UIColor TintColor => Pin.TintColor.ToUIColor();
        public CGPoint Anchor => new CGPoint(Pin.Anchor.X, Pin.Anchor.Y);
        public int ZIndex => Pin.ZIndex;
        public ImageSource ImageSource => Pin.ImageSource;
    }
}