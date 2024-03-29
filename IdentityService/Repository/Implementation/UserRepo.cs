﻿using AutoMapper;
using IdentityService.Data;
using IdentityService.Data.Model;
using IdentityService.Models.Dtos;
using IdentityService.Models.Shared;
using IdentityService.Repository.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace IdentityService.Repository.Implementation
{
    public class UserRepo : IUserRepo
    {
        private User _user = new User();
        private readonly IConfiguration _conf;
        private readonly IdentityDBContext _context;
        private readonly IMapper _mapper;

        public UserRepo(IConfiguration conf, IdentityDBContext context, IMapper mapper)
        {
            _conf = conf;
            _context = context;
            _mapper = mapper;
        }
       
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmc = new HMACSHA512())
            {
                passwordSalt = hmc.Key;
                passwordHash = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public string LoginUser(UserDtos request)
        {
            var token = "";
           if(request.userName != _user.Username)
            {
                return "Wrong user";
            }
            if (!VerifyPasswordHash(request.password, _user.PasswordHash, _user.PasswordSalt))
            {
                return "";
            }
            token = CreateToken(_user);
            return token;
        }

        public User RegisterUser(UserDtos request)
        {
            var response = new ResponseClient<string>();

               
            CreatePasswordHash(request.password,out byte[] passwordHash, out byte[] passwordSalt);

            _user.Username = request.userName;
            _user.PasswordSalt = passwordSalt;
            _user.PasswordHash = passwordHash;

            var res = _mapper.Map<Userinfo>(_user);

            return _user;

        }

        private bool VerifyPasswordHash(string password,  byte[] passwordHash,  byte[] passwordSalt)
        {
 
            using (var hmc = new HMACSHA512(passwordSalt))
            {
               var computedHas = hmc.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHas.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_conf.GetSection("Jwt:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);
               
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
