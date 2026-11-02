-- ============================================================================
-- SCRIPT DE BASE DE DATOS: BD_ViajesMundoMaya (CORREGIDO)
-- MOTOR: SQL Server 2022
-- ============================================================================

USE master;
GO

-- Si ya existe la base de datos previa con tablas a medio crear, se elimina para recrear limpia
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BD_ViajesMundoMaya')
BEGIN
    ALTER DATABASE BD_ViajesMundoMaya SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE BD_ViajesMundoMaya;
END
GO

CREATE DATABASE BD_ViajesMundoMaya;
GO

USE BD_ViajesMundoMaya;
GO

-- ============================================================================
-- 1. TABLAS CATÁLOGO / INDEPENDIENTES
-- ============================================================================

CREATE TABLE Tbl_Clientes (
    CodigoCliente INT IDENTITY(1,1) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    DPI VARCHAR(20) NULL,
    Telefono VARCHAR(20) NULL,
    CorreoElectronico VARCHAR(150) NULL,
    Direccion VARCHAR(250) NULL,
    FechaRegistro DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Clientes PRIMARY KEY (CodigoCliente)
);
GO

CREATE TABLE Tbl_Empleados (
    CodigoEmpleado INT IDENTITY(1,1) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Puesto VARCHAR(100) NOT NULL,
    Telefono VARCHAR(20) NULL,
    CorreoElectronico VARCHAR(150) NULL,
    FechaContratacion DATE NOT NULL,
    Salario DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Empleados PRIMARY KEY (CodigoEmpleado)
);
GO

CREATE TABLE Tbl_Proveedores (
    CodigoProveedor INT IDENTITY(1,1) NOT NULL,
    NombreProveedor VARCHAR(150) NOT NULL,
    TipoProveedor VARCHAR(50) NOT NULL,
    Contacto VARCHAR(100) NULL,
    Telefono VARCHAR(20) NULL,
    CorreoElectronico VARCHAR(150) NULL,
    Direccion VARCHAR(250) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Proveedores PRIMARY KEY (CodigoProveedor)
);
GO

CREATE TABLE Tbl_MetodosPago (
    CodigoMetodoPago INT IDENTITY(1,1) NOT NULL,
    NombreMetodoPago VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    EntidadFinanciera VARCHAR(150) NULL,
    Descripcion VARCHAR(200) NULL,
    ComisionPorcentaje DECIMAL(6,2) NOT NULL DEFAULT 0.00,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_MetodosPago PRIMARY KEY (CodigoMetodoPago)
);
GO

CREATE TABLE Tbl_Destinos (
    CodigoDestino INT IDENTITY(1,1) NOT NULL,
    NombreDestino VARCHAR(150) NOT NULL,
    Pais VARCHAR(100) NOT NULL,
    Departamento VARCHAR(100) NULL,
    Ciudad VARCHAR(100) NULL,
    Descripcion VARCHAR(300) NULL,
    Imagen VARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Destinos PRIMARY KEY (CodigoDestino)
);
GO

CREATE TABLE Tbl_TiposTransporte (
    CodigoTipoTransporte INT IDENTITY(1,1) NOT NULL,
    NombreTipo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200) NULL,
    Capacidad INT NOT NULL DEFAULT 0,
    EquipajePermitido VARCHAR(100) NULL,
    Comodidades VARCHAR(200) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_TiposTransporte PRIMARY KEY (CodigoTipoTransporte)
);
GO

-- ============================================================================
-- 2. TABLAS RELACIONADAS A PROVEEDORES Y DESTINOS
-- ============================================================================

CREATE TABLE Tbl_Hoteles (
    CodigoHotel INT IDENTITY(1,1) NOT NULL,
    CodigoDestino INT NOT NULL,
    NombreHotel VARCHAR(150) NOT NULL,
    Categoria VARCHAR(20) NULL,
    Direccion VARCHAR(250) NULL,
    Telefono VARCHAR(20) NULL,
    ServiciosIncluidos VARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Hoteles PRIMARY KEY (CodigoHotel),
    CONSTRAINT FK_Hoteles_Destinos FOREIGN KEY (CodigoDestino) 
        REFERENCES Tbl_Destinos (CodigoDestino)
);
GO

CREATE TABLE Tbl_Tours (
    CodigoTour INT IDENTITY(1,1) NOT NULL,
    CodigoDestino INT NOT NULL,
    NombreTour VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(300) NULL,
    DuracionHoras INT NOT NULL DEFAULT 0,
    PrecioPorPersona DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    Incluye VARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Tours PRIMARY KEY (CodigoTour),
    CONSTRAINT FK_Tours_Destinos FOREIGN KEY (CodigoDestino) 
        REFERENCES Tbl_Destinos (CodigoDestino)
);
GO

CREATE TABLE Tbl_ServiciosProveedor (
    CodigoServicioProv INT IDENTITY(1,1) NOT NULL,
    CodigoProveedor INT NOT NULL,
    TipoServicio VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(300) NULL,
    PrecioReferencia DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    Moneda VARCHAR(10) NOT NULL DEFAULT 'GTQ',
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_ServiciosProveedor PRIMARY KEY (CodigoServicioProv),
    CONSTRAINT FK_ServiciosProveedor_Proveedores FOREIGN KEY (CodigoProveedor) 
        REFERENCES Tbl_Proveedores (CodigoProveedor)
);
GO

-- ============================================================================
-- 3. PAQUETES TURÍSTICOS Y SUS DETALLES
-- ============================================================================

CREATE TABLE Tbl_Paquetes (
    CodigoPaquete INT IDENTITY(1,1) NOT NULL,
    CodigoProveedor INT NOT NULL,
    NombrePaquete VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(300) NULL,
    PrecioBase DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    DuracionDias INT NOT NULL DEFAULT 1,
    Incluye VARCHAR(300) NULL,
    Imagen VARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Paquetes PRIMARY KEY (CodigoPaquete),
    CONSTRAINT FK_Paquetes_Proveedores FOREIGN KEY (CodigoProveedor) 
        REFERENCES Tbl_Proveedores (CodigoProveedor)
);
GO

CREATE TABLE Tbl_Itinerarios (
    CodigoItinerario INT IDENTITY(1,1) NOT NULL,
    CodigoPaquete INT NOT NULL,
    Dia INT NOT NULL,
    DescripcionActividad VARCHAR(300) NOT NULL,
    Lugar VARCHAR(150) NULL,
    HoraInicio TIME NULL,
    HoraFin TIME NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Itinerarios PRIMARY KEY (CodigoItinerario),
    CONSTRAINT FK_Itinerarios_Paquetes FOREIGN KEY (CodigoPaquete) 
        REFERENCES Tbl_Paquetes (CodigoPaquete)
);
GO

CREATE TABLE Tbl_PaqueteTours (
    CodigoPaqueteTour INT IDENTITY(1,1) NOT NULL,
    CodigoPaquete INT NOT NULL,
    CodigoTour INT NOT NULL,
    Incluido BIT NOT NULL DEFAULT 1,
    Orden INT NOT NULL DEFAULT 1,
    Observaciones VARCHAR(200) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_PaqueteTours PRIMARY KEY (CodigoPaqueteTour),
    CONSTRAINT FK_PaqueteTours_Paquetes FOREIGN KEY (CodigoPaquete) 
        REFERENCES Tbl_Paquetes (CodigoPaquete),
    CONSTRAINT FK_PaqueteTours_Tours FOREIGN KEY (CodigoTour) 
        REFERENCES Tbl_Tours (CodigoTour)
);
GO

CREATE TABLE Tbl_PaqueteHoteles (
    CodigoPaqueteHotel INT IDENTITY(1,1) NOT NULL,
    CodigoPaquete INT NOT NULL,
    CodigoHotel INT NOT NULL,
    Noches INT NOT NULL DEFAULT 1,
    TipoHabitacion VARCHAR(100) NULL,
    IncluyeDesayuno BIT NOT NULL DEFAULT 0,
    PrecioPorNoche DECIMAL(12,2) NOT NULL DEFAULT 0.00,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_PaqueteHoteles PRIMARY KEY (CodigoPaqueteHotel),
    CONSTRAINT FK_PaqueteHoteles_Paquetes FOREIGN KEY (CodigoPaquete) 
        REFERENCES Tbl_Paquetes (CodigoPaquete),
    CONSTRAINT FK_PaqueteHoteles_Hoteles FOREIGN KEY (CodigoHotel) 
        REFERENCES Tbl_Hoteles (CodigoHotel)
);
GO

