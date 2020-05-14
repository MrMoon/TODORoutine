using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Shared {
    class Logging {
        /**
         * Logging Parser for the parameter Errors and Exceptions
         * @mehtodName : the method that called this function
         * @args : a pair of //parameter name , parameter value// of the errors to log parameters
         * return a String of logs
         **/
        public static string paramenterLogging(params Pair[] args) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            foreach(Pair pair in args) {
                stringBuilder.Append(pair.first);
                if (string.IsNullOrEmpty(pair.second)) stringBuilder.Append("Null or Empty");
                stringBuilder.Append(" , ");
            }
            stringBuilder.Append(" }");
            return stringBuilder.ToString();
        }
    }
}
