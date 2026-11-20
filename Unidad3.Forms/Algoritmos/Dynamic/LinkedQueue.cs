namespace Lineales.Core.Dynamic;

/// <summary>Cola FIFO dinámica: inserta en cola y retira en cabeza, ambas O(1).</summary>
public sealed class LinkedQueue<T>
{
    private readonly SinglyLinkedList<T> list = new();
    public int Count => list.Count;
    public bool IsEmpty => list.IsEmpty;
    public void Enqueue(T value) => list.AddLast(value);
    public T Dequeue() => list.RemoveFirst();
    public T Peek() => list.PeekFirst();
    public T[] ToArray() => list.ToArray(); // Frente primero.
}
