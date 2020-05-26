using NLog;
using System;
using System.Linq;
using System.Text;

namespace TODORoutine.Shared {
    /**
     * Genral Logging Class that handle Error and Info Logging
     **/
    class Logging {

        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /**
         * Logging Parser for the parameter Errors and Exceptions
         * @mehtodName : the method that called this function
         * @args : a pair of //parameter name , parameter value// of the errors to log parameters
         * return a String of logs
         **/
        public static String paramenterLogging(String methodName , bool flag , params Pair[] args) {
            StringBuilder StringBuilder = new StringBuilder();
            StringBuilder.Append("Method ");
            StringBuilder.Append(methodName);
            StringBuilder.Append('\n'); 
            StringBuilder.Append("{ ");
            foreach(Pair pair in args) {
                StringBuilder.Append(pair.first);
                StringBuilder.Append(" - ");
                StringBuilder.Append(String.IsNullOrEmpty(pair.second) ? "Null or Empty" : pair.second);
                if (pair != args[args.Count() - 1]) StringBuilder.Append(" , ");
            }
            StringBuilder.Append(" }");
            if (flag) logger.Error(StringBuilder.ToString());
            else logger.Info(StringBuilder.ToString());
            return StringBuilder.ToString();
        }

        /**
         * Normal Logging 
         * 
         * @isError : is this an Error Message
         * @args : the Message Info
         * 
         * return the Message that was Logged
         **/
        public static String logInfo(bool isError  , params String[] args) {
            StringBuilder StringBuilder = new StringBuilder();
            StringBuilder.Append("{ ");
            foreach(String str in args) {
                StringBuilder.Append(str);
                if(str != args[args.Count() - 1]) StringBuilder.Append(" , ");
            }
            String s = StringBuilder.ToString();
            if (isError) logger.Error(s);
            else logger.Info(s);
            return s;
        }
    }
}
