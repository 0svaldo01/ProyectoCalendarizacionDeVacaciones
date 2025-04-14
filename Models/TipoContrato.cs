using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class TipoContrato
{
    public int Id { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
