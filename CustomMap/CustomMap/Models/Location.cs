using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CustomMap.Annotations;
using CustomMap.Map;

namespace CustomMap.Models
{
    
    public class Location : INotifyPropertyChanged
    {
        private string _pinIcon;

        public Location(Position position, string address, string label, bool isCommercial, PinColor pinColor)
        {
            Position = position;
            Address = address;
            Label = label;
            IsCommercial = isCommercial;
            SetPinIcon(pinColor);
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Position Position { get; }
        public string Address { get; }
        public string Label { get; }


        public string PinIcon
        {
            get => _pinIcon;
            set
            {
                if (value != this._pinIcon)
                {
                    _pinIcon = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsCommercial { get; }
        public bool IsSelected { get; private set; }

        public string GetPinIcon => GetPinIconHelper();

        private string GetPinIconHelper()
        {
            if (IsSelected && IsCommercial)
                return "kzm_yellow.png";
            
            if (IsSelected && !IsCommercial)
                return "cs_yellow.png";

            return PinIcon;
        }

        public void MarkSelected()
        {
            IsSelected = true;
        }

        public void MarkDeselected()
        {
            IsSelected = false;
        }

        public void SetPinIcon(PinColor pinColor)
        {
            switch (pinColor)
            {
                case PinColor.Black:
                    PinIcon = IsCommercial ? "kzm_black.png" : "cs_black.png";
                    break;
                case PinColor.Blue:
                    PinIcon = IsCommercial ? "kzm_blue.png" : "cs_blue.png";
                    break;
                case PinColor.Green:
                    PinIcon = IsCommercial ? "kzm_green.png" : "cs_green.png";
                    break;
                case PinColor.Red:
                    PinIcon = IsCommercial ? "kzm_red.png" : "cs_red.png";
                    break;
                case PinColor.Yellow:
                    PinIcon = IsCommercial ? "kzm_yellow.png" : "cs_yellow.png";
                    break;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}