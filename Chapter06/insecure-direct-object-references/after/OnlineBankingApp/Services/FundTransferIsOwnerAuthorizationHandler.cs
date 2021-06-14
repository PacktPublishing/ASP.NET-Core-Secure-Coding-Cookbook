using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Authorization
{
    public class FundTransferIsOwnerAuthorizationHandler
                : AuthorizationHandler<FundTransferOwnerRequirement, FundTransfer>
    {
        UserManager<Customer> _userManager;

        public FundTransferIsOwnerAuthorizationHandler(UserManager<Customer> 
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   FundTransferOwnerRequirement requirement,
                                   FundTransfer resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (resource.CustomerID == _userManager.GetUserId(context.User))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class FundTransferOwnerRequirement : IAuthorizationRequirement { }    

}