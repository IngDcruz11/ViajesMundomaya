using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class PaqueteTour
	{
		public int CodigoPaqueteTour { get; set; }
		public int CodigoPaquete { get; set; }
		public int CodigoTour { get; set; }
		public bool Incluido { get; set; }
		public int Orden { get; set; }
		public string Observaciones { get; set; } = string.Empty;
		public string NombrePaquete { get; set; } = string.Empty;
		public string NombreTour { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
