using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string? Title { get; set; }

        public string Overview { get; set; }

        public bool IsWatched { get; set; } 
    }
}
