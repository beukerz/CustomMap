namespace CustomMap.Models
{
    public class PinColorPicker
    {
        public string Name { get; }
        public PinColor PinColor { get; }

        public PinColorPicker(string name, PinColor pinColor)
        {
            Name = name;
            PinColor = pinColor;
        }
    }
}