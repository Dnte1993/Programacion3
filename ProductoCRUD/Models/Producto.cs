// Models/Producto.cs
namespace ProductoCRUD.Models
{
    public class Producto
    {
        public int Id { get; set; } // Clave primaria (PK) - Auto-incremental
        public string Nombre { get; set; } = string.Empty; // NOT NULL
        public string? Descripcion { get; set; } // Nullable - campo opcional
        public decimal Precio { get; set; } // Tipo decimal para valores monetarios
        public int Stock { get; set; } // Cantidad disponible en inventario
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow; // Auditoría
    }
}