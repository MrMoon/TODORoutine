using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general.dto;
using TODORoutine.models;

namespace TODORoutine.database.sharing.dto {
    interface ShareDTO : DatabaseDTO<Share> {

        List<Document> getAllDocumentsIds(String userId);
    }
}
