using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EleRepairs.Models
{
    public class RepairsGenreViewModel
    {
        public List<Repairs> Repairs { get; set; }
        public SelectList Genres { get; set; }
        public string RepairsGenre { get; set; }
        public string SearchString { get; set; }
    }
}