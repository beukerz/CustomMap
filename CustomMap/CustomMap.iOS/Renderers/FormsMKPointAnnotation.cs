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
        public readonly CustomPin CustomPin;

        public FormsMKPointAnnotation(CustomPin customPin) : base()
        {
            CustomPin = customPin;
            
            Title = customPin.Label;
            Subtitle = customPin.Address ?? string.Empty;
            Coordinate = new CLLocationCoordinate2D(customPin.Position.Latitude, customPin.Position.Longitude);
        }

        public UIColor TintColor => CustomPin.TintColor.ToUIColor();
        public CGPoint Anchor => new CGPoint(CustomPin.Anchor.X, CustomPin.Anchor.Y);
        public int ZIndex => CustomPin.ZIndex;
        public ImageSource ImageSource => CustomPin.ImageSource;
    }
}