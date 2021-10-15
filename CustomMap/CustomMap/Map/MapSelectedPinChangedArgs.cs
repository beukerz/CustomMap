namespace CustomMap.Map
{
    public class MapSelectedPinChangedArgs
    {
        public CustomPin OldValue { get; }
        public CustomPin NewValue { get; }

        public MapSelectedPinChangedArgs(CustomPin oldValue, CustomPin newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}