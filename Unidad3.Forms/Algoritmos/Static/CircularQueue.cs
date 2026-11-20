namespace Lineales.Core.Static;

/// <summary>Cola FIFO circular de capacidad fija. Count diferencia llena y vacía.</summary>
public sealed class CircularQueue<T>
{
    private readonly T[] items;
    private int front, rear, count; // rear = próxima posición de inserción.
    public CircularQueue(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        items = new T[capacity];
    }
    public int Capacity => items.Length;
    public int Count => count;
    public int Front => front;
    public int Rear => rear;
    public bool IsEmpty() => count == 0;
    public bool IsFull() => count == Capacity;
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();
    public void Enqueue(T value)
    {
        if (IsFull()) throw new InvalidOperationException("Cola circular llena.");
        items[rear] = value;
        rear = (rear + 1) % Capacity;
        count++;
    }
    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("Cola circular vacía.");
        return items[front];
    }
    public T Dequeue()
    {
        T value = Peek();
        items[front] = default!;
        front = (front + 1) % Capacity;
        count--;
        return value;
    }
    public T[] ToArray()
    {
        var result = new T[count];
        for (int i = 0; i < count; i++)
            result[i] = items[(front + i) % Capacity];
        return result;
    }
}
