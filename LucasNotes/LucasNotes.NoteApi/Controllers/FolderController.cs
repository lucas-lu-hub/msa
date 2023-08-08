using LucasNotes.NoteApi.Controllers.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.NoteApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FolderController : Controller
    {
        [HttpGet]
        public async Task<string> GetFolders()
        {
            return string.Empty;
        }

        [HttpDelete]
        public async Task<bool> DeleteFolder(int id)
        {
            return true;
        }

        [HttpPost]
        public async Task<bool> UpdateFolderName(UpdateFolderNameInput input)
        {
            return true;
        }

        [HttpPost]
        public async Task<bool> UpdateFolderParent(UpdateFolderParent input)
        {
            return true;
        }

        [HttpPost]
        public async Task<bool> AddFolder(FolderDto input)
        {
            return true;
        }
    }
}
