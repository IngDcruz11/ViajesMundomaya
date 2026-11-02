using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class Paquete
	{
		public int CodigoPaquete { get; set; }
		public int CodigoProveedor { get; set; }
		public string NombrePaquete { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public decimal PrecioBase { get; set; }
		public int DuracionDias { get; set; }
		public string Incluye { get; set; } = string.Empty;
		public string Imagen { get; set; } = string.Empty;
		public string NombreProveedor { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
