using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; } //APi deki appsetting deki değerleri okumak için
        private TokenOptions _tokenOptions; // değerleri tutmak için
        private DateTime _accessTokenExpiration; //accesstoken ne zaman geçersiz olacak
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            //_tokenoptions bu süreyi configurationdan alıcak
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

        }

        //token oluşumu için gerekli olan şeyler
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //token ne zamamn geçersiz olacak = şu andan itibaren dakika ekle _tokenoptionstan alıcak süreyi
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            // securitykey'i ise _tokenoptions daki SecurityKey'den alıyor onu ise SecurityKeyHelper bu sınıftaki
            //CreateSecurityKey bu methodu kullanarak yaparız
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);//hangi anahtar hangi algoritma
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);//jwt üretimi ve bunun için gerekli olan parametreler
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }
        //method ile jwt oluşumu
        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        //kullanıcı ve claim bilgisini parametere alarak bir tane claim listesi oluşturma
        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();//IEnumerable base'i
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());

            return claims;
        }
    }
}
