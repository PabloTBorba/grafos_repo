using GrafoApp.Classes.Enums;
using GrafoApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GrafoApp.Classes
{
    public class GrafoModelAssembler : IDisposable
    {
        #region Atributos privados

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        private const string GRAFO1_NAME = "Grafo1.json";
        private const string GRAFO2_NAME = "Grafo2.json";
        private const string GRAFO3_NAME = "Grafo3.json";
        private const string GRAFO4_NAME = "Grafo4.json";
        private const string GRAFO5_NAME = "Grafo5.json";
        private const string GRAFO6_NAME = "Grafo6.json";
        private const string GRAFO7_NAME = "k5.json";
        private const string GRAFO8_NAME = "k33.json";

        #endregion Atributos privados

        #region Construtor

        public GrafoModelAssembler(IHostingEnvironment hostingEnvironment, ILogger logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }

        #endregion Construtor

        #region Métodos privados

        private List<GrafoJsonModel> DeserializeGrafoJson(string nomeArquivo)
        {
            var verticesList = new List<GrafoJsonModel>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "json", nomeArquivo);

            try
            {
                var json = System.IO.File.ReadAllText(filePath);
                verticesList = JsonConvert
                    .DeserializeObject<IEnumerable<GrafoJsonModel>>(json)
                    .ToList();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Erro ao ler o arquivo json: {e.Message}");
            }

            return verticesList;
        }

        private List<GrafoJsonModel> LeituraGrafosJson(GrafosIndiceEnum grafoIndice)
        {
            var grafoNome = string.Empty;

            switch (grafoIndice)
            {
                case GrafosIndiceEnum.Grafo1:
                    grafoNome = GRAFO1_NAME;
                    break;

                case GrafosIndiceEnum.Grafo2:
                    grafoNome = GRAFO2_NAME;
                    break;

                case GrafosIndiceEnum.Grafo3:
                    grafoNome = GRAFO3_NAME;
                    break;

                case GrafosIndiceEnum.Grafo4:
                    grafoNome = GRAFO4_NAME;
                    break;

                case GrafosIndiceEnum.Grafo5:
                    grafoNome = GRAFO5_NAME;
                    break;

                case GrafosIndiceEnum.Grafo6:
                    grafoNome = GRAFO6_NAME;
                    break;

                case GrafosIndiceEnum.Grafo7:
                    grafoNome = GRAFO7_NAME;
                    break;

                case GrafosIndiceEnum.Grafo8:
                    grafoNome = GRAFO8_NAME;
                    break;
            }

            var grafoJson = DeserializeGrafoJson(grafoNome);
            return grafoJson;
        }

        #endregion Métodos privados

        #region Métodos públicos

        public GrafoModel SetGrafoModel(GrafosIndiceEnum grafoIndice)
        {
            var grafoJson = LeituraGrafosJson(grafoIndice);
            var grafoModel = new GrafoModel();
            var listVertices = new List<VerticeModel>();
            var listArestas = new List<ArestaModel>();

            ///primeiro o array de vértices
            foreach (var item in grafoJson)
            {
                var vertice = new VerticeModel();
                vertice.VerticeName = item.Name.Trim();
                string[] strCoordenadas = item.Coordenates.Split(',');
                ///Rodando no docker/heroku, tive que remover as conversões de pontos abaixo
                ///Para executar no windows, remover comentários dos comandos, caso contrário
                ///irá gerar valores sem decimal
                //strCoordenadas[0] = strCoordenadas[0].Trim().Replace('.', ',');
                //strCoordenadas[1] = strCoordenadas[1].Trim().Replace('.', ',');
                vertice.CoordX = Math.Round(Convert.ToDecimal(strCoordenadas[0]), 2);
                vertice.CoordY = Math.Round(Convert.ToDecimal(strCoordenadas[1]), 2);
                listVertices.Add(vertice);
            }

            ///depois o array de arestas
            foreach (var item in grafoJson)
            {
                if (item.Vertices != null && item.Vertices.Any())
                {
                    var verticeA = listVertices
                        .Where(lv => lv.VerticeName == item.Name.Trim())
                        .Single();

                    foreach (var jVertice in item.Vertices)
                    {
                        var aresta = new ArestaModel();
                        aresta.VerticeA = verticeA;
                        aresta.VerticeB = listVertices
                            .Where(lv => lv.VerticeName == jVertice.Trim())
                            .Single();
                        aresta.CustoAresta = MathUtils.CalcularAresta(aresta.VerticeA, aresta.VerticeB);
                        listArestas.Add(aresta);
                    }
                }
            }

            grafoModel.Vertices = listVertices;
            grafoModel.Arestas = listArestas;
            return grafoModel;
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
                    // TODO: descartar estado gerenciado (objetos gerenciados).
                }

                // TODO: liberar recursos não gerenciados (objetos não gerenciados) e substituir um finalizador abaixo.
                // TODO: definir campos grandes como nulos.

                disposedValue = true;
            }
        }

        // TODO: substituir um finalizador somente se Dispose(bool disposing) acima tiver o código para liberar recursos não gerenciados.
        // ~GrafoModelAssembler()
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