using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODORoutine.Shared {
    /**
     * Genral Logging Class that handle Error and Info Logging
     **/
    class Logging {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /**
         * Logging Parser for the parameter Errors and Exceptions
         * @mehtodName : the method that called this function
         * @args : a pair of //parameter name , parameter value// of the errors to log parameters
         * return a String of logs
         **/
        public static string paramenterLogging(String methodName , bool flag , params Pair[] args) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Method ");
            stringBuilder.Append(methodName);
            stringBuilder.Append('\n'); 
            stringBuilder.Append("{ ");
            foreach(Pair pair in args) {
                stringBuilder.Append(pair.first);
                stringBuilder.Append(" - ");
                stringBuilder.Append(string.IsNullOrEmpty(pair.second) ? "Null or Empty" : pair.second);
                if (pair != args[args.Count() - 1]) stringBuilder.Append(" , ");
            }
            stringBuilder.Append(" }");
            if (flag) logger.Error(stringBuilder.ToString());
            else logger.Info(stringBuilder.ToString());
            return stringBuilder.ToString();
        }

        public static string logInfo(params String[] args) {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{ ");
            foreach(String str in args) {
                stringBuilder.Append(str);
                if(str != args[args.Count() - 1]) stringBuilder.Append(" , ");
            }
            String s = stringBuilder.ToString();
            logger.Info(s);
            return s;
        }
    }
}
