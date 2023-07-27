namespace LucasNotes.UserApi.Controllers.Dto
{
    public class UserDto
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Gender { get; set; }
        public string Email { get; set; }
    }
}
