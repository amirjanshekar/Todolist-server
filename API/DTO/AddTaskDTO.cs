using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class AddTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Date { get; set; }

        public int UserId { get; set; }
    }
}