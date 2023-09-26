using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Service.DTOs.Jobs
{
    public class JobForCreationDto
    {
        public decimal Price { get; set; }
        public string JobTitle { get; set; }
        public string Phone { get; set; }
        public string Conditions { get; set; }
        public string Description { get; set; }
        public string CompanyName { get; set; }
    }
}
