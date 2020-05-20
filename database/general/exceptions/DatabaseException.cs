using System;
using TODORoutine.Shared;

namespace TODORoutine.exceptions {

    /**
     * Main User Genral Exception Class
     **/
    class DatabaseException : Exception {
        public DatabaseException(String message) : base(message) {
            Logging.paramenterLogging(nameof(DatabaseException) , true , new Pair(nameof(message) , message));
        }
    }
}
