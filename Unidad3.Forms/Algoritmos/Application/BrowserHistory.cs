using Lineales.Core.Dynamic;
namespace Lineales.Core.Application;

/// <summary>Historial simulado: no abre sitios ni realiza conexiones de red.</summary>
public sealed class BrowserHistory
{
    private readonly LinkedStack<string> back = new();
    private readonly LinkedStack<string> forward = new();
    public string Current { get; private set; } = "Inicio";
    public int BackCount => back.Count;
    public int ForwardCount => forward.Count;
    public bool CanBack => !back.IsEmpty;
    public bool CanForward => !forward.IsEmpty;
    public void Visit(string page)
    {
        if (string.IsNullOrWhiteSpace(page))
            throw new ArgumentException("Escribe una página o dirección.", nameof(page));
        page = page.Trim();
        if (page.Length > 200)
            throw new ArgumentException("Máximo 200 caracteres.", nameof(page));
        back.Push(Current);
        Current = page;
        forward.Clear(); // Una visita nueva descarta la rama de avance.
    }
    public bool Back()
    {
        if (!CanBack) return false;
        forward.Push(Current);
        Current = back.Pop();
        return true;
    }
    public bool Forward()
    {
        if (!CanForward) return false;
        back.Push(Current);
        Current = forward.Pop();
        return true;
    }
    public void Reset()
    {
        back.Clear(); forward.Clear(); Current = "Inicio";
    }
    public string[] BackPages() => back.ToArray();
    public string[] ForwardPages() => forward.ToArray();
}
