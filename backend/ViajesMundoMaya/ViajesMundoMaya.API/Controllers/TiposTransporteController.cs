using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TiposTransporteController : ControllerBase
	{
		private readonly TipoTransporteNegocio tipoTransporteNegocio;

		public TiposTransporteController()
		{
			tipoTransporteNegocio = new TipoTransporteNegocio();
		}

		[HttpGet("ConsultarTiposTransporte")]
		public ActionResult<RespuestaApi<List<TipoTransporteConsultaDTO>>> ConsultarTiposTransporte()
		{
			try
			{
				List<TipoTransporte> lista = tipoTransporteNegocio.ConsultarTiposTransporte();

				List<TipoTransporteConsultaDTO> listaConsulta = lista.Select(item => new TipoTransporteConsultaDTO
				{
					CodigoTipoTransporte = item.CodigoTipoTransporte,
					NombreTipo = item.NombreTipo,
					Descripcion = item.Descripcion,
					Capacidad = item.Capacidad,
					EquipajePermitido = item.EquipajePermitido,
					Comodidades = item.Comodidades,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<TipoTransporteConsultaDTO>>
				{
					Exito = true,
					Mensaje = "TiposTransporte consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<TipoTransporteConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar tipostransporte.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarTipoTransporte")]
		public ActionResult<RespuestaApi<bool>> AgregarTipoTransporte([FromBody] TipoTransporteAgregarDTO tipoTransporteAgregarDTO)
		{
			try
			{
				TipoTransporte entidad = new TipoTransporte
				{
					NombreTipo = tipoTransporteAgregarDTO.NombreTipo,
					Descripcion = tipoTransporteAgregarDTO.Descripcion,
					Capacidad = tipoTransporteAgregarDTO.Capacidad,
					EquipajePermitido = tipoTransporteAgregarDTO.EquipajePermitido,
					Comodidades = tipoTransporteAgregarDTO.Comodidades
				};

				bool resultado = tipoTransporteNegocio.AgregarTipoTransporte(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "TipoTransporte agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarTipoTransporte/{codigoTipoTransporte:int}")]
		public ActionResult<RespuestaApi<TipoTransporteConsultaDTO>> BuscarTipoTransporte(int codigoTipoTransporte)
		{
			try
			{
				TipoTransporte? entidad = tipoTransporteNegocio.BuscarTipoTransporte(codigoTipoTransporte);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<TipoTransporteConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				TipoTransporteConsultaDTO dto = new TipoTransporteConsultaDTO
				{
					CodigoTipoTransporte = entidad.CodigoTipoTransporte,
					NombreTipo = entidad.NombreTipo,
					Descripcion = entidad.Descripcion,
					Capacidad = entidad.Capacidad,
					EquipajePermitido = entidad.EquipajePermitido,
					Comodidades = entidad.Comodidades,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<TipoTransporteConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<TipoTransporteConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarTipoTransporte")]
		public ActionResult<RespuestaApi<bool>> EditarTipoTransporte([FromBody] TipoTransporteEditarDTO tipoTransporteEditarDTO)
		{
			try
			{
				TipoTransporte entidad = new TipoTransporte
				{
					CodigoTipoTransporte = tipoTransporteEditarDTO.CodigoTipoTransporte,
					NombreTipo = tipoTransporteEditarDTO.NombreTipo,
					Descripcion = tipoTransporteEditarDTO.Descripcion,
					Capacidad = tipoTransporteEditarDTO.Capacidad,
					EquipajePermitido = tipoTransporteEditarDTO.EquipajePermitido,
					Comodidades = tipoTransporteEditarDTO.Comodidades,
					Estado = tipoTransporteEditarDTO.Estado
				};

				bool resultado = tipoTransporteNegocio.EditarTipoTransporte(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "TipoTransporte editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarTipoTransporte/{codigoTipoTransporte:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarTipoTransporte(int codigoTipoTransporte)
		{
			try
			{
				bool resultado = tipoTransporteNegocio.EliminarTipoTransporte(codigoTipoTransporte);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "TipoTransporte eliminado correctamente." : "No fue posible eliminar el registro.",
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
