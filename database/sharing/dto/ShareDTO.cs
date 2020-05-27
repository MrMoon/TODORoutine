using System;
using System.Collections.Generic;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.sharing.dto {

    /**
     * Main Share Data Transfer Layer
     **/
    interface ShareDTO : DatabaseDTO<Share> {

        List<Document> getAllDocumentsIds(String userId);
    }
}
