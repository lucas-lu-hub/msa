namespace LucasNotes.NoteApi.Controllers.Dto
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FolderId { get; set; }
        public string Content { get; set; }

    }
}
