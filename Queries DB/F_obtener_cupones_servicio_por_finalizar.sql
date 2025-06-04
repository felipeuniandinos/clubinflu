CREATE OR REPLACE FUNCTION obtener_cupones_por_finalizar(id_influencer_input BIGINT)
RETURNS TABLE(codigo VARCHAR) AS
$$
BEGIN
    RETURN QUERY
    SELECT c.codigo
    FROM CuponServicio c
    WHERE c.idInfluencer = id_influencer_input
      AND c.idEstadoCupon = 4
      AND (
          -- Si tiene YouTube, debe haber pasado más de 7 días
          (
              EXISTS (
                  SELECT 1
                  FROM InfluencerRedSocial irs
                  JOIN RedSocial rs ON irs.idRedSocial = rs.idRedSocial
                  WHERE irs.idInfluencer = id_influencer_input
                    AND rs.redSocial ILIKE 'YouTube'
                    AND irs.activo = TRUE
                    AND rs.activo = TRUE
              )
              AND c.fechaRedencion <= CURRENT_DATE - INTERVAL '7 days'
          )
          -- Si NO tiene YouTube, debe haber pasado más de 1 día
          OR (
              NOT EXISTS (
                  SELECT 1
                  FROM InfluencerRedSocial irs
                  JOIN RedSocial rs ON irs.idRedSocial = rs.idRedSocial
                  WHERE irs.idInfluencer = id_influencer_input
                    AND rs.redSocial ILIKE 'YouTube'
                    AND irs.activo = TRUE
                    AND rs.activo = TRUE
              )
              AND c.fechaRedencion <= CURRENT_DATE - INTERVAL '1 day'
          )
      );
END;
$$ LANGUAGE plpgsql;

SELECT * FROM obtener_cupones_por_finalizar(1);


