using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TODORoutine.general.constants;
using TODORoutine.Models;

namespace TODORoutine.graph {
    class Graph {

        private int[] inDegree;
        private List<int>[] graph;

        public Graph(int n) {
            this.graph = new List<int>[n];
            for (int i = 0 ; i < n ; ++i) graph[i] = new List<int>();
            inDegree = new int[n];
        }

        public void add(int u , int v) {
            graph[u].Add(v);
            ++inDegree[v];
        }

        public List<int> topologicalSorting(int n) {
            List<int> sorted = new List<int>();
            Queue<int> q = new Queue<int>();
            for (int i = 0 ; i < n ; ++i) if (inDegree[i] == 0) q.Enqueue(i);
            while(q.Count > 0) {
                int node = q.Dequeue();
                sorted.Add(node);
                foreach(int u in graph[node]) {
                    --inDegree[u];
                    if (inDegree[u] == 0) q.Enqueue(u);
                }
            }
            sorted.RemoveAt(0);
            if (sorted.Count != n) throw new ArgumentException(UserMessages.CYCLE);
            return sorted;
        }
    }
}
