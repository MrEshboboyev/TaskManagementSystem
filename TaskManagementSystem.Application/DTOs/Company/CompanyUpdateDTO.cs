using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.DTOs.Company
{
    public class CompanyUpdateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string OwnerId { get; set; }
    }
}
