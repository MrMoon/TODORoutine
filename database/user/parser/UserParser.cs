using TODORoutine.database.general.parser;
using TODORoutine.models;

namespace TODORoutine.database.user.parser {
    /**
     * Main User Database Parser that handle Insert and Update Statments , 
     * Parsing Columns and SQL Statments for the user class
     **/
    interface UserParser : DatabaseParser<User> {
        
    }
}
