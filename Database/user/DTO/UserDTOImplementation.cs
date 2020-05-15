using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.database.user.exceptions;
using TODORoutine.exceptions;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.Database.user.DTO {
    /**
     * Main User Data Transfer Class that will handle that communication between class for the User 
     **/
    class UserDTOImplementation : UserDTO {

        private static String tableName = DatabaseConstants.TABEL_TODOROUTINE;
        private static UserDTO dto = null;
        private static DatabaseDriver driver = null;
        private static DatabaseParser parser = null;

        private UserDTOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DatabaseParserImplementation.getInstance();
        }

        public static UserDTO getInstance() {
            if (dto == null) dto = new UserDTOImplementation();
            return dto;
        }

        /**
         * User SQL statment based on the username
         * 
         * @username : the username in the select statment
         * 
         * Return an SQL Select Statment for the User
         **/
        public String getIdQuery(String username) {
            throw new NotImplementedException();
        }

        /**
         * User SQL statment based on the id
         * 
         * @id : the id in the select statment
         * 
         * Return an SQL Select Statment for the notesId
         **/
        public String getNotesIdQuery(String id) {
            throw new NotImplementedException();
        }

        /**
         * User SQL Statment based on the id
         * 
         * @id : the id in the select statment
         * 
         * Retunr an SQL Select Statment for the username
         **/
        public String getUsernameQuery(String id) {
            throw new NotImplementedException();
        }

        /**Compare two users and return the diffrent columns in an 
         * @oldUser : Main user to compate to
         * @newUser : User to compare to the main user
         * 
         * return an arraylist of diffrences
         **/
        public ArrayList compare(User oldUser , User newUser) {
            if (oldUser == null || newUser == null) throw new UserException(UserConstants.INVALID("Null") + 
                Logging.paramenterLogging(nameof(compare) , true , new Pair(nameof(oldUser) , oldUser.toString())
                , new Pair(nameof(newUser) , newUser.toString())));

            Logging.paramenterLogging(nameof(compare) , false , new Pair(nameof(oldUser) , oldUser.toString())
                , new Pair(nameof(newUser) , newUser.toString()));

            ArrayList list = new ArrayList();

            if (!oldUser.getFullName().Equals(newUser.getFullName())) list.Add(DatabaseConstants`.COLUMN_FULLNAME);
            if (!oldUser.getNotesId().Equals(newUser.getNotesId())) list.Add(DatabaseConstants.COLUMN_NOTESID);
            if (!oldUser.getUsername().Equals(newUser.getUsername())) list.Add(DatabaseConstants.COLUMN_USERNAME);

            return list;
        }
    }
}
