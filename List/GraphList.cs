namespace TrabalhoFinal.List
{
    internal class GraphList
    {
        private int countNodes;
        private int countEdges;
        private static int INF = 999999;
        private LinkedList<Tuple<int, int>>[] adjacencyList;

        public GraphList(int vertices)
        {
            adjacencyList = new LinkedList<Tuple<int, int>>[vertices];

            for (int i = 0; i < adjacencyList.Length; ++i)
            {
                adjacencyList[i] = new LinkedList<Tuple<int, int>>();
            }
            countNodes = vertices;
        }
        // read graph from file
        public GraphList(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            string[] temp = lines[0].Split(' ');
            bool v = int.TryParse(temp[0], out int vertices);
            bool e = int.TryParse(temp[1], out int arestas);
            adjacencyList = new LinkedList<Tuple<int, int>>[vertices];

            for (int i = 0; i < adjacencyList.Length; ++i)
            {
                adjacencyList[i] = new LinkedList<Tuple<int, int>>();
            }
            countNodes = vertices;
            countEdges = arestas;

            for (int i = 1; i < lines.Length; ++i)
            {
                string[] line = lines[i].Split(' ');
                int source = int.Parse(line[0]);
                int sink = int.Parse(line[1]);
                int weight = int.Parse(line[2]);
                addEdge(source, sink, weight);
            }
        }
        
        public static GraphList asciiMazeToGraphList(string filename)
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            int vertices = lines.Length * lines[0].Length;
            GraphList graphList = new GraphList(vertices);
            int source = 0;
            int sink = 0;
            int count = 0;
            for (int i = 0; i < lines.Length; ++i)
            {
                for (int j = 0; j < lines[i].Length; ++j)
                {
                    if (lines[i][j] == 'S')
                    {
                        source = count;
                    }
                    else if (lines[i][j] == 'E')
                    {
                        sink = count;
                    }
                    else if (lines[i][j] == ' ')
                    {
                        if (j + 1 < lines[i].Length && lines[i][j + 1] == ' ')
                        {
                            graphList.addEdge(count, count + 1, 1);
                        }
                        if (i + 1 < lines.Length && lines[i + 1][j] == ' ')
                        {
                            graphList.addEdge(count, count + lines[i].Length, 1);
                        }
                    }
                    ++count;
                }
            }
            graphList.addEdge(source, sink, 1);
            return graphList;
        }
        // dijkstra algorithm from to
        public int dijkstra(int from, int to)
        {
            int[] distance = new int[countNodes];
            bool[] visited = new bool[countNodes];
            for (int i = 0; i < countNodes; ++i)
            {
                distance[i] = INF;
                visited[i] = false;
            }
            distance[from] = 0;
            for (int i = 0; i < countNodes - 1; ++i)
            {
                int u = minDistance(distance, visited);
                visited[u] = true;
                foreach (Tuple<int, int> edge in adjacencyList[u])
                {
                    int v = edge.Item1;
                    int weight = edge.Item2;
                    if (!visited[v] && distance[u] != INF && distance[u] + weight < distance[v])
                    {
                        distance[v] = distance[u] + weight;
                    }
                }
            }
            return distance[to];
        }
        // override ToString
        public override string ToString()
        {
            string s = "GraphList: " + countNodes + " nodes, " + countEdges + " edges \r \n";
            for (int i = 0; i < adjacencyList.Length; ++i)
            {
                s += i + ": ";
                foreach (Tuple<int, int> edge in adjacencyList[i])
                {
                    s += edge.Item1 + " (" + edge.Item2 + ") ";
                }
                s += "\r \n";
            }
            return s;
        }
        // dfs iterative
        public void dfsIterative(int v)
        {
            bool[] visited = new bool[countNodes];
            Stack<int> stack = new Stack<int>();
            stack.Push(v);
            while (stack.Count > 0)
            {
                v = stack.Pop();
                if (!visited[v])
                {
                    visited[v] = true;
                    Console.Write(v + " ");
                    foreach (Tuple<int, int> edge in adjacencyList[v])
                    {
                        if (!visited[edge.Item1])
                        {
                            stack.Push(edge.Item1);
                        }
                    }
                }
            }
        }

        // add edge
        public void addEdge(int startVertex, int endVertex, int weight)
        {
            adjacencyList[startVertex].AddFirst(new Tuple<int, int>(endVertex, weight));
            this.countEdges++;
        }
        // get count of nodes
        public int CountNodes
        {
            get { return countNodes; }
        }
        // get count of edges
        public int CountEdges
        {
            get { return countEdges; }
        }
        // get adjacency list
        public LinkedList<Tuple<int, int>>[] AdjacencyList
        {
            get { return adjacencyList; }
        }

        // dfs
        public void dfs(int startVertex)
        {
            bool[] visited = new bool[countNodes];
            dfs(startVertex, visited);
        }
        private void dfs(int startVertex, bool[] visited)
        {
            visited[startVertex] = true;
            Console.Write(startVertex + " ");
            foreach (var item in adjacencyList[startVertex])
            {
                if (!visited[item.Item1])
                {
                    dfs(item.Item1, visited);
                }
            }
        }
        // bfs
        public void bfs(int startVertex)
        {
            bool[] visited = new bool[countNodes];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(startVertex);
            visited[startVertex] = true;
            while (queue.Count != 0)
            {
                int currentVertex = queue.Dequeue();
                Console.Write(currentVertex + " ");
                foreach (var item in adjacencyList[currentVertex])
                {
                    if (!visited[item.Item1])
                    {
                        queue.Enqueue(item.Item1);
                        visited[item.Item1] = true;
                    }
                }
            }
        }
        // min distance
        private int minDistance(int[] distance, bool[] visited)
        {
            int min = INF;
            int minIndex = -1;
            for (int i = 0; i < countNodes; ++i)
            {
                if (!visited[i] && distance[i] <= min)
                {
                    min = distance[i];
                    minIndex = i;
                }
            }
            return minIndex;
        }


    }
}
