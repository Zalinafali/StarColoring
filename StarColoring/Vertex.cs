using System.Collections.Generic;
using System.Linq;

namespace StarColoring
{
    public class Vertex
    {
        public int Number
        {
            get;
        }

        #region Properties
        public int Color
        {
            get;
            private set;
        }

        /// <summary>
        /// Neighbours of the vertex (vertices connected by the edge to the vertex).
        /// </summary>
        public List<Vertex> Neighbours
        {
            get;
            set;
        }

        /// <summary>
        /// Vertex degree.
        /// </summary>
        public int Deg
        {
            get => Neighbours.Count();
        }

        public List<int> AvailableColors
        {
            get;
            set;
        }

        public List<int> ForbiddenColors
        {
            get;
            set;
        }
        #endregion

        public Vertex(int vertexNumber)
        {
            Number = vertexNumber;
            Color = 0;
            Neighbours = new List<Vertex>();
            AvailableColors = new List<int>();
            ForbiddenColors = new List<int>();
        }

        #region Public methods
        public bool AddEdge(Vertex vertex)
        {
            if (this.Neighbours.Contains(vertex))
                return false;

            Neighbours.Add(vertex);
            vertex.Neighbours.Add(this);
            return true;
        }

        public void ForbidColor(int color)
        {
            if (!ForbiddenColors.Contains(color))
                ForbiddenColors.Add(color);

            AvailableColors.Remove(color);
        }

        /// <summary>
        /// Color vertex with the smallest available color.
        /// </summary>
        /// <returns></returns>
        public int ColorVertex()
        {
            if(Color == 0)
            {
                Color = AvailableColors.First();
                AvailableColors.Remove(Color);
                return Color;
            }

            return Color;
        }

        /// <summary>
        /// Not colored neighbour with the largest degree.
        /// </summary>
        /// <returns></returns>
        public Vertex NextVertex()
        {
            Vertex result = null;

            int maxDeg = 0;
            foreach(var v in Neighbours)
            {
                if(v.Color == 0 && v.Deg > maxDeg)
                {
                    result = v;
                    maxDeg = result.Deg;
                }
            }

            return result;
        }
        #endregion
    }
}
