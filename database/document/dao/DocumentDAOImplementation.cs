using System;
using System.Collections.Generic;
using System.Data.SQLite;
using TODORoutine.database.general;
using TODORoutine.database.general.dao;
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
    class DocumentDAOImplementation : DatabaseDAOImplementation<Document> , DocumentDAO {

        private readonly String idColumn = DatabaseConstants.COLUMN_ID;
        private readonly String tableName = DatabaseConstants.TABLE_DOCUMENT;
        private static DocumentDAO documentDAO = null;
        private DocumentParser parser = null;
        private DatabaseDriver driver = null;

        private DocumentDAOImplementation() {
            driver = DatabaseDriverImplementation.getInstance();
            parser = DocumentParserImplementation.getInstance();
            driver.createTable(DatabaseConstants.CREATE_DOCUMENT_TABLE);
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
        public override Document find(SQLiteDataReader dataReader) {
            if(dataReader.Read()) {
                Document document = new Document();
                document.setId(dataReader[idColumn].ToString());
                document.setOwner(dataReader[DatabaseConstants.COLUMN_OWENER].ToString());
                document.setDocument((byte[]) dataReader[DatabaseConstants.COLUMN_DOCUMENT]);
                return document;
            }
            throw new DatabaseException(DatabaseConstants.NOT_FOUND("404"));
        }

        /**
        * deleting the document from the Database
        * 
        * @document : the document that will get deleted
        * 
        * return true if and only if the delete operation was done successfully
        **/
        public override bool delete(String id) {
            //Logging
            Logging.paramenterLogging(nameof(delete) , false
                , new Pair(nameof(id) , id));
            //Deleting document from database
            try {
                driver.executeQuery(parser.getDelete(tableName , idColumn , id));
                return true;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(delete) , true , new Pair(nameof(id) , id));
            //Document not found in the database
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(id));
        }

        /**
         * Getting the document from the database based on the id
         * 
         * @id : the document id to search for
         * 
         * return document if found and throw an Exception otherwise
         **/
        public override Document findById(String id) {
            //Logging
            Logging.paramenterLogging(nameof(findById) , false , new Pair(nameof(id) , id));
            try {
                //Finding the document
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName ,
                                                DatabaseConstants.COLUMN_ID , DatabaseConstants.ALL , id));
                //Reading the the Record from the database
                Document document = find(reader);
                reader.Close();
                return document;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
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
        public override bool save(Document document) {
            //Logging
            Logging.paramenterLogging(nameof(save) , false
                , new Pair(nameof(document) , document.ToString()));
            //Inserting User into the Database
            try {
                SQLiteConnection connection = new SQLiteConnection(DatabaseConstants.CONNECTION_STRING);
                SQLiteCommand command = driver.getBLOBCommand(connection , parser.getInsert(document) , DatabaseConstants.DOCUMENT_PARAMETER , document.getDocument());
                command.ExecuteNonQuery();
                connection.Close();
                return true;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * updatting the document in the SQL Database
         * 
         * @document : the document that will get updated
         * @columns : the column in the database that will be updated
         * 
         * return true if and only if the updating operation was successfull
         **/
        public override bool update(Document document , params String[] columns) {
            //Logging
            Logging.paramenterLogging(nameof(update) , false , new Pair(nameof(document) , document.ToString()));
            //Updating
            try {
                return driver.executeQuery(parser.getUpdate(tableName ,
                    DatabaseConstants.COLUMN_ID , document.getId() , document , columns)) != -1;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(update) , true , new Pair(nameof(document) , document.ToString()));
            //Documnet was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(document.ToString()));
        }

        /**
         * Getting all Documents in a range
         * 
         * @lastId : the last id that was read before
         * 
         * return a list of document ids
         **/
        public List<String> findAllDocuments(String lastId = "1") {
            //Logging
            Logging.paramenterLogging(nameof(findAllDocuments) , false , new Pair(nameof(lastId) , lastId));
            //Finding Documents
            return findAll(parser , tableName , "" , lastId);
        }

        /**
        * Getting all Documents from based on the owner's id
        * 
        * @ownerId : the ownerId to search for
        * 
        * return a list of document ids
        **/
        public List<String> findByOwnerId(String ownerId) {
            //Logging
            Logging.paramenterLogging(nameof(findByOwnerId) , false , new Pair(nameof(ownerId) , ownerId));
            //Finding Docuemnts
            try {
                SQLiteDataReader reader = driver.getReader(parser.getSelect(tableName , 
                                        DatabaseConstants.COLUMN_OWENER , idColumn , ownerId));
                List<String> documentsIds = new List<String>();
                while (reader.Read()) documentsIds.Add(reader[idColumn].ToString());
                reader.Close();
                return documentsIds;
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            //Logging
            Logging.paramenterLogging(nameof(findByOwnerId) , true , new Pair(nameof(ownerId) , ownerId));
            //Documnet was not found
            throw new DatabaseException(DatabaseConstants.NOT_FOUND(ownerId));
        }
    }
}
