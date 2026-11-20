namespace Lineales.Core.Dynamic;

/// <summary>Lista simplemente enlazada propia, con cabeza, cola y nodos dinámicos.</summary>
public sealed class SinglyLinkedList<T>
{
    private sealed class Node
    {
        public T Value;
        public Node? Next;
        public Node(T value) { Value = value; }
    }
    private Node? head, tail;
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;
    public void AddFirst(T value)
    {
        var node = new Node(value) { Next = head };
        head = node;
        if (tail is null) tail = node;
        Count++;
    }
    public void AddLast(T value)
    {
        var node = new Node(value);
        if (tail is null) head = node;
        else tail.Next = node;
        tail = node;
        Count++;
    }
    public T PeekFirst()
    {
        if (head is null) throw new InvalidOperationException("Lista vacía.");
        return head.Value;
    }
    public T RemoveFirst()
    {
        T value = PeekFirst();
        head = head!.Next;
        Count--;
        if (head is null) tail = null;
        return value;
    }
    /// <summary>Elimina la primera coincidencia; buscar cuesta O(n).</summary>
    public bool Remove(T value)
    {
        Node? previous = null;
        Node? current = head;
        while (current is not null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, value))
            {
                if (previous is null) head = current.Next;
                else previous.Next = current.Next;
                if (current == tail) tail = previous;
                Count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }
    public int IndexOf(T value)
    {
        int index = 0;
        for (Node? node = head; node is not null; node = node.Next, index++)
            if (EqualityComparer<T>.Default.Equals(node.Value, value)) return index;
        return -1;
    }
    public T GetAt(int index)
    {
        if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException(nameof(index));
        Node? node = head;
        for (int i = 0; i < index; i++) node = node!.Next;
        return node!.Value;
    }
    public void Clear() { head = tail = null; Count = 0; }
    public T[] ToArray()
    {
        var result = new T[Count];
        int i = 0;
        for (Node? node = head; node is not null; node = node.Next)
            result[i++] = node.Value;
        return result;
    }
}
