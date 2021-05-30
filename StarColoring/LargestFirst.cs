using System.Collections.Generic;
using System.Linq;

namespace StarColoring
{
    public static class LargestFirst
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

            // 1) Sort vertices in non-increasing order
            graph.NotColoredVertices = LargestFirstSort(graph.NotColoredVertices);

            // 5) Repeat 2 -> 3 -> 4 to color all vertices
            while(graph.NotColoredVertices.Count > 0)
            {
                // 2) Choose first not colored vertex from the list
                currentVertex = graph.NotColoredVertices.First();

                // 3) Color vertex
                var forbiddenColor = currentVertex.ColorVertex();
                graph.NotColoredVertices.Remove(currentVertex);
                graph.ColoredVertices.Add(currentVertex);

                // 4) Forbid used color in distance-2
                foreach (var v1 in currentVertex.Neighbours)
                {
                    v1.ForbidColor(forbiddenColor);
                    foreach (var v2 in v1.Neighbours)
                    {
                        v2.ForbidColor(forbiddenColor);
                    }
                }
            }

            // 6) Return number of used colors
            return graph.GetNumberOfUsedColors();
        }

        private static List<Vertex> LargestFirstSort(List<Vertex> vertices)
        {
            var result = new List<Vertex>();

            var orderedVertices = vertices.OrderByDescending(v => v.Deg);

            foreach(var v in orderedVertices)
            {
                result.Add(v);
            }

            return result;
        }
    }
}
