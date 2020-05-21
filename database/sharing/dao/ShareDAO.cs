using System;
using System.Collections.Generic;
using TODORoutine.database.general.dao;
using TODORoutine.models;

namespace TODORoutine.database.sharing.dao {
    interface ShareDAO : DatabaseDAO<Share> {

        List<String> findAllDocumentsIds(String userId);

    }
}
