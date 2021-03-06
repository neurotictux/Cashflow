using Api.Infra.Resources;

namespace Cashflow.Api.Infra.Resources.User
{
  public static class UserResources
  {
    public static ResourceBuilder ByEmail => new ResourceBuilder("User.GetByEmail.sql");
    public static ResourceBuilder ById => new ResourceBuilder("User.GetById.sql");
    public static ResourceBuilder All => new ResourceBuilder("User.GetAll.sql");
    public static ResourceBuilder Insert = new ResourceBuilder("User.Insert.sql");
  }
}