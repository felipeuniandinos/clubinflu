DROP FUNCTION obtener_ofertas_servicios_por_empresa(bigint);
CREATE OR REPLACE FUNCTION obtener_ofertas_servicios_por_empresa(
    p_id_empresa BIGINT
)
RETURNS TABLE (
    idOfertaServicio BIGINT,
    nombre VARCHAR,
    direccion TEXT,
    imagen VARCHAR,
    descripcion TEXT,
    fechaInicio DATE,
    fechaFin DATE,
    horaInicio TIME,
    horaFin TIME,
    cuposDisponibles INT,
    fechaCreacion DATE,
    activo BOOLEAN,
    idCategoriaOferta BIGINT,
    nombreCategoriaOferta VARCHAR,
    idEmpresa BIGINT,
    nombreEmpresa VARCHAR,
    ciudad VARCHAR,
    estado VARCHAR,
    pais VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        os.idOfertaServicio,
        os.nombre,
        os.direccion,
        os.imagen,
        os.descripcion,
        os.fechaInicio,
        os.fechaFin,
        os.horaInicio,
        os.horaFin,
        os.cuposDisponibles,
        os.fechaCreacion,
        os.activo,
        os.idCategoriaOferta,
        cat.nombre,
        os.idEmpresa,
        e.nombre,
        c.ciudad,
        est.estado,
        p.pais
    FROM OfertaServicio os
    JOIN CategoriaOferta cat ON os.idCategoriaOferta = cat.idCategoriaOferta
    JOIN Empresa e ON os.idEmpresa = e.idEmpresa
    JOIN Ciudad c ON e.idCiudad = c.idCiudad
    JOIN Estado est ON c.idEstado = est.idEstado
    JOIN Pais p ON est.idPais = p.idPais
 	ORDER BY os.fechaCreacion DESC; 
END;
$$ LANGUAGE plpgsql;

SELECT * FROM obtener_ofertas_servicios_por_empresa(1);
