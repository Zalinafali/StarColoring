using System;
using System.Collections.Generic;
using System.Linq;

namespace StarColoring
{
    public static class SmallestLast
    {
        public static int StarChromaticNumber(Graph graph, Graph subGraph)
        {
            #region Trivial cases
            var numberOfVertices = graph.Vertices.Count();
            if (numberOfVertices == 0)
                return 0;
            if (numberOfVertices == 1)
                return 1;
            #endregion

            graph.VerticesList = graph.Vertices.ToList();
            subGraph.VerticesList = subGraph.Vertices.ToList();

            graph.NotColoredVertices = graph.Vertices.ToList();

            // 1) Build SmallestLast vertex sequence
            var vertexSequence = new Stack<Vertex>();
            {
                int currentVertexNumber;
                Vertex mainVertex, subVertex;

                while (subGraph.VerticesList.Count > 0)
                {
                    currentVertexNumber = FindSmallestDegVertex(subGraph.VerticesList);
                    mainVertex = graph.VerticesList.Find(v => v.Number == currentVertexNumber);
                    subVertex = subGraph.VerticesList.Find(v => v.Number == currentVertexNumber);

                    if (!subGraph.RemoveVertexFromGraph(subVertex))
                        throw new Exception();

                    vertexSequence.Push(mainVertex);
                }
            }

            // 2) Color greedy vertices in the sequence
            Vertex currentVertex;
            int forbiddenColor;
            while (vertexSequence.Count > 0)
            {
                currentVertex = vertexSequence.Pop();
                forbiddenColor = currentVertex.ColorVertex();

                foreach (var v1 in currentVertex.Neighbours)
                {
                    v1.ForbidColor(forbiddenColor);
                    foreach (var v2 in v1.Neighbours)
                    {
                        v2.ForbidColor(forbiddenColor);
                    }
                }
            }

            return graph.GetNumberOfUsedColors();
        }

        #region Private methods
        /// <summary>
        /// Returns number of the vertex with the smallest degree
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns></returns>
        private static int FindSmallestDegVertex(List<Vertex> vertices)
        {
            int vertexNumber = -1;

            int minDeg = vertices.Count + 1;
            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i].Deg < minDeg)
                {
                    vertexNumber = vertices[i].Number;
                    minDeg = vertices[i].Deg;
                }
            }

            return vertexNumber;
        }
        #endregion
    }
}
