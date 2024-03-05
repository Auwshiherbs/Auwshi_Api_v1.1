using Microsoft.AspNetCore.Mvc;

namespace Awushi.Web.Models
{
    public class CustomeProblemDetails : ProblemDetails
    {
        public IDictionary<string, string[]> Errorrs { get; set; } = new Dictionary<string, string[]>();
    }
}
