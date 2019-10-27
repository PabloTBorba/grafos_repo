namespace GrafoApp.Models
{
    public class HomeModel
    {
        public GrafoDados Grafo1Info { get; set; }
        public string Grafo1CaminhoEuleriano { get; set; }
        public string Grafo1CaminhoHamiltoniano { get; set; }
        public string Grafo1MaiorTriangulo { get; set; }

        public GrafoDados Grafo2Info { get; set; }
        public string Grafo2CaminhoEuleriano { get; set; }
        public string Grafo2CaminhoHamiltoniano { get; set; }
        public string Grafo2MaiorTriangulo { get; set; }

        public GrafoDados Grafo3Info { get; set; }
        public string Grafo3CaminhoEuleriano { get; set; }
        public string Grafo3CaminhoHamiltoniano { get; set; }
        public string Grafo3MaiorTriangulo { get; set; }

        public GrafoDados Grafo4Info { get; set; }
        public string Grafo4CaminhoEuleriano { get; set; }
        public string Grafo4CaminhoHamiltoniano { get; set; }
        public string Grafo4MaiorTriangulo { get; set; }

        public GrafoDados Grafo5Info { get; set; }
        public string Grafo5CaminhoEuleriano { get; set; }
        public string Grafo5CaminhoHamiltoniano { get; set; }
        public string Grafo5MaiorTriangulo { get; set; }

        public GrafoDados Grafo6Info { get; set; }
        public string Grafo6CaminhoEuleriano { get; set; }
        public string Grafo6CaminhoHamiltoniano { get; set; }
        public string Grafo6MaiorTriangulo { get; set; }

        public GrafoDados Grafo7Info { get; set; }
        public string Grafo7CaminhoEuleriano { get; set; }
        public string Grafo7CaminhoHamiltoniano { get; set; }
        public string Grafo7MaiorTriangulo { get; set; }

        public GrafoDados Grafo8Info { get; set; }
        public string Grafo8CaminhoEuleriano { get; set; }
        public string Grafo8CaminhoHamiltoniano { get; set; }
        public string Grafo8MaiorTriangulo { get; set; }
    }

    public class GrafoDados
    {
        public string GrafoData { get; set; }

        /// <summary>
        /// Máximo/mínimo valor para X axis
        /// </summary>
        public int GrafoXAxisValue { get; set; }

        /// <summary>
        /// Máximo/mínimo valor para Y axis
        /// </summary>
        public int GrafoYAxisValue { get; set; }
    }
}