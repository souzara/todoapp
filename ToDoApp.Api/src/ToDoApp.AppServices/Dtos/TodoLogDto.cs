using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.AppServices.Dtos
{
    public class TodoLogDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public int TodoId { get; set; }
    }
}
