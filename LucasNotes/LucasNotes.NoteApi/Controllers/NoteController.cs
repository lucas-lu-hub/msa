using LucasNotes.NoteApi.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.NoteApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NoteController : Controller
    {
        [HttpGet]
        public async Task<List<NoteDto>> GetNotes()
        {
            return await Task.FromResult(new List<NoteDto>());
        }


        [HttpGet]
        public async Task<NoteDto> GetNoteById(int id)
        {
            return await Task.FromResult(new NoteDto());
        }


        [HttpPost]
        public async Task<bool> UpdateNote(NoteDto note)
        {
            return await Task.FromResult(true);
        }


        [HttpPost]
        public async Task<bool> UpdateNotes(List<NoteDto> notes)
        {
            return await Task.FromResult(true);
        }


        [HttpDelete]
        public async Task<bool> DeleteNotes([FromBody]List<int> ids)
        {
            return await Task.FromResult(true);
        }


        [HttpPost]
        public async Task<bool> AddNote([FromBody]AddNoteInput note)
        {
            return await Task.FromResult(true);
        }
    }
}
