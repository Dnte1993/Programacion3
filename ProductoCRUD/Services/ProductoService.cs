using ProductoCRUD.Data;
using ProductoCRUD.Models;

namespace ProductoCRUD.Services
{
    public class ProductoService
    {
        // CREATE: Insertar un nuevo producto
        public void CrearProducto(string nombre, string? descripcion, decimal precio, int stock)
        {
            using var context = new AppDbContext();

            var nuevoProducto = new Producto
            {
                Nombre = nombre,
                Descripcion = descripcion,
                Precio = precio,
                Stock = stock,
                FechaCreacion = DateTime.UtcNow
            };

            context.Productos.Add(nuevoProducto);
            context.SaveChanges();

            Console.WriteLine($"Producto creado con ID: {nuevoProducto.Id}");
        }
        // READ 1: Obtener todos los productos
        public List<Producto> ObtenerTodos()
        {
            using var context = new AppDbContext();
            return context.Productos
                .OrderBy(p => p.Nombre) // ORDER BY Nombre ASC
                .ToList(); // Ejecuta la consulta (SELECT *)
        }

        // READ 2: Obtener por ID
        public Producto? ObtenerPorId(int id)
        {
            using var context = new AppDbContext();
            return context.Productos
                .FirstOrDefault(p => p.Id == id); // SELECT TOP 1 WHERE Id = @id
            // Retorna null si no existe
        }

        // READ 3: Buscar por nombre (filtro parcial)
        public List<Producto> BuscarPorNombre(string termino)
        {
            using var context = new AppDbContext();
            return context.Productos
                .Where(p => p.Nombre.Contains(termino)) // LIKE '%termino%'
                .OrderBy(p => p.Precio)
                .ToList();
        }
        // UPDATE: Modificar un producto existente
        public bool ActualizarProducto(int id, string nuevoNombre, decimal nuevoPrecio, int nuevoStock)
        {
            using var context = new AppDbContext();

            // 1. Buscar el producto (queda tracked por el context)
            var producto = context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                Console.WriteLine($"Producto con ID {id} no encontrado.");
                return false;
            }

            // 2. Modificar las propiedades (EF Core detecta los cambios)
            producto.Nombre = nuevoNombre;
            producto.Precio = nuevoPrecio;
            producto.Stock = nuevoStock;

            // 3. SaveChanges genera solo el UPDATE para las columnas modificadas
            context.SaveChanges();
            Console.WriteLine($"Producto {id} actualizado correctamente.");
            return true;
        }
        // DELETE: Eliminar un producto por ID
        public bool EliminarProducto(int id)
        {
            using var context = new AppDbContext();

            var producto = context.Productos.FirstOrDefault(p => p.Id == id);
            if (producto == null)
            {
                Console.WriteLine($"Producto con ID {id} no encontrado.");
                return false;
            }

            context.Productos.Remove(producto);
            context.SaveChanges();

            Console.WriteLine($"Producto {id} eliminado correctamente.");
            return true;
        }
    }
}
