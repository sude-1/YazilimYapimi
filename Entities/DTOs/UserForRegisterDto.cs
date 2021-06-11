using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{

    //kullanıcının kayıt olması için gerekli özellikler
    public class UserForRegisterDto : IDto
    { 
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string TcKimlik { get; set; }
        public string Telefon { get; set; }
    }
}
