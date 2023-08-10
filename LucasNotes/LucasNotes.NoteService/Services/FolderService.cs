using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using LucasNotes.NoteService.Protos.Folder;
using LucasNotes.NoteService.Repositories.Interface;

namespace LucasNotes.NoteService.Services
{
    public class FolderService : FolderManager.FolderManagerBase
    {
        private readonly IFolderRepository _folderRepository;

        public FolderService(IFolderRepository folderRepository)
        {
            _folderRepository = folderRepository;
        }
        public override async Task<SuccessMsg> AddFolder(FolderDto request, ServerCallContext context)
        {
            return new SuccessMsg
            {
                Success = await _folderRepository.AddFolderAsync(request)
            };
        }

        public override async Task<SuccessMsg> DeleteFolder(DeleteFolderRequest request, ServerCallContext context)
        {
            if (!await _folderRepository.HasPermissionAsync(request.Id, request.UserId))
            {
                return new SuccessMsg { Success = false };
            }
            return new SuccessMsg
            {
                Success = await _folderRepository.DeleteFolderAsync(request.Id)
            };
        }

        public override async Task<SuccessMsg> UpdateFolderName(UpdateFolderNameRequest request, ServerCallContext context)
        {
            if (!await _folderRepository.HasPermissionAsync(request.Id, request.UserId))
            {
                return new SuccessMsg { Success = false };
            }
            return new SuccessMsg
            {
                Success = await _folderRepository.UpdateFolderNameAsync(request)
            };
        }

        public override async Task<SuccessMsg> UpdateFolderParent(UpdateFolderParentRequest request, ServerCallContext context)
        {
            if (!await _folderRepository.HasPermissionAsync(request.Id, request.UserId))
            {
                return new SuccessMsg { Success = false };
            }
            return new SuccessMsg
            {
                Success = await _folderRepository.UpdateFolderParentAsync(request)
            };
        }

        public override async Task<GetFoldersReply> GetFolders(GetFoldersRequest request, ServerCallContext context)
        {
            var result = new GetFoldersReply();
            result.Folders.AddRange(await _folderRepository.GetFoldersAsync(request.UserId));
            return result;
        }
    }
}
