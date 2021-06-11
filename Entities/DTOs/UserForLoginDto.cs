using Core.Entities;

namespace Entities.DTOs
{
    public class UserForLoginDto : IDto
    {
        //kullanıcın giriş yapması için gerekli bilgiler
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
