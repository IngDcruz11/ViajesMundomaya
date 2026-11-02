using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.DTO.DTO
{
	public class TipoTransporteAgregarDTO
	{
		public string NombreTipo { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public int Capacidad { get; set; }
		public string EquipajePermitido { get; set; } = string.Empty;
		public string Comodidades { get; set; } = string.Empty;
	}
}
