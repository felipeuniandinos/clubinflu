DROP FUNCTION obtener_ofertas_por_filtro(bigint,bigint);
CREATE OR REPLACE FUNCTION obtener_ofertas_por_filtro(
    p_id_categoria_oferta BIGINT,
    p_id_estado BIGINT
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
    nombreCategoriaOferta varchar,
    idEmpresa BIGINT,
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
        c.ciudad,
        est.estado,
        p.pais
    FROM OfertaServicio os
    JOIN Empresa e ON os.idEmpresa = e.idEmpresa
    JOIN Ciudad c ON e.idCiudad = c.idCiudad
    JOIN Estado est ON c.idEstado = est.idEstado
    JOIN Pais p ON est.idPais = p.idPais
	JOIN Categoriaoferta cat ON os.idCategoriaOferta = cat.idcategoriaoferta
    WHERE (p_id_categoria_oferta = 0 OR os.idCategoriaOferta = p_id_categoria_oferta) 
      AND (p_id_estado = 0 OR est.idEstado = p_id_estado)
      AND os.activo = TRUE
      AND EXISTS (
          SELECT 1
          FROM CuponServico cs
          WHERE cs.idOfertaServicio = os.idOfertaServicio
            AND cs.idEstadoCupon = 1 
            AND cs.idInfluencer IS NULL 
      );
END;
$$ LANGUAGE plpgsql;


SELECT * FROM obtener_ofertas_por_filtro(1,1);
