using Google.Protobuf.WellKnownTypes;
using LucasNotes.NoteService.Protos.Folder;

namespace LucasNotes.NoteService.Repositories.Interface
{
    public interface IFolderRepository
    {
        Task<bool> AddFolderAsync(FolderDto request);
        Task<bool> DeleteFolderAsync(int id);
        Task<bool> UpdateFolderNameAsync(UpdateFolderNameRequest request);
        Task<bool> UpdateFolderParentAsync(UpdateFolderParentRequest request);
        Task<List<FolderDto>> GetFoldersAsync(int userId);
        Task<bool> HasPermissionAsync(int folderId, int userId);
    }
}
