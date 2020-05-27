using System;
using System.Collections.Generic;
using System.Text;
using TODORoutine.database.document.dao;
using TODORoutine.database.general.dao;
using TODORoutine.database.parsers;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.document.dto {

    /**
     * Documnet Data Transfor Layer Implementation to comunicate between the data access layer and the application layer
     * Handles Transfor Operations
     **/
    class DocumentDTOImplementation : DocumentDTO {

        private readonly DocumentDAO documentDAO = null;
        private static DocumentDTO documentDTO = null;

        private DocumentDTOImplementation() {
            Logging.singlton(nameof(DocumentDTO));
            documentDAO = DocumentDAOImplementation.getInstance();
        }

        public static DocumentDTO getInstance() {
            if (documentDTO == null) documentDTO = new DocumentDTOImplementation();
            return documentDTO;
        }

        /**
         * Return Documents with a limit specified in the data access layer
         * 
         * @lastId : the lastId that from the previous call
         * 
         * return a list of documents
         **/
        public List<Document> getAll(String lastId = "1") {
            try {
                List<Document> documents = new List<Document>();
                documentDAO.findAllDocuments(lastId).ForEach(id => documents.Add(documentDAO.findById(id)));
                return documents;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Document>();
        }

        /**
         * Getting the document based on the id
         * 
         * @id : the id of the document to search for
         * 
         * return the document if it was found and null otherwise
         **/
        public Document getById(String id) {
            try {
                return documentDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Saving the document
         * 
         * @document : the document to save
         * 
         * return true if and only if the save opearation was successfull
         **/
        public bool save(Document document) {
            try {
                bool flag = documentDAO.save(document);
                if(flag) {
                    document.setId(DatabaseDAOImplementation<Document>.getLastId(DatabaseConstants.TABLE_DOCUMENT));
                    return true;
                }
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Deleting the document based on the id
         * 
         * @document : the document to delete
         * 
         * return true if and only if the delete opearation was successfull
         **/
        public bool delete(String id) {
            try {
                return documentDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
         * Updating the document based on the id
         * 
         * @document : the document to update
         * @columns : the columns in the database to update
         * 
         * return true if and only if the update opearation was successfull
         **/
        public bool update(Document document , params String[] columns) {
            try {
                return documentDAO.update(document , columns);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return false;
        }

        /**
        * Return Documents based on the ownerId
        * 
        * @owenrId : the owenrId to search for
        * 
        * return a list of documents for the ownerId
        **/
        public List<Document> getAllByOwnerId(String owenrId) {
            try {
                List<Document> documents = new List<Document>();
                documentDAO.findByOwnerId(owenrId).ForEach(id => documents.Add(documentDAO.findById(id)));
                return documents;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Document>();
        }

        /**
         * Getting Document Content From it's id
         * 
         * @id : the docuemnt id
         * 
         * return the document content
         **/
        public String getDocuement(String id) {
            try {
                return Encoding.Default.GetString(documentDAO.findDocumentBytes(id));
            } catch (Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }
    }
}
