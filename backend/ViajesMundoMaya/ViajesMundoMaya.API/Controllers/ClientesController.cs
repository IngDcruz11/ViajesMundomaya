using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClientesController : ControllerBase
	{
		private readonly ClienteNegocio clienteNegocio;

		public ClientesController()
		{
			clienteNegocio = new ClienteNegocio();
		}

		[HttpGet("ConsultarClientes")]
		public ActionResult<RespuestaApi<List<ClienteConsultaDTO>>> ConsultarClientes()
		{
			try
			{
				List<Cliente> lista = clienteNegocio.ConsultarClientes();

				List<ClienteConsultaDTO> listaConsulta = lista.Select(item => new ClienteConsultaDTO
				{
					CodigoCliente = item.CodigoCliente,
					Nombres = item.Nombres,
					Apellidos = item.Apellidos,
					DPI = item.DPI,
					Telefono = item.Telefono,
					CorreoElectronico = item.CorreoElectronico,
					Direccion = item.Direccion,
					FechaRegistro = item.FechaRegistro,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ClienteConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Clientes consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ClienteConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar clientes.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarCliente")]
		public ActionResult<RespuestaApi<bool>> AgregarCliente([FromBody] ClienteAgregarDTO clienteAgregarDTO)
		{
			try
			{
				Cliente entidad = new Cliente
				{
					Nombres = clienteAgregarDTO.Nombres,
					Apellidos = clienteAgregarDTO.Apellidos,
					DPI = clienteAgregarDTO.DPI,
					Telefono = clienteAgregarDTO.Telefono,
					CorreoElectronico = clienteAgregarDTO.CorreoElectronico,
					Direccion = clienteAgregarDTO.Direccion
				};

				bool resultado = clienteNegocio.AgregarCliente(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Cliente agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarCliente/{codigoCliente:int}")]
		public ActionResult<RespuestaApi<ClienteConsultaDTO>> BuscarCliente(int codigoCliente)
		{
			try
			{
				Cliente? entidad = clienteNegocio.BuscarCliente(codigoCliente);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ClienteConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ClienteConsultaDTO dto = new ClienteConsultaDTO
				{
					CodigoCliente = entidad.CodigoCliente,
					Nombres = entidad.Nombres,
					Apellidos = entidad.Apellidos,
					DPI = entidad.DPI,
					Telefono = entidad.Telefono,
					CorreoElectronico = entidad.CorreoElectronico,
					Direccion = entidad.Direccion,
					FechaRegistro = entidad.FechaRegistro,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ClienteConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ClienteConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarCliente")]
		public ActionResult<RespuestaApi<bool>> EditarCliente([FromBody] ClienteEditarDTO clienteEditarDTO)
		{
			try
			{
				Cliente entidad = new Cliente
				{
					CodigoCliente = clienteEditarDTO.CodigoCliente,
					Nombres = clienteEditarDTO.Nombres,
					Apellidos = clienteEditarDTO.Apellidos,
					DPI = clienteEditarDTO.DPI,
					Telefono = clienteEditarDTO.Telefono,
					CorreoElectronico = clienteEditarDTO.CorreoElectronico,
					Direccion = clienteEditarDTO.Direccion,
					Estado = clienteEditarDTO.Estado
				};

				bool resultado = clienteNegocio.EditarCliente(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Cliente editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarCliente/{codigoCliente:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarCliente(int codigoCliente)
		{
			try
			{
				bool resultado = clienteNegocio.EliminarCliente(codigoCliente);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Cliente eliminado correctamente." : "No fue posible eliminar el registro.",
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
