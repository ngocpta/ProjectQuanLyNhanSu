using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Models;

namespace WebAPI.Security
{
  public class RoleAttribute : Attribute, IActionFilter
  {
    private const string TokenPrefix = "Bearer ";

    private string _roles;

    public RoleAttribute()
    {
      _roles = "authenticated";
    }

    public RoleAttribute(string roles)
    {
      _roles = roles.Trim().ToLower();
    }

    private static string GetHeaderToken(HttpContext context)
    {
      context.Request?.Headers.TryGetValue("Authorization", out var token);
      return token;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
      if (_roles.Equals("public")) return;

      var httpContext = context.HttpContext;
      var token = GetHeaderToken(httpContext);
      if (token == null || !token.Contains(TokenPrefix))
      {
        context.Result = new StatusCodeResult(401);
      }
      else
      {
        token = token.Substring(TokenPrefix.Length);
        var result = TokenProvider.TokenValidate(token, out var decoded);
        if (!result)
          context.Result = new StatusCodeResult(401);
        else
        {
          var memberRepository = httpContext.RequestServices.GetService<Member>();
          //var member = memberRepository.Username(decoded.Sub);
            var member = memberRepository.Username;
          if (member != decoded.Sub)
            context.Result = new StatusCodeResult(401);
          else
          {
            
          }
        }
      }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
  }
}