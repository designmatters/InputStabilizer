namespace InputStabilizer
{
    public class InputStabilizer<T>(int signalThreshold)
    {
        private readonly Queue<T> _buffer = new(signalThreshold);
        private T _lastStableInput = default!;

        public T? GetStableInput(T input)
        {
            if (_buffer.Count == signalThreshold)
                _buffer.Dequeue();

            _buffer.Enqueue(input);

            if (_buffer.Count != signalThreshold || !_buffer.All(i => EqualityComparer<T>.Default.Equals(i, input)))
                return _lastStableInput;
            
            if (EqualityComparer<T>.Default.Equals(_lastStableInput, input))
                return _lastStableInput;

            _lastStableInput = input;
            _buffer.Clear();
            return input;

        }
    }
}