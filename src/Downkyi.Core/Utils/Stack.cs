namespace Downkyi.Core.Utils;

public class Stack<T>
{
    private int _maxSize;
    private int _top = -1;
    private T[] _data;

    public int Count => _maxSize;
    public bool IsFull { get => _top == _maxSize - 1; }
    public bool IsEmpty { get => _top == -1; }

    public Stack(int maxSize)
    {
        _maxSize = maxSize;
        _data = new T[maxSize];
    }

    public void Push(T value)
    {
        if (IsFull)
        {
            return;
        }
        _data[++_top] = value;
    }

    public T? Pop()
    {
        if (IsEmpty)
        {
            return default;
        }

        return _data[_top--];
    }

    public T? Peek()
    {
        if (IsEmpty)
        {
            return default;
        }

        return _data[_top];
    }

    //public Stack<T>? Traverse()
    //{
    //    if (IsEmpty) { return default; }

    //    for (int i = _top; i >= 0; i--)
    //    {
    //        // TODO
    //    }
    //}

}
