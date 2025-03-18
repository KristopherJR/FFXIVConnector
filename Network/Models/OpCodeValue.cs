namespace FFXIVConnector.Network.Models
{
    public struct OpCodeValue
    {
        private readonly int _value;
        private readonly Type _enumType;

        public OpCodeValue(Enum enumValue)
        {
            _value = Convert.ToInt32(enumValue);
            _enumType = enumValue.GetType();
        }

        public int Value => _value;
        public Type EnumType => _enumType;

        public T As<T>() where T : Enum
        {
            if (_enumType != typeof(T))
                throw new InvalidCastException($"Opcode is not of type {typeof(T).Name}");

            return (T)(object)_value;
        }

        public override string ToString() => $"{_enumType.Name}.{Enum.GetName(_enumType, _value)} ({_value})";
    }
}
