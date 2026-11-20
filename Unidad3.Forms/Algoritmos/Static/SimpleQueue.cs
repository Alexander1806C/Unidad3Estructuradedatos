namespace Lineales.Core.Static;

/// <summary>Cola FIFO lineal, sin desplazar ni envolver índices.
/// Al vaciarla reinicia los índices; mientras tanto no reutiliza el prefijo.</summary>
public sealed class SimpleQueue<T>
{
    private readonly T[] items;
    private int front = 0;
    private int rear = -1; // Último índice insertado, no próxima posición.
    public SimpleQueue(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        items = new T[capacity];
    }
    public int Capacity => items.Length;
    public int Count => rear - front + 1;
    public int Front => front;
    public int Rear => rear;
    public bool IsEmpty() => Count == 0;
    // Llena para insertar: puede quedar espacio inutilizable al principio.
    public bool IsFull() => rear == Capacity - 1;
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();
    public void Enqueue(T value)
    {
        if (IsFull()) throw new InvalidOperationException(
            "Sin espacio al final; la cola simple no reutiliza el prefijo hasta vaciarse.");
        items[++rear] = value;
    }
    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("Cola vacía.");
        return items[front];
    }
    public T Dequeue()
    {
        T value = Peek();
        items[front++] = default!;
        if (front > rear) { front = 0; rear = -1; }
        return value;
    }
    public T[] ToArray()
    {
        var result = new T[Count];
        for (int i = 0; i < result.Length; i++) result[i] = items[front + i];
        return result;
    }
}
