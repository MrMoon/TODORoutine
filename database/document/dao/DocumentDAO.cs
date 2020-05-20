using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.document.dao {

    /**
     * Document Data Access Layer to comunicate with the database
     **/
    interface DocumentDAO : DatabaseDAO<Document> {
        List<String> findByOwnerId(String ownerId);
        List<String> findAllDocuments(String lastId = "1");
    }
}
