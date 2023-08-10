using CommonLib.Consts;
using CommonLib.Interface;
using Grpc.Net.Client;
using LucasNotes.NoteApi.Controllers.Dto;
using LucasNotes.NoteService.Protos.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.NoteApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class NoteController : Controller
    {
        private readonly IConsulService _consulService;

        public NoteController(IConsulService consulService)
        {
            _consulService = consulService;
        }
        [HttpGet]
        public async Task<List<Dto.NoteDto>> GetNotes()
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var notes = await client.GetNotesAsync(new Google.Protobuf.WellKnownTypes.Empty());
                return notes.Notes.Select(item => new Dto.NoteDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Content = item.Content,
                    FolderId = item.FolderId,
                    Tag = item.Tag
                }).ToList();
            }
        }


        [HttpGet]
        public async Task<Dto.NoteDto> GetNoteById(int id)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var result = await client.GetNoteByIdAsync(new GetNoteByIdRequest()
                {
                    Id = id,
                    UserId = userId
                });
                return new Dto.NoteDto
                {
                    Id = result.Id,
                    Name = result.Name,
                    Content = result.Content,
                    FolderId = result.FolderId,
                    Tag = result.Tag
                };
            }
        }


        [HttpPost]
        public async Task<bool> UpdateNote(Dto.NoteDto note)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var result = await client.UpdateNoteAsync(new NoteService.Protos.Note.NoteDto
                {
                    Id = note.Id,
                    Name = note.Name,
                    Content = note.Content,
                    FolderId = note.FolderId,
                    Tag = note.Tag,
                    UserId = userId
                });
                return result.Success;
            }
        }


        [HttpPost]
        public async Task<bool> UpdateNotes(List<Dto.NoteDto> notes)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var requestDto = new UpdateNotesRequest();
                requestDto.Notes.AddRange(notes.Select(note => new NoteService.Protos.Note.NoteDto
                {
                    Id = note.Id,
                    Name = note.Name,
                    Content = note.Content,
                    FolderId = note.FolderId,
                    Tag = note.Tag,
                    UserId = userId
                }));
                var result = await client.UpdateNotesAsync(requestDto);
                return result.Success;
            }
        }


        [HttpDelete]
        public async Task<bool> DeleteNotes([FromBody] List<int> ids)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var requestDto = new DeleteNotesRequest();
                requestDto.Ids.AddRange(ids);
                requestDto.UserId = userId;
                var result = await client.DeleteNotesAsync(requestDto);
                return result.Success;
            }
        }


        [HttpPost]
        public async Task<bool> AddNote([FromBody] Dto.NoteDto note)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new NoteManager.NoteManagerClient(channel);
                var result = await client.AddNoteAsync(new AddNoteRequest
                {
                    Note = new NoteService.Protos.Note.NoteDto
                    {
                        Id = note.Id,
                        Name = note.Name,
                        Content = note.Content,
                        FolderId = note.FolderId,
                        Tag = note.Tag,
                        UserId = userId
                    },
                    UserId = userId
                });
                return result.Success;
            }
        }
    }
}
