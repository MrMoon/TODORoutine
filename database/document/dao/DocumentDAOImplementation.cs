using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using TODORoutine.database.general;
using TODORoutine.database.parsers;
using TODORoutine.Database;
using TODORoutine.exceptions;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.document.dao {

    /**
     * Document Data Access Layer Implementation to comunicate with the database
     * Handles Database Operation for documents
     **/
    public class DocumentDAOImplementation : DocumentDAO {

        private readonly String tableName = DatabaseConstants.TABLE_DOCUMENT;
        private static DocumentDAO documentDAO = null;
        private DocumentParser parser = null;
        private DatabaseDriver driver = null;

        private DocumentDAOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DocumentParserImplementation.getInstance();
        }

        public static DocumentDAO getInstance() {
            if (documentDAO == null) documentDAO = new DocumentDAOImplementation();
            return documentDAO;
        }

        /**
        * Reading the document from the database reader
        * 
        * @dataReader : SQLite Reader that read from the database
        * 
        * return the read document from the database
        **/
        public Document getT(SQLiteDataReader dataReader) {
            Document document = new Document();
            //TODO get document
            document.setId(dataReader[DatabaseConstants.COLUMN_DOCUMENTID].ToString());
            document.setOwner(dataReader[DatabaseConstants.COLUMN_OWENER].ToString());
            return document;
        }

        /**
        * deleting the document from the Database
        * 
        * @document : the document that will get deleted
        * 
        * return true if and only if the delete operation was done successfully
        **/
        public bool delete(Document document) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(document) , document.toString()));
            //Deleting document from database
            try {
                driver.executeQuery(parser.getDelete(tableName , DatabaseConstants.COLUMN_USERID , document.getId()));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
         * Getting the document from the database based on the id
         * 
         * @id : the document id to search for
         * 
         * return document if found and throw an Exception otherwise
         **/
        public Document findById(string id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            //Getting the document
            SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName ,
                                            DatabaseConstants.COLUMN_USERID , DatabaseConstants.ALL , id));
            //Reading the the Record from the database
            while (reader.Read()) {
                Document document = getT(reader);
                Logging.logInfo(false , nameof(findById) , DatabaseConstants.FOUND(id) , document.toString());
                reader.Close();
                return document;
            }
            reader.Close();
            //Logging
            Logging.paramenterLogging(nameof(findById) , true , new Pair(nameof(id) , id));
            //Document not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));

        }

        /**
         * Inserting the document into the SQL Database
         * 
         * @document : the document that will get inserted
         * 
         * return ture if and only if the document was saved successfully
         **/
        public bool save(Document document) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(document) , document.toString()));
            //Inserting User into the Database
            try {
                driver.executeQuery(parser.getInsert(document));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
         * updatting the document in the SQL Database
         * 
         * @document : the document that will get updated
         * @columns : the column in the database that will be updated
         * 
         * return true if and only if the updating operation was successfull
         **/
        public bool update(Document document , params string[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(document) , document.toString()));
            //Updating
            try {
                driver.executeQuery(parser.getUpdate(tableName , DatabaseConstants.COLUMN_USERID , document.getId() , document , columns));
            } catch (SQLiteException e) {
                Logging.logInfo(true , e.Data.ToString());
                return false;
            }
            return true;
        }

        /**
         * Getting all Documents from the range of lastId - 1 to lastId + 20
         * 
         * @lastId : the last id that was read before
         * 
         * return a list of document ids
         **/
        public List<String> getDocuments(String lastId = "0") {
            //Logging
            Logging.logInfo(false , nameof(getDocuments));
            //Getting 20 Document
            List<String> documentsIds = new List<String>();
            int x = int.Parse(lastId);
            String query = parser.getSelect(tableName , "" , DatabaseConstants.COLUMN_DOCUMENTID , "" , lastId.Equals("0") ? 0 : x - 1 , lastId.Equals("0") ? 20 : x + 20);
            SQLiteDataReader reader = driver.getReader(query);
            try {
                while(reader.Read()) {
                    documentsIds.Add(reader[DatabaseConstants.COLUMN_DOCUMENTID].ToString());
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Data.ToString());
                return new List<String>();
            }
            //Logging
            Logging.logInfo(false , nameof(documentsIds));
            return documentsIds;
        }

        /**
        * Getting all Documents from based on the owner's id
        * 
        * @ownerId : the ownerId to search for
        * 
        * return a list of document ids
        **/
        public List<string> getByOwnerId(string ownerId) {
            List<String> documentsIds = new List<String>();
            String query = parser.getSelect(tableName , DatabaseConstants.COLUMN_OWENER , DatabaseConstants.COLUMN_DOCUMENTID , ownerId);
            SQLiteDataReader reader = driver.getReader(query);
            try {
                while (reader.Read()) {
                    documentsIds.Add(reader[DatabaseConstants.COLUMN_DOCUMENTID].ToString());
                }
            } catch (Exception e) {
                Logging.logInfo(true , e.Data.ToString());
                return new List<String>();
            }
            //Logging
            Logging.logInfo(false , nameof(documentsIds));
            return documentsIds;
        }
    }
}
