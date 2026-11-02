using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaquetesController : ControllerBase
	{
		private readonly PaqueteNegocio paqueteNegocio;

		public PaquetesController()
		{
			paqueteNegocio = new PaqueteNegocio();
		}

		[HttpGet("ConsultarPaquetes")]
		public ActionResult<RespuestaApi<List<PaqueteConsultaDTO>>> ConsultarPaquetes()
		{
			try
			{
				List<Paquete> lista = paqueteNegocio.ConsultarPaquetes();

				List<PaqueteConsultaDTO> listaConsulta = lista.Select(item => new PaqueteConsultaDTO
				{
					CodigoPaquete = item.CodigoPaquete,
					CodigoProveedor = item.CodigoProveedor,
					NombrePaquete = item.NombrePaquete,
					Descripcion = item.Descripcion,
					PrecioBase = item.PrecioBase,
					DuracionDias = item.DuracionDias,
					Incluye = item.Incluye,
					Imagen = item.Imagen,
					NombreProveedor = item.NombreProveedor,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<PaqueteConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Paquetes consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<PaqueteConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar paquetes.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarPaquete")]
		public ActionResult<RespuestaApi<bool>> AgregarPaquete([FromBody] PaqueteAgregarDTO paqueteAgregarDTO)
		{
			try
			{
				Paquete entidad = new Paquete
				{
					CodigoProveedor = paqueteAgregarDTO.CodigoProveedor,
					NombrePaquete = paqueteAgregarDTO.NombrePaquete,
					Descripcion = paqueteAgregarDTO.Descripcion,
					PrecioBase = paqueteAgregarDTO.PrecioBase,
					DuracionDias = paqueteAgregarDTO.DuracionDias,
					Incluye = paqueteAgregarDTO.Incluye,
					Imagen = paqueteAgregarDTO.Imagen
				};

				bool resultado = paqueteNegocio.AgregarPaquete(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Paquete agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarPaquete/{codigoPaquete:int}")]
		public ActionResult<RespuestaApi<PaqueteConsultaDTO>> BuscarPaquete(int codigoPaquete)
		{
			try
			{
				Paquete? entidad = paqueteNegocio.BuscarPaquete(codigoPaquete);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<PaqueteConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				PaqueteConsultaDTO dto = new PaqueteConsultaDTO
				{
					CodigoPaquete = entidad.CodigoPaquete,
					CodigoProveedor = entidad.CodigoProveedor,
					NombrePaquete = entidad.NombrePaquete,
					Descripcion = entidad.Descripcion,
					PrecioBase = entidad.PrecioBase,
					DuracionDias = entidad.DuracionDias,
					Incluye = entidad.Incluye,
					Imagen = entidad.Imagen,
					NombreProveedor = entidad.NombreProveedor,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<PaqueteConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<PaqueteConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarPaquete")]
		public ActionResult<RespuestaApi<bool>> EditarPaquete([FromBody] PaqueteEditarDTO paqueteEditarDTO)
		{
			try
			{
				Paquete entidad = new Paquete
				{
					CodigoPaquete = paqueteEditarDTO.CodigoPaquete,
					CodigoProveedor = paqueteEditarDTO.CodigoProveedor,
					NombrePaquete = paqueteEditarDTO.NombrePaquete,
					Descripcion = paqueteEditarDTO.Descripcion,
					PrecioBase = paqueteEditarDTO.PrecioBase,
					DuracionDias = paqueteEditarDTO.DuracionDias,
					Incluye = paqueteEditarDTO.Incluye,
					Imagen = paqueteEditarDTO.Imagen,
					Estado = paqueteEditarDTO.Estado
				};

				bool resultado = paqueteNegocio.EditarPaquete(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Paquete editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarPaquete/{codigoPaquete:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarPaquete(int codigoPaquete)
		{
			try
			{
				bool resultado = paqueteNegocio.EliminarPaquete(codigoPaquete);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Paquete eliminado correctamente." : "No fue posible eliminar el registro.",
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
