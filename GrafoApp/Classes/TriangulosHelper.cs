using GrafoApp.Classes.Enums;
using GrafoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Classes
{
    public class TriangulosHelper : IDisposable
    {
        private readonly ListGrafos _listGrafos;

        public TriangulosHelper(ListGrafos listGrafos)
        {
            _listGrafos = listGrafos;
        }

        internal class TrianguloModel 
        { 
            internal TrianguloModel()
            {
                Vertices = new List<VerticeModel>();
            }

            internal List<VerticeModel> Vertices { get; set; }
            internal decimal Area { get; set; }
        }

        private GrafoModel GetGrafoAtual(GrafosIndiceEnum grafoIndice)
        {
            switch (grafoIndice)
            {
                case GrafosIndiceEnum.Grafo1:
                    return _listGrafos.Grafo1;
                case GrafosIndiceEnum.Grafo2:
                    return _listGrafos.Grafo2;
                case GrafosIndiceEnum.Grafo3:
                    return _listGrafos.Grafo3;
                case GrafosIndiceEnum.Grafo4:
                    return _listGrafos.Grafo4;
                case GrafosIndiceEnum.Grafo5:
                    return _listGrafos.Grafo5;
                case GrafosIndiceEnum.Grafo6:
                    return _listGrafos.Grafo6;
                case GrafosIndiceEnum.Grafo7:
                    return _listGrafos.Grafo7;
                default:
                    return _listGrafos.Grafo8;
            }
        }

        /// <summary>
        /// Busca combinações únicas de x (count) itens para a lista T passada por parâmetro
        /// </summary>
        /// <typeparam name="T">any</typeparam>
        /// <param name="items">IEnumerable<any></param>
        /// <param name="count">int</param>
        /// <returns>IEnumerable<any></returns>
        private IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count = 3)
        {
            var i = 0;

            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                    {
                        yield return new T[] { item }.Concat(result);
                    }
                }

                i++;
            }
        }

        /// <summary>
        /// Usa a matriz adjacente de um triângulo (matriz[3, 3]) para
        /// verificar se é triângulo.
        /// Se a soma de cada item na linha = 2 AND a diagonal de índices (0,0)-(1,1)-(2,2) = 0, então é triângulo
        /// </summary>
        /// <param name="matrizAdj">int[,]</param>
        /// <returns>bool</returns>
        private bool IsTriangle(int[,] matrizAdj)
        {
            ///verifica primeiro a diagonal, se não for, nem soma as demais
            var somaLinha1 = matrizAdj[0, 0] + matrizAdj[1, 1] + matrizAdj[2, 2];

            if (somaLinha1 == 0)
            {
                somaLinha1 = matrizAdj[0, 0] + matrizAdj[0, 1] + matrizAdj[0, 2];
                var somaLinha2 = matrizAdj[1, 0] + matrizAdj[1, 1] + matrizAdj[1, 2];
                var somalinha3 = matrizAdj[2, 0] + matrizAdj[2, 1] + matrizAdj[2, 2];

                if (somaLinha1 == 2 && somaLinha2 == 2 && somalinha3 == 2)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Cálculo da área, usando coordenadas: Ax(By - Cy) + Bx(Cy - Ay) + Cx(Ay - By) / 2
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        private decimal CalcularArea(List<VerticeModel> vertices)
        {
            var vertice1 = vertices.ElementAt(0);
            var vertice2 = vertices.ElementAt(1);
            var vertice3 = vertices.ElementAt(2);

            var area = (vertice1.CoordX * (vertice2.CoordY - vertice3.CoordY)) +
                (vertice2.CoordX * (vertice3.CoordY - vertice1.CoordY)) +
                (vertice3.CoordX * (vertice1.CoordY - vertice2.CoordY));
            area = Math.Abs(Math.Round(area / 2, 2));
            return area;
        }

        private List<TrianguloModel> GetMaiorTrianguloGrafo(GrafosIndiceEnum grafoIndice)
        {
            var triangulos = new List<TrianguloModel>();
            var grafoAtual = new GrafoModel();
            grafoAtual = GetGrafoAtual(grafoIndice);
            
            ///primeiro pegamos a lista de combinações possíveis de 3 vertices para lista de vértices do grafo
            var permutacoes = GetPermutations(grafoAtual.Vertices.Select(v => v.VerticeName).AsEnumerable());

            foreach (var item in permutacoes)
            {
                ///monto um GrafoModel com os vertices para obter a matriz adjacente dessa permutação
                ///as arestas não preciso selecionar, passo a list original
                var tempGrafoModel = new GrafoModel();

                foreach (var vertice in item)
                {
                    tempGrafoModel.Vertices.Add(grafoAtual.Vertices
                        .Where(v => v.VerticeName == vertice)
                        .Single());
                }

                tempGrafoModel.Arestas = grafoAtual.Arestas;
                var matrizAdj = MathUtils.GerarMatrizAdjacente(tempGrafoModel);

                ///uso a matriz para verificar se é um triângulo
                if (IsTriangle(matrizAdj))
                {
                    var triangulo = new TrianguloModel();
                    triangulo.Vertices = tempGrafoModel.Vertices;
                    triangulo.Area = CalcularArea(tempGrafoModel.Vertices);
                    triangulos.Add(triangulo);
                }
            }

            if (triangulos != null && triangulos.Any())
                return triangulos.Where(t => t.Area == triangulos.Select(tri => tri.Area).Max()).ToList();

            return null;
        }

        private string GetStringMaiorTriangulo(GrafosIndiceEnum grafoIndice)
        {
            var strTriangulo = string.Empty;
            var triangulos = GetMaiorTrianguloGrafo(grafoIndice);

            if (triangulos != null && triangulos.Any())
            {
                foreach (var triangulo in triangulos)
                {
                    strTriangulo = $"{strTriangulo}(";

                    foreach (var vertice in triangulo.Vertices)
                        strTriangulo = $"{strTriangulo}{vertice.VerticeName}, ";

                    strTriangulo = strTriangulo.Substring(0, (strTriangulo.Length - 2)) +
                        ") - Área: " + triangulo.Area.ToString();

                    strTriangulo = strTriangulo + " - " + MathUtils.RetornaAngulosTriangulo(triangulo.Vertices) + " / ";
                }

                var indice = ((int)grafoIndice + 1).ToString();
                strTriangulo = strTriangulo.Substring(0, strTriangulo.Length - 3);
                strTriangulo = $"Maior(es) triângulo(s) do grafo {indice}: {strTriangulo}";
            }
            else
                strTriangulo = "Grafo não possui triângulos entre seus vértices";
            
            return strTriangulo;
        }

        public string MaiorTrianguloGrafo(GrafosIndiceEnum grafoIndice)
        {
            return GetStringMaiorTriangulo(grafoIndice);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: descartar estado gerenciado (objetos gerenciados).
                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        // ~TriangulosHelper()
        // {
        //   // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
        //   Dispose(false);
        // }

        // Código adicionado para implementar corretamente o padrão descartável.
        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            // TODO: remover marca de comentário da linha a seguir se o finalizador for substituído acima.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}