using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.document.dto {

    /**
     * Documnet Data Transfor Layer to comunicate between the data access layer and the application layer
     **/
    public interface DocumentDTO : DatabaseDTO<Document> {
        List<Document> findAll(String startId);
        List<Document> findAllByOwnerId(String owenrId);
    }
}
