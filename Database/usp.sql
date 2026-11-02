USE BD_ViajesMundoMaya;
GO

-- ============================================================================
-- 1. TABLA: Tbl_Clientes
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarClientes
Descripción  : Lista todos los clientes registrados.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarClientes
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoCliente, Nombres, Apellidos, DPI, Telefono, CorreoElectronico, Direccion, FechaRegistro, Estado
        FROM Tbl_Clientes
        ORDER BY CodigoCliente DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar clientes: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarClientePorId
Descripción  : Busca un cliente según su identificador único.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarClientePorId
    @CodigoCliente INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Clientes WHERE CodigoCliente = @CodigoCliente)
        BEGIN
            RAISERROR('El cliente consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoCliente, Nombres, Apellidos, DPI, Telefono, CorreoElectronico, Direccion, FechaRegistro, Estado
        FROM Tbl_Clientes
        WHERE CodigoCliente = @CodigoCliente;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar cliente: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarCliente
Descripción  : Registra un nuevo cliente en el sistema.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarCliente
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DPI VARCHAR(20) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @Direccion VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_Clientes (Nombres, Apellidos, DPI, Telefono, CorreoElectronico, Direccion, FechaRegistro, Estado)
        VALUES (@Nombres, @Apellidos, @DPI, @Telefono, @CorreoElectronico, @Direccion, CAST(GETDATE() AS DATE), 1);

        SELECT SCOPE_IDENTITY() AS CodigoClienteGenerado, 'Cliente registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar cliente: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarCliente
Descripción  : Modifica la información de un cliente existente.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarCliente
    @CodigoCliente INT,
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DPI VARCHAR(20) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @Direccion VARCHAR(250) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Clientes WHERE CodigoCliente = @CodigoCliente)
        BEGIN
            RAISERROR('El cliente que desea modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Clientes
        SET Nombres = @Nombres,
            Apellidos = @Apellidos,
            DPI = @DPI,
            Telefono = @Telefono,
            CorreoElectronico = @CorreoElectronico,
            Direccion = @Direccion,
            Estado = @Estado
        WHERE CodigoCliente = @CodigoCliente;

        SELECT @CodigoCliente AS CodigoCliente, 'Cliente actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar cliente: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarCliente
Descripción  : Realiza la baja lógica de un cliente.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarCliente
    @CodigoCliente INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Clientes WHERE CodigoCliente = @CodigoCliente)
        BEGIN
            RAISERROR('El cliente que intenta desactivar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Clientes
        SET Estado = 0
        WHERE CodigoCliente = @CodigoCliente;

        SELECT @CodigoCliente AS CodigoCliente, 'Cliente desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al eliminar cliente: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 2. TABLA: Tbl_Empleados
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarEmpleados
Descripción  : Lista todos los colaboradores y operadores de la agencia.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarEmpleados
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoEmpleado, Nombres, Apellidos, Puesto, Telefono, CorreoElectronico, FechaContratacion, Salario, Estado
        FROM Tbl_Empleados
        ORDER BY CodigoEmpleado DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar empleados: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarEmpleadoPorId
Descripción  : Obtiene la información de un empleado específico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarEmpleadoPorId
    @CodigoEmpleado INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Empleados WHERE CodigoEmpleado = @CodigoEmpleado)
        BEGIN
            RAISERROR('El empleado indicado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoEmpleado, Nombres, Apellidos, Puesto, Telefono, CorreoElectronico, FechaContratacion, Salario, Estado
        FROM Tbl_Empleados
        WHERE CodigoEmpleado = @CodigoEmpleado;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar empleado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarEmpleado
Descripción  : Inserta un nuevo empleado en la base de datos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarEmpleado
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @Puesto VARCHAR(100),
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @FechaContratacion DATE,
    @Salario DECIMAL(12,2)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_Empleados (Nombres, Apellidos, Puesto, Telefono, CorreoElectronico, FechaContratacion, Salario, Estado)
        VALUES (@Nombres, @Apellidos, @Puesto, @Telefono, @CorreoElectronico, @FechaContratacion, @Salario, 1);

        SELECT SCOPE_IDENTITY() AS CodigoEmpleadoGenerado, 'Empleado registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar empleado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarEmpleado
Descripción  : Actualiza el puesto, sueldo o datos de un empleado.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarEmpleado
    @CodigoEmpleado INT,
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @Puesto VARCHAR(100),
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @FechaContratacion DATE,
    @Salario DECIMAL(12,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Empleados WHERE CodigoEmpleado = @CodigoEmpleado)
        BEGIN
            RAISERROR('El empleado a modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Empleados
        SET Nombres = @Nombres,
            Apellidos = @Apellidos,
            Puesto = @Puesto,
            Telefono = @Telefono,
            CorreoElectronico = @CorreoElectronico,
            FechaContratacion = @FechaContratacion,
            Salario = @Salario,
            Estado = @Estado
        WHERE CodigoEmpleado = @CodigoEmpleado;

        SELECT @CodigoEmpleado AS CodigoEmpleado, 'Empleado actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar empleado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarEmpleado
Descripción  : Desactiva a un empleado del sistema.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarEmpleado
    @CodigoEmpleado INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Empleados WHERE CodigoEmpleado = @CodigoEmpleado)
        BEGIN
            RAISERROR('El empleado a eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Empleados
        SET Estado = 0
        WHERE CodigoEmpleado = @CodigoEmpleado;

        SELECT @CodigoEmpleado AS CodigoEmpleado, 'Empleado desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar empleado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 3. TABLA: Tbl_Proveedores
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarProveedores
Descripción  : Obtiene la lista completa de proveedores turísticos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarProveedores
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoProveedor, NombreProveedor, TipoProveedor, Contacto, Telefono, CorreoElectronico, Direccion, Estado
        FROM Tbl_Proveedores
        ORDER BY CodigoProveedor DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar proveedores: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarProveedorPorId
Descripción  : Consulta un proveedor por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarProveedorPorId
    @CodigoProveedor INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoProveedor, NombreProveedor, TipoProveedor, Contacto, Telefono, CorreoElectronico, Direccion, Estado
        FROM Tbl_Proveedores
        WHERE CodigoProveedor = @CodigoProveedor;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarProveedor
Descripción  : Registra un proveedor (aerolíneas, operadores, transportes, etc.).
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarProveedor
    @NombreProveedor VARCHAR(150),
    @TipoProveedor VARCHAR(50),
    @Contacto VARCHAR(100) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @Direccion VARCHAR(250) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_Proveedores (NombreProveedor, TipoProveedor, Contacto, Telefono, CorreoElectronico, Direccion, Estado)
        VALUES (@NombreProveedor, @TipoProveedor, @Contacto, @Telefono, @CorreoElectronico, @Direccion, 1);

        SELECT SCOPE_IDENTITY() AS CodigoProveedorGenerado, 'Proveedor agregado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarProveedor
Descripción  : Modifica la información comercial de un proveedor.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarProveedor
    @CodigoProveedor INT,
    @NombreProveedor VARCHAR(150),
    @TipoProveedor VARCHAR(50),
    @Contacto VARCHAR(100) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @CorreoElectronico VARCHAR(150) = NULL,
    @Direccion VARCHAR(250) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor indicado a modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Proveedores
        SET NombreProveedor = @NombreProveedor,
            TipoProveedor = @TipoProveedor,
            Contacto = @Contacto,
            Telefono = @Telefono,
            CorreoElectronico = @CorreoElectronico,
            Direccion = @Direccion,
            Estado = @Estado
        WHERE CodigoProveedor = @CodigoProveedor;

        SELECT @CodigoProveedor AS CodigoProveedor, 'Proveedor actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarProveedor
Descripción  : Desactiva un proveedor del catálogo.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarProveedor
    @CodigoProveedor INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Proveedores
        SET Estado = 0
        WHERE CodigoProveedor = @CodigoProveedor;

        SELECT @CodigoProveedor AS CodigoProveedor, 'Proveedor desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 4. TABLA: Tbl_MetodosPago
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarMetodosPago
Descripción  : Obtiene los métodos de pago habilitados.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarMetodosPago
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoMetodoPago, NombreMetodoPago, Tipo, EntidadFinanciera, Descripcion, ComisionPorcentaje, Estado
        FROM Tbl_MetodosPago
        ORDER BY CodigoMetodoPago ASC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar métodos de pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarMetodoPagoPorId
Descripción  : Busca un método de pago por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarMetodoPagoPorId
    @CodigoMetodoPago INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_MetodosPago WHERE CodigoMetodoPago = @CodigoMetodoPago)
        BEGIN
            RAISERROR('El método de pago consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoMetodoPago, NombreMetodoPago, Tipo, EntidadFinanciera, Descripcion, ComisionPorcentaje, Estado
        FROM Tbl_MetodosPago
        WHERE CodigoMetodoPago = @CodigoMetodoPago;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el método de pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarMetodoPago
Descripción  : Inserta un nuevo método de pago.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarMetodoPago
    @NombreMetodoPago VARCHAR(100),
    @Tipo VARCHAR(50),
    @EntidadFinanciera VARCHAR(150) = NULL,
    @Descripcion VARCHAR(200) = NULL,
    @ComisionPorcentaje DECIMAL(6,2) = 0.00
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_MetodosPago (NombreMetodoPago, Tipo, EntidadFinanciera, Descripcion, ComisionPorcentaje, Estado)
        VALUES (@NombreMetodoPago, @Tipo, @EntidadFinanciera, @Descripcion, @ComisionPorcentaje, 1);

        SELECT SCOPE_IDENTITY() AS CodigoMetodoPagoGenerado, 'Método de pago registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el método de pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarMetodoPago
Descripción  : Modifica las condiciones de un método de pago.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarMetodoPago
    @CodigoMetodoPago INT,
    @NombreMetodoPago VARCHAR(100),
    @Tipo VARCHAR(50),
    @EntidadFinanciera VARCHAR(150) = NULL,
    @Descripcion VARCHAR(200) = NULL,
    @ComisionPorcentaje DECIMAL(6,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_MetodosPago WHERE CodigoMetodoPago = @CodigoMetodoPago)
        BEGIN
            RAISERROR('El método de pago a modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_MetodosPago
        SET NombreMetodoPago = @NombreMetodoPago,
            Tipo = @Tipo,
            EntidadFinanciera = @EntidadFinanciera,
            Descripcion = @Descripcion,
            ComisionPorcentaje = @ComisionPorcentaje,
            Estado = @Estado
        WHERE CodigoMetodoPago = @CodigoMetodoPago;

        SELECT @CodigoMetodoPago AS CodigoMetodoPago, 'Método de pago actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el método de pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarMetodoPago
Descripción  : Desactiva un método de pago.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarMetodoPago
    @CodigoMetodoPago INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_MetodosPago WHERE CodigoMetodoPago = @CodigoMetodoPago)
        BEGIN
            RAISERROR('El método de pago a eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_MetodosPago
        SET Estado = 0
        WHERE CodigoMetodoPago = @CodigoMetodoPago;

        SELECT @CodigoMetodoPago AS CodigoMetodoPago, 'Método de pago desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el método de pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 5. TABLA: Tbl_Destinos
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarDestinos
Descripción  : Lista todos los destinos turísticos configurados.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarDestinos
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoDestino, NombreDestino, Pais, Departamento, Ciudad, Descripcion, Imagen, Estado
        FROM Tbl_Destinos
        ORDER BY CodigoDestino DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar los destinos: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarDestinoPorId
Descripción  : Obtiene la información de un destino según su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarDestinoPorId
    @CodigoDestino INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino turístico consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoDestino, NombreDestino, Pais, Departamento, Ciudad, Descripcion, Imagen, Estado
        FROM Tbl_Destinos
        WHERE CodigoDestino = @CodigoDestino;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el destino: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarDestino
Descripción  : Registra un nuevo destino turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarDestino
    @NombreDestino VARCHAR(150),
    @Pais VARCHAR(100),
    @Departamento VARCHAR(100) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Descripcion VARCHAR(300) = NULL,
    @Imagen VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_Destinos (NombreDestino, Pais, Departamento, Ciudad, Descripcion, Imagen, Estado)
        VALUES (@NombreDestino, @Pais, @Departamento, @Ciudad, @Descripcion, @Imagen, 1);

        SELECT SCOPE_IDENTITY() AS CodigoDestinoGenerado, 'Destino agregado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el destino: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarDestino
Descripción  : Actualiza los detalles descriptivos y geográficos de un destino.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarDestino
    @CodigoDestino INT,
    @NombreDestino VARCHAR(150),
    @Pais VARCHAR(100),
    @Departamento VARCHAR(100) = NULL,
    @Ciudad VARCHAR(100) = NULL,
    @Descripcion VARCHAR(300) = NULL,
    @Imagen VARCHAR(300) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino que desea modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Destinos
        SET NombreDestino = @NombreDestino,
            Pais = @Pais,
            Departamento = @Departamento,
            Ciudad = @Ciudad,
            Descripcion = @Descripcion,
            Imagen = @Imagen,
            Estado = @Estado
        WHERE CodigoDestino = @CodigoDestino;

        SELECT @CodigoDestino AS CodigoDestino, 'Destino actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el destino: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarDestino
Descripción  : Desactiva un destino turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarDestino
    @CodigoDestino INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino a eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Destinos
        SET Estado = 0
        WHERE CodigoDestino = @CodigoDestino;

        SELECT @CodigoDestino AS CodigoDestino, 'Destino desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el destino: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 6. TABLA: Tbl_TiposTransporte
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarTiposTransporte
Descripción  : Consulta los tipos de vehículos y transportes de la agencia.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarTiposTransporte
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT CodigoTipoTransporte, NombreTipo, Descripcion, Capacidad, EquipajePermitido, Comodidades, Estado
        FROM Tbl_TiposTransporte
        ORDER BY CodigoTipoTransporte ASC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar tipos de transporte: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarTipoTransportePorId
Descripción  : Busca un medio de transporte según su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarTipoTransportePorId
    @CodigoTipoTransporte INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_TiposTransporte WHERE CodigoTipoTransporte = @CodigoTipoTransporte)
        BEGIN
            RAISERROR('El tipo de transporte indicado no existe.', 16, 1);
            RETURN;
        END

        SELECT CodigoTipoTransporte, NombreTipo, Descripcion, Capacidad, EquipajePermitido, Comodidades, Estado
        FROM Tbl_TiposTransporte
        WHERE CodigoTipoTransporte = @CodigoTipoTransporte;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar tipo de transporte: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarTipoTransporte
Descripción  : Inserta un nuevo tipo de transporte.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarTipoTransporte
    @NombreTipo VARCHAR(100),
    @Descripcion VARCHAR(200) = NULL,
    @Capacidad INT = 0,
    @EquipajePermitido VARCHAR(100) = NULL,
    @Comodidades VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO Tbl_TiposTransporte (NombreTipo, Descripcion, Capacidad, EquipajePermitido, Comodidades, Estado)
        VALUES (@NombreTipo, @Descripcion, @Capacidad, @EquipajePermitido, @Comodidades, 1);

        SELECT SCOPE_IDENTITY() AS CodigoTipoTransporteGenerado, 'Tipo de transporte registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar tipo de transporte: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarTipoTransporte
Descripción  : Modifica las características de un tipo de transporte.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarTipoTransporte
    @CodigoTipoTransporte INT,
    @NombreTipo VARCHAR(100),
    @Descripcion VARCHAR(200) = NULL,
    @Capacidad INT,
    @EquipajePermitido VARCHAR(100) = NULL,
    @Comodidades VARCHAR(200) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_TiposTransporte WHERE CodigoTipoTransporte = @CodigoTipoTransporte)
        BEGIN
            RAISERROR('El tipo de transporte a modificar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_TiposTransporte
        SET NombreTipo = @NombreTipo,
            Descripcion = @Descripcion,
            Capacidad = @Capacidad,
            EquipajePermitido = @EquipajePermitido,
            Comodidades = @Comodidades,
            Estado = @Estado
        WHERE CodigoTipoTransporte = @CodigoTipoTransporte;

        SELECT @CodigoTipoTransporte AS CodigoTipoTransporte, 'Tipo de transporte actualizado correctamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar tipo de transporte: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarTipoTransporte
Descripción  : Desactiva un tipo de transporte.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarTipoTransporte
    @CodigoTipoTransporte INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_TiposTransporte WHERE CodigoTipoTransporte = @CodigoTipoTransporte)
        BEGIN
            RAISERROR('El tipo de transporte a eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_TiposTransporte
        SET Estado = 0
        WHERE CodigoTipoTransporte = @CodigoTipoTransporte;

        SELECT @CodigoTipoTransporte AS CodigoTipoTransporte, 'Tipo de transporte desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el tipo de transporte: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 7. TABLA: Tbl_Hoteles
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarHoteles
Descripción  : Lista todos los hoteles asociados a destinos turísticos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarHoteles
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT h.CodigoHotel, h.CodigoDestino, d.NombreDestino, h.NombreHotel, h.Categoria, h.Direccion, h.Telefono, h.ServiciosIncluidos, h.Estado
        FROM Tbl_Hoteles h
        INNER JOIN Tbl_Destinos d ON h.CodigoDestino = d.CodigoDestino
        ORDER BY h.CodigoHotel DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar hoteles: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarHotelPorId
Descripción  : Retorna la ficha de un hotel por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarHotelPorId
    @CodigoHotel INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Hoteles WHERE CodigoHotel = @CodigoHotel)
        BEGIN
            RAISERROR('El hotel solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT h.CodigoHotel, h.CodigoDestino, d.NombreDestino, h.NombreHotel, h.Categoria, h.Direccion, h.Telefono, h.ServiciosIncluidos, h.Estado
        FROM Tbl_Hoteles h
        INNER JOIN Tbl_Destinos d ON h.CodigoDestino = d.CodigoDestino
        WHERE h.CodigoHotel = @CodigoHotel;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el hotel: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarHotel
Descripción  : Registra un hotel validando que el destino exista.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarHotel
    @CodigoDestino INT,
    @NombreHotel VARCHAR(150),
    @Categoria VARCHAR(20) = NULL,
    @Direccion VARCHAR(250) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @ServiciosIncluidos VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino seleccionado para el hotel no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Hoteles (CodigoDestino, NombreHotel, Categoria, Direccion, Telefono, ServiciosIncluidos, Estado)
        VALUES (@CodigoDestino, @NombreHotel, @Categoria, @Direccion, @Telefono, @ServiciosIncluidos, 1);

        SELECT SCOPE_IDENTITY() AS CodigoHotelGenerado, 'Hotel registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el hotel: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarHotel
Descripción  : Modifica las condiciones y datos de un hotel.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarHotel
    @CodigoHotel INT,
    @CodigoDestino INT,
    @NombreHotel VARCHAR(150),
    @Categoria VARCHAR(20) = NULL,
    @Direccion VARCHAR(250) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @ServiciosIncluidos VARCHAR(300) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Hoteles WHERE CodigoHotel = @CodigoHotel)
        BEGIN
            RAISERROR('El hotel a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino asociado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Hoteles
        SET CodigoDestino = @CodigoDestino,
            NombreHotel = @NombreHotel,
            Categoria = @Categoria,
            Direccion = @Direccion,
            Telefono = @Telefono,
            ServiciosIncluidos = @ServiciosIncluidos,
            Estado = @Estado
        WHERE CodigoHotel = @CodigoHotel;

        SELECT @CodigoHotel AS CodigoHotel, 'Hotel actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el hotel: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarHotel
Descripción  : Desactiva un hotel.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarHotel
    @CodigoHotel INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Hoteles WHERE CodigoHotel = @CodigoHotel)
        BEGIN
            RAISERROR('El hotel que desea desactivar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Hoteles
        SET Estado = 0
        WHERE CodigoHotel = @CodigoHotel;

        SELECT @CodigoHotel AS CodigoHotel, 'Hotel desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el hotel: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 8. TABLA: Tbl_Tours
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarTours
Descripción  : Lista todos los tours con información de su destino.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarTours
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT t.CodigoTour, t.CodigoDestino, d.NombreDestino, t.NombreTour, t.Descripcion, t.DuracionHoras, t.PrecioPorPersona, t.Incluye, t.Estado
        FROM Tbl_Tours t
        INNER JOIN Tbl_Destinos d ON t.CodigoDestino = d.CodigoDestino
        ORDER BY t.CodigoTour DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar los tours: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarTourPorId
Descripción  : Obtiene la información de un tour por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarTourPorId
    @CodigoTour INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Tours WHERE CodigoTour = @CodigoTour)
        BEGIN
            RAISERROR('El tour consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT t.CodigoTour, t.CodigoDestino, d.NombreDestino, t.NombreTour, t.Descripcion, t.DuracionHoras, t.PrecioPorPersona, t.Incluye, t.Estado
        FROM Tbl_Tours t
        INNER JOIN Tbl_Destinos d ON t.CodigoDestino = d.CodigoDestino
        WHERE t.CodigoTour = @CodigoTour;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el tour: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarTour
Descripción  : Registra una nueva actividad turística o excursión.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarTour
    @CodigoDestino INT,
    @NombreTour VARCHAR(150),
    @Descripcion VARCHAR(300) = NULL,
    @DuracionHoras INT = 0,
    @PrecioPorPersona DECIMAL(12,2) = 0.00,
    @Incluye VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino para el tour no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Tours (CodigoDestino, NombreTour, Descripcion, DuracionHoras, PrecioPorPersona, Incluye, Estado)
        VALUES (@CodigoDestino, @NombreTour, @Descripcion, @DuracionHoras, @PrecioPorPersona, @Incluye, 1);

        SELECT SCOPE_IDENTITY() AS CodigoTourGenerado, 'Tour registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el tour: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarTour
Descripción  : Actualiza el precio, duración o detalles de un tour.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarTour
    @CodigoTour INT,
    @CodigoDestino INT,
    @NombreTour VARCHAR(150),
    @Descripcion VARCHAR(300) = NULL,
    @DuracionHoras INT,
    @PrecioPorPersona DECIMAL(12,2),
    @Incluye VARCHAR(300) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Tours WHERE CodigoTour = @CodigoTour)
        BEGIN
            RAISERROR('El tour a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Destinos WHERE CodigoDestino = @CodigoDestino)
        BEGIN
            RAISERROR('El destino seleccionado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Tours
        SET CodigoDestino = @CodigoDestino,
            NombreTour = @NombreTour,
            Descripcion = @Descripcion,
            DuracionHoras = @DuracionHoras,
            PrecioPorPersona = @PrecioPorPersona,
            Incluye = @Incluye,
            Estado = @Estado
        WHERE CodigoTour = @CodigoTour;

        SELECT @CodigoTour AS CodigoTour, 'Tour actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el tour: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarTour
Descripción  : Desactiva un tour.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarTour
    @CodigoTour INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Tours WHERE CodigoTour = @CodigoTour)
        BEGIN
            RAISERROR('El tour que intenta eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Tours
        SET Estado = 0
        WHERE CodigoTour = @CodigoTour;

        SELECT @CodigoTour AS CodigoTour, 'Tour desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el tour: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 9. TABLA: Tbl_ServiciosProveedor
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarServiciosProveedor
Descripción  : Consulta los servicios ofrecidos por los distintos proveedores.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarServiciosProveedor
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT sp.CodigoServicioProv, sp.CodigoProveedor, p.NombreProveedor, sp.TipoServicio, sp.Descripcion, sp.PrecioReferencia, sp.Moneda, sp.Estado
        FROM Tbl_ServiciosProveedor sp
        INNER JOIN Tbl_Proveedores p ON sp.CodigoProveedor = p.CodigoProveedor
        ORDER BY sp.CodigoServicioProv DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar servicios de proveedores: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarServicioProveedorPorId
Descripción  : Busca un servicio de proveedor según su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarServicioProveedorPorId
    @CodigoServicioProv INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ServiciosProveedor WHERE CodigoServicioProv = @CodigoServicioProv)
        BEGIN
            RAISERROR('El servicio de proveedor consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT sp.CodigoServicioProv, sp.CodigoProveedor, p.NombreProveedor, sp.TipoServicio, sp.Descripcion, sp.PrecioReferencia, sp.Moneda, sp.Estado
        FROM Tbl_ServiciosProveedor sp
        INNER JOIN Tbl_Proveedores p ON sp.CodigoProveedor = p.CodigoProveedor
        WHERE sp.CodigoServicioProv = @CodigoServicioProv;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar servicio de proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarServicioProveedor
Descripción  : Inserta un nuevo servicio de catálogo para un proveedor.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarServicioProveedor
    @CodigoProveedor INT,
    @TipoServicio VARCHAR(50),
    @Descripcion VARCHAR(300) = NULL,
    @PrecioReferencia DECIMAL(12,2) = 0.00,
    @Moneda VARCHAR(10) = 'GTQ'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor asignado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_ServiciosProveedor (CodigoProveedor, TipoServicio, Descripcion, PrecioReferencia, Moneda, Estado)
        VALUES (@CodigoProveedor, @TipoServicio, @Descripcion, @PrecioReferencia, @Moneda, 1);

        SELECT SCOPE_IDENTITY() AS CodigoServicioProvGenerado, 'Servicio de proveedor registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al agregar servicio de proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarServicioProveedor
Descripción  : Modifica tarifas y características del servicio de un proveedor.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarServicioProveedor
    @CodigoServicioProv INT,
    @CodigoProveedor INT,
    @TipoServicio VARCHAR(50),
    @Descripcion VARCHAR(300) = NULL,
    @PrecioReferencia DECIMAL(12,2),
    @Moneda VARCHAR(10),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ServiciosProveedor WHERE CodigoServicioProv = @CodigoServicioProv)
        BEGIN
            RAISERROR('El servicio a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_ServiciosProveedor
        SET CodigoProveedor = @CodigoProveedor,
            TipoServicio = @TipoServicio,
            Descripcion = @Descripcion,
            PrecioReferencia = @PrecioReferencia,
            Moneda = @Moneda,
            Estado = @Estado
        WHERE CodigoServicioProv = @CodigoServicioProv;

        SELECT @CodigoServicioProv AS CodigoServicioProv, 'Servicio de proveedor actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar servicio de proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarServicioProveedor
Descripción  : Desactiva un servicio de proveedor.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarServicioProveedor
    @CodigoServicioProv INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ServiciosProveedor WHERE CodigoServicioProv = @CodigoServicioProv)
        BEGIN
            RAISERROR('El servicio que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_ServiciosProveedor
        SET Estado = 0
        WHERE CodigoServicioProv = @CodigoServicioProv;

        SELECT @CodigoServicioProv AS CodigoServicioProv, 'Servicio de proveedor desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el servicio de proveedor: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 10. TABLA: Tbl_Paquetes
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarPaquetes
Descripción  : Lista todos los paquetes turísticos armados y disponibles.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarPaquetes
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT p.CodigoPaquete, p.CodigoProveedor, pr.NombreProveedor, p.NombrePaquete, p.Descripcion, p.PrecioBase, p.DuracionDias, p.Incluye, p.Imagen, p.Estado
        FROM Tbl_Paquetes p
        INNER JOIN Tbl_Proveedores pr ON p.CodigoProveedor = pr.CodigoProveedor
        ORDER BY p.CodigoPaquete DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar los paquetes: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarPaquetePorId
Descripción  : Retorna la información completa de un paquete turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarPaquetePorId
    @CodigoPaquete INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete turístico solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT p.CodigoPaquete, p.CodigoProveedor, pr.NombreProveedor, p.NombrePaquete, p.Descripcion, p.PrecioBase, p.DuracionDias, p.Incluye, p.Imagen, p.Estado
        FROM Tbl_Paquetes p
        INNER JOIN Tbl_Proveedores pr ON p.CodigoProveedor = pr.CodigoProveedor
        WHERE p.CodigoPaquete = @CodigoPaquete;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarPaquete
Descripción  : Inserta un nuevo paquete turístico validando el proveedor.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarPaquete
    @CodigoProveedor INT,
    @NombrePaquete VARCHAR(150),
    @Descripcion VARCHAR(300) = NULL,
    @PrecioBase DECIMAL(12,2) = 0.00,
    @DuracionDias INT = 1,
    @Incluye VARCHAR(300) = NULL,
    @Imagen VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor asignado al paquete no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Paquetes (CodigoProveedor, NombrePaquete, Descripcion, PrecioBase, DuracionDias, Incluye, Imagen, Estado)
        VALUES (@CodigoProveedor, @NombrePaquete, @Descripcion, @PrecioBase, @DuracionDias, @Incluye, @Imagen, 1);

        SELECT SCOPE_IDENTITY() AS CodigoPaqueteGenerado, 'Paquete turístico agregado correctamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al agregar el paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarPaquete
Descripción  : Modifica tarifas, días o datos de un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarPaquete
    @CodigoPaquete INT,
    @CodigoProveedor INT,
    @NombrePaquete VARCHAR(150),
    @Descripcion VARCHAR(300) = NULL,
    @PrecioBase DECIMAL(12,2),
    @DuracionDias INT,
    @Incluye VARCHAR(300) = NULL,
    @Imagen VARCHAR(300) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Proveedores WHERE CodigoProveedor = @CodigoProveedor)
        BEGIN
            RAISERROR('El proveedor seleccionado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Paquetes
        SET CodigoProveedor = @CodigoProveedor,
            NombrePaquete = @NombrePaquete,
            Descripcion = @Descripcion,
            PrecioBase = @PrecioBase,
            DuracionDias = @DuracionDias,
            Incluye = @Incluye,
            Imagen = @Imagen,
            Estado = @Estado
        WHERE CodigoPaquete = @CodigoPaquete;

        SELECT @CodigoPaquete AS CodigoPaquete, 'Paquete actualizado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarPaquete
Descripción  : Desactiva un paquete turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarPaquete
    @CodigoPaquete INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete a eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Paquetes
        SET Estado = 0
        WHERE CodigoPaquete = @CodigoPaquete;

        SELECT @CodigoPaquete AS CodigoPaquete, 'Paquete desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 11. TABLA: Tbl_Itinerarios
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarItinerarios
Descripción  : Lista las actividades diarias asociadas a paquetes turísticos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarItinerarios
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT i.CodigoItinerario, i.CodigoPaquete, p.NombrePaquete, i.Dia, i.DescripcionActividad, i.Lugar, i.HoraInicio, i.HoraFin, i.Estado
        FROM Tbl_Itinerarios i
        INNER JOIN Tbl_Paquetes p ON i.CodigoPaquete = p.CodigoPaquete
        ORDER BY i.CodigoPaquete ASC, i.Dia ASC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar itinerarios: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarItinerarioPorId
Descripción  : Retorna una actividad del itinerario por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarItinerarioPorId
    @CodigoItinerario INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Itinerarios WHERE CodigoItinerario = @CodigoItinerario)
        BEGIN
            RAISERROR('El itinerario solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT i.CodigoItinerario, i.CodigoPaquete, p.NombrePaquete, i.Dia, i.DescripcionActividad, i.Lugar, i.HoraInicio, i.HoraFin, i.Estado
        FROM Tbl_Itinerarios i
        INNER JOIN Tbl_Paquetes p ON i.CodigoPaquete = p.CodigoPaquete
        WHERE i.CodigoItinerario = @CodigoItinerario;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el itinerario: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarItinerario
Descripción  : Registra una actividad al itinerario de un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarItinerario
    @CodigoPaquete INT,
    @Dia INT,
    @DescripcionActividad VARCHAR(300),
    @Lugar VARCHAR(150) = NULL,
    @HoraInicio TIME = NULL,
    @HoraFin TIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Itinerarios (CodigoPaquete, Dia, DescripcionActividad, Lugar, HoraInicio, HoraFin, Estado)
        VALUES (@CodigoPaquete, @Dia, @DescripcionActividad, @Lugar, @HoraInicio, @HoraFin, 1);

        SELECT SCOPE_IDENTITY() AS CodigoItinerarioGenerado, 'Actividad agregada al itinerario exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el itinerario: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarItinerario
Descripción  : Modifica una actividad o cronograma de un itinerario.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarItinerario
    @CodigoItinerario INT,
    @CodigoPaquete INT,
    @Dia INT,
    @DescripcionActividad VARCHAR(300),
    @Lugar VARCHAR(150) = NULL,
    @HoraInicio TIME = NULL,
    @HoraFin TIME = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Itinerarios WHERE CodigoItinerario = @CodigoItinerario)
        BEGIN
            RAISERROR('El itinerario a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete asociado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Itinerarios
        SET CodigoPaquete = @CodigoPaquete,
            Dia = @Dia,
            DescripcionActividad = @DescripcionActividad,
            Lugar = @Lugar,
            HoraInicio = @HoraInicio,
            HoraFin = @HoraFin,
            Estado = @Estado
        WHERE CodigoItinerario = @CodigoItinerario;

        SELECT @CodigoItinerario AS CodigoItinerario, 'Itinerario modificado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el itinerario: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarItinerario
Descripción  : Desactiva una actividad del itinerario.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarItinerario
    @CodigoItinerario INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Itinerarios WHERE CodigoItinerario = @CodigoItinerario)
        BEGIN
            RAISERROR('El itinerario que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Itinerarios
        SET Estado = 0
        WHERE CodigoItinerario = @CodigoItinerario;

        SELECT @CodigoItinerario AS CodigoItinerario, 'Itinerario desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el itinerario: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 12. TABLA: Tbl_PaqueteTours
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarPaqueteTours
Descripción  : Lista todos los tours vinculados a paquetes turísticos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarPaqueteTours
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT pt.CodigoPaqueteTour, pt.CodigoPaquete, p.NombrePaquete, pt.CodigoTour, t.NombreTour, pt.Incluido, pt.Orden, pt.Observaciones, pt.Estado
        FROM Tbl_PaqueteTours pt
        INNER JOIN Tbl_Paquetes p ON pt.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_Tours t ON pt.CodigoTour = t.CodigoTour
        ORDER BY pt.CodigoPaquete ASC, pt.Orden ASC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar tours de paquetes: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarPaqueteTourPorId
Descripción  : Busca un tour asignado a un paquete por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarPaqueteTourPorId
    @CodigoPaqueteTour INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteTours WHERE CodigoPaqueteTour = @CodigoPaqueteTour)
        BEGIN
            RAISERROR('El registro de tour en paquete solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT pt.CodigoPaqueteTour, pt.CodigoPaquete, p.NombrePaquete, pt.CodigoTour, t.NombreTour, pt.Incluido, pt.Orden, pt.Observaciones, pt.Estado
        FROM Tbl_PaqueteTours pt
        INNER JOIN Tbl_Paquetes p ON pt.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_Tours t ON pt.CodigoTour = t.CodigoTour
        WHERE pt.CodigoPaqueteTour = @CodigoPaqueteTour;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar tour de paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarPaqueteTour
Descripción  : Asocia un tour a un paquete turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarPaqueteTour
    @CodigoPaquete INT,
    @CodigoTour INT,
    @Incluido BIT = 1,
    @Orden INT = 1,
    @Observaciones VARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Tours WHERE CodigoTour = @CodigoTour)
        BEGIN
            RAISERROR('El tour indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_PaqueteTours (CodigoPaquete, CodigoTour, Incluido, Orden, Observaciones, Estado)
        VALUES (@CodigoPaquete, @CodigoTour, @Incluido, @Orden, @Observaciones, 1);

        SELECT SCOPE_IDENTITY() AS CodigoPaqueteTourGenerado, 'Tour asociado al paquete exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al asociar tour al paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarPaqueteTour
Descripción  : Modifica el orden u observaciones de un tour dentro del paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarPaqueteTour
    @CodigoPaqueteTour INT,
    @CodigoPaquete INT,
    @CodigoTour INT,
    @Incluido BIT,
    @Orden INT,
    @Observaciones VARCHAR(200) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteTours WHERE CodigoPaqueteTour = @CodigoPaqueteTour)
        BEGIN
            RAISERROR('El registro a editar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Tours WHERE CodigoTour = @CodigoTour)
        BEGIN
            RAISERROR('El tour no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_PaqueteTours
        SET CodigoPaquete = @CodigoPaquete,
            CodigoTour = @CodigoTour,
            Incluido = @Incluido,
            Orden = @Orden,
            Observaciones = @Observaciones,
            Estado = @Estado
        WHERE CodigoPaqueteTour = @CodigoPaqueteTour;

        SELECT @CodigoPaqueteTour AS CodigoPaqueteTour, 'Asociación de tour actualizada correctamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar tour del paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarPaqueteTour
Descripción  : Desactiva un tour de un paquete turístico.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarPaqueteTour
    @CodigoPaqueteTour INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteTours WHERE CodigoPaqueteTour = @CodigoPaqueteTour)
        BEGIN
            RAISERROR('El registro que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_PaqueteTours
        SET Estado = 0
        WHERE CodigoPaqueteTour = @CodigoPaqueteTour;

        SELECT @CodigoPaqueteTour AS CodigoPaqueteTour, 'Tour removido del paquete exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al remover tour del paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 13. TABLA: Tbl_PaqueteHoteles
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarPaqueteHoteles
Descripción  : Lista las opciones hoteleras asignadas a paquetes turísticos.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarPaqueteHoteles
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT ph.CodigoPaqueteHotel, ph.CodigoPaquete, p.NombrePaquete, ph.CodigoHotel, h.NombreHotel, ph.Noches, ph.TipoHabitacion, ph.IncluyeDesayuno, ph.PrecioPorNoche, ph.Estado
        FROM Tbl_PaqueteHoteles ph
        INNER JOIN Tbl_Paquetes p ON ph.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_Hoteles h ON ph.CodigoHotel = h.CodigoHotel
        ORDER BY ph.CodigoPaqueteHotel DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar hospedajes de paquetes: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarPaqueteHotelPorId
Descripción  : Consulta un hospedaje asignado a un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarPaqueteHotelPorId
    @CodigoPaqueteHotel INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteHoteles WHERE CodigoPaqueteHotel = @CodigoPaqueteHotel)
        BEGIN
            RAISERROR('El registro de hospedaje en paquete solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT ph.CodigoPaqueteHotel, ph.CodigoPaquete, p.NombrePaquete, ph.CodigoHotel, h.NombreHotel, ph.Noches, ph.TipoHabitacion, ph.IncluyeDesayuno, ph.PrecioPorNoche, ph.Estado
        FROM Tbl_PaqueteHoteles ph
        INNER JOIN Tbl_Paquetes p ON ph.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_Hoteles h ON ph.CodigoHotel = h.CodigoHotel
        WHERE ph.CodigoPaqueteHotel = @CodigoPaqueteHotel;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar hotel del paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarPaqueteHotel
Descripción  : Vincula un hotel, tipo de habitación y noches a un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarPaqueteHotel
    @CodigoPaquete INT,
    @CodigoHotel INT,
    @Noches INT = 1,
    @TipoHabitacion VARCHAR(100) = NULL,
    @IncluyeDesayuno BIT = 0,
    @PrecioPorNoche DECIMAL(12,2) = 0.00
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Hoteles WHERE CodigoHotel = @CodigoHotel)
        BEGIN
            RAISERROR('El hotel indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_PaqueteHoteles (CodigoPaquete, CodigoHotel, Noches, TipoHabitacion, IncluyeDesayuno, PrecioPorNoche, Estado)
        VALUES (@CodigoPaquete, @CodigoHotel, @Noches, @TipoHabitacion, @IncluyeDesayuno, @PrecioPorNoche, 1);

        SELECT SCOPE_IDENTITY() AS CodigoPaqueteHotelGenerado, 'Hotel asociado al paquete exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al asociar hotel al paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarPaqueteHotel
Descripción  : Modifica las noches o tarifa de hotel en un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarPaqueteHotel
    @CodigoPaqueteHotel INT,
    @CodigoPaquete INT,
    @CodigoHotel INT,
    @Noches INT,
    @TipoHabitacion VARCHAR(100) = NULL,
    @IncluyeDesayuno BIT,
    @PrecioPorNoche DECIMAL(12,2),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteHoteles WHERE CodigoPaqueteHotel = @CodigoPaqueteHotel)
        BEGIN
            RAISERROR('El hospedaje en paquete a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Hoteles WHERE CodigoHotel = @CodigoHotel)
        BEGIN
            RAISERROR('El hotel indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_PaqueteHoteles
        SET CodigoPaquete = @CodigoPaquete,
            CodigoHotel = @CodigoHotel,
            Noches = @Noches,
            TipoHabitacion = @TipoHabitacion,
            IncluyeDesayuno = @IncluyeDesayuno,
            PrecioPorNoche = @PrecioPorNoche,
            Estado = @Estado
        WHERE CodigoPaqueteHotel = @CodigoPaqueteHotel;

        SELECT @CodigoPaqueteHotel AS CodigoPaqueteHotel, 'Hospedaje del paquete actualizado correctamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar hotel del paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarPaqueteHotel
Descripción  : Desactiva un hotel asignado a un paquete.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarPaqueteHotel
    @CodigoPaqueteHotel INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_PaqueteHoteles WHERE CodigoPaqueteHotel = @CodigoPaqueteHotel)
        BEGIN
            RAISERROR('El hospedaje que intenta desactivar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_PaqueteHoteles
        SET Estado = 0
        WHERE CodigoPaqueteHotel = @CodigoPaqueteHotel;

        SELECT @CodigoPaqueteHotel AS CodigoPaqueteHotel, 'Hospedaje de paquete desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar hotel del paquete: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 14. TABLA: Tbl_Traslados
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarTraslados
Descripción  : Lista todos los traslados y logísticas de transporte terrestre/aéreo.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarTraslados
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT t.CodigoTraslado, t.CodigoPaquete, p.NombrePaquete, t.CodigoTipoTransporte, tt.NombreTipo AS TipoTransporte, t.Origen, t.Destino, t.FechaServicio, t.HoraSalida, t.HoraLlegada, t.Estado
        FROM Tbl_Traslados t
        INNER JOIN Tbl_Paquetes p ON t.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_TiposTransporte tt ON t.CodigoTipoTransporte = tt.CodigoTipoTransporte
        ORDER BY t.CodigoTraslado DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar traslados: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarTrasladoPorId
Descripción  : Retorna los detalles de un traslado por su código.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarTrasladoPorId
    @CodigoTraslado INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Traslados WHERE CodigoTraslado = @CodigoTraslado)
        BEGIN
            RAISERROR('El traslado solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT t.CodigoTraslado, t.CodigoPaquete, p.NombrePaquete, t.CodigoTipoTransporte, tt.NombreTipo AS TipoTransporte, t.Origen, t.Destino, t.FechaServicio, t.HoraSalida, t.HoraLlegada, t.Estado
        FROM Tbl_Traslados t
        INNER JOIN Tbl_Paquetes p ON t.CodigoPaquete = p.CodigoPaquete
        INNER JOIN Tbl_TiposTransporte tt ON t.CodigoTipoTransporte = tt.CodigoTipoTransporte
        WHERE t.CodigoTraslado = @CodigoTraslado;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el traslado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarTraslado
Descripción  : Inserta un traslado asociado a un paquete y a un medio de transporte.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarTraslado
    @CodigoPaquete INT,
    @CodigoTipoTransporte INT,
    @Origen VARCHAR(150),
    @Destino VARCHAR(150),
    @FechaServicio DATE,
    @HoraSalida TIME = NULL,
    @HoraLlegada TIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete turístico no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_TiposTransporte WHERE CodigoTipoTransporte = @CodigoTipoTransporte)
        BEGIN
            RAISERROR('El tipo de transporte no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Traslados (CodigoPaquete, CodigoTipoTransporte, Origen, Destino, FechaServicio, HoraSalida, HoraLlegada, Estado)
        VALUES (@CodigoPaquete, @CodigoTipoTransporte, @Origen, @Destino, @FechaServicio, @HoraSalida, @HoraLlegada, 1);

        SELECT SCOPE_IDENTITY() AS CodigoTrasladoGenerado, 'Traslado registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el traslado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarTraslado
Descripción  : Actualiza el origen, destino u horario de un traslado.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarTraslado
    @CodigoTraslado INT,
    @CodigoPaquete INT,
    @CodigoTipoTransporte INT,
    @Origen VARCHAR(150),
    @Destino VARCHAR(150),
    @FechaServicio DATE,
    @HoraSalida TIME = NULL,
    @HoraLlegada TIME = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Traslados WHERE CodigoTraslado = @CodigoTraslado)
        BEGIN
            RAISERROR('El traslado a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_TiposTransporte WHERE CodigoTipoTransporte = @CodigoTipoTransporte)
        BEGIN
            RAISERROR('El tipo de transporte indicado no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Traslados
        SET CodigoPaquete = @CodigoPaquete,
            CodigoTipoTransporte = @CodigoTipoTransporte,
            Origen = @Origen,
            Destino = @Destino,
            FechaServicio = @FechaServicio,
            HoraSalida = @HoraSalida,
            HoraLlegada = @HoraLlegada,
            Estado = @Estado
        WHERE CodigoTraslado = @CodigoTraslado;

        SELECT @CodigoTraslado AS CodigoTraslado, 'Traslado modificado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el traslado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarTraslado
Descripción  : Desactiva un traslado.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarTraslado
    @CodigoTraslado INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Traslados WHERE CodigoTraslado = @CodigoTraslado)
        BEGIN
            RAISERROR('El traslado que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Traslados
        SET Estado = 0
        WHERE CodigoTraslado = @CodigoTraslado;

        SELECT @CodigoTraslado AS CodigoTraslado, 'Traslado desactivado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el traslado: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 15. TABLA: Tbl_Reservas
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarReservas
Descripción  : Lista las reservaciones con nombres de clientes, empleados y paquetes.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarReservas
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT r.CodigoReserva, r.CodigoCliente, CONCAT(c.Nombres, ' ', c.Apellidos) AS Cliente, r.CodigoEmpleado, CONCAT(e.Nombres, ' ', e.Apellidos) AS Empleado, r.CodigoPaquete, p.NombrePaquete, r.FechaReserva, r.FechaViajeInicio, r.FechaViajeFin, r.EstadoReserva, r.Observaciones, r.Estado
        FROM Tbl_Reservas r
        INNER JOIN Tbl_Clientes c ON r.CodigoCliente = c.CodigoCliente
        INNER JOIN Tbl_Empleados e ON r.CodigoEmpleado = e.CodigoEmpleado
        INNER JOIN Tbl_Paquetes p ON r.CodigoPaquete = p.CodigoPaquete
        ORDER BY r.CodigoReserva DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar reservaciones: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarReservaPorId
Descripción  : Retorna los datos completos de una reserva específica.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarReservaPorId
    @CodigoReserva INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación solicitada no existe.', 16, 1);
            RETURN;
        END

        SELECT r.CodigoReserva, r.CodigoCliente, CONCAT(c.Nombres, ' ', c.Apellidos) AS Cliente, r.CodigoEmpleado, CONCAT(e.Nombres, ' ', e.Apellidos) AS Empleado, r.CodigoPaquete, p.NombrePaquete, r.FechaReserva, r.FechaViajeInicio, r.FechaViajeFin, r.EstadoReserva, r.Observaciones, r.Estado
        FROM Tbl_Reservas r
        INNER JOIN Tbl_Clientes c ON r.CodigoCliente = c.CodigoCliente
        INNER JOIN Tbl_Empleados e ON r.CodigoEmpleado = e.CodigoEmpleado
        INNER JOIN Tbl_Paquetes p ON r.CodigoPaquete = p.CodigoPaquete
        WHERE r.CodigoReserva = @CodigoReserva;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar la reservación: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarReserva
Descripción  : Registra una nueva reservación validando fechas y llaves foráneas.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarReserva
    @CodigoCliente INT,
    @CodigoEmpleado INT,
    @CodigoPaquete INT,
    @FechaViajeInicio DATE,
    @FechaViajeFin DATE,
    @EstadoReserva VARCHAR(20) = 'Pendiente',
    @Observaciones VARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Clientes WHERE CodigoCliente = @CodigoCliente)
        BEGIN
            RAISERROR('El cliente indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Empleados WHERE CodigoEmpleado = @CodigoEmpleado)
        BEGIN
            RAISERROR('El empleado indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete turístico indicado no existe.', 16, 1);
            RETURN;
        END

        IF @FechaViajeFin < @FechaViajeInicio
        BEGIN
            RAISERROR('La fecha de fin de viaje no puede ser anterior a la de inicio.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Reservas (CodigoCliente, CodigoEmpleado, CodigoPaquete, FechaReserva, FechaViajeInicio, FechaViajeFin, EstadoReserva, Observaciones, Estado)
        VALUES (@CodigoCliente, @CodigoEmpleado, @CodigoPaquete, GETDATE(), @FechaViajeInicio, @FechaViajeFin, @EstadoReserva, @Observaciones, 1);

        SELECT SCOPE_IDENTITY() AS CodigoReservaGenerada, 'Reservación registrada exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar la reservación: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarReserva
Descripción  : Modifica fechas, estado o asignación de una reserva.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarReserva
    @CodigoReserva INT,
    @CodigoCliente INT,
    @CodigoEmpleado INT,
    @CodigoPaquete INT,
    @FechaViajeInicio DATE,
    @FechaViajeFin DATE,
    @EstadoReserva VARCHAR(20),
    @Observaciones VARCHAR(300) = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Clientes WHERE CodigoCliente = @CodigoCliente)
        BEGIN
            RAISERROR('El cliente indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Empleados WHERE CodigoEmpleado = @CodigoEmpleado)
        BEGIN
            RAISERROR('El empleado indicado no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Paquetes WHERE CodigoPaquete = @CodigoPaquete)
        BEGIN
            RAISERROR('El paquete indicado no existe.', 16, 1);
            RETURN;
        END

        IF @FechaViajeFin < @FechaViajeInicio
        BEGIN
            RAISERROR('La fecha de fin de viaje no puede ser anterior a la de inicio.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Reservas
        SET CodigoCliente = @CodigoCliente,
            CodigoEmpleado = @CodigoEmpleado,
            CodigoPaquete = @CodigoPaquete,
            FechaViajeInicio = @FechaViajeInicio,
            FechaViajeFin = @FechaViajeFin,
            EstadoReserva = @EstadoReserva,
            Observaciones = @Observaciones,
            Estado = @Estado
        WHERE CodigoReserva = @CodigoReserva;

        SELECT @CodigoReserva AS CodigoReserva, 'Reservación actualizada correctamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar la reservación: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarReserva
Descripción  : Cancela o desactiva una reservación.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarReserva
    @CodigoReserva INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación que desea eliminar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Reservas
        SET Estado = 0, EstadoReserva = 'Cancelada'
        WHERE CodigoReserva = @CodigoReserva;

        SELECT @CodigoReserva AS CodigoReserva, 'Reservación cancelada/desactivada exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al cancelar la reservación: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 16. TABLA: Tbl_ReservaPersonas
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarReservaPersonas
Descripción  : Lista los pasajeros y acompañantes registrados en viajes.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarReservaPersonas
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT rp.CodigoReservaPersona, rp.CodigoReserva, rp.Nombres, rp.Apellidos, rp.DPI, rp.FechaNacimiento, rp.Parentesco, rp.Edad, rp.Estado
        FROM Tbl_ReservaPersonas rp
        ORDER BY rp.CodigoReservaPersona DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar personas de reserva: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarReservaPersonaPorId
Descripción  : Retorna la información de un pasajero por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarReservaPersonaPorId
    @CodigoReservaPersona INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ReservaPersonas WHERE CodigoReservaPersona = @CodigoReservaPersona)
        BEGIN
            RAISERROR('El pasajero consultado no existe.', 16, 1);
            RETURN;
        END

        SELECT rp.CodigoReservaPersona, rp.CodigoReserva, rp.Nombres, rp.Apellidos, rp.DPI, rp.FechaNacimiento, rp.Parentesco, rp.Edad, rp.Estado
        FROM Tbl_ReservaPersonas rp
        WHERE rp.CodigoReservaPersona = @CodigoReservaPersona;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el pasajero: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarReservaPersona
Descripción  : Registra un viajero al grupo de una reservación.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarReservaPersona
    @CodigoReserva INT,
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DPI VARCHAR(20) = NULL,
    @FechaNacimiento DATE = NULL,
    @Parentesco VARCHAR(50) = NULL,
    @Edad INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación a la que desea asociar el viajero no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_ReservaPersonas (CodigoReserva, Nombres, Apellidos, DPI, FechaNacimiento, Parentesco, Edad, Estado)
        VALUES (@CodigoReserva, @Nombres, @Apellidos, @DPI, @FechaNacimiento, @Parentesco, @Edad, 1);

        SELECT SCOPE_IDENTITY() AS CodigoReservaPersonaGenerado, 'Pasajero agregado a la reservación exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al agregar pasajero a la reservación: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarReservaPersona
Descripción  : Modifica los datos personales de un viajero.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarReservaPersona
    @CodigoReservaPersona INT,
    @CodigoReserva INT,
    @Nombres VARCHAR(100),
    @Apellidos VARCHAR(100),
    @DPI VARCHAR(20) = NULL,
    @FechaNacimiento DATE = NULL,
    @Parentesco VARCHAR(50) = NULL,
    @Edad INT = NULL,
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ReservaPersonas WHERE CodigoReservaPersona = @CodigoReservaPersona)
        BEGIN
            RAISERROR('El registro del pasajero no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación indicada no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_ReservaPersonas
        SET CodigoReserva = @CodigoReserva,
            Nombres = @Nombres,
            Apellidos = @Apellidos,
            DPI = @DPI,
            FechaNacimiento = @FechaNacimiento,
            Parentesco = @Parentesco,
            Edad = @Edad,
            Estado = @Estado
        WHERE CodigoReservaPersona = @CodigoReservaPersona;

        SELECT @CodigoReservaPersona AS CodigoReservaPersona, 'Datos del pasajero actualizados exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el pasajero: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarReservaPersona
Descripción  : Desactiva un pasajero de la reservación.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarReservaPersona
    @CodigoReservaPersona INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_ReservaPersonas WHERE CodigoReservaPersona = @CodigoReservaPersona)
        BEGIN
            RAISERROR('El pasajero que desea desactivar no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_ReservaPersonas
        SET Estado = 0
        WHERE CodigoReservaPersona = @CodigoReservaPersona;

        SELECT @CodigoReservaPersona AS CodigoReservaPersona, 'Pasajero desactivado de la reserva exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al desactivar el pasajero: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

-- ============================================================================
-- 17. TABLA: Tbl_Pagos
-- ============================================================================

/*
===============================================================================
Procedimiento: sp_ConsultarPagos
Descripción  : Lista todas las transacciones de pago registradas.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_ConsultarPagos
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT p.CodigoPago, p.CodigoReserva, p.CodigoMetodoPago, mp.NombreMetodoPago, p.FechaPago, p.Monto, p.Referencia, p.NumeroTransaccion, p.EstadoPago, p.Estado
        FROM Tbl_Pagos p
        INNER JOIN Tbl_Reservas r ON p.CodigoReserva = r.CodigoReserva
        INNER JOIN Tbl_MetodosPago mp ON p.CodigoMetodoPago = mp.CodigoMetodoPago
        ORDER BY p.CodigoPago DESC;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al consultar pagos: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_BuscarPagoPorId
Descripción  : Obtiene el detalle de un comprobante de pago por su ID.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_BuscarPagoPorId
    @CodigoPago INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Pagos WHERE CodigoPago = @CodigoPago)
        BEGIN
            RAISERROR('El pago solicitado no existe.', 16, 1);
            RETURN;
        END

        SELECT p.CodigoPago, p.CodigoReserva, p.CodigoMetodoPago, mp.NombreMetodoPago, p.FechaPago, p.Monto, p.Referencia, p.NumeroTransaccion, p.EstadoPago, p.Estado
        FROM Tbl_Pagos p
        INNER JOIN Tbl_Reservas r ON p.CodigoReserva = r.CodigoReserva
        INNER JOIN Tbl_MetodosPago mp ON p.CodigoMetodoPago = mp.CodigoMetodoPago
        WHERE p.CodigoPago = @CodigoPago;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al buscar el pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_AgregarPago
Descripción  : Registra un abono/pago a una reservación verificando el monto y FKs.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_AgregarPago
    @CodigoReserva INT,
    @CodigoMetodoPago INT,
    @Monto DECIMAL(12,2),
    @Referencia VARCHAR(100) = NULL,
    @NumeroTransaccion VARCHAR(100) = NULL,
    @EstadoPago VARCHAR(20) = 'Completado'
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación a la cual se abona el pago no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_MetodosPago WHERE CodigoMetodoPago = @CodigoMetodoPago)
        BEGIN
            RAISERROR('El método de pago especificado no existe.', 16, 1);
            RETURN;
        END

        IF @Monto <= 0
        BEGIN
            RAISERROR('El monto a abonar debe ser mayor a 0.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        INSERT INTO Tbl_Pagos (CodigoReserva, CodigoMetodoPago, FechaPago, Monto, Referencia, NumeroTransaccion, EstadoPago, Estado)
        VALUES (@CodigoReserva, @CodigoMetodoPago, GETDATE(), @Monto, @Referencia, @NumeroTransaccion, @EstadoPago, 1);

        SELECT SCOPE_IDENTITY() AS CodigoPagoGenerado, 'Pago registrado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al registrar el pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EditarPago
Descripción  : Rectifica datos de un pago registrado.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EditarPago
    @CodigoPago INT,
    @CodigoReserva INT,
    @CodigoMetodoPago INT,
    @Monto DECIMAL(12,2),
    @Referencia VARCHAR(100) = NULL,
    @NumeroTransaccion VARCHAR(100) = NULL,
    @EstadoPago VARCHAR(20),
    @Estado BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Pagos WHERE CodigoPago = @CodigoPago)
        BEGIN
            RAISERROR('El registro de pago a modificar no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_Reservas WHERE CodigoReserva = @CodigoReserva)
        BEGIN
            RAISERROR('La reservación asociada no existe.', 16, 1);
            RETURN;
        END

        IF NOT EXISTS (SELECT 1 FROM Tbl_MetodosPago WHERE CodigoMetodoPago = @CodigoMetodoPago)
        BEGIN
            RAISERROR('El método de pago no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Pagos
        SET CodigoReserva = @CodigoReserva,
            CodigoMetodoPago = @CodigoMetodoPago,
            Monto = @Monto,
            Referencia = @Referencia,
            NumeroTransaccion = @NumeroTransaccion,
            EstadoPago = @EstadoPago,
            Estado = @Estado
        WHERE CodigoPago = @CodigoPago;

        SELECT @CodigoPago AS CodigoPago, 'Pago modificado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al actualizar el pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO

/*
===============================================================================
Procedimiento: sp_EliminarPago
Descripción  : Anula o desactiva un registro de pago.
===============================================================================
*/
CREATE OR ALTER PROCEDURE sp_EliminarPago
    @CodigoPago INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Tbl_Pagos WHERE CodigoPago = @CodigoPago)
        BEGIN
            RAISERROR('El pago que intenta anular no existe.', 16, 1);
            RETURN;
        END

        BEGIN TRANSACTION;

        UPDATE Tbl_Pagos
        SET Estado = 0, EstadoPago = 'Anulado'
        WHERE CodigoPago = @CodigoPago;

        SELECT @CodigoPago AS CodigoPago, 'Pago anulado exitosamente.' AS Mensaje;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @ErrorMsg NVARCHAR(4000) = 'Error al anular el pago: ' + ERROR_MESSAGE();
        RAISERROR(@ErrorMsg, 16, 1);
    END CATCH
END;
GO