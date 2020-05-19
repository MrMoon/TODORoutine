using System;
using System.Collections.Generic;
using TODORoutine.database.document.dao;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.document.dto {

    /**
     * Documnet Data Transfor Layer Implementation to comunicate between the data access layer and the application layer
     * Handles Transfor Operations
     **/
    class DocumentDTOImplementation : DocumentDTO {

        private DocumentDAO documentDAO = null;
        private static DocumentDTO documentDTO = null;

        private DocumentDTOImplementation() {
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
        public List<Document> findAll(String lastId) {
            List<Document> documents = new List<Document>();
            try {
                documentDAO.findAllDocuments(lastId).ForEach(id => documents.Add(documentDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return documents;
            }
            return documents;
        }

        /**
         * Getting the document based on the id
         * 
         * @id : the id of the document to search for
         * 
         * return the document if it was found and null otherwise
         **/
        public Document getById(String id) {
            Document document = null;
            try {
                document = documentDAO.findById(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return document;
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
                documentDAO.save(document);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
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
                documentDAO.delete(id);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
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
                documentDAO.update(document , columns);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
            return true;
        }

        /**
        * Return Documents based on the ownerId
        * 
        * @owenrId : the owenrId to search for
        * 
        * return a list of documents for the ownerId
        **/
        public List<Document> findAllByOwnerId(String owenrId) {
            List<Document> documents = new List<Document>();
            try {
                documentDAO.findByOwnerId(owenrId).ForEach(id => documents.Add(documentDAO.findById(id)));
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return documents;
        }
    }
}
