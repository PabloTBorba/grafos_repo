using System.Collections.Generic;

namespace GrafoApp.Models
{
    public class GrafoJsonModel
    {
        public string Name { get; set; }
        public string Coordenates { get; set; }
        public IEnumerable<string> Vertices { get; set; } 
    }
}