﻿using TODORoutine.database.parsers;
using TODORoutine.models;

namespace TODORoutine.database.sharing.parser {

    /**
     * Main Share Parser that handle insert and delete statement for the share class
     **/
    interface ShareParser : DatabaseParser<Share> {
        
    }
}