using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class PaqueteTourAgregarDTO
	{
		public int CodigoPaquete { get; set; }
		public int CodigoTour { get; set; }
		public bool Incluido { get; set; }
		public int Orden { get; set; }
		public string Observaciones { get; set; } = string.Empty;
	}
}
