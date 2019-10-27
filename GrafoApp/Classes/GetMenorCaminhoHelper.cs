using GrafoApp.Classes.Enums;
using GrafoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Classes
{
    public class GetMenorCaminhoHelper : IDisposable
    {
        private readonly GrafoModel _grafoModel;

        public GetMenorCaminhoHelper(GrafoModel grafoModel)
        {
            _grafoModel = grafoModel;
        }

        /// <summary>
        /// Usa uma versão do algoritmo de Dijkstra pra montar o caminho - lista de índices
        /// dos vértices, conforme a matriz de custos passada e o ponto inicial e final
        /// </summary>
        /// <param name="matrizCustos"></param>
        /// <param name="verticeIni"></param>
        /// <param name="verticeFim"></param>
        /// <returns></returns>
        private List<int> AlgoritmoDijkstra(decimal[,] matrizCustos, int verticeIni, int verticeFim)
        {
            var n = matrizCustos.GetLength(0);
            var custo = new decimal[n];

            for (int i = 0; i < n; i++)
            {
                custo[i] = int.MaxValue;
            }

            custo[verticeIni] = 0.0m;

            var visitado = new bool[n];
            var anterior = new int?[n];

            while (true)
            {
                decimal mincusto = int.MaxValue;
                var verticeMaisProx = 0;

                for (int i = 0; i < n; i++)
                {
                    if (!visitado[i] && mincusto > custo[i])
                    {
                        mincusto = custo[i];
                        verticeMaisProx = i;
                    }
                }

                if (mincusto == int.MaxValue)
                {
                    break;
                }

                visitado[verticeMaisProx] = true;

                for (int i = 0; i < n; i++)
                {
                    if (matrizCustos[verticeMaisProx, i] > 0)
                    {
                        var caminhoVerticeMaisProx = custo[verticeMaisProx];
                        var custoVerticeMaisProx = matrizCustos[verticeMaisProx, i];

                        var totalcusto = caminhoVerticeMaisProx + custoVerticeMaisProx;

                        if (totalcusto < custo[i])
                        {
                            custo[i] = totalcusto;
                            anterior[i] = verticeMaisProx;
                        }
                    }
                }
            }

            if (custo[verticeFim] == int.MaxValue)
                return null;

            var caminho = new LinkedList<int>();
            int? verticeAtual = verticeFim;

            while (verticeAtual != null)
            {
                caminho.AddFirst(verticeAtual.Value);
                verticeAtual = anterior[verticeAtual.Value];
            }

            return caminho.ToList();
        }

        /// <summary>
        /// Monta o menor caminho e retorna a string indicando também custo
        /// </summary>
        /// <param name="verticeIni">string</param>
        /// <param name="verticeFim">string</param>
        /// <returns>string</returns>
        private string MenorCaminho(string verticeIni, string verticeFim)
        {
            var matrizCustos = MathUtils.GerarMatrizCustos(_grafoModel);
            var indIni = _grafoModel.Vertices.FindIndex(v => v.VerticeName == verticeIni);

            if (verticeIni.Equals(verticeFim))
                return "Os vértices informados são iguais";

            if (indIni < 0)
                return "Vértice inicial não encontrado";

            var indFim = _grafoModel.Vertices.FindIndex(v => v.VerticeName == verticeFim);

            if (indFim < 0)
                return "Vértice final não encontrado";

            var listVertsIndex = AlgoritmoDijkstra(matrizCustos, indIni, indFim);

            if (listVertsIndex != null && listVertsIndex.Any())
            {
                var caminho = $"Menor caminho de {verticeIni} até {verticeFim}:";
                var listVertices = new List<string>();
                var custoTotal = 0.0m;

                for (int i = 0; i < listVertsIndex.Count; i++)
                {
                    if (i + 1 < listVertsIndex.Count)
                        custoTotal += matrizCustos[listVertsIndex[i], listVertsIndex[i + 1]];
                    
                    listVertices.Add(_grafoModel.Vertices
                        .ElementAt(listVertsIndex[i])
                        .VerticeName);
                }

                var caminhoFormatado = string.Join("-->", listVertices);
                caminho = $"{caminho} {caminhoFormatado} / Custo total: {custoTotal.ToString()}";
                return caminho;
            }

            return "Não existe caminho entre os vértices";
        }

        public string GetMenorCaminho(string verticeIni, string verticeFim)
        {
            return MenorCaminho(verticeIni, verticeFim);
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
        // ~GetMenorCaminhoHelper()
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

        #endregion IDisposable Support
    }
}