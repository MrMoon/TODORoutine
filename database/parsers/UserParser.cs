using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.parsers;
using TODORoutine.database.user.exceptions;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.Shared {
    /**
     * Main User Database Parser that handle Parsing Columns and SQL Statments for the user class
     **/
    class UserParser {

        /**
         * Column name in the database into a user filed
         * 
         * @column : the column in the database
         * @user : the user to return the field from
         * 
         * return a user field String value based on the database column
         **/
        public static String getUserFieldFromColumn(string column , User user) {
            //Logging
            Logging.paramenterLogging(nameof(getUserFieldFromColumn) , false
                    , new Pair(nameof(column) , column) , new Pair(nameof(user) , user.toString()));
            //Getting user filed
            if (column.Equals(DatabaseConstants.COLUMN_FULLNAME)) return user.getFullName();
            if (column.Equals(DatabaseConstants.COLUMN_NOTESID)) return user.getNotesId();
            if (column.Equals(DatabaseConstants.COLUMN_USERID)) return user.getId();
            if (column.Equals(DatabaseConstants.COLUMN_USERNAME)) return user.getUsername();
            //Column is invalid
            throw new UserException(UserConstants.INVALID(column));
        }
    }
}
