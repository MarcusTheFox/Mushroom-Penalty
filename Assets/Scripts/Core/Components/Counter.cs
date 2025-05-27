namespace Core.Components
{
    public class Counter
    {
        public int Value { get; private set; }
        
        public void Increase() => Value++;
        public void Reset() => Value = 0;
    }
}