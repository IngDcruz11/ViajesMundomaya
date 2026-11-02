using Microsoft.AspNetCore.Mvc;
using ViajesMundoMaya.DTO.DTO;
using ViajesMundoMaya.Entidades.Entidades;
using ViajesMundoMaya.Negocio.Servicios;

namespace ViajesMundoMaya.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProveedoresController : ControllerBase
	{
		private readonly ProveedorNegocio proveedorNegocio;

		public ProveedoresController()
		{
			proveedorNegocio = new ProveedorNegocio();
		}

		[HttpGet("ConsultarProveedores")]
		public ActionResult<RespuestaApi<List<ProveedorConsultaDTO>>> ConsultarProveedores()
		{
			try
			{
				List<Proveedor> lista = proveedorNegocio.ConsultarProveedores();

				List<ProveedorConsultaDTO> listaConsulta = lista.Select(item => new ProveedorConsultaDTO
				{
					CodigoProveedor = item.CodigoProveedor,
					NombreProveedor = item.NombreProveedor,
					TipoProveedor = item.TipoProveedor,
					Contacto = item.Contacto,
					Telefono = item.Telefono,
					CorreoElectronico = item.CorreoElectronico,
					Direccion = item.Direccion,
					Estado = item.Estado
				}).ToList();

				return Ok(new RespuestaApi<List<ProveedorConsultaDTO>>
				{
					Exito = true,
					Mensaje = "Proveedores consultados correctamente.",
					Datos = listaConsulta,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<List<ProveedorConsultaDTO>>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al consultar proveedores.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPost("AgregarProveedor")]
		public ActionResult<RespuestaApi<bool>> AgregarProveedor([FromBody] ProveedorAgregarDTO proveedorAgregarDTO)
		{
			try
			{
				Proveedor entidad = new Proveedor
				{
					NombreProveedor = proveedorAgregarDTO.NombreProveedor,
					TipoProveedor = proveedorAgregarDTO.TipoProveedor,
					Contacto = proveedorAgregarDTO.Contacto,
					Telefono = proveedorAgregarDTO.Telefono,
					CorreoElectronico = proveedorAgregarDTO.CorreoElectronico,
					Direccion = proveedorAgregarDTO.Direccion
				};

				bool resultado = proveedorNegocio.AgregarProveedor(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Proveedor agregado correctamente." : "No fue posible agregar el registro.",
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

		[HttpGet("BuscarProveedor/{codigoProveedor:int}")]
		public ActionResult<RespuestaApi<ProveedorConsultaDTO>> BuscarProveedor(int codigoProveedor)
		{
			try
			{
				Proveedor? entidad = proveedorNegocio.BuscarProveedor(codigoProveedor);

				if (entidad == null)
				{
					return NotFound(new RespuestaApi<ProveedorConsultaDTO>
					{
						Exito = false,
						Mensaje = "No se encontró el registro solicitado.",
						Datos = null,
						Detalle = null
					});
				}

				ProveedorConsultaDTO dto = new ProveedorConsultaDTO
				{
					CodigoProveedor = entidad.CodigoProveedor,
					NombreProveedor = entidad.NombreProveedor,
					TipoProveedor = entidad.TipoProveedor,
					Contacto = entidad.Contacto,
					Telefono = entidad.Telefono,
					CorreoElectronico = entidad.CorreoElectronico,
					Direccion = entidad.Direccion,
					Estado = entidad.Estado
				};

				return Ok(new RespuestaApi<ProveedorConsultaDTO>
				{
					Exito = true,
					Mensaje = "Registro encontrado correctamente.",
					Datos = dto,
					Detalle = null
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new RespuestaApi<ProveedorConsultaDTO>
				{
					Exito = false,
					Mensaje = "Ocurrió un error al buscar el registro.",
					Datos = null,
					Detalle = ex.Message
				});
			}
		}

		[HttpPut("EditarProveedor")]
		public ActionResult<RespuestaApi<bool>> EditarProveedor([FromBody] ProveedorEditarDTO proveedorEditarDTO)
		{
			try
			{
				Proveedor entidad = new Proveedor
				{
					CodigoProveedor = proveedorEditarDTO.CodigoProveedor,
					NombreProveedor = proveedorEditarDTO.NombreProveedor,
					TipoProveedor = proveedorEditarDTO.TipoProveedor,
					Contacto = proveedorEditarDTO.Contacto,
					Telefono = proveedorEditarDTO.Telefono,
					CorreoElectronico = proveedorEditarDTO.CorreoElectronico,
					Direccion = proveedorEditarDTO.Direccion,
					Estado = proveedorEditarDTO.Estado
				};

				bool resultado = proveedorNegocio.EditarProveedor(entidad);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Proveedor editado correctamente." : "No fue posible editar el registro.",
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

		[HttpDelete("EliminarProveedor/{codigoProveedor:int}")]
		public ActionResult<RespuestaApi<bool>> EliminarProveedor(int codigoProveedor)
		{
			try
			{
				bool resultado = proveedorNegocio.EliminarProveedor(codigoProveedor);

				return Ok(new RespuestaApi<bool>
				{
					Exito = resultado,
					Mensaje = resultado ? "Proveedor eliminado correctamente." : "No fue posible eliminar el registro.",
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
