using TODORoutine.database.parsers;
using TODORoutine.Models;

namespace TODORoutine.Database.Shared {
    /**
     * Main User Database Parser that handle Insert and Update Statments , 
     * Parsing Columns and SQL Statments for the user class
     **/
    public interface UserParser : DatabaseParser<User> {
        
    }
}
