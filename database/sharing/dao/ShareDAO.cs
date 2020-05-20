using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.sharing.dao {
    interface ShareDAO : DatabaseDAO<Share> {

        List<String> findAllDocumentsIds(String userId);

    }
}
