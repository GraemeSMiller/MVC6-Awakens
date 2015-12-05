using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Authorization.Infrastructure;
using MVC6Awakens.ViewModels.Characters;


namespace MVC6Awakens.Infrastructure.Security
{
    public class CharacterCreateAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, CharacterCreate>
    {
        protected override void Handle(AuthorizationContext context,
            OperationAuthorizationRequirement requirement,
            CharacterCreate resource)
        {
            if (context.User.IsInRole("Administrator"))
            {
                context.Succeed(requirement);
                return;
            }

            //if (context.User.GetUserId() == resource.CreatorId)
            //{
            //    context.Succeed(requirement);
            //    return;
            //}
            context.Succeed(requirement);
        }
    }
}
