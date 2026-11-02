using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViajesMundoMaya.Entidades.Entidades
{
	public class Destino
	{
		public int CodigoDestino { get; set; }
		public string NombreDestino { get; set; } = string.Empty;
		public string Pais { get; set; } = string.Empty;
		public string Departamento { get; set; } = string.Empty;
		public string Ciudad { get; set; } = string.Empty;
		public string Descripcion { get; set; } = string.Empty;
		public string Imagen { get; set; } = string.Empty;
		public bool Estado { get; set; }
	}
}
