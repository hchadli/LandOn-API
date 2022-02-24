
using LandonApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LandonAPI.Infrastructure
{
    public class LinkRewriter
    {
        private readonly IUrlHelper _urlHelper;

        public LinkRewriter(IUrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        // ne need to call this method just before our response is serialized and sent to the client,
        // after the controller has created the response model
        // we can do this by creating a result filter and adding it to ASP.NET CORE pipeline.
        // Result filters run before and after a result is processed, and that will let us intercept the response right before it's sent to the client
        public Link Rewrite(Link original)
        {
            if (original == null) return null;

            return new Link
            {
                Href = _urlHelper.Link(original.RouteName, original.RouteValues),
                Method = original.Method,
                Relations = original.Relations,
            };
        }
    }
}
