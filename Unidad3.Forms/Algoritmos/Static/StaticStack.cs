namespace Lineales.Core.Static;

/// <summary>Pila LIFO con un único arreglo de capacidad fija.</summary>
public sealed class StaticStack<T>
{
    private readonly T[] items;
    private int top = -1; // Índice del último elemento ocupado.
    public StaticStack(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        items = new T[capacity];
    }
    public int Capacity => items.Length;
    public int Count => top + 1;
    public int Top => top;
    public bool IsEmpty() => top == -1;
    public bool IsFull() => top == Capacity - 1;
    // Alias exactos del enunciado, además de la convención C#.
    public bool isEmpty() => IsEmpty();
    public bool isFull() => IsFull();
    public void Push(T value)
    {
        if (IsFull()) throw new InvalidOperationException("Pila llena.");
        items[++top] = value;
    }
    public T Peek()
    {
        if (IsEmpty()) throw new InvalidOperationException("Pila vacía.");
        return items[top];
    }
    public T Pop()
    {
        T value = Peek(); // Valida antes de modificar.
        items[top--] = default!; // Libera referencias, no la capacidad.
        return value;
    }
    /// <summary>Copia para mostrar; orden desde la cima hacia la base.</summary>
    public T[] ToArray()
    {
        var result = new T[Count];
        for (int i = 0; i < result.Length; i++) result[i] = items[top - i];
        return result;
    }
}
