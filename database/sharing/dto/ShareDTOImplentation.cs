using System;
using System.Collections.Generic;
using TODORoutine.database.document.dto;
using TODORoutine.database.sharing.dao;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.sharing.dto {
    
    /**
     * Main Implementation for the Share Data Transfer 
     * Handles tarnser operation between Data Layer and the user
     **/
    class ShareDTOImplementation : ShareDTO {

        private static ShareDTO shareDTO = null;
        private readonly ShareDAO shareDAO = null;
        private readonly DocumentDTO documentDTO = null;

        private ShareDTOImplementation() {
            Logging.singlton(nameof(ShareDTO));
            shareDAO = ShareDAOImplementation.getInstance();
            documentDTO = DocumentDTOImplementation.getInstance();
        }

        public static ShareDTO getInstance() {
            if (shareDTO == null) shareDTO = new ShareDTOImplementation();
            return shareDTO;
        }

        /**
         * Deleting the share from the database
         * 
         * @userId : the user id to delete the share 
         * 
         * return ture if and only if the delete operation was done successfully and false otherwise
         **/
        public bool delete(String userId) {
            try {
                return shareDAO.delete(userId);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * Getting all documnets Id from the share 
         * 
         * @userId : the user id to get from
         * 
         * return a list of documnents if it was found and an empty list otherwise
         **/
        public List<Document> getAllDocumentsIds(String userId) {
            try {
                List<Document> documents = new List<Document>();
                shareDAO.findAllDocumentsIds(userId).ForEach(id => documents.Add(documentDTO.getById(id)));
                return documents;
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return new List<Document>();
        }

        /**
         * Getting share by user Id
         * 
         * @userId : the user id the get
         * 
         * Return a share if it was found and an null otherwise
         **/
        public Share getById(String userId) {
            try {
                return shareDAO.findById(userId);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        /**
         * Saving the share in the database
         * 
         * @share : the share to save in the database
         * 
         * return ture if and only if the save operation was done successfully and false otherwise
         **/
        public bool save(Share share) {
            try {
                return shareDAO.save(share);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

        /**
         * Updating the share in the database
         * 
         * @share : the share to save in the database
         * 
         * return ture if and only if the update operation was done successfully and false otherwise
         **/
        public bool update(Share share , params String[] columns) {
            try {
                return shareDAO.update(share , columns);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }
    }
}
