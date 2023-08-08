using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.NoteService.Protos;

namespace LucasNotes.NoteService.Repositories.Interface
{
    public interface INoteRepository
    {
        Task<bool> AddNote(AddNoteRequest request);
        Task<bool> DeleteNotes(List<int> ids);
        Task<NoteDto> GetNoteById(int id);
        Task<List<NoteDto>> GetNotes(List<int> ids);
        Task<bool> UpdateNote(NoteDto note);
        Task<bool> UpdateNotes(List<NoteDto> notes);
    }
}
