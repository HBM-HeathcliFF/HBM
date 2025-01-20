namespace HBM.Identity.Models
{
    public class UpdateUserRoleViewModel
    {
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public string ReturnUrl { get; set; }
    }
}