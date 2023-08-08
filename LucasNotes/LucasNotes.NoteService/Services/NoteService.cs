using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.NoteService.Protos;
using Microsoft.AspNetCore.Identity;

namespace LucasNotes.NoteService.Services
{
    public class NoteService : NoteManager.NoteManagerBase
    {
        public override Task<SuccessMsg> AddNote(AddNoteRequest request, ServerCallContext context)
        {
            return base.AddNote(request, context);
        }

        public override Task<SuccessMsg> DeleteNotes(DeleteNotesRequest request, ServerCallContext context)
        {
            return base.DeleteNotes(request, context);
        }

        public override Task<NoteDto> GetNoteById(GetNoteByIdRequest request, ServerCallContext context)
        {
            return base.GetNoteById(request, context);
        }

        public override Task<GetNotesReply> GetNotes(Empty request, ServerCallContext context)
        {
            return base.GetNotes(request, context);
        }

        public override Task<SuccessMsg> UpdateNote(NoteDto request, ServerCallContext context)
        {
            return base.UpdateNote(request, context);
        }

        public override Task<SuccessMsg> UpdateNotes(UpdateNotesRequest request, ServerCallContext context)
        {
            return base.UpdateNotes(request, context);
        }
    }
}
