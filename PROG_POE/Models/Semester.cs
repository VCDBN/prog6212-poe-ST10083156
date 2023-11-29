using System;
using System.Collections.Generic;

namespace PROG_POE.Models;

public partial class Semester
{
    public static int NumWeeks {  get; set; }
    public int Id { get; set; }

    public int StudyHours { get; set; }

    public string? Code { get; set; }

    public string? Username { get; set; }

    public int WeekNum { get; set; }

    public virtual Module? CodeNavigation { get; set; }

    public virtual User? UsernameNavigation { get; set; }
}
