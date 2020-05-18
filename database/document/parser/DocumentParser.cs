﻿using System;
using TODORoutine.database.parsers;
using TODORoutine.models;
using TODORoutine.Shared;

namespace TODORoutine.database.document {

    /**
     * Document Database Parser for handling sql statments for the documents
     **/
    public interface DocumentParser : DatabaseParser<Document> {
        Pair insertDocumentWithBLOB(Document document);
    }
}
