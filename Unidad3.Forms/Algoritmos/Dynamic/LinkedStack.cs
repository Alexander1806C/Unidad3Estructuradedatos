namespace Lineales.Core.Dynamic;

/// <summary>Pila LIFO dinámica basada en la lista propia; no usa Stack de .NET.</summary>
public sealed class LinkedStack<T>
{
    private readonly SinglyLinkedList<T> list = new();
    public int Count => list.Count;
    public bool IsEmpty => list.IsEmpty;
    public void Push(T value) => list.AddFirst(value);
    public T Pop() => list.RemoveFirst();
    public T Peek() => list.PeekFirst();
    public void Clear() => list.Clear();
    public T[] ToArray() => list.ToArray(); // Cima primero.
}
