using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using OnlineBankingApp.Models;

namespace OnlineBankingApp.Authorization
{
    public static class PrincipalPermission{
        public static List<Func<AuthorizationHandlerContext, bool>> Criteria = new List<Func<AuthorizationHandlerContext, bool>>
        {
            CanCreateFundTransfer,
            CanViewFundTransfer
        };

        public static bool CanCreateFundTransfer(this AuthorizationHandlerContext ctx)
        {
            return ctx.User.IsInRole(Role.ActiveCustomer.ToString());
        }
        public static bool CanViewFundTransfer(this AuthorizationHandlerContext ctx)
        {
            return ctx.User.IsInRole(Role.ActiveCustomer.ToString()) ||
                    ctx.User.IsInRole(Role.SuspendedCustomer.ToString());
        }

    }
}