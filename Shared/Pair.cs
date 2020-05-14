using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Shared {
    public class Pair {
        public Pair() { }

        public Pair(String first , String second) {
            this.first = first;
            this.second = second;
        }

        public String first { get; set; }
        public String second { get; set; }

        public String toString() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            stringBuilder.Append(first);
            stringBuilder.Append(" , ");
            stringBuilder.Append(second);
            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }
    }
}
