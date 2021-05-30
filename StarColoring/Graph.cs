using System.Collections.Generic;

namespace StarColoring
{
    public class Graph
    {
        public string Name
        {
            get;
            set;
        }

        public Vertex[] Vertices
        {
            get;
            set;
        }

        public List<Vertex> VerticesList
        {
            get;
            set;
        }

        public int[] Colors
        {
            get;
            set;
        }

        public List<Vertex> ColoredVertices
        {
            get;
            set;
        }

        public List<Vertex> NotColoredVertices
        {
            get;
            set;
        }

        #region Public methods
        public int GetNumberOfUsedColors()
        {
            var result = 0;
            foreach(var v in Vertices)
            {
                if (v.Color > result)
                    result = v.Color;
            }

            return result;
        }

        /// <summary>
        /// Removes given vertex only from the VerticesList
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool RemoveVertexFromGraph(Vertex vertex)
        {
            foreach(var v in vertex.Neighbours)
            {
                v.Neighbours.Remove(vertex);
            }

            if(this.VerticesList.Remove(vertex))
                return true;

            return false;
        }
        #endregion
    }
}
