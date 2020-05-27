using System;
using System.Collections.Generic;
using TODORoutine.general.constants;

namespace TODORoutine.graph {

    /**
     * Basic Graph Model that handles graph algorihtms
     **/
    class Graph {

        private int[] inDegree; //the in degree of a node
        private List<int>[] graph; //the adjacency list representation for a graph

        public Graph(int n) {
            this.graph = new List<int>[n];
            for (int i = 0 ; i < n ; ++i) graph[i] = new List<int>();
            inDegree = new int[n];
        }

        /**
         * Connect parent node with child node  and update the in degree of the child
         * 
         * @u : the parent node
         * @v : the child node
         **/
        public void add(int parent , int child) {
            graph[parent].Add(child);
            ++inDegree[child];
        }

        /**
         * Topological Sorting for the graph tha was build
         * 
         * @numberOfNodes : the number of nodes in the graph
         * 
         * return a topological sort if it was found and throw an exception otherwise
         **/
        public List<int> topologicalSorting(int numberOfNodes) {
            List<int> sorted = new List<int>();
            Queue<int> q = new Queue<int>();
            for (int i = 0 ; i < numberOfNodes ; ++i) if (inDegree[i] == 0) q.Enqueue(i);
            while(q.Count > 0) {
                int node = q.Dequeue();
                sorted.Add(node);
                foreach(int u in graph[node]) {
                    --inDegree[u];
                    if (inDegree[u] == 0) q.Enqueue(u);
                }
            }
            sorted.RemoveAt(0);
            if (sorted.Count != numberOfNodes) throw new ArgumentException(UserMessages.CYCLE);
            return sorted;
        }
    }
}
