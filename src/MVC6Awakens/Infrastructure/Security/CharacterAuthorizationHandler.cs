using System;
using System.Security.Claims;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;

using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.Characters;


namespace MVC6Awakens.Infrastructure.Security
{
    public class CharacterAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Character>
    {
        protected override void Handle(AuthorizationContext context,
            OperationAuthorizationRequirement requirement,
            Character resource)
        {
            var admin = context.User.IsInRole("Administrator");
            if (admin)
            {
                context.Succeed(requirement);
                return;
            }
            var userId = context.User.GetUserId();
            if (userId == resource.CreatorId)
            {
                context.Succeed(requirement);
                return;
            }
            context.Fail();
        }
    }
}
