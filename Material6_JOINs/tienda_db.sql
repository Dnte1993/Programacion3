-- Clientes inactivos corregido para SQLite
SELECT 
    c.nombre, 
    MAX(o.fecha_orden) AS ultima_orden,
    CASE 
        WHEN MAX(o.fecha_orden) IS NULL THEN 'Nunca ha comprado'
        WHEN (julianday('now') - julianday(MAX(o.fecha_orden))) > 30 THEN 'Inactivo'
        ELSE 'Activo recientemente'
    END AS estado_actividad
FROM clientes c
LEFT JOIN ordenes o ON c.id_cliente = o.id_cliente
GROUP BY c.id_cliente, c.nombre
HAVING MAX(o.fecha_orden) IS NULL 
   OR (julianday('now') - julianday(MAX(o.fecha_orden))) > 30;