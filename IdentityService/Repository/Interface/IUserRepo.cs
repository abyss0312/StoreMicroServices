using IdentityService.Models.Dtos;
using IdentityService.Models.Shared;

namespace IdentityService.Repository.Interface
{
    public interface IUserRepo
    {
        User RegisterUser(UserDtos request);
        string LoginUser(UserDtos request);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);


    }
}
