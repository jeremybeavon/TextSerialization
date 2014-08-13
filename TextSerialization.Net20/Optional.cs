namespace TextSerialization
{
    public sealed class Optional<T>
        where T : class
    {
        public Optional(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
