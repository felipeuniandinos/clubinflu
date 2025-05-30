CREATE OR REPLACE FUNCTION validar_videos_cupon(id_cupon_servicio BIGINT)
RETURNS TEXT AS $$
DECLARE
    cantidad_videos INT;
    id_influencer BIGINT;
    tiene_youtube BOOLEAN := FALSE;
    suma_seguidores INT := 0;
    videos_requeridos INT := 0;
BEGIN
    -- Obtener la cantidad de videos asociados al cupón
    SELECT COUNT(*) INTO cantidad_videos
    FROM VideoCupon
    WHERE idCuponServicio = id_cupon_servicio;

    -- Obtener el id del influencer asociado al cupón
    SELECT idInfluencer INTO id_influencer
    FROM CuponServicio
    WHERE idCuponServicio = id_cupon_servicio;

    IF id_influencer IS NULL THEN
        RETURN 'El cupón no tiene un influencer asociado';
    END IF;

    -- Verificar si tiene Youtube y sumar los seguidores
    SELECT 
        SUM(numeroSeguidores),
        BOOL_OR(rs.redSocial ILIKE 'Youtube')
    INTO suma_seguidores, tiene_youtube
    FROM InfluencerRedSocial irs
    JOIN RedSocial rs ON rs.idRedSocial = irs.idRedSocial
    WHERE irs.idInfluencer = id_influencer
      AND irs.activo = TRUE
      AND rs.activo = TRUE;

    -- Determinar cuántos videos se requieren
    IF tiene_youtube THEN
        videos_requeridos := 1;
    ELSE
        IF suma_seguidores BETWEEN 10000 AND 39999 THEN
            videos_requeridos := 3;
        ELSIF suma_seguidores BETWEEN 40000 AND 99999 THEN
            videos_requeridos := 2;
        ELSIF suma_seguidores >= 100000 THEN
            videos_requeridos := 1;
        ELSE
            RETURN 'El influencer no cumple con el mínimo de seguidores';
        END IF;
    END IF;

    -- Validar si se cumplen los requisitos
    IF cantidad_videos >= videos_requeridos THEN
        RETURN 'Correcto';
    ELSE
        RETURN FORMAT('Faltan %s video(s)', videos_requeridos - cantidad_videos);
    END IF;
END;
$$ LANGUAGE plpgsql;

SELECT validar_videos_cupon(53);
