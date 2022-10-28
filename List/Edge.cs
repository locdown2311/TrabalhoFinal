namespace TrabalhoFinal.List
{
    internal class Edge
    {
        private int source;
        private int sink;
        private int weight;

        public Edge(int source, int sink, int weight)
        {
            this.source = source;
            this.sink = sink;
            this.weight = weight;
        }
        
        public int Source { get { return source; } }
        public int Sink { get { return sink; } }    
        public int Weight { get { return weight; } }

        public override string ToString()
        {
            return source + " ==> " + sink + " (W:" + weight + ")";
        }
    }
}
