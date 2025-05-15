-- DROP TABLES (ordenado para evitar conflictos por claves foráneas)
DROP TABLE IF EXISTS VideoPublicidad;
DROP TABLE IF EXISTS CuponServico;
DROP TABLE IF EXISTS OfertaServicio;
DROP TABLE IF EXISTS CategoriaOferta;
DROP TABLE IF EXISTS EstadoCupon;
DROP TABLE IF EXISTS InfluencerRedSocial;
DROP TABLE IF EXISTS UsuarioInfluencer;
DROP TABLE IF EXISTS UsuarioAdministrador;
DROP TABLE IF EXISTS Influencer;
DROP TABLE IF EXISTS TarjetaPago;
DROP TABLE IF EXISTS UsuarioEmpresa;
DROP TABLE IF EXISTS RedSocial;
DROP TABLE IF EXISTS Empresa;
DROP TABLE IF EXISTS EstadoUsuario;
DROP TABLE IF EXISTS Ciudad;
DROP TABLE IF EXISTS Estado;
DROP TABLE IF EXISTS Pais;
DROP TABLE IF EXISTS Genero;

-- CREATE TABLES

CREATE TABLE EstadoUsuario (
    idEstadoUsuario BIGSERIAL PRIMARY KEY,
    estadoUsuario VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Pais (
    idPais BIGSERIAL PRIMARY KEY,
    pais VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Estado (
    idEstado BIGSERIAL PRIMARY KEY,
    idPais BIGINT NOT NULL,
    estado VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (idPais) REFERENCES Pais(idPais)
);

CREATE TABLE Ciudad (
    idCiudad BIGSERIAL PRIMARY KEY,
    idEstado BIGINT NOT NULL,
    ciudad VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (idEstado) REFERENCES Estado(idEstado)
);

CREATE TABLE Genero (
    idGenero BIGSERIAL PRIMARY KEY,
    genero VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE RedSocial (
    idRedSocial BIGSERIAL PRIMARY KEY,
    redSocial VARCHAR(100) NOT NULL,
    activo BOOLEAN DEFAULT TRUE
);

CREATE TABLE Empresa (
    idEmpresa BIGSERIAL PRIMARY KEY,
    idCiudad BIGINT NOT NULL,
    idCiudad2 BIGINT NULL,
    idCiudad3 BIGINT NULL,
    idCiudad4 BIGINT NULL,
    nombre VARCHAR(100) NOT NULL,
    nif VARCHAR(20) NOT NULL,
    url VARCHAR(100),
    numeroContacto VARCHAR(10),
    sector TEXT NOT NULL,
    direccion VARCHAR(500) NOT NULL,
    FOREIGN KEY (idCiudad) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad2) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad3) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad4) REFERENCES Ciudad(idCiudad)
);

CREATE TABLE TarjetaPago (
    idTarjetaPago BIGSERIAL PRIMARY KEY,
    idEmpresa BIGINT NOT NULL,
    numeroTarjeta VARCHAR(40) NOT NULL,
    nombreTitular VARCHAR(100) NOT NULL,
    fechaExpiracion DATE NOT NULL,
    codigoSeguridad VARCHAR(10) NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa)
);

CREATE TABLE UsuarioEmpresa (
    idUsuarioEmpresa BIGSERIAL PRIMARY KEY,
    idEmpresa BIGINT NOT NULL,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa),
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE Influencer (
    idInfluencer BIGSERIAL PRIMARY KEY,
    idCiudad BIGINT NOT NULL,
    idCiudad2 BIGINT NULL,
    idCiudad3 BIGINT NULL,
    idCiudad4 BIGINT NULL,
    idGenero BIGINT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    fechaNacimiento DATE NOT NULL,
    numeroContacto VARCHAR(10),
    FOREIGN KEY (idCiudad) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad2) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad3) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idCiudad4) REFERENCES Ciudad(idCiudad),
    FOREIGN KEY (idGenero) REFERENCES Genero(idGenero)
);

CREATE TABLE UsuarioInfluencer (
    idUsuarioInfluencer BIGSERIAL PRIMARY KEY,
    idInfluencer BIGINT NOT NULL,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idInfluencer) REFERENCES Influencer(idInfluencer),
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE UsuarioAdministrador (
    idUsuarioAdministrador BIGSERIAL PRIMARY KEY,
    idEstadoUsuario BIGINT NOT NULL,
    correo VARCHAR(500) NOT NULL,
    clave VARCHAR(500) NOT NULL,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idEstadoUsuario) REFERENCES EstadoUsuario(idEstadoUsuario)
);

CREATE TABLE InfluencerRedSocial (
    idInfluencerRedSocial BIGSERIAL PRIMARY KEY,
    idInfluencer BIGINT NOT NULL,
    idRedSocial BIGINT NOT NULL,
    numeroSeguidores INT NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    fechaCreacion DATE NOT NULL,
    fechaActualizacion DATE NOT NULL,
    FOREIGN KEY (idInfluencer) REFERENCES Influencer(idInfluencer),
    FOREIGN KEY (idRedSocial) REFERENCES RedSocial(idRedSocial)
);

CREATE TABLE CategoriaOferta (
    idCategoriaOferta BIGSERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL
);

CREATE TABLE OfertaServicio (
    idOfertaServicio BIGSERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    direccion TEXT NOT NULL,
    imagen VARCHAR(500) NOT NULL,
    descripcion TEXT NOT NULL,
    fechaInicio DATE NOT NULL,
    fechaFin DATE NOT NULL,
    horaInicio TIME NOT NULL,
    horaFin TIME NOT NULL,
    cuposDisponibles INT NOT NULL,
    fechaCreacion DATE NOT NULL,
    activo BOOLEAN DEFAULT TRUE,
    idCategoriaOferta BIGINT NOT NULL,
    idEmpresa BIGINT NOT NULL,
    FOREIGN KEY (idCategoriaOferta) REFERENCES CategoriaOferta(idCategoriaOferta),
    FOREIGN KEY (idEmpresa) REFERENCES Empresa(idEmpresa)
);

CREATE TABLE EstadoCupon (
    idEstadoCupon BIGSERIAL PRIMARY KEY,
    estadoCupon VARCHAR(100) NOT NULL
);

CREATE TABLE CuponServico (
    idCuponServicio BIGSERIAL PRIMARY KEY,
    codigo VARCHAR(200) NOT NULL,
    fechaRedencion DATE,
    idOfertaServicio BIGINT NOT NULL,
    idEstadoCupon BIGINT NOT NULL,
    idInfluencer BIGINT,
    FOREIGN KEY (idOfertaServicio) REFERENCES OfertaServicio(idOfertaServicio),
    FOREIGN KEY (idEstadoCupon) REFERENCES EstadoCupon(idEstadoCupon),
    FOREIGN KEY (idInfluencer) REFERENCES Influencer(idInfluencer)
);

CREATE TABLE VideoPublicidad (
	idVideoPublicidad BIGSERIAL PRIMARY KEY,
	videoPublicidad VARCHAR(200) NOT NULL,
	fechaCreacion DATE,
	idCuponServicio BIGINT NOT NULL,
	FOREIGN KEY (idCuponServicio) REFERENCES CuponServico(idCuponServicio)
);