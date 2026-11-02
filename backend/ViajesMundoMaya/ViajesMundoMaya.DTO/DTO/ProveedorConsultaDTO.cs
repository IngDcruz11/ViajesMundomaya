using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class ProveedorConsultaDTO
	{
		public int CodigoProveedor { get; set; }
		public string NombreProveedor { get; set; } = string.Empty;
		public string TipoProveedor { get; set; } = string.Empty;
		public string Contacto { get; set; } = string.Empty;
		public string Telefono { get; set; } = string.Empty;
		public string CorreoElectronico { get; set; } = string.Empty;
		public string Direccion { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
