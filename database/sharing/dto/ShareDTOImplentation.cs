using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.document.dao;
using TODORoutine.database.document.dto;
using TODORoutine.database.parsers;
using TODORoutine.database.sharing.dao;
using TODORoutine.models;
using TODORoutine.Models;
using TODORoutine.Shared;

namespace TODORoutine.database.sharing.dto {
    class ShareDTOImplentation : ShareDTO {

        private static ShareDTO shareDTO = null;
        private ShareDAO shareDAO = null;
        private DocumentDTO documentDTO = null;

        private ShareDTOImplentation() {
            shareDAO = ShareDAOImplentation.getInstance();
            documentDTO = DocumentDTOImplementation.getInstance();
        }

        public static ShareDTO getInstance() {
            if (shareDTO == null) shareDTO = new ShareDTOImplentation();
            return shareDTO;
        }

        public bool delete(String userId) {
            try {
                return shareDAO.delete(userId);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

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

        public Share getById(String userId) {
            try {
                return shareDAO.findById(userId);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
            }
            return null;
        }

        public bool save(Share share) {
            try {
                return shareDAO.save(share);
            } catch(Exception e) {
                Logging.logInfo(true , e.Message);
                return false;
            }
        }

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
