using ProductoCRUD.Services;

Console.WriteLine("=== CRUD de Productos con EF Core ===\n");
var service = new ProductoService();

// --- CREATE ---
Console.WriteLine("--- Creando productos ---");
service.CrearProducto("Laptop Dell XPS 15", "Laptop de alto rendimiento", 28999.99m, 10);
service.CrearProducto("Mouse Logitech MX Master", "Mouse inalámbrico ergonómico", 1599.00m, 50);
service.CrearProducto("Teclado Mecánico Keychron", "Teclado con switches Cherry MX", 2499.50m, 30);

// --- READ ---
Console.WriteLine("\n--- Todos los productos ---");
var productos = service.ObtenerTodos();
foreach (var p in productos)
{
    Console.WriteLine($"[{p.Id}] {p.Nombre} - ${p.Precio:F2} | Stock: {p.Stock}");
}

// --- READ por ID ---
Console.WriteLine("\n--- Buscar producto ID=1 ---");
var producto = service.ObtenerPorId(1);
Console.WriteLine(producto != null ? $"Encontrado: {producto.Nombre}" : "No encontrado");

// --- UPDATE ---
Console.WriteLine("\n--- Actualizando producto ID=1 ---");
service.ActualizarProducto(1, "Laptop Dell XPS 15 (2024)", 31999.99m, 8);

// --- DELETE ---
Console.WriteLine("\n--- Eliminando producto ID=2 ---");
service.EliminarProducto(2);

// --- READ final ---
Console.WriteLine("\n--- Estado final de productos ---");
service.ObtenerTodos().ForEach(p =>
    Console.WriteLine($"[{p.Id}] {p.Nombre} - ${p.Precio:F2}")
);