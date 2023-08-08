namespace LucasNotes.NoteApi.Controllers.Dto
{
    public class AddNoteInput
    {
        public int UserId { get; set; }
        public NoteDto Note { get; set; }
    }
}
