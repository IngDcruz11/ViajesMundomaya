using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class ClienteEditarDTO
	{
		public int CodigoCliente { get; set; }
		public string Nombres { get; set; } = string.Empty;
		public string Apellidos { get; set; } = string.Empty;
		public string DPI { get; set; } = string.Empty;
		public string Telefono { get; set; } = string.Empty;
		public string CorreoElectronico { get; set; } = string.Empty;
		public string Direccion { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
