using GrafoApp.Classes;
using GrafoApp.Classes.Enums;
using GrafoApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Controllers
{
    public class HomeController : Controller
    {
        #region Atributos privados

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        private GrafoDataModel grafoDataModel;
        private const int COUNT_GRAFOS = 8;

        #endregion Atributos privados

        #region Construtor

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
            grafoDataModel = new GrafoDataModel();
            SetGrafos();
        }

        #endregion Construtor

        #region Métodos privados

        /// <summary>
        /// Monta os dados de cada grafo e a matriz de adjacência correspondente
        /// </summary>
        private void SetGrafos()
        {
            using (var assembler = new GrafoModelAssembler(_hostingEnvironment, _logger))
            {
                for (var i = 0; i < COUNT_GRAFOS; i++)
                {
                    GrafosIndiceEnum grafoIndice = (GrafosIndiceEnum)i;

                    switch (grafoIndice)
                    {
                        case GrafosIndiceEnum.Grafo1:
                            grafoDataModel.ListGrafos.Grafo1 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo1 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo1);
                            break;

                        case GrafosIndiceEnum.Grafo2:
                            grafoDataModel.ListGrafos.Grafo2 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo2 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo2);
                            break;

                        case GrafosIndiceEnum.Grafo3:
                            grafoDataModel.ListGrafos.Grafo3 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo3 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo3);
                            break;

                        case GrafosIndiceEnum.Grafo4:
                            grafoDataModel.ListGrafos.Grafo4 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo4 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo4);
                            break;

                        case GrafosIndiceEnum.Grafo5:
                            grafoDataModel.ListGrafos.Grafo5 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo5 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo5);
                            break;

                        case GrafosIndiceEnum.Grafo6:
                            grafoDataModel.ListGrafos.Grafo6 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo6 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo6);
                            break;

                        case GrafosIndiceEnum.Grafo7:
                            grafoDataModel.ListGrafos.Grafo7 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo7 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo7);
                            break;

                        case GrafosIndiceEnum.Grafo8:
                            grafoDataModel.ListGrafos.Grafo8 = assembler.SetGrafoModel(grafoIndice);
                            grafoDataModel.MatrizAdjGrafo8 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo8);
                            break;
                    }
                }
            }
        }

        private GrafoDados SetChartData(List<ArestaModel> arestasGrafo)
        {
            var grafoDados = new GrafoDados();
            var chartData = new List<ChartDataModel>();

            foreach(var aresta in arestasGrafo)
            {
                var dataVertice = new List<DataVertice>();

                dataVertice.Add(new DataVertice
                {
                    label = aresta.VerticeA.VerticeName,
                    x = aresta.VerticeA.CoordX,
                    y = aresta.VerticeA.CoordY
                });

                dataVertice.Add(new DataVertice
                {
                    label = aresta.VerticeB.VerticeName,
                    x = aresta.VerticeB.CoordX,
                    y = aresta.VerticeB.CoordY
                });

                chartData.Add(new ChartDataModel
                {
                    data = dataVertice
                });
            }

            grafoDados.GrafoData = JsonConvert.SerializeObject(chartData);

            var maxValue = Math.Abs(arestasGrafo
                .Select(a => a.VerticeA.CoordX)
                .Max());
            var minValue = Math.Abs(arestasGrafo
                .Select(a => a.VerticeA.CoordX)
                .Min());
            grafoDados.GrafoXAxisValue = ((maxValue > minValue) ? decimal.ToInt32(maxValue) : decimal.ToInt32(minValue)) + 2;

            maxValue = Math.Abs(arestasGrafo
                .Select(a => a.VerticeA.CoordY)
                .Max());
            minValue = Math.Abs(arestasGrafo
                .Select(a => a.VerticeA.CoordY)
                .Min());
            grafoDados.GrafoYAxisValue = ((maxValue > minValue) ? decimal.ToInt32(maxValue) : decimal.ToInt32(minValue)) + 2;

            return grafoDados;
        }

        private void SetCaminhosEulerianos(HomeModel homeModel)
        {
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo1.Vertices, 
                grafoDataModel.MatrizAdjGrafo1))
            {
                homeModel.Grafo1CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo2.Vertices, 
                grafoDataModel.MatrizAdjGrafo2))
            {
                homeModel.Grafo2CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo3.Vertices, 
                grafoDataModel.MatrizAdjGrafo3))
            {
                homeModel.Grafo3CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo4.Vertices, 
                grafoDataModel.MatrizAdjGrafo4))
            {
                homeModel.Grafo4CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo5.Vertices, 
                grafoDataModel.MatrizAdjGrafo5))
            {
                homeModel.Grafo5CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo6.Vertices, 
                grafoDataModel.MatrizAdjGrafo6))
            {
                homeModel.Grafo6CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo7.Vertices, 
                grafoDataModel.MatrizAdjGrafo7))
            {
                homeModel.Grafo7CaminhoEuleriano = helper.CaminhoEuleriano();
            }
            using (var helper = new GetCaminhoEulerianoHelper(grafoDataModel.ListGrafos.Grafo8.Vertices, 
                grafoDataModel.MatrizAdjGrafo8))
            {
                homeModel.Grafo8CaminhoEuleriano = helper.CaminhoEuleriano();
            }
        }

        private void SetTriangulosGrafo(HomeModel homeModel)
        {
            using(var helper = new TriangulosHelper(grafoDataModel.ListGrafos))
            {
                homeModel.Grafo1MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo1);
                homeModel.Grafo2MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo2);
                homeModel.Grafo3MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo3);
                homeModel.Grafo4MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo4);
                homeModel.Grafo5MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo5);
                homeModel.Grafo6MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo6);
                homeModel.Grafo7MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo7);
                homeModel.Grafo8MaiorTriangulo = helper.MaiorTrianguloGrafo(GrafosIndiceEnum.Grafo8);
            }
        }

        private void SetCaminhosHamiltonianos(HomeModel homeModel)
        {
            ///tem que setar de novo as matrizes adjacentes, pois elas foram zeradas na utilização anterior
            grafoDataModel.MatrizAdjGrafo1 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo1);
            grafoDataModel.MatrizAdjGrafo2 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo2);
            grafoDataModel.MatrizAdjGrafo3 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo3);
            grafoDataModel.MatrizAdjGrafo4 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo4);
            grafoDataModel.MatrizAdjGrafo5 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo5);
            grafoDataModel.MatrizAdjGrafo6 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo6);
            grafoDataModel.MatrizAdjGrafo7 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo7);
            grafoDataModel.MatrizAdjGrafo8 = MathUtils.GerarMatrizAdjacente(grafoDataModel.ListGrafos.Grafo8);

            using (var helper = new GetCaminhoHamiltonianoHelper(grafoDataModel))
            {
                homeModel.Grafo1CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo1);
                homeModel.Grafo2CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo2);
                homeModel.Grafo3CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo3);
                homeModel.Grafo4CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo4);
                homeModel.Grafo5CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo5);
                homeModel.Grafo6CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo6);
                homeModel.Grafo7CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo7);
                homeModel.Grafo8CaminhoHamiltoniano = helper.GetCaminhoHamiltoniano(GrafosIndiceEnum.Grafo8);
            }
        }

        private HomeModel SetHomeViewData()
        {
            var homeModel = new HomeModel();
            ///grafos
            homeModel.Grafo1Info = SetChartData(grafoDataModel.ListGrafos.Grafo1.Arestas);
            homeModel.Grafo2Info = SetChartData(grafoDataModel.ListGrafos.Grafo2.Arestas);
            homeModel.Grafo3Info = SetChartData(grafoDataModel.ListGrafos.Grafo3.Arestas);
            homeModel.Grafo4Info = SetChartData(grafoDataModel.ListGrafos.Grafo4.Arestas);
            homeModel.Grafo5Info = SetChartData(grafoDataModel.ListGrafos.Grafo5.Arestas);
            homeModel.Grafo6Info = SetChartData(grafoDataModel.ListGrafos.Grafo6.Arestas);
            homeModel.Grafo7Info = SetChartData(grafoDataModel.ListGrafos.Grafo7.Arestas);
            homeModel.Grafo8Info = SetChartData(grafoDataModel.ListGrafos.Grafo8.Arestas);

            ///caminho euleriano
            SetCaminhosEulerianos(homeModel);

            ///Caminho hamiltonianos
            SetCaminhosHamiltonianos(homeModel);

            ///continuar aqui com triângulos dos grafos! efetuar as chamadas e colocar textos no html
            SetTriangulosGrafo(homeModel);

            return homeModel;
        }

        #endregion Métodos privados

        #region Métodos GET

        [HttpGet]
        public IActionResult Index()
        {
            var homeModel = SetHomeViewData();
            return View(homeModel);
        }

        #endregion Métodos GET

        /// <summary>
        /// Método retorna o menor caminho (chamada Ajax)
        /// </summary>
        /// <param name="caminhoFormModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetMenorCaminho([FromBody]CaminhoFormModel caminhoFormModel)
        {
            var indice = int.Parse(caminhoFormModel.TabId.Substring(caminhoFormModel.TabId.Length - 1, 1));
            var indiceGrafo = (GrafosIndiceEnum)indice - 1;
            var grafoModel = new GrafoModel();

            switch (indiceGrafo)
            {
                case GrafosIndiceEnum.Grafo1:
                    grafoModel = grafoDataModel.ListGrafos.Grafo1;
                    break;
                case GrafosIndiceEnum.Grafo2:
                    grafoModel = grafoDataModel.ListGrafos.Grafo2;
                    break;
                case GrafosIndiceEnum.Grafo3:
                    grafoModel = grafoDataModel.ListGrafos.Grafo3;
                    break;
                case GrafosIndiceEnum.Grafo4:
                    grafoModel = grafoDataModel.ListGrafos.Grafo4;
                    break;
                case GrafosIndiceEnum.Grafo5:
                    grafoModel = grafoDataModel.ListGrafos.Grafo5;
                    break;
                case GrafosIndiceEnum.Grafo6:
                    grafoModel = grafoDataModel.ListGrafos.Grafo6;
                    break;
                case GrafosIndiceEnum.Grafo7:
                    grafoModel = grafoDataModel.ListGrafos.Grafo7;
                    break;
                case GrafosIndiceEnum.Grafo8:
                    grafoModel = grafoDataModel.ListGrafos.Grafo8;
                    break;
            }

            var caminho = string.Empty;

            using (var getCaminho = new GetMenorCaminhoHelper(grafoModel))
            {
                caminho = getCaminho.GetMenorCaminho(caminhoFormModel.VerticeA.ToUpper(), 
                    caminhoFormModel.VerticeB.ToUpper());
            }

            return new JsonResult(caminho);
        }
    }
}