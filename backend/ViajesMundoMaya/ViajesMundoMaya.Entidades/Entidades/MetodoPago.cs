using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class MetodoPago
	{
		public int CodigoMetodoPago { get; set; }
		public string NombreMetodoPago { get; set; } = string.Empty;
		public string Tipo { get; set; } = string.Empty;
		public string EntidadFinanciera { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public decimal ComisionPorcentaje { get; set; }
		public bool Estado { get; set; }
	}
}
