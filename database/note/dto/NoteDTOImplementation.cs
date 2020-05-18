namespace TODORoutine.database.note.dto {
    public class NoteDTOImplementation : NoteDTO {

        private static NoteDTO noteDTO = null;

        private NoteDTOImplementation() { }

        public NoteDTO getInstance() {
            if (noteDTO == null) noteDTO = new NoteDTOImplementation();
            return noteDTO;
        }
    }
}
