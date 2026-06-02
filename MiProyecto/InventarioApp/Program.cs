using InventarioApp;

var servicio = new InventarioService();
string rutaArchivo = "inventario.json";

// Cargar el inventario al iniciar
Console.WriteLine("=== INICIANDO SISTEMA ===");
servicio.CargarInventario(rutaArchivo);

string opcion;
do
{
    Console.WriteLine("\n=== INVENTARIO APP ===");
    Console.WriteLine("[1] Mostrar todos");
    Console.WriteLine("[2] Agregar producto");
    Console.WriteLine("[3] Eliminar por ID");
    Console.WriteLine("[4] Filtrar por categoría");
    Console.WriteLine("[5] Buscar por nombre");
    Console.WriteLine("[6] Ordenar por precio");
    Console.WriteLine("[7] Resumen del inventario");
    Console.WriteLine("[8] Actualizar producto");
    Console.WriteLine("[9] Ver stock bajo");
    Console.WriteLine("[0] Salir");

    Console.Write("\nOpción: ");
    opcion = Console.ReadLine() ?? "0";

    switch (opcion)
    {
        case "1": servicio.MostrarTodos(); break;
        case "2":
            string nombre, cat;
            decimal precio;
            int cant;

            while (true)
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine()!;
                if (!string.IsNullOrWhiteSpace(nombre)) break;
                Console.WriteLine("Error: El nombre no puede estar vacío.");
            }

            while (true)
            {
                Console.Write("Categoría: ");
                cat = Console.ReadLine()!;
                if (!string.IsNullOrWhiteSpace(cat)) break;
                Console.WriteLine("Error: La categoría no puede estar vacía.");
            }

            while (true)
            {
                Console.Write("Precio: ");
                if (decimal.TryParse(Console.ReadLine(), out precio) && precio > 0) break;
                Console.WriteLine("Error: Ingresa un precio válido mayor a 0.");
            }

            while (true)
            {
                Console.Write("Cantidad: ");
                if (int.TryParse(Console.ReadLine(), out cant) && cant >= 1) break;
                Console.WriteLine("Error: La cantidad debe ser mayor o igual a 1.");
            }

            servicio.Agregar(nombre, cat, precio, cant);
            break;

        case "3":
            Console.Write("ID a eliminar: ");
            servicio.Eliminar(int.Parse(Console.ReadLine()!));
            break;
        case "4":
            Console.Write("Categoría: ");
            servicio.FiltrarPorCategoria(Console.ReadLine()!);
            break;
        case "5":
            Console.Write("Texto a buscar: ");
            servicio.BuscarPorNombre(Console.ReadLine()!);
            break;
        case "6": servicio.OrdenarPorPrecio(); break;
        case "7": servicio.MostrarResumen(); break;
        case "8":
            Console.Write("ID del producto a actualizar: ");
            int idUpdate = int.Parse(Console.ReadLine()!);

            Console.Write("Nuevo precio (presiona Enter para no cambiarlo): ");
            string inputPrecio = Console.ReadLine()!;
            decimal? precioUpdate = string.IsNullOrWhiteSpace(inputPrecio) ? null : decimal.Parse(inputPrecio);

            Console.Write("Nueva cantidad (presiona Enter para no cambiarla): ");
            string inputCant = Console.ReadLine()!;
            int? cantUpdate = string.IsNullOrWhiteSpace(inputCant) ? null : int.Parse(inputCant);

            servicio.UpdateProducto(idUpdate, precioUpdate, cantUpdate);
            break;
        case "9":
            Console.Write("Ingresa el límite (umbral) para considerar stock bajo: ");
            if (int.TryParse(Console.ReadLine(), out int umbral))
            {
                servicio.MostrarStockBajo(umbral);
            }
            else
            {
                Console.WriteLine("Error: Ingresa un número válido.");
            }
            break;
        case "0":
            // Guardar el inventario justo antes de salir
            servicio.GuardarInventario(rutaArchivo);
            Console.WriteLine("¡Hasta luego!");
            break;
        default: Console.WriteLine("Opción no válida."); break;
    }
} while (opcion != "0");