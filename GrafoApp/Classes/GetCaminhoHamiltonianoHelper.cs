using GrafoApp.Classes.Enums;
using GrafoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Classes
{
    public class GetCaminhoHamiltonianoHelper : IDisposable
    {
        private readonly GrafoDataModel _grafoDataModel;
        private int[,] _matrizAdjAtual;
        private int _totalVertices;
        private List<VerticeModel> listVertices;

        public GetCaminhoHamiltonianoHelper(GrafoDataModel grafoDataModel)
        {
            _grafoDataModel = grafoDataModel;
        }

        /// <summary>
        /// Matriz e vértices usados atualmente, conforme o grafo
        /// </summary>
        /// <param name="grafoIndice"></param>
        private void SetMatrizAtual(GrafosIndiceEnum grafoIndice)
        {
            _matrizAdjAtual = new int[_totalVertices, _totalVertices];
            listVertices = new List<VerticeModel>();

            switch (grafoIndice)
            {
                case GrafosIndiceEnum.Grafo1:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo1;
                    listVertices = _grafoDataModel.ListGrafos.Grafo1.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo2:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo2;
                    listVertices = _grafoDataModel.ListGrafos.Grafo2.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo3:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo3;
                    listVertices = _grafoDataModel.ListGrafos.Grafo3.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo4:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo4;
                    listVertices = _grafoDataModel.ListGrafos.Grafo4.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo5:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo5;
                    listVertices = _grafoDataModel.ListGrafos.Grafo5.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo6:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo6;
                    listVertices = _grafoDataModel.ListGrafos.Grafo6.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo7:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo7;
                    listVertices = _grafoDataModel.ListGrafos.Grafo7.Vertices;
                    break;

                case GrafosIndiceEnum.Grafo8:
                    _matrizAdjAtual = _grafoDataModel.MatrizAdjGrafo8;
                    listVertices = _grafoDataModel.ListGrafos.Grafo8.Vertices;
                    break;
            }

            _totalVertices = _matrizAdjAtual.GetLength(0);
        }

        /// <summary>
        /// Verifica se é possível adicionar o vértice ao caminho
        /// </summary>
        /// <param name="v"></param>
        /// <param name="caminho"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool PodeAdicionar(int v, int[] caminho, int pos)
        {
            if (_matrizAdjAtual[caminho[pos - 1], v] == 0)
                return false;

            for (int i = 0; i < pos; i++)
                if (caminho[i] == v)
                    return false;

            return true;
        }

        /// <summary>
        /// Confere os diferentes vértices como candidados possíveis a serem adicionados
        /// ao caminho hamiltoniano
        /// </summary>
        /// <param name="caminho"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        private bool VerificaVerticesCaminho(int[] caminho, int pos)
        {
            if (pos == _totalVertices)
                return (_matrizAdjAtual[caminho[pos - 1], caminho[0]] == 1);

            for (int v = 1; v < _totalVertices; v++)
            {
                if (PodeAdicionar(v, caminho, pos))
                {
                    caminho[pos] = v;

                    if (VerificaVerticesCaminho(caminho, pos + 1))
                        return true;

                    caminho[pos] = -1;
                }
            }

            return false;
        }

        /// <summary>
        /// Monta o caminho (se existir) e retorna uma string com as informações
        /// </summary>
        /// <param name="grafoIndice"></param>
        /// <returns></returns>
        private string CaminhoHamiltoniano(GrafosIndiceEnum grafoIndice)
        {
            SetMatrizAtual(grafoIndice);
            var caminho = new int[_totalVertices];

            for (int i = 0; i < _totalVertices; i++)
                caminho[i] = -1;

            caminho[0] = 0;

            if (!VerificaVerticesCaminho(caminho, 1))
                return "Não existe caminho hamiltoniano para o grafo";

            var strCaminho = string.Empty;

            for (int i = 0; i < caminho.Length; i++)
                strCaminho = $"{strCaminho}{listVertices.ElementAt(caminho[i]).VerticeName}-->";

            strCaminho = "Caminho hamiltoniano do grafo: " + strCaminho.Substring(0, strCaminho.Length - 3);
            return strCaminho;
        }

        public string GetCaminhoHamiltoniano(GrafosIndiceEnum grafoIndice)
        {
            return CaminhoHamiltoniano(grafoIndice);
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
        // ~GetCaminhoHamiltonianoHelper()
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