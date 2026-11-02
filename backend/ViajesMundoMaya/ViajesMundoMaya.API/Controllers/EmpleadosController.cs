using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmpleadosController : ControllerBase
	{
		private readonly EmpleadoNegocio empleadoNegocio;

		public EmpleadosController()
		{
			empleadoNegocio = new EmpleadoNegocio();
		}

		[HttpGet("ConsultarEmpleados")]
		public ActionResult<RespuestaApi<List<EmpleadoConsultaDTO>>> ConsultarEmpleados()
		{
			try
			{
				List<Empleado> lista = empleadoNegocio.ConsultarEmpleados();

				List<EmpleadoConsultaDTO> listaConsulta = lista.Select(item => new EmpleadoConsultaDTO
				{
					CodigoEmpleado = item.CodigoEmpleado,
					Nombres = item.Nombres,
					Apellidos = item.Apellidos,
					Puesto = item.Puesto,
					Telefono = item.Telefono,
					CorreoElectronico = item.CorreoElectronico,
					FechaContratacion = item.FechaContratacion,
					Salario = item.Salario,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<EmpleadoConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Empleados consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<EmpleadoConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar empleados.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarEmpleado")]
		public ActionResult<RespuestaApi<bool>> AgregarEmpleado([FromBody] EmpleadoAgregarDTO empleadoAgregarDTO)
		{
			try
			{
				Empleado entidad = new Empleado
				{
					Nombres = empleadoAgregarDTO.Nombres,
					Apellidos = empleadoAgregarDTO.Apellidos,
					Puesto = empleadoAgregarDTO.Puesto,
					Telefono = empleadoAgregarDTO.Telefono,
					CorreoElectronico = empleadoAgregarDTO.CorreoElectronico,
					FechaContratacion = empleadoAgregarDTO.FechaContratacion,
					Salario = empleadoAgregarDTO.Salario
				};

				bool resultado = empleadoNegocio.AgregarEmpleado(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Empleado agregado correctamente." : "No fue posible agregar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al agregar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}

		[HttpGet("BuscarEmpleado/{codigoEmpleado:int}")]
		public ActionResult<RespuestaApi<EmpleadoConsultaDTO>> BuscarEmpleado(int codigoEmpleado)
		{
			try
			{
				Empleado? entidad = empleadoNegocio.BuscarEmpleado(codigoEmpleado);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<EmpleadoConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				EmpleadoConsultaDTO dto = new EmpleadoConsultaDTO
				{
					CodigoEmpleado = entidad.CodigoEmpleado,
					Nombres = entidad.Nombres,
					Apellidos = entidad.Apellidos,
					Puesto = entidad.Puesto,
					Telefono = entidad.Telefono,
					CorreoElectronico = entidad.CorreoElectronico,
					FechaContratacion = entidad.FechaContratacion,
					Salario = entidad.Salario,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<EmpleadoConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<EmpleadoConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarEmpleado")]
		public ActionResult<RespuestaApi<bool>> EditarEmpleado([FromBody] EmpleadoEditarDTO empleadoEditarDTO)
		{
			try
			{
				Empleado entidad = new Empleado
				{
					CodigoEmpleado = empleadoEditarDTO.CodigoEmpleado,
					Nombres = empleadoEditarDTO.Nombres,
					Apellidos = empleadoEditarDTO.Apellidos,
					Puesto = empleadoEditarDTO.Puesto,
					Telefono = empleadoEditarDTO.Telefono,
					CorreoElectronico = empleadoEditarDTO.CorreoElectronico,
					FechaContratacion = empleadoEditarDTO.FechaContratacion,
					Salario = empleadoEditarDTO.Salario,
					Estado = empleadoEditarDTO.Estado
				};

				bool resultado = empleadoNegocio.EditarEmpleado(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Empleado editado correctamente." : "No fue posible editar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al editar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}

		[HttpDelete("EliminarEmpleado/{codigoEmpleado:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarEmpleado(int codigoEmpleado)
		{
			try
			{
				bool resultado = empleadoNegocio.EliminarEmpleado(codigoEmpleado);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Empleado eliminado correctamente." : "No fue posible eliminar el registro.",
					Datos = resultado,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<bool>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al eliminar el registro.",
					Datos = false,
					Detalle = ex.Message
				});
			}
		}
	}
}
