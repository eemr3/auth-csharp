using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthBlog.Models;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace AuthBlog.Services.Token;

public class TokenGenerator
{

  private readonly IConfiguration _configuration;

  public TokenGenerator(IConfiguration configuration)
  {
    _configuration = configuration;
  }
  public string Generator(User user)
  {
    string alternativeKey = "BnPVLnJNzGo/ZSGAkbDrqdn2+ptErwpwJPC5bfiv4F1ySwvW26ET71u+7A2SXjAY";
    var secret = _configuration["Jwt:SecretKey"] ?? alternativeKey;
    var expiresHours = 4;

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
      Subject = AddClaims(user),
      SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        SecurityAlgorithms.HmacSha256Signature
      ),
      Expires = DateTime.Now.AddHours(expiresHours)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  private ClaimsIdentity AddClaims(User user)
  {
    var claims = new ClaimsIdentity();
    claims.AddClaim(new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
    claims.AddClaim(new Claim(ClaimTypes.Email, user.Email!));
    claims.AddClaim(new Claim(ClaimTypes.Name, user.Name!));
    claims.AddClaim(new Claim(ClaimTypes.Role, user.Role!));

    return claims;
  }
}