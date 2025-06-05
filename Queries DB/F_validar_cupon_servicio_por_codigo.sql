CREATE OR REPLACE FUNCTION validar_cupon_servicio_por_codigo(
    p_codigoCuponServicio VARCHAR(200)
)
RETURNS TEXT AS $$

DECLARE
    v_idEstadoCupon INT;
    v_idEstadoCuponNoRedimido INT;
    v_idEstadoCuponRedimido INT;
    v_idEstadoCuponExpirado INT;
    v_idEstadoCuponValidado INT;

BEGIN
    -- Obtener los IDs de los estados
    SELECT idEstadoCupon INTO v_idEstadoCuponNoRedimido FROM estadoCupon WHERE estadoCupon = 'No redimido';
    SELECT idEstadoCupon INTO v_idEstadoCuponRedimido FROM estadoCupon WHERE estadoCupon = 'Redimido';
    SELECT idEstadoCupon INTO v_idEstadoCuponExpirado FROM estadoCupon WHERE estadoCupon = 'Expirado';
    SELECT idEstadoCupon INTO v_idEstadoCuponValidado FROM estadoCupon WHERE estadoCupon = 'Validado';

    -- Obtener el estado del cupón
    SELECT idEstadoCupon
    INTO v_idEstadoCupon
    FROM CuponServicio
    WHERE codigo = p_codigoCuponServicio;

    IF NOT FOUND THEN
        RETURN 'No existe cupón de servicio con este código.';
    END IF;

    IF v_idEstadoCupon = v_idEstadoCuponNoRedimido THEN
        RETURN 'El cupón no se encuentra redimido';
    ELSIF v_idEstadoCupon = v_idEstadoCuponExpirado THEN
        RETURN 'El cupón se encuentra expirado.';
    ELSIF v_idEstadoCupon = v_idEstadoCuponValidado THEN
        RETURN 'El cupón ya se encuentra validado.';
    ELSIF v_idEstadoCupon = v_idEstadoCuponRedimido THEN
        UPDATE CuponServicio
        SET idEstadoCupon = v_idEstadoCuponValidado,
			fechaRedencion = CURRENT_DATE
        WHERE codigo = p_codigoCuponServicio;
    END IF;

    RETURN 'El cupón ha sido validado correctamente.';
END;
$$ LANGUAGE plpgsql;


SELECT * from validar_cupon_servicio_por_codigo('CUPON1004');