using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.NoteService.Protos.Note;
using LucasNotes.NoteService.Repositories.Interface;
using Microsoft.AspNetCore.Identity;

namespace LucasNotes.NoteService.Services
{
    public class NoteService : NoteManager.NoteManagerBase
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public override async Task<SuccessMsg> AddNote(AddNoteRequest request, ServerCallContext context)
        {
            return new SuccessMsg()
            {
                Success = await _noteRepository.AddNoteAsync(request)
            };
        }

        public override async Task<SuccessMsg> DeleteNotes(DeleteNotesRequest request, ServerCallContext context)
        {
            return new SuccessMsg()
            {
                Success = await _noteRepository.DeleteNotesAsync(request.Ids.ToList())
            };
        }

        public override async Task<NoteDto> GetNoteById(GetNoteByIdRequest request, ServerCallContext context)
        {
            return await _noteRepository.GetNoteByIdAsync(request.Id);
        }

        public override async Task<GetNotesReply> GetNotes(Empty request, ServerCallContext context)
        {
            var result = new GetNotesReply();
            var Notes = await _noteRepository.GetNotesAsync(null);
            result.Notes.AddRange(Notes);
            return result;
        }

        public override async Task<GetNotesReply> GetNoteByFolderId(GetNoteByFolderIdRequest request, ServerCallContext context)
        {
            var result = new GetNotesReply();
            var Notes = await _noteRepository.GetNotesByFolderIdAsync(request.FolderId);
            result.Notes.AddRange(Notes);
            return result;
        }

        public override async Task<SuccessMsg> UpdateNote(NoteDto request, ServerCallContext context)
        {
            return new SuccessMsg
            {
                Success = await _noteRepository.UpdateNoteAsync(request)
            };
        }

        public override async Task<SuccessMsg> UpdateNotes(UpdateNotesRequest request, ServerCallContext context)
        {
            return new SuccessMsg()
            {
                Success = await _noteRepository.UpdateNotesAsync(request.Notes.ToList())
            };
        }
    }
}
