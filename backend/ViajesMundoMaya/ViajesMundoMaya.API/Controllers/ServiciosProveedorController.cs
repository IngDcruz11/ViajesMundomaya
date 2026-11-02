using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ServiciosProveedorController : ControllerBase
	{
		private readonly ServicioProveedorNegocio servicioProveedorNegocio;

		public ServiciosProveedorController()
		{
			servicioProveedorNegocio = new ServicioProveedorNegocio();
		}

		[HttpGet("ConsultarServiciosProveedor")]
		public ActionResult<RespuestaApi<List<ServicioProveedorConsultaDTO>>> ConsultarServiciosProveedor()
		{
			try
			{
				List<ServicioProveedor> lista = servicioProveedorNegocio.ConsultarServiciosProveedor();

				List<ServicioProveedorConsultaDTO> listaConsulta = lista.Select(item => new ServicioProveedorConsultaDTO
				{
					CodigoServicioProv = item.CodigoServicioProv,
					CodigoProveedor = item.CodigoProveedor,
					TipoServicio = item.TipoServicio,
					Descripcion = item.Descripcion,
					PrecioReferencia = item.PrecioReferencia,
					Moneda = item.Moneda,
					NombreProveedor = item.NombreProveedor,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ServicioProveedorConsultaDTO>>
				{
					Exito = true,
					Mensaje = "ServiciosProveedor consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ServicioProveedorConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar serviciosproveedor.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarServicioProveedor")]
		public ActionResult<RespuestaApi<bool>> AgregarServicioProveedor([FromBody] ServicioProveedorAgregarDTO servicioProveedorAgregarDTO)
		{
			try
			{
				ServicioProveedor entidad = new ServicioProveedor
				{
					CodigoProveedor = servicioProveedorAgregarDTO.CodigoProveedor,
					TipoServicio = servicioProveedorAgregarDTO.TipoServicio,
					Descripcion = servicioProveedorAgregarDTO.Descripcion,
					PrecioReferencia = servicioProveedorAgregarDTO.PrecioReferencia,
					Moneda = servicioProveedorAgregarDTO.Moneda
				};

				bool resultado = servicioProveedorNegocio.AgregarServicioProveedor(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ServicioProveedor agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarServicioProveedor/{codigoServicioProv:int}")]
		public ActionResult<RespuestaApi<ServicioProveedorConsultaDTO>> BuscarServicioProveedor(int codigoServicioProv)
		{
			try
			{
				ServicioProveedor? entidad = servicioProveedorNegocio.BuscarServicioProveedor(codigoServicioProv);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ServicioProveedorConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ServicioProveedorConsultaDTO dto = new ServicioProveedorConsultaDTO
				{
					CodigoServicioProv = entidad.CodigoServicioProv,
					CodigoProveedor = entidad.CodigoProveedor,
					TipoServicio = entidad.TipoServicio,
					Descripcion = entidad.Descripcion,
					PrecioReferencia = entidad.PrecioReferencia,
					Moneda = entidad.Moneda,
					NombreProveedor = entidad.NombreProveedor,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ServicioProveedorConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ServicioProveedorConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarServicioProveedor")]
		public ActionResult<RespuestaApi<bool>> EditarServicioProveedor([FromBody] ServicioProveedorEditarDTO servicioProveedorEditarDTO)
		{
			try
			{
				ServicioProveedor entidad = new ServicioProveedor
				{
					CodigoServicioProv = servicioProveedorEditarDTO.CodigoServicioProv,
					CodigoProveedor = servicioProveedorEditarDTO.CodigoProveedor,
					TipoServicio = servicioProveedorEditarDTO.TipoServicio,
					Descripcion = servicioProveedorEditarDTO.Descripcion,
					PrecioReferencia = servicioProveedorEditarDTO.PrecioReferencia,
					Moneda = servicioProveedorEditarDTO.Moneda,
					Estado = servicioProveedorEditarDTO.Estado
				};

				bool resultado = servicioProveedorNegocio.EditarServicioProveedor(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ServicioProveedor editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarServicioProveedor/{codigoServicioProv:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarServicioProveedor(int codigoServicioProv)
		{
			try
			{
				bool resultado = servicioProveedorNegocio.EliminarServicioProveedor(codigoServicioProv);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "ServicioProveedor eliminado correctamente." : "No fue posible eliminar el registro.",
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
