using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.document.dao;
using TODORoutine.models;

namespace TODORoutine.database.document.dto {

    /**
     * Documnet Data Transfor Layer Implementation to comunicate between the data access layer and the application layer
     * Handles Transfor Operations
     **/
    public class DocumentDTOImplementation : DocumentDTO {

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
         * @startId : the startId for the range of the documents
         * 
         * return a list of documents
         **/
        public List<Document> findAll(String startId) {
            List<Document> documents = new List<Document>();
            List<String> ids = documentDAO.getDocuments(startId);
            ids.ForEach(id => documents.Add(documentDAO.findById(id)));
            return documents;
        }

        /**
         * Getting the document based on the id
         * 
         * @id : the id of the document to search for
         * 
         * return the document if it was found
         **/
        public Document getTById(string id) => documentDAO.findById(id);

        /**
         * Saving the document
         * 
         * @document : the document to save
         * 
         * return true if and only if the save opearation was successfull
         **/
        public bool saveT(Document document) => documentDAO.save(document);

        /**
         * Deleting the document based on the id
         * 
         * @document : the document to delete
         * 
         * return true if and only if the delete opearation was successfull
         **/
        public bool deleteT(Document document) => documentDAO.delete(document);

        /**
         * Updating the document based on the id
         * 
         * @document : the document to update
         * @columns : the columns in the database to update
         * 
         * return true if and only if the update opearation was successfull
         **/
        public bool updateT(Document document , params string[] columns) => documentDAO.update(document , columns);

        /**
        * Return Documents based on the ownerId
        * 
        * @owenrId : the owenrId to search for
        * 
        * return a list of documents for the ownerId
        **/
        public List<Document> findAllByOwnerId(string owenrId) {
            List<Document> documents = new List<Document>();
            List<String> ids = documentDAO.getByOwnerId(owenrId);
            ids.ForEach(id => documents.Add(documentDAO.findById(id)));
            return documents;
        }
    }
}
