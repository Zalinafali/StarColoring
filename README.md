# StarColoring

Simple program for finding star chromatic number of a given graph in WPF.

# Custom algortihms implemented:
- Greedy
- Largest First
- Smallest Last

# Input graphs
3 graphs are hardcoded in the application.

There is an option to load custom graph (one at a time) from a file with extension ".cg" and with proper formatting.

File has 3 sections:
- name
- vertices (with a number of vertices)
- edges (encoded as a pair of vertices, which are joined by an edge)

Example of a custom graph file:

name:TestGraph;
vertices:6;
edges:0-1,1-2,2-3,3-4,4-5,5-1;

![TestGraph](https://user-images.githubusercontent.com/38260620/120112714-3a668b80-c177-11eb-9f45-97544654b22d.png)

