using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CustomMap.Annotations;
using Xamarin.Forms.Maps;

namespace CustomMap.Models
{
    
    public class Location : INotifyPropertyChanged
    {
        public Location(Position position, string address, string label, bool isCommercial, PinColor pinColor, Dictionary<string, string> tags)
        {
            Position = position;
            Address = address;
            Label = String.Empty;
            IsCommercial = isCommercial;
            Tags = tags;
            SetPinIcon(pinColor);
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Position Position { get; }
        public string Address { get; }
        public string Label { get; }

        public Dictionary<string, string> Tags { get; }

        private string _displayedPinIcon;
        public string DisplayedPinIcon
        {
            get => _displayedPinIcon;
            private set
            {
                if (value != this._displayedPinIcon)
                {
                    _displayedPinIcon = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private string PinIcon { get; set; }

        public bool IsCommercial { get; }
        public bool IsSelected { get; private set; }

        public void MarkSelected()
        {
            IsSelected = true;
            PinIcon = _displayedPinIcon;
            if (IsCommercial)
                DisplayedPinIcon = "kzm_yellow.png";
            if (!IsCommercial)
                DisplayedPinIcon = "cs_yellow.png";
        }

        public void MarkDeselected()
        {
            IsSelected = false;
            DisplayedPinIcon = PinIcon;
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

            DisplayedPinIcon = PinIcon;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}