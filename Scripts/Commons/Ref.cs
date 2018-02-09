namespace Commons
{
    public class Ref<T>
    {
        public T Value;

        public Ref(T value)
        {
            Value = value;
        }

        public Ref()
        {
        }

        public override string ToString()
        {
            return (Value == null) ? "" : string.Format("{0}", Value);
        }
    }
}