using System;
using System.Collections.Generic;

namespace ProyectoCalendarizacionDeVacaciones.Models;

public partial class Departamentos
{
    public int Id { get; set; }

    public string NombreDepartamento { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
