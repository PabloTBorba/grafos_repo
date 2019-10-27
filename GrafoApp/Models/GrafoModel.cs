using System.Collections.Generic;

namespace GrafoApp.Models
{
    public class GrafoModel
    {
        public GrafoModel()
        {
            Vertices = new List<VerticeModel>();
            Arestas = new List<ArestaModel>();
        }

        public List<VerticeModel> Vertices { get; set; }
        public List<ArestaModel> Arestas { get; set; }
    }

    public class VerticeModel
    {
        public string VerticeName { get; set; }
        public decimal CoordX { get; set; }
        public decimal CoordY { get; set; }
    }

    public class ArestaModel
    {
        public VerticeModel VerticeA { get; set; }
        public VerticeModel VerticeB { get; set; }
        public decimal CustoAresta { get; set; }
    }
}