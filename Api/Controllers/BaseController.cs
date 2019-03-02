using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using FinanceApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Cashflow.Api.Controllers
{
  /// <summary>
  /// Base Controller
  /// </summary>
  public abstract class BaseController : Controller
  {
    /// <summary>
    /// Identificador do usuário logado
    /// </summary>
    protected int UserId
    {
      get
      {
        var userId = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Sid).Value;
        return Convert.ToInt32(userId);
      }
    }

    /// <summary>
    /// Chamado quando ocorre de validação em entidades
    /// </summary>
    protected void ThrowValidationError(string error) => throw new ValidateException(error);
  }
}