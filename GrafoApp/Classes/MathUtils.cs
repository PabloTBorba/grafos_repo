using GrafoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GrafoApp.Classes
{
    public static class MathUtils
    {
        /// <summary>
        /// Monta a matriz de adjacência do grafo
        /// </summary>
        /// <param name="grafo">class</param>
        /// <returns>int[,]</returns>
        public static int[,] GerarMatrizAdjacente(GrafoModel grafo)
        {
            var nodeCount = grafo.Vertices.Count;
            var matriz = new int[nodeCount, nodeCount];
            List<VerticeModel> listAuxVertices = grafo.Vertices.ToList();
            var possuiConexao = true;
            int i = 0;

            foreach (var vertice in grafo.Vertices)
            {
                int j = 0;
                
                foreach(var vertAux in listAuxVertices)
                {
                    possuiConexao = grafo.Arestas
                        .Where(a => a.VerticeA.VerticeName.Equals(vertice.VerticeName))
                        .Where(a => a.VerticeB.VerticeName.Equals(vertAux.VerticeName))
                        .Any();

                    ///verifica o inverso
                    if (!possuiConexao)
                    {
                        possuiConexao = grafo.Arestas
                            .Where(a => a.VerticeA.VerticeName.Equals(vertAux.VerticeName))
                            .Where(a => a.VerticeB.VerticeName.Equals(vertice.VerticeName))
                            .Any();
                    }

                    matriz[i, j] = (possuiConexao) ? 1 : 0;
                    j++;
                }

                i++;
            }

            return matriz;
        }

        /// <summary>
        /// Monta a matriz de custos entre os vértices
        /// </summary>
        /// <param name="grafo">class</param>
        /// <returns>int[,]</returns>
        public static decimal[,] GerarMatrizCustos(GrafoModel grafo)
        {
            var nodeCount = grafo.Vertices.Count;
            var matriz = new decimal[nodeCount, nodeCount];
            List<VerticeModel> listAuxVertices = grafo.Vertices.ToList();
            decimal custo;
            int i = 0;

            foreach (var vertice in grafo.Vertices)
            {
                int j = 0;

                foreach (var vertAux in listAuxVertices)
                {
                    custo = 0.0m;

                    if (i != j)
                    {
                        custo = grafo.Arestas
                            .Where(a => a.VerticeA.VerticeName.Equals(vertice.VerticeName))
                            .Where(a => a.VerticeB.VerticeName.Equals(vertAux.VerticeName))
                            .Select(a => a.CustoAresta)
                            .DefaultIfEmpty(0.0m)
                            .FirstOrDefault();

                        ///verifica o inverso
                        if (custo == 0)
                        {
                            custo = grafo.Arestas
                                .Where(a => a.VerticeA.VerticeName.Equals(vertAux.VerticeName))
                                .Where(a => a.VerticeB.VerticeName.Equals(vertice.VerticeName))
                                .Select(a => a.CustoAresta)
                                .DefaultIfEmpty(0.0m)
                                .FirstOrDefault();
                        }
                    }

                    matriz[i, j] = custo;
                    j++;
                }

                i++;
            }

            return matriz;
        }

        /// <summary>
        /// Retorna valor parcial necessário para calcular tamanho aresta e ângulos de triângulo
        /// </summary>
        /// <param name="verticeA">class</param>
        /// <param name="verticeB">class</param>
        /// <returns>double</returns>
        private static double SomaVerticesAoQuadrado(VerticeModel verticeA, VerticeModel verticeB)
        {
            var valorA = Math.Pow((double)verticeB.CoordX - (double)verticeA.CoordX, 2);
            var valorB = Math.Pow((double)verticeB.CoordY - (double)verticeA.CoordY, 2);
            return valorA + valorB;
        }

        /// <summary>
        /// Cálculo do custo aresta = distância entre os vértices da aresta
        /// Math = raiz quadrada((Xb - Xa)² + (Yb - Ya)²)
        /// </summary>
        /// <param name="verticeA">class</param>
        /// <param name="verticeB">class</param>
        /// <returns>decimal</returns>
        public static decimal CalcularAresta(VerticeModel verticeA, VerticeModel verticeB)
        {
            var verticesAoQuadrado = SomaVerticesAoQuadrado(verticeA, verticeB);
            return Math.Round((decimal)Math.Sqrt(verticesAoQuadrado), 2);
        }

        /// <summary>
        /// Calcula os ângulos do triângulo utilizando a lei dos cossenos
        /// </summary>
        /// <param name="vertices">List<class></param>
        /// <returns>string</returns>
        public static string RetornaAngulosTriangulo(List<VerticeModel> vertices)
        {
            var a2 = SomaVerticesAoQuadrado(vertices.ElementAt(1), vertices.ElementAt(2));
            var b2 = SomaVerticesAoQuadrado(vertices.ElementAt(0), vertices.ElementAt(2));
            var c2 = SomaVerticesAoQuadrado(vertices.ElementAt(0), vertices.ElementAt(1));

            var a = Math.Round(Math.Sqrt(a2), 2);
            var b = Math.Round(Math.Sqrt(b2), 2);
            var c = Math.Round(Math.Sqrt(c2), 2);

            ///utilizando a lei dos cossenos
            var aAngle = Math.Acos((b2 + c2 - a2) / (2 * b * c));
            var bAngle = Math.Acos((a2 + c2 - b2) / (2 * a * c));
            var cAngle = Math.Acos((a2 + b2 - c2) / (2 * a * b));

            ///convertendo para graus
            aAngle = Math.Round(aAngle * 180 / Math.PI);
            bAngle = Math.Round(bAngle * 180 / Math.PI);
            cAngle = Math.Round(cAngle * 180 / Math.PI);

            return $"Ângulos do triângulo: {aAngle.ToString()}, {bAngle.ToString()}, {cAngle.ToString()}";
        }
    }
}