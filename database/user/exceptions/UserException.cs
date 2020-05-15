using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.Shared;

namespace TODORoutine.exceptions {
    /**
     * Main User Genral Exception Class
     **/
    class UserException : Exception {
        public UserException(String message) : base(message) {
            Logging.paramenterLogging(nameof(UserException) , true , new Pair(nameof(message) , message));
        }
    }
}
