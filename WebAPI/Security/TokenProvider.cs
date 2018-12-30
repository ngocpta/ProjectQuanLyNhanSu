using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Security
{
  public static class TokenProvider
  {
    private const string TokenSecretKey = "Culacgiontan,Cuonglonggiangthe";

    public static string TokenGenerate(Member member)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecretKey));
      var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
      var now = DateTime.Now;
      var claims = new[]
      {
        new Claim("sub", member.Username.ToString()),
        new Claim("iat", new DateTimeOffset(now).ToUnixTimeMilliseconds().ToString(),
          ClaimValueTypes.Integer64),
        new Claim("role", member.Type.ToString(), ClaimValueTypes.Integer)
      };

      var jwt = new JwtSecurityToken(
        claims: claims,
        notBefore: now,
        expires: now.AddMinutes(120),
        signingCredentials: signingCredentials);
      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
      return encodedJwt;
    }

    public static bool TokenValidate(string token, out Token data)
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSecretKey));
      var validationParameters =
        new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = key,
          ValidateLifetime = true,
          ValidateAudience = false,
          ValidateIssuer = false,
          RequireExpirationTime = true
        };
      data = new Token();
      try
      {
        new JwtSecurityTokenHandler()
          .ValidateToken(token, validationParameters, out var rawValidatedToken);
        var claims = ((JwtSecurityToken) rawValidatedToken).Claims;
        foreach (var claim in claims)
        {
          if (claim.Type.Equals("sub"))
            data.Sub = claim.Value;
          if (claim.Type.Equals("role"))
            data.Role = claim.Value;
        }

        return true;
      }
      catch (SecurityTokenExpiredException ex)
      {
        return false;
      }
      catch (SecurityTokenValidationException stvex)
      {
        return false;
      }
      catch (ArgumentException argex)
      {
        return false;
      }
    }
  }
}