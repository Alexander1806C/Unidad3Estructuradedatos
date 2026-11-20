using Lineales.Core.Static;
using Lineales.Core.Dynamic;
using Lineales.Core.Application;

namespace Lineales.Forms;

public partial class MainForm : Form
{
    private StaticStack<int> stack = new(5);
    private SimpleQueue<int> simple = new(5);
    private CircularQueue<int> circular = new(5);
    private readonly SinglyLinkedList<int> linked = new();
    private readonly BrowserHistory history = new();
    private int activeKind;
    private int capacity = 5;

    public MainForm()
    {
        InitializeComponent();
        cmbType.SelectedIndex = 0;
        RefreshStatic(); RefreshLinked(); RefreshHistory("Inicio de sesión");
    }
    private void Guard(Action action)
    {
        try { action(); }
        catch (Exception ex) when (ex is InvalidOperationException || ex is ArgumentException)
        { MessageBox.Show(this, ex.Message, "Operación no realizada", MessageBoxButtons.OK, MessageBoxIcon.Information); }
    }
    private int ReadInteger(TextBox box)
    {
        if (!int.TryParse(box.Text, out int value))
            throw new ArgumentException("Escribe un número entero válido.");
        return value;
    }
    private void Create_Click(object? sender, EventArgs e)
    {
        capacity = (int)numCapacity.Value;
        activeKind = cmbType.SelectedIndex;
        stack = new(capacity); simple = new(capacity); circular = new(capacity);
        RefreshStatic("Estructura vacía creada.");
    }
    private void Insert_Click(object? sender, EventArgs e) => Guard(() =>
    {
        int value = ReadInteger(txtValue);
        switch(activeKind) { case 0: stack.Push(value); break; case 1: simple.Enqueue(value); break; default: circular.Enqueue(value); break; }
        RefreshStatic($"Insertado: {value}");
    });
    private void Remove_Click(object? sender, EventArgs e) => Guard(() =>
    {
        int value = activeKind switch { 0 => stack.Pop(), 1 => simple.Dequeue(), _ => circular.Dequeue() };
        RefreshStatic($"Retirado: {value}");
    });
    private void Peek_Click(object? sender, EventArgs e) => Guard(() =>
    {
        int value = activeKind switch { 0 => stack.Peek(), 1 => simple.Peek(), _ => circular.Peek() };
        RefreshStatic($"Peek: {value} (no se eliminó)");
    });
    private void RefreshStatic(string message = "Lista para probar.")
    {
        string type = activeKind switch {0 => "Pila", 1 => "Cola simple", _ => "Cola circular"};
        int count = activeKind switch {0 => stack.Count, 1 => simple.Count, _ => circular.Count};
        bool full = activeKind switch {0 => stack.IsFull(), 1 => simple.IsFull(), _ => circular.IsFull()};
        string indexes = activeKind switch {0 => $"top={stack.Top}",1 => $"front={simple.Front}, rear={simple.Rear}",_ => $"front={circular.Front}, rear={circular.Rear} (próxima inserción)"};
        lblStatic.Text = $"{type} activa | capacidad={capacity} | Count={count} | {indexes}\nIsEmpty={count == 0} | IsFull={full} | {message}";
        txtStaticNote.Text = activeKind == 1
            ? "Cola simple sin desplazamiento: no reutiliza el prefijo hasta quedar vacía. IsFull indica que no se puede insertar al final."
            : activeKind == 2 ? "Cola circular: rear señala la próxima inserción. front puede ser igual a rear tanto vacía como llena; Count distingue los casos."
            : "Pila LIFO: se inserta y se retira por top. La casilla vacía no equivale al valor cero.";
        arrayPanel.Invalidate();
    }
    private void Array_Paint(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        int[] logical = activeKind switch {0 => stack.ToArray(),1 => simple.ToArray(),_ => circular.ToArray()};
        var occupied = new bool[capacity]; var values = new int[capacity];
        for(int i=0;i<logical.Length;i++)
        {
            int index = activeKind switch {0 => stack.Top-i,1 => simple.Front+i,_ => (circular.Front+i)%capacity};
            occupied[index]=true;values[index]=logical[i];
        }
        float width = Math.Min(88,(arrayPanel.Width-40f)/capacity);
        using var font = new Font("Segoe UI",13,FontStyle.Bold);
        using var noteFont = new Font("Segoe UI",9);
        using var fill = new SolidBrush(Color.FromArgb(225,244,242));
        using var line = new Pen(Color.Teal,2);
        for(int i=0;i<capacity;i++)
        {
            float x=20+i*width;
            g.FillRectangle(occupied[i]?fill:Brushes.WhiteSmoke,x,50,width-7,62);
            g.DrawRectangle(line,x,50,width-7,62);
            string text=occupied[i]?values[i].ToString():"vacía";
            var size=g.MeasureString(text,occupied[i]?font:noteFont);
            g.DrawString(text,occupied[i]?font:noteFont,Brushes.DarkSlateGray,x+(width-7-size.Width)/2,70);
            g.DrawString($"[{i}]",noteFont,Brushes.DimGray,x+10,125);
        }
        string sequence=string.Join(", ",logical);
        g.DrawString((activeKind==0?"Cima a base: ":"Frente a final: ")+sequence,noteFont,Brushes.DarkSlateGray,20,175);
    }
    private void First_Click(object? sender, EventArgs e) => Guard(()=> { linked.AddFirst(ReadInteger(txtNode));RefreshLinked(); });
    private void Last_Click(object? sender, EventArgs e) => Guard(()=> { linked.AddLast(ReadInteger(txtNode));RefreshLinked(); });
    private void DeleteNode_Click(object? sender, EventArgs e) => Guard(()=>
    {
        bool removed=linked.Remove(ReadInteger(txtNode));RefreshLinked();
        if(!removed) MessageBox.Show(this,"El valor no está en la lista.");
    });
    private void ClearNodes_Click(object? sender, EventArgs e) {linked.Clear();RefreshLinked();}
    private void Native_Click(object? sender, EventArgs e) => txtNative.Text = NativeExamples.Run().Replace("\n",Environment.NewLine);
    private void RefreshLinked()
    {
        lblLinked.Text = $"Nodos: {linked.Count}. Cabeza a cola:";
        txtLinked.Text = linked.IsEmpty ? "head = null; tail = null" : string.Join("  →  ",linked.ToArray())+"  →  null";
    }
    private void Visit_Click(object? sender, EventArgs e) => Guard(()=> {history.Visit(txtPage.Text);RefreshHistory("Visitar: "+history.Current);});
    private void Back_Click(object? sender, EventArgs e) { if(history.Back())RefreshHistory("Atrás"); }
    private void Forward_Click(object? sender, EventArgs e) { if(history.Forward())RefreshHistory("Adelante"); }
    private void Reset_Click(object? sender, EventArgs e) {history.Reset();txtLog.Clear();RefreshHistory("Reiniciar sesión");}
    private void Example_Click(object? sender, EventArgs e)
    {
        history.Reset();txtLog.Clear();RefreshHistory("Inicio");
        foreach(string page in new[]{"A","B","C"}) { history.Visit(page);RefreshHistory("Visitar "+page); }
        history.Back();RefreshHistory("Atrás");history.Back();RefreshHistory("Atrás");
        history.Forward();RefreshHistory("Adelante");history.Visit("D");RefreshHistory("Visitar D: nueva rama");
    }
    private void RefreshHistory(string action)
    {
        lblCurrent.Text = "Página actual: "+history.Current;
        btnBack.Enabled=history.CanBack;btnForward.Enabled=history.CanForward;
        lstBack.Items.Clear();lstForward.Items.Clear();
        foreach(var page in history.BackPages())lstBack.Items.Add(page);
        foreach(var page in history.ForwardPages())lstForward.Items.Add(page);
        txtLog.AppendText($"{action} | actual={history.Current} | atrás=[{string.Join(",",history.BackPages())}] | adelante=[{string.Join(",",history.ForwardPages())}]"+Environment.NewLine);
    }
    private void Export_Click(object? sender, EventArgs e)
    {
        using var dialog = new SaveFileDialog {Filter="Texto (*.txt)|*.txt",FileName="prueba_historial.txt"};
        if(dialog.ShowDialog(this)!=DialogResult.OK)return;
        try {File.WriteAllText(dialog.FileName,txtLog.Text);MessageBox.Show(this,"Registro guardado.");}
        catch(Exception ex) when(ex is IOException || ex is UnauthorizedAccessException) {MessageBox.Show(this,ex.Message,"No se guardó el archivo");}
    }
}
public class ArrayCanvas : Panel
{
    public ArrayCanvas() {DoubleBuffered=true;ResizeRedraw=true;}
}
