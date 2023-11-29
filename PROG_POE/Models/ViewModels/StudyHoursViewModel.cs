using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PROG_POE.Models.ViewModels
{
    public class StudyHoursViewModel
    {
        public string SelectedModule {  get; set; }
        public List<SelectListItem> ModulesSelectList { get; set; }
    }
}
