-- Reto 1: Listar productos con su categoría, precio y stock
SELECT 
    p.nombre AS producto, 
    c.nombre_categoria, 
    p.precio, 
    p.stock
FROM productos p
LEFT JOIN categorias c ON p.id_categoria = c.id_categoria;

-- Reto 2: Ingreso mensual (excluyendo cancelaciones)
SELECT 
    strftime('%Y-%m', o.fecha_orden) AS mes_anio,
    SUM(do.cantidad * do.precio_unitario) AS total_ingresos,
    COUNT(DISTINCT o.id_orden) AS ordenes_unicas,
    COUNT(DISTINCT do.id_producto) AS productos_unicos
FROM ordenes o
JOIN detalle_ordenes do ON o.id_orden = do.id_orden
WHERE o.estado != 'cancelado'
GROUP BY mes_anio;

-- Reto 3: Categorías sin ventas en marzo de 2024
SELECT c.nombre_categoria
FROM categorias c
LEFT JOIN productos p ON c.id_categoria = p.id_categoria
LEFT JOIN detalle_ordenes do ON p.id_producto = do.id_producto
LEFT JOIN ordenes o ON do.id_orden = o.id_orden 
    AND o.fecha_orden BETWEEN '2024-03-01' AND '2024-03-31'
WHERE o.id_orden IS NULL
GROUP BY c.nombre_categoria;

-- Reto 4: Ranking de clientes por ciudad (El cliente que más gasta en cada una)
WITH GastoCliente AS (
    SELECT 
        c.ciudad, 
        c.nombre, 
        SUM(do.cantidad * do.precio_unitario) AS total_gastado
    FROM clientes c
    JOIN ordenes o ON c.id_cliente = o.id_cliente
    JOIN detalle_ordenes do ON o.id_orden = do.id_orden
    GROUP BY c.ciudad, c.nombre
)
SELECT ciudad, nombre, total_gastado
FROM (
    SELECT 
        ciudad, 
        nombre, 
        total_gastado,
        RANK() OVER (PARTITION BY ciudad ORDER BY total_gastado DESC) as ranking
    FROM GastoCliente
) 
WHERE ranking = 1;