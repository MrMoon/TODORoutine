using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.parsers;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.Shared {
    /**
     * Main User Database Parser that handle Parsing Columns and SQL Statments
     **/
    class DatabaseUserParser {
        /**
         * Parse a user data and return the wanted column 
         * @user : the user that will be parsed
         * @column : the column that will be check for
         * 
         * return a user field based on the column or throw an Exception
         **/
        public static String getColumnFromUserObject(User user , String column) {

            if(!DatabaseValidator.isValidParameters(column) && !DatabaseValidator.isValidUser(user)) 
                throw new ArgumentException(Logging.paramenterLogging(nameof(getColumnFromUserObject) , true 
                                            , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.toString())));
            Logging.paramenterLogging(nameof(getColumnFromUserObject) , false
                                            , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.toString()));
            column = column.ToUpper();
            if (column.Equals(DatabaseConstants.COLUMN_FULLNAME)) return user.getFullName();
            else if (column.Equals(DatabaseConstants.COLUMN_NOTESID)) return user.getNotesId();
            else if (column.Equals(DatabaseConstants.COLUMN_USERID)) return user.getId();
            else if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return user.getUsername();

            throw new ArgumentException("Unknown Column " + Logging.paramenterLogging(nameof(getColumnFromUserObject) , true
                                            , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.toString())));
        }
    }
}
