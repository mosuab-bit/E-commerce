using System;
using System.Security.Claims;

namespace API.Entities;

public static class ClaimsPrincipalExtensions
{
  public static string GetUsername(this ClaimsPrincipal user)
  {
    return user.Identity?.Name?? throw new UnauthorizedAccessException();
  }
}
