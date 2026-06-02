namespace InventarioApp;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int Cantidad { get; set; }

    public Producto(int id, string nombre, string categoria, decimal precio, int cantidad)
    {
        Id = id;
        Nombre = nombre;
        Categoria = categoria;
        Precio = precio;
        Cantidad = cantidad;
    }

    public override string ToString()
    {
        return $"[{Id}] {Nombre,-20} | {Categoria,-12} | ${Precio,8:F2} | Stock: {Cantidad}";
    }
}