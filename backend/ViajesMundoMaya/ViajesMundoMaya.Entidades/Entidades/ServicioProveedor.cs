using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class ServicioProveedor
	{
		public int CodigoServicioProv { get; set; }
		public int CodigoProveedor { get; set; }
		public string TipoServicio { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public decimal PrecioReferencia { get; set; }
		public string Moneda { get; set; } = string.Empty;
		public string NombreProveedor { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
