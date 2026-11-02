using System;
using System.Collections.Generic;
using System.Text;

namespace ViajesMundoMaya.DTO.DTO
{
	public class RespuestaApi<T>
	{
		public bool Exito { get; set; }

		public string Mensaje { get; set; } = string.Empty;

		public T? Datos { get; set; }

		public string? Detalle { get; set; }
	}
}
