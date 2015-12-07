using Microsoft.AspNet.Authorization.Infrastructure;


namespace MVC6Awakens.Infrastructure.Security
{
    public static class CharacterOperations
    {
        public static OperationAuthorizationRequirement Manage = new OperationAuthorizationRequirement { Name = "Manage" };
        public static OperationAuthorizationRequirement Publish = new OperationAuthorizationRequirement { Name = "Publish" };
    }
}