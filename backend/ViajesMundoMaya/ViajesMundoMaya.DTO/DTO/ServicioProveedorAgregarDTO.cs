using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class ServicioProveedorAgregarDTO
	{
		public int CodigoProveedor { get; set; }
		public string TipoServicio { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public decimal PrecioReferencia { get; set; }
		public string Moneda { get; set; } = string.Empty;
	}
}
