using CommonLib.Consts;
using CommonLib.Interface;
using Grpc.Net.Client;
using LucasNotes.NoteApi.Controllers.Dto;
using LucasNotes.NoteService.Protos.Folder;
using LucasNotes.NoteService.Protos.Note;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LucasNotes.NoteApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class FolderController : Controller
    {
        private readonly IConsulService _consulService;

        public FolderController(IConsulService consulService)
        {
            _consulService = consulService;
        }

        [HttpGet]
        public async Task<List<Dto.FolderDto>> GetFolders()
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new FolderManager.FolderManagerClient(channel);
                var folders = await client.GetFoldersAsync(new GetFoldersRequest { UserId = userId });
                var result = folders.Folders.Select(item => new Dto.FolderDto
                {
                    Id = item.Id,
                    ParentId = item.ParentId > 0 ? item.ParentId : null,
                    Name = item.Name
                }).ToList();
                result.ForEach(item =>
                {
                    if (!item.ParentId.HasValue)
                    {
                        return;
                    }
                    var parent = result.FirstOrDefault(f => f.Id == item.ParentId);
                    parent?.Children.Add(item);
                });
                return result;
            }
        }

        [HttpDelete]
        public async Task<bool> DeleteFolder(int id)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new FolderManager.FolderManagerClient(channel);
                var result = await client.DeleteFolderAsync(new DeleteFolderRequest()
                {
                    Id = id,
                    UserId = userId
                });
                return result.Success;
            }
        }

        [HttpPost]
        public async Task<bool> UpdateFolderName(UpdateFolderNameInput input)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new FolderManager.FolderManagerClient(channel);
                var result = await client.UpdateFolderNameAsync(new UpdateFolderNameRequest()
                {
                    Id = input.Id,
                    Name = input.Name,
                    UserId = userId
                });
                return result.Success;
            }
        }

        [HttpPost]
        public async Task<bool> UpdateFolderParent(UpdateFolderParent input)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new FolderManager.FolderManagerClient(channel);
                var result = await client.UpdateFolderParentAsync(new UpdateFolderParentRequest()
                {
                    Id = input.Id,
                    ParentId = input.ParentId,
                    UserId = userId
                });
                return result.Success;
            }
        }

        [HttpPost]
        public async Task<bool> AddFolder(Dto.FolderDto input)
        {
            int.TryParse(User.FindFirst("UserId")?.Value, out var userId);
            using (var channel = GrpcChannel.ForAddress(await _consulService.GetUrlFromServiceNameAsync(ServiceNames.NoteService)))
            {
                var client = new FolderManager.FolderManagerClient(channel);
                var result = await client.AddFolderAsync(new NoteService.Protos.Folder.FolderDto()
                {
                    Id = input.Id,
                    Name = input.Name,
                    ParentId = input.Id,
                });
                return result.Success;
            }
        }
    }
}
