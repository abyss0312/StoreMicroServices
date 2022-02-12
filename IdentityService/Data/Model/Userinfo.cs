using System.ComponentModel.DataAnnotations;

namespace IdentityService.Data.Model
{
    public class Userinfo
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
    }
}
