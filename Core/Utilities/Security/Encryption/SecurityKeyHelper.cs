using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SecurityKeyHelper
    {   
        //WebAPI de appsetting.kson içinde SecurityKey içindeki string değerle encryption geçilmiyor 
        //byte[] olması gerekli securitykeyhelper onu byte[] yapıyor 
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
