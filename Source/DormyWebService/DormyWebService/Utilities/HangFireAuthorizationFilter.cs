using DormyWebService.Entities.AccountEntities;
using Hangfire.Dashboard;

namespace DormyWebService.Utilities
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        //Filter who get to go to HangFire's DashBoard
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            //Allow all authenticated users to see the Dashboard (potentially dangerous).
            //return httpContext.User.Identity.IsAuthenticated;

            //For Admin only
            //return httpContext.User.IsInRole(Role.Admin);

            //Don't check authentication at all
            return true;
        }
    }
}