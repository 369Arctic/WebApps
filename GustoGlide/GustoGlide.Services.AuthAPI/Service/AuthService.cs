using GustoGlide.Services.AuthAPI.Data;
using GustoGlide.Services.AuthAPI.Models;
using GustoGlide.Services.AuthAPI.Models.Dto;
using GustoGlide.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace GustoGlide.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext db,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Создаем роль если ее не существует
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto()
                {
                    User = null,
                    Token = ""
                };
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDto = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser newUser = new()
            {
                UserName = registrationRequestDto.Email,
                Name = registrationRequestDto.Name,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
            };
            try
            {
                // создаем нового юзера и записываем в БД
                var result = await _userManager.CreateAsync(newUser, registrationRequestDto.Password);

                if (result.Succeeded)
                {
                    // получаем пользователя из БД на основе Email
                    var userFromDb = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                    // на основе полученного пользователя формируем UserDto
                    UserDto userDto = new()
                    {
                        Id = userFromDb.Id,
                        Email = userFromDb.Email,
                        Name = userFromDb.Name,
                        PhoneNumber = userFromDb.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                //TODO:
                //Обработать возможные ошибки
            }
            return "Error encountered";
        }
    }
}
