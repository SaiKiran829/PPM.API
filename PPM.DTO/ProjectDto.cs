using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.DTO
{
    public class ProjectDto
    {
        public string projectName { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int id { get; set; }

        public ProjectDto(string projectname, string startdate, string enddate, int Id)
        {
            this.projectName = projectname;
            this.startDate = startdate;
            this.endDate = enddate;
            this.id = Id;
        }
        public ProjectDto()
        {

        }
    }
}
