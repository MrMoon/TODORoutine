using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general;
using TODORoutine.database.general.dao;
using TODORoutine.database.parsers;
using TODORoutine.database.sharing.parser;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.shared.csv;
using TODORoutine.Shared;

namespace TODORoutine.database.sharing.dao {
    class ShareDAOImplentation : DatabaseDAOImplementation<Share> , ShareDAO {

        private readonly String tableName = DatabaseConstants.TABLE_DOCUMENT_SHARE;
        private readonly String documentsIds = DatabaseConstants.COLUMN_DOCUMENTSIDS;
        private readonly String idColumn = DatabaseConstants.COLUMN_USERID;
        private static ShareDAO shareDAO = null;
        private DatabaseDriver driver = null;
        private ShareParser parser = null;

        private ShareDAOImplentation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = ShareParserImplentation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_DOCUMENT_SHARE_TABLE);
        }

        public static ShareDAO getInstance() {
            if (shareDAO == null) shareDAO = new ShareDAOImplentation();
            return shareDAO;
        }

        /**
         * Getting the share from the SQLiteDataReader
         * 
         * @reader : the SQLiteDataReader
         * 
         * return a share from the reader
         **/
        public override Share find(SQLiteDataReader reader) {
            if(reader.Read()) {
                Share share = new Share();
                share.userId = reader[idColumn].ToString();
                share.documentsIds = CSVParser.CSV2List(reader[documentsIds].ToString()).ToHashSet();
                return share;
            }

            throw new DatabaseException(DatabaseConstants.NOT_FOUND("404"));
        }

        /**
         * Deleting the userDocuments base on it's userId
         * 
         * @userId : the userId of the user
         * 
         * return true if and only if the delete operation was successfull
         **/
        public override bool delete(string userId) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false , new Pair(nameof(userId) , userId));
            //Deleting the Notebook from database
            try {
                driver.executeQuery(parser.getDelete(tableName , idColumn , userId));
                return true;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true , new Pair(nameof(userId) , userId));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(userId));
        }

        /**
         * Getting all Documents Id
         * 
         * @userId : the user to get the documents from
         * 
         * return a list of ids
         **/
        public List<string> findAllDocumentsIds(string userId) {
            //Logging
            Logging.paramenterLogging(nameof(findAllDocumentsIds) , false , new Pair(nameof(userId) , userId));
            //Getting all ids
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , documentsIds , userId));
                List<String> documnetsIds = CSVParser.CSV2List(reader[documentsIds].ToString());
                reader.Close();
                return documnetsIds;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findAllDocumentsIds) , true , new Pair(nameof(userId) , userId));
            //Not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(userId));
        }

        /**
         * Finding the share based on it's id
         * 
         * @userId : the user Id to search for
         * 
         * return share if it was found and throw an Exception otherwise
         **/
        public override Share findById(string userId) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(userId) , userId));
            //Getting all ids
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , idColumn , documentsIds , userId));
                Share share = find(reader);
                reader.Close();
                return share;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(userId) , userId));
            //Not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(userId));
        }

        /**
        * Saving the share to the database
        * 
        * @share : the share to save
        * 
        * return true if and only if the notebook was save successfully and false otherwise
        **/
        public override bool save(Share share) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false , new Pair(nameof(share) , share.toString()));
            //Getting all ids
            try {
                return driver.executeQuery(parser.getInsert(share)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
        * Updating the share in the database
        * 
        * @share : the share to update
        * @columns : the columns to update
        * 
        * return true if and only if the update was successfull and false otherwise
        **/
        public override bool update(Share share , params string[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(columns) , columns.ToString()) , new Pair(nameof(share) , share.toString()));
            //Updating the notebook
            try {
                return driver.executeQuery(parser.getUpdate(tableName , idColumn , share.userId , share , columns)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(update) , true , new Pair(nameof(columns) , columns.ToString()) , new Pair(nameof(share) , share.toString()));
            //Notebook was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(share.toString()));
        }
    }
}
