using AutoMapper;
using IdentityService.Data.Model;
using IdentityService.Models.Dtos;
using IdentityService.Models.Shared;

namespace IdentityService.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, Userinfo>();
        }
    }
}
