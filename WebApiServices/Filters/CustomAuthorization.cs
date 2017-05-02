using System.Web.Http;
using System.Web.Http.Controllers;

namespace WebApiServices.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Do custom authorization here: Find actionContext.RequestContext.Claims or Claims
            base.OnAuthorization(actionContext);
        }
    }
}