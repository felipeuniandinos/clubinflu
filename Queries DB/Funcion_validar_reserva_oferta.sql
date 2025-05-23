CREATE OR REPLACE FUNCTION validar_reserva_oferta(
    p_idOfertaServicio BIGINT,
    p_idInfluencer BIGINT
)
RETURNS TEXT AS $$
DECLARE
    v_cuposDisponibles INT;
    v_fechaFin DATE;
    v_fechaInicio DATE;
    v_ultimaRedencion DATE;
BEGIN
    -- Validación 1: Verificar que la oferta existe y no esté expirada
	SELECT COUNT(*)
	INTO v_cuposDisponibles
	FROM CuponServicio
	WHERE idOfertaServicio = p_idOfertaServicio
	  AND idInfluencer IS NULL;
	
    SELECT fechaInicio, fechaFin
    INTO v_fechaInicio, v_fechaFin
    FROM OfertaServicio
    WHERE idOfertaServicio = p_idOfertaServicio;

    IF NOT FOUND THEN
        RETURN 'Error: La oferta de servicio no existe.';
    END IF;

    IF CURRENT_DATE < v_fechaInicio THEN
        RETURN 'Error: La oferta de servicio aún no comienza.';
    END IF;

	IF CURRENT_DATE > v_fechaFin THEN
        RETURN 'Error: La oferta de servicio está expirada.';
    END IF;

    -- Validación 2: Verificar que haya cupos disponibles
    IF v_cuposDisponibles <= 0 THEN
        RETURN 'Error: No hay cupos disponibles para esta oferta.';
    END IF;

    -- Validación 3: El influencer no puede redimir más de un cupón por mes para la misma oferta
    SELECT MAX(cs.fechaRedencion)
    INTO v_ultimaRedencion
    FROM CuponServicio cs
    WHERE cs.idOfertaServicio = p_idOfertaServicio
      AND cs.idInfluencer = p_idInfluencer;

    IF v_ultimaRedencion IS NOT NULL AND v_ultimaRedencion > CURRENT_DATE - INTERVAL '1 month' THEN
        RETURN 'Error: El influencer ya redimió un cupón para esta oferta en el último mes.';
    END IF;

    -- Si pasa todas las validaciones
    RETURN 'Validación exitosa: Puede redimir el cupón.';
END;
$$ LANGUAGE plpgsql;

SELECT validar_reserva_oferta(4, 1);
