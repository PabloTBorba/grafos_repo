using GrafoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Classes
{
    public class GetCaminhoEulerianoHelper : IDisposable
    {
        #region Atributos privados

        private readonly int _totalVertices;
        private readonly List<VerticeModel> _vertices;
        private int[,] _matrizAdjGrafo;

        #endregion Atributos privados

        #region Construtor

        public GetCaminhoEulerianoHelper(List<VerticeModel> vertices, int[,] matrizAdjGrafo)
        {
            _vertices = vertices;
            _matrizAdjGrafo = matrizAdjGrafo;
            _totalVertices = _matrizAdjGrafo.GetLength(0);
        }

        #endregion Construtor

        #region Métodos privados

        /// <summary>
        /// Retorna quantidade de arestas conectadas ao vértice
        /// </summary>
        /// <param name="i">int</param>
        /// <returns>int</returns>
        private int GetGrauVertice(int i)
        {
            var grau = 0;

            for (var j = 0; j < _totalVertices; j++)
                if (_matrizAdjGrafo[i, j] == 1)
                    grau++;

            return grau;
        }

        /// <summary>
        /// Retorna índice de um vértice como início do caminho/circuito euleriano
        /// Se retornar 0, indica que grafo não possui caminho/circuito euleriano
        /// Segundo item da tupla é a contagem de vértices com grau par
        /// </summary>
        /// <returns>Tuple<int, int></returns>
        private Tuple<int, int> GetRaizComContagem()
        {
            var raiz = 1;
            var contagemPares = 0;

            for (var i = 0; i < _totalVertices; i++)
            {
                if ((GetGrauVertice(i) % 2) != 0)
                {
                    contagemPares++;
                    raiz = i; ///raiz vai virar o índice do vértice
                }
            }

            raiz = (contagemPares != 0 && contagemPares != 2) ? 0 : raiz;
            return Tuple.Create(raiz, contagemPares);
        }

        /// <summary>
        /// Retorna índice do vértice na lista de vértices
        /// </summary>
        /// <param name="verticeName"></param>
        /// <returns></returns>
        private int GetIndiceVertice(string verticeName)
        {
            return _vertices.FindIndex(v => v.VerticeName == verticeName);
        }

        /// <summary>
        /// Verifica se já visitou vértices adjacentes
        /// </summary>
        /// <param name="x">int</param>
        /// <returns>bool</returns>
        private bool VisitouAdjacentes(int x)
        {
            for (var y = 0; y < _totalVertices; y++)
                if (_matrizAdjGrafo[x, y] == 1)
                    return false;

            return true;
        }

        /// <summary>
        /// Retorna os vértices do caminho/circuito euleriano
        /// </summary>
        /// <param name="raiz"></param>
        /// <returns></returns>
        private List<string> GetCaminhoEuleriano(int raiz)
        {
            var listaVerticesCaminho = new List<string>();
            int indice;
            var tempPath = new Stack<string>();
            tempPath.Clear();
            tempPath.Push(_vertices.ElementAt(raiz).VerticeName);

            while (tempPath.Count != 0)
            {
                indice = GetIndiceVertice(tempPath.Peek());

                if (VisitouAdjacentes(indice))
                {
                    ///remove da pilha e adiciona se já visitou todos adjacentes
                    listaVerticesCaminho.Add(tempPath.Pop());
                }
                else
                {
                    ///confere se vértice já visitado (== 0), se ainda não, adiciona à pilha e
                    ///marca como visitado na matriz
                    for (var y = 0; y < _totalVertices; y++)
                    {
                        if (_matrizAdjGrafo[indice, y] == 1)
                        {
                            _matrizAdjGrafo[indice, y] = 0;
                            _matrizAdjGrafo[y, indice] = 0;
                            tempPath.Push(_vertices.ElementAt(y).VerticeName);
                            break;
                        }
                    }
                }
            }

            return listaVerticesCaminho;
        }

        #endregion Métodos privados

        #region Métodos públicos

        public string CaminhoEuleriano()
        {
            var strCaminho = string.Empty;
            var raizComContagem = GetRaizComContagem(); //item1 - raiz / item2 - contagem pares

            if (raizComContagem.Item1 != 0)
            {
                var caminho = GetCaminhoEuleriano(raizComContagem.Item1);

                foreach (var item in caminho)
                    strCaminho = $"{strCaminho}{item}-->";

                strCaminho = strCaminho.Substring(0, (strCaminho.Length - 3));
                strCaminho = (raizComContagem.Item2 != 0) ?
                    $"Caminho euleriano do grafo: {strCaminho}" :
                    $"Circuito euleriano do grafo: {strCaminho}";
            }
            else
                strCaminho = "Grafo não possui caminho/circuito euleriano";

            return strCaminho;
        }

        #endregion Métodos públicos

        #region IDisposable Support

        private bool disposedValue = false; // Para detectar chamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _matrizAdjGrafo = null;
                }

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        ~GetCaminhoEulerianoHelper()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(false);
        }

        // Código adicionado para implementar corretamente o padrão descartável.
        public void Dispose()
        {
            // Não altere este código. Coloque o código de limpeza em Dispose(bool disposing) acima.
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}