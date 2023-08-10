namespace LucasNotes.NoteApi.Controllers.Dto
{
    public class FolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<FolderDto> Children { get; set; } = new();
    }
}
