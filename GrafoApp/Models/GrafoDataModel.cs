using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrafoApp.Models
{
    public class GrafoDataModel
    {
        public GrafoDataModel()
        {
            ListGrafos = new ListGrafos();
        }

        public ListGrafos ListGrafos { get; set; }

        public int[,] MatrizAdjGrafo1 { get; set; }
        public int[,] MatrizAdjGrafo2 { get; set; }
        public int[,] MatrizAdjGrafo3 { get; set; }
        public int[,] MatrizAdjGrafo4 { get; set; }
        public int[,] MatrizAdjGrafo5 { get; set; }
        public int[,] MatrizAdjGrafo6 { get; set; }
        public int[,] MatrizAdjGrafo7 { get; set; }
        public int[,] MatrizAdjGrafo8 { get; set; }
    }

    public class ListGrafos
    {
        public GrafoModel Grafo1;
        public GrafoModel Grafo2;
        public GrafoModel Grafo3;
        public GrafoModel Grafo4;
        public GrafoModel Grafo5;
        public GrafoModel Grafo6;
        public GrafoModel Grafo7;
        public GrafoModel Grafo8;
    }
}
