using System;
using TODORoutine.general.logging;
using TODORoutine.models;

namespace TODORoutine.database.general.exception {

    /**
     * Main User Genral Exception Class
     **/
    class DatabaseException : Exception {
        public DatabaseException(String message) : base(message) => 
            Logging.paramenterLogging(ToString() , true , new Pair(nameof(message) , message));
    }
}
