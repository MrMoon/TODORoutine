using TODORoutine.database.general.parser;
using TODORoutine.models;

namespace TODORoutine.database.note.parser {

    /**
     * Main Note Database Parser that handle Insert and Update Statments for the Notes , 
     * Parsing Columns and SQL Statments for the note class
     **/
    interface NoteParser : DatabaseParser<Note> {

    }
}
