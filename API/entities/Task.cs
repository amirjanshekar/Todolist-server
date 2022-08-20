using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.entities
{
    public class Task
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Date { get; set; }

        public bool IsChecked { get; set; } = false;
    }
}