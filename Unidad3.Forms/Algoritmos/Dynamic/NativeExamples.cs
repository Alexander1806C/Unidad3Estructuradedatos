namespace Lineales.Core.Dynamic;

/// <summary>Demostración permitida en 3.2. No forma parte de las estructuras de 3.1.</summary>
public static class NativeExamples
{
    public static string Run()
    {
        var stack = new Stack<string>();
        stack.Push("A"); stack.Push("B");
        var queue = new Queue<string>();
        queue.Enqueue("A"); queue.Enqueue("B");
        var arrayList = new List<int>(); // Arreglo redimensionable, NO lista enlazada.
        arrayList.Add(10); arrayList.Add(20); arrayList.Insert(1, 15);
        var linked = new LinkedList<string>(); // Lista DOBLEMENTE enlazada de .NET.
        linked.AddLast("Ana"); linked.AddLast("Luis"); linked.AddFirst("Eva");
        return $"Stack<T>.Pop: {stack.Pop()} (LIFO)\n" +
            $"Queue<T>.Dequeue: {queue.Dequeue()} (FIFO)\n" +
            $"List<T>: {string.Join(", ", arrayList)}; Count={arrayList.Count}, Capacity={arrayList.Capacity}\n" +
            $"LinkedList<T>: {string.Join(", ", linked)}";
    }
}
