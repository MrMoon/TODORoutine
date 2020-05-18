using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.document.dao {

    /**
     * Document Data Access Layer to comunicate with the database
     **/
    public interface DocumentDAO : DatabaseDAO<Document> {
        List<String> getByOwnerId(String ownerId);
        List<String> getDocuments(String lastId = "0");
    }
}