CREATE TABLE Tbl_Traslados (
    CodigoTraslado INT IDENTITY(1,1) NOT NULL,
    CodigoPaquete INT NOT NULL,
    CodigoTipoTransporte INT NOT NULL,
    Origen VARCHAR(150) NOT NULL,
    Destino VARCHAR(150) NOT NULL,
    FechaServicio DATE NOT NULL,
    HoraSalida TIME NULL,
    HoraLlegada TIME NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Traslados PRIMARY KEY (CodigoTraslado),
    CONSTRAINT FK_Traslados_Paquetes FOREIGN KEY (CodigoPaquete) 
        REFERENCES Tbl_Paquetes (CodigoPaquete),
    CONSTRAINT FK_Traslados_TiposTransporte FOREIGN KEY (CodigoTipoTransporte) 
        REFERENCES Tbl_TiposTransporte (CodigoTipoTransporte)
);
GO

-- ============================================================================
-- 4. GESTIÓN DE RESERVAS, PASAJEROS Y PAGOS
-- ============================================================================

CREATE TABLE Tbl_Reservas (
    CodigoReserva INT IDENTITY(1,1) NOT NULL,
    CodigoCliente INT NOT NULL,
    CodigoEmpleado INT NOT NULL,
    CodigoPaquete INT NOT NULL,
    FechaReserva DATETIME NOT NULL DEFAULT GETDATE(),
    FechaViajeInicio DATE NOT NULL,
    FechaViajeFin DATE NOT NULL,
    EstadoReserva VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    Observaciones VARCHAR(300) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Reservas PRIMARY KEY (CodigoReserva),
    CONSTRAINT FK_Reservas_Clientes FOREIGN KEY (CodigoCliente) 
        REFERENCES Tbl_Clientes (CodigoCliente),
    CONSTRAINT FK_Reservas_Empleados FOREIGN KEY (CodigoEmpleado) 
        REFERENCES Tbl_Empleados (CodigoEmpleado),
    CONSTRAINT FK_Reservas_Paquetes FOREIGN KEY (CodigoPaquete) 
        REFERENCES Tbl_Paquetes (CodigoPaquete)
);
GO

CREATE TABLE Tbl_ReservaPersonas (
    CodigoReservaPersona INT IDENTITY(1,1) NOT NULL,
    CodigoReserva INT NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    DPI VARCHAR(20) NULL,
    FechaNacimiento DATE NULL,
    Parentesco VARCHAR(50) NULL,
    Edad INT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_ReservaPersonas PRIMARY KEY (CodigoReservaPersona),
    CONSTRAINT FK_ReservaPersonas_Reservas FOREIGN KEY (CodigoReserva) 
        REFERENCES Tbl_Reservas (CodigoReserva)
);
GO

CREATE TABLE Tbl_Pagos (
    CodigoPago INT IDENTITY(1,1) NOT NULL,
    CodigoReserva INT NOT NULL,
    CodigoMetodoPago INT NOT NULL,
    FechaPago DATETIME NOT NULL DEFAULT GETDATE(),
    Monto DECIMAL(12,2) NOT NULL,
    Referencia VARCHAR(100) NULL,
    NumeroTransaccion VARCHAR(100) NULL,
    EstadoPago VARCHAR(20) NOT NULL DEFAULT 'Completado',
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT PK_Tbl_Pagos PRIMARY KEY (CodigoPago),
    CONSTRAINT FK_Pagos_Reservas FOREIGN KEY (CodigoReserva) 
        REFERENCES Tbl_Reservas (CodigoReserva),
    CONSTRAINT FK_Pagos_MetodosPago FOREIGN KEY (CodigoMetodoPago) 
        REFERENCES Tbl_MetodosPago (CodigoMetodoPago)
);
GO




