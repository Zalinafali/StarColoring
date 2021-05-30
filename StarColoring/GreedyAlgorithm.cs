using System;
using System.Collections.Generic;
using System.Linq;

namespace StarColoring
{
    public static class GreedyAlgorithm
    {
        public static int StarChromaticNumber(Graph graph)
        {
            #region Trivial cases
            var numberOfVertices = graph.Vertices.Count();
            if (numberOfVertices == 0)
                return 0;
            if (numberOfVertices == 1)
                return 1;
            #endregion

            graph.NotColoredVertices = graph.Vertices.ToList();
            graph.ColoredVertices = new List<Vertex>();

            Vertex currentVertex = null;

            // 1) Choose starting vertex (biggest degree)
            var currentVertexIndex = FindMaxDegVertex(graph.Vertices.ToList());

            // 2) Color vertex
            currentVertex = graph.Vertices[currentVertexIndex];
            var forbiddenColor = currentVertex.ColorVertex();
            graph.NotColoredVertices.RemoveAt(currentVertexIndex);
            graph.ColoredVertices.Add(currentVertex);

            // 3) Forbid used color in distance-2
            foreach(var v1 in currentVertex.Neighbours)
            {
                v1.ForbidColor(forbiddenColor);
                foreach(var v2 in v1.Neighbours)
                {
                    v2.ForbidColor(forbiddenColor);
                }
            }

            // 7) Repeat 4 -> 5 -> 6 to color every vertex
            while (true)
            {
                // 4) Move to the next neighbour vertex with the highest degree
                currentVertexIndex = FindNotColoredMaxDegVertex(currentVertex.Neighbours);

                int previousColoredIndex = -1;
                if (currentVertexIndex < 0)     // All neighbours are colored
                {
                    previousColoredIndex = graph.ColoredVertices.Count - 2;    // Previous colored vertex
                    currentVertexIndex = FindNotColoredMaxDegVertex(graph.ColoredVertices[previousColoredIndex].Neighbours);
                    while (currentVertexIndex < 0)
                    {
                        if (--previousColoredIndex < 0)
                            return graph.GetNumberOfUsedColors();   // 8) No more vertices to color - return number of used colors

                        currentVertexIndex = FindNotColoredMaxDegVertex(graph.ColoredVertices[previousColoredIndex].Neighbours);
                    }
                }

                if (previousColoredIndex < 0 && currentVertexIndex >= 0)
                {
                    currentVertex = currentVertex.Neighbours[currentVertexIndex];
                }
                else if (previousColoredIndex >= 0 && currentVertexIndex >= 0)
                {
                    currentVertex = graph.ColoredVertices[previousColoredIndex].Neighbours[currentVertexIndex];
                }
                else
                {
                    throw new Exception();
                }

                // 5) Color current vertex
                forbiddenColor = currentVertex.ColorVertex();
                graph.NotColoredVertices.Remove(currentVertex);
                graph.ColoredVertices.Add(currentVertex);

                // 6) Forbid used color in distance-2
                foreach (var v1 in currentVertex.Neighbours)
                {
                    v1.ForbidColor(forbiddenColor);
                    foreach (var v2 in v1.Neighbours)
                    {
                        v2.ForbidColor(forbiddenColor);
                    }
                }
            }
        }

        #region Private methods
        private static int FindMaxDegVertex(List<Vertex> vertices)
        {
            int vertexIndex = -1;

            int maxDeg = 0;
            for(int i = 0; i < vertices.Count(); i++)
            {
                if(vertices[i].Deg > maxDeg)
                {
                    vertexIndex = i;
                    maxDeg = vertices[i].Deg;
                }
            }

            return vertexIndex;
        }

        private static int FindNotColoredMaxDegVertex(List<Vertex> vertices)
        {
            int vertexIndex = -1;

            int maxDeg = 0;
            for (int i = 0; i < vertices.Count(); i++)
            {
                if (vertices[i].Color == 0 && vertices[i].Deg > maxDeg)
                {
                    vertexIndex = i;
                    maxDeg = vertices[i].Deg;
                }
            }

            return vertexIndex;
        }
        #endregion
    }
}
