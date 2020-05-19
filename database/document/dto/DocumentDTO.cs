using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.document.dto {

    /**
     * Documnet Data Transfor Layer to comunicate between the data access layer and the application layer
     **/
    interface DocumentDTO : DatabaseDTO<Document> {
        List<Document> findAll(String lastId);
        List<Document> findAllByOwnerId(String owenrId);
    }
}
