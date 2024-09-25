using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.DTOs.Company
{
    public class CompanyDeleteDTO
    {
        public int CompanyId { get; set; }
        public string OwnerId { get; set; }
    }
}
