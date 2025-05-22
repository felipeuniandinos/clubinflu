DROP FUNCTION obtener_cupones_servicio_por_oferta_servico(bigint);
CREATE OR REPLACE FUNCTION obtener_cupones_servicio_por_oferta_servico(
    p_id_oferta_servicio BIGINT
)
RETURNS TABLE (
    idCuponServicio BIGINT,
    codigo VARCHAR,
    fecharedencion DATE,
    idOfertaServicio BIGINT,
    nombreEstadoCupon VARCHAR,
    nombreInfluencer VARCHAR
) AS $$
BEGIN 
    RETURN QUERY
    SELECT 
    	cs.idCuponServicio,
    	cs.codigo,
    	cs.fecharedencion,
    	cs.idOfertaServicio,
    	ec.estadoCupon,
    	i.nombre
    FROM CuponServicio cs
    JOIN EstadoCupon ec ON cs.idestadocupon = ec.idestadocupon
    LEFT JOIN influencer i ON cs.idInfluencer = i.idInfluencer
    WHERE cs.idOfertaServicio =  p_id_oferta_servicio;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM obtener_cupones_servicio_por_oferta_servico(1);
