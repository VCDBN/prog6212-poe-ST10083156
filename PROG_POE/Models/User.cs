using System;
using System.Collections.Generic;

namespace PROG_POE.Models;

public partial class User
{
    public string Username { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Module> Modules { get; set; } = new List<Module>();

    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
