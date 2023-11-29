using System;
using System.Collections.Generic;

namespace PROG_POE.Models;

public partial class Module
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Credits { get; set; }

    public int ClassHours { get; set; }

    public string? Username { get; set; }

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();

    public virtual User? UsernameNavigation { get; set; }
}
