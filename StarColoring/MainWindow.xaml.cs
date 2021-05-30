using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace StarColoring
{

    public partial class MainWindow : Window
    {
        private Graph graph1 = new Graph();
        private Graph graph2 = new Graph();
        private Graph graph3 = new Graph();

        private bool isCustomGraphLoaded = false;
        private string customGraphData = null;
        private Graph customGraph = null;

        private bool isWorking = false;

        private const string RESULT_GREEDY = "Greedy:\t";
        private const string RESULT_LF = "LargesFirst:\t";
        private const string RESULT_SL = "SmallestLast:\t";

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Events
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (isWorking)
                return;

            isWorking = true;

            #region Greedy Algorithm
            graph1 = BuildGraph1();

            graph2 = BuildGraph2();

            graph3 = BuildGraph3();


            var result1 = GreedyAlgorithm.StarChromaticNumber(graph1);
            var result2 = GreedyAlgorithm.StarChromaticNumber(graph2);
            var result3 = GreedyAlgorithm.StarChromaticNumber(graph3);

            var resultInfo = "Graph1: " + result1 + " Graph2: " + result2 + " Graph3: " + result3;

            if (isCustomGraphLoaded)
            {
                customGraph = BuildCustomGraph(customGraphData);           
            }
            if (isCustomGraphLoaded)
            {
                var resultCustom = GreedyAlgorithm.StarChromaticNumber(customGraph);

                resultInfo += " Custom: " + resultCustom;
            }

            GreedyResultLabel.Content = resultInfo;
            #endregion

            #region LargestFirst Algorithm
            graph1 = BuildGraph1();

            graph2 = BuildGraph2();

            graph3 = BuildGraph3();

            result1 = LargestFirst.StarChromaticNumber(graph1);
            result2 = LargestFirst.StarChromaticNumber(graph2);
            result3 = LargestFirst.StarChromaticNumber(graph3);

            resultInfo = "Graph1: " + result1 + " Graph2: " + result2 + " Graph3: " + result3;

            if (isCustomGraphLoaded)
            {
                customGraph = BuildCustomGraph(customGraphData);
            }
            if (isCustomGraphLoaded)
            {
                var resultCustom = LargestFirst.StarChromaticNumber(customGraph);

                resultInfo += " Custom: " + resultCustom;
            }

            LargestFirstResultLabel.Content = resultInfo;
            #endregion

            #region SmallestLast Algorithm
            graph1 = BuildGraph1();

            graph2 = BuildGraph2();

            graph3 = BuildGraph3();

            result1 = SmallestLast.StarChromaticNumber(graph1, BuildGraph1());
            result2 = SmallestLast.StarChromaticNumber(graph2, BuildGraph2());
            result3 = SmallestLast.StarChromaticNumber(graph3, BuildGraph3());

            resultInfo = "Graph1: " + result1 + " Graph2: " + result2 + " Graph3: " + result3;

            if (isCustomGraphLoaded)
            {
                customGraph = BuildCustomGraph(customGraphData);   
            }
            if (isCustomGraphLoaded)
            {
                var resultCustom = SmallestLast.StarChromaticNumber(customGraph, BuildCustomGraph(customGraphData));

                resultInfo += " Custom: " + resultCustom;
            }

            SmallestLastResultLabel.Content = resultInfo;
            #endregion

            isWorking = false;
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Custom Graph file";
            ofd.Filter = "Custom Graph file|*.cg;";

            if (ofd.ShowDialog() == true)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
                try
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        var inputData = reader.ReadToEnd();
                        customGraphData = Regex.Replace(inputData, @"\t|\n|\r| ", "");
                    }

                    if(customGraphData.Length > 0)
                    {
                        isCustomGraphLoaded = true;
                        MessageBox.Show("Custom Graph loaded successfully!", "Loading...", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        isCustomGraphLoaded = false;
                        customGraphData = null;
                        throw new ArgumentNullException();
                    }
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("File empty or corrupted!", "Loading...", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Builds default Graph 1
        /// </summary>
        /// <returns></returns>
        private Graph BuildGraph1()
        {
            const int numOfVertices = 11;

            var vertices = new Vertex[numOfVertices];

            for(int i = 0; i < numOfVertices; i++)
            {
                vertices[i] = new Vertex(i);
            }

            vertices[0].AddEdge(vertices[1]);
            vertices[0].AddEdge(vertices[2]);

            vertices[1].AddEdge(vertices[2]);
            vertices[1].AddEdge(vertices[3]);
            vertices[1].AddEdge(vertices[4]);

            vertices[2].AddEdge(vertices[3]);
            vertices[2].AddEdge(vertices[4]);

            vertices[3].AddEdge(vertices[5]);
            vertices[3].AddEdge(vertices[4]);

            vertices[4].AddEdge(vertices[5]);

            vertices[5].AddEdge(vertices[6]);
            vertices[5].AddEdge(vertices[7]);

            vertices[6].AddEdge(vertices[8]);

            vertices[7].AddEdge(vertices[8]);

            vertices[8].AddEdge(vertices[9]);
            vertices[8].AddEdge(vertices[10]);

            vertices[9].AddEdge(vertices[10]);

            int[] colors = new int[vertices.Count()];
            for (int i = 1; i <= vertices.Count(); i++)
                colors[i-1] = i;

            var result = new Graph() { Vertices = vertices, Colors = colors };
            foreach(var v in result.Vertices)
            {
                v.AvailableColors = result.Colors.ToList();
            }

            return result;
        }

        /// <summary>
        /// Builds default Graph 2
        /// </summary>
        /// <returns></returns>
        private Graph BuildGraph2()
        {
            const int numOfVertices = 12;

            var vertices = new Vertex[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                vertices[i] = new Vertex(i);
            }

            vertices[0].AddEdge(vertices[1]);
            vertices[0].AddEdge(vertices[2]);
            vertices[0].AddEdge(vertices[3]);

            vertices[1].AddEdge(vertices[2]);
            vertices[1].AddEdge(vertices[4]);
            vertices[1].AddEdge(vertices[9]);

            vertices[2].AddEdge(vertices[7]);
            vertices[2].AddEdge(vertices[10]);

            vertices[3].AddEdge(vertices[5]);
            vertices[3].AddEdge(vertices[6]);

            vertices[4].AddEdge(vertices[5]);
            vertices[4].AddEdge(vertices[9]);

            vertices[5].AddEdge(vertices[8]);

            vertices[6].AddEdge(vertices[7]);
            vertices[6].AddEdge(vertices[8]);

            vertices[7].AddEdge(vertices[10]);

            vertices[8].AddEdge(vertices[11]);

            vertices[9].AddEdge(vertices[10]);
            vertices[9].AddEdge(vertices[11]);

            vertices[10].AddEdge(vertices[11]);

            int[] colors = new int[vertices.Count()];
            for (int i = 1; i <= vertices.Count(); i++)
                colors[i - 1] = i;

            var result = new Graph() { Vertices = vertices, Colors = colors };
            foreach (var v in result.Vertices)
            {
                v.AvailableColors = result.Colors.ToList();
            }

            return result;
        }

        /// <summary>
        /// Builds default Graph 3
        /// </summary>
        /// <returns></returns>
        private Graph BuildGraph3()
        {
            const int numOfVertices = 13;

            var vertices = new Vertex[numOfVertices];

            for (int i = 0; i < numOfVertices; i++)
            {
                vertices[i] = new Vertex(i);
            }

            vertices[0].AddEdge(vertices[1]);
            vertices[0].AddEdge(vertices[2]);
            vertices[0].AddEdge(vertices[3]);
            vertices[0].AddEdge(vertices[4]);
            vertices[0].AddEdge(vertices[7]);
            vertices[0].AddEdge(vertices[8]);
            vertices[0].AddEdge(vertices[12]);

            vertices[1].AddEdge(vertices[4]);

            vertices[2].AddEdge(vertices[3]);
            vertices[2].AddEdge(vertices[6]);
            vertices[2].AddEdge(vertices[7]);

            vertices[3].AddEdge(vertices[4]);
            vertices[3].AddEdge(vertices[5]);
            vertices[3].AddEdge(vertices[6]);

            vertices[4].AddEdge(vertices[5]);
            vertices[4].AddEdge(vertices[9]);
            vertices[4].AddEdge(vertices[11]);
            vertices[4].AddEdge(vertices[12]);

            vertices[5].AddEdge(vertices[6]);
            vertices[5].AddEdge(vertices[9]);

            vertices[6].AddEdge(vertices[7]);
            vertices[6].AddEdge(vertices[9]);
            vertices[6].AddEdge(vertices[10]);

            vertices[7].AddEdge(vertices[10]);
            vertices[7].AddEdge(vertices[12]);

            vertices[9].AddEdge(vertices[12]);

            vertices[9].AddEdge(vertices[10]);
            vertices[9].AddEdge(vertices[12]);

            vertices[11].AddEdge(vertices[12]);

            int[] colors = new int[vertices.Count()];
            for (int i = 1; i <= vertices.Count(); i++)
                colors[i - 1] = i;

            var result = new Graph() { Vertices = vertices, Colors = colors };
            foreach (var v in result.Vertices)
            {
                v.AvailableColors = result.Colors.ToList();
            }

            return result;
        }

        private Graph BuildCustomGraph(string graphData)
        {
            if (graphData == null)
                return null;

            try
            {
                var sections = graphData.Split(';');
                if (sections.Length != 4)
                {
                    throw new FileFormatException();                  
                }

                var result = new Graph();

                #region Name
                var nameSection = sections[0].Split(':');
                if (nameSection.Length != 2 || nameSection[0] != "name")
                {
                    throw new FileFormatException();
                }
                result.Name = nameSection[1];
                #endregion

                #region Vertices
                var verticesSection = sections[1].Split(':');
                if (verticesSection.Length != 2 || verticesSection[0] != "vertices")
                {
                    throw new FileFormatException();
                }
                var vertexNumber = Convert.ToInt32(verticesSection[1]);

                var vertices = new Vertex[vertexNumber];

                for (int i = 0; i < vertexNumber; i++)
                {
                    vertices[i] = new Vertex(i);
                }
                #endregion

                #region Edges
                var edgesSection = sections[2].Split(':');
                if (edgesSection.Length != 2 || edgesSection[0] != "edges")
                {
                    throw new FileFormatException();
                }

                var edges = edgesSection[1].Split(',');
                foreach(var edge in edges)
                {
                    var terminations = edge.Split('-');
                    if (terminations.Length != 2)
                    {
                        throw new FileFormatException();
                    }

                    vertices[Convert.ToInt32(terminations[0])].AddEdge(vertices[Convert.ToInt32(terminations[1])]);
                }
                #endregion

                #region Build result graph
                int[] colors = new int[vertices.Count()];
                for (int i = 1; i <= vertices.Count(); i++)
                    colors[i - 1] = i;

                result.Vertices = vertices;
                result.Colors = colors;

                foreach (var v in result.Vertices)
                {
                    v.AvailableColors = result.Colors.ToList();
                }
                #endregion

                return result;
            }
            catch(FileFormatException)
            {
                MessageBox.Show("Wrong file format!");
                isCustomGraphLoaded = false;
                return null;
            }
            catch(FormatException)
            {
                MessageBox.Show("Format exception during parsing!");
                isCustomGraphLoaded = false;
                return null;
            }
        }
        #endregion
    }
}
