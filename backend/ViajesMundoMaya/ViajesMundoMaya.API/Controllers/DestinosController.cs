using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DestinosController : ControllerBase
	{
		private readonly DestinoNegocio destinoNegocio;

		public DestinosController()
		{
			destinoNegocio = new DestinoNegocio();
		}

		[HttpGet("ConsultarDestinos")]
		public ActionResult<RespuestaApi<List<DestinoConsultaDTO>>> ConsultarDestinos()
		{
			try
			{
				List<Destino> lista = destinoNegocio.ConsultarDestinos();

				List<DestinoConsultaDTO> listaConsulta = lista.Select(item => new DestinoConsultaDTO
				{
					CodigoDestino = item.CodigoDestino,
					NombreDestino = item.NombreDestino,
					Pais = item.Pais,
					Departamento = item.Departamento,
					Ciudad = item.Ciudad,
					Descripcion = item.Descripcion,
					Imagen = item.Imagen,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<DestinoConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Destinos consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<DestinoConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar destinos.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarDestino")]
		public ActionResult<RespuestaApi<bool>> AgregarDestino([FromBody] DestinoAgregarDTO destinoAgregarDTO)
		{
			try
			{
				Destino entidad = new Destino
				{
					NombreDestino = destinoAgregarDTO.NombreDestino,
					Pais = destinoAgregarDTO.Pais,
					Departamento = destinoAgregarDTO.Departamento,
					Ciudad = destinoAgregarDTO.Ciudad,
					Descripcion = destinoAgregarDTO.Descripcion,
					Imagen = destinoAgregarDTO.Imagen
				};

				bool resultado = destinoNegocio.AgregarDestino(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Destino agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarDestino/{codigoDestino:int}")]
		public ActionResult<RespuestaApi<DestinoConsultaDTO>> BuscarDestino(int codigoDestino)
		{
			try
			{
				Destino? entidad = destinoNegocio.BuscarDestino(codigoDestino);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<DestinoConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				DestinoConsultaDTO dto = new DestinoConsultaDTO
				{
					CodigoDestino = entidad.CodigoDestino,
					NombreDestino = entidad.NombreDestino,
					Pais = entidad.Pais,
					Departamento = entidad.Departamento,
					Ciudad = entidad.Ciudad,
					Descripcion = entidad.Descripcion,
					Imagen = entidad.Imagen,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<DestinoConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<DestinoConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarDestino")]
		public ActionResult<RespuestaApi<bool>> EditarDestino([FromBody] DestinoEditarDTO destinoEditarDTO)
		{
			try
			{
				Destino entidad = new Destino
				{
					CodigoDestino = destinoEditarDTO.CodigoDestino,
					NombreDestino = destinoEditarDTO.NombreDestino,
					Pais = destinoEditarDTO.Pais,
					Departamento = destinoEditarDTO.Departamento,
					Ciudad = destinoEditarDTO.Ciudad,
					Descripcion = destinoEditarDTO.Descripcion,
					Imagen = destinoEditarDTO.Imagen,
					Estado = destinoEditarDTO.Estado
				};

				bool resultado = destinoNegocio.EditarDestino(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Destino editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarDestino/{codigoDestino:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarDestino(int codigoDestino)
		{
			try
			{
				bool resultado = destinoNegocio.EliminarDestino(codigoDestino);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Destino eliminado correctamente." : "No fue posible eliminar el registro.",
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
