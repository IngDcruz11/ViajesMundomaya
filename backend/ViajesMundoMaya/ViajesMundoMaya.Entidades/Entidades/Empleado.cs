using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class Empleado
	{
		public int CodigoEmpleado { get; set; }
		public string Nombres { get; set; } = string.Empty;
		public string Apellidos { get; set; } = string.Empty;
		public string Puesto { get; set; } = string.Empty;
		public string Telefono { get; set; } = string.Empty;
		public string CorreoElectronico { get; set; } = string.Empty;
		public DateOnly FechaContratacion { get; set; }
		public decimal Salario { get; set; }
		public bool Estado { get; set; }
	}
}
