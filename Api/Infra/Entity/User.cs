using System;
using System.Collections.Generic;

namespace Cashflow.Api.Infra.Entity
{
  public class User
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime LastAccess { get; set; }

    public ICollection<Payment> Payments { get; set; }
  }
}