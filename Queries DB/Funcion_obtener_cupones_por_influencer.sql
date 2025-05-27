DROP FUNCTION obtener_cupones_por_influencer(bigint);
CREATE OR REPLACE FUNCTION obtener_cupones_por_influencer(p_id_influencer BIGINT)
RETURNS TABLE (
    nombreOfertaServicio VARCHAR,
    codigo VARCHAR,
    fechaRedencion DATE,
    nombreEstadoCupon VARCHAR
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        os.nombre AS nombreOfertaServicio,
        cs.codigo AS codigo,
        cs.fechaRedencion AS fechaRedencion,
        ec.estadoCupon AS nombreEstadoCupon
    FROM CuponServicio cs
    JOIN OfertaServicio os ON cs.idOfertaServicio = os.idOfertaServicio
    JOIN EstadoCupon ec ON cs.idEstadoCupon = ec.idEstadoCupon
    WHERE cs.idInfluencer = p_id_influencer
   ORDER BY 
    CASE LOWER(ec.estadoCupon)
        WHEN 'no redimido' THEN 1
        WHEN 'redimido' THEN 2
        WHEN 'expirado' THEN 3
        ELSE 4
    END,
    cs.fechaRedencion NULLS LAST;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM obtener_cupones_por_influencer(1);

