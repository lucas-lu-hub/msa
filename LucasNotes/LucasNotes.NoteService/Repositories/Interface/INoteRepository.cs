using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.NoteService.Protos.Note;

namespace LucasNotes.NoteService.Repositories.Interface
{
    public interface INoteRepository
    {
        Task<bool> AddNoteAsync(AddNoteRequest request);
        Task<bool> DeleteNotesAsync(List<int> ids);
        Task<NoteDto> GetNoteByIdAsync(int id);
        Task<List<NoteDto>> GetNotesAsync(List<int> ids);
        Task<List<NoteDto>> GetNotesByFolderIdAsync(int folderId);
        Task<bool> UpdateNoteAsync(NoteDto note);
        Task<bool> UpdateNotesAsync(List<NoteDto> notes);
    }
}
