using Microsoft.AspNetCore.Http;

namespace CostIncomeCalculator.Helpers
{
    /// <summary>
    /// Contains request headers extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Add application error headers
        /// </summary>
        /// <param name="response">Http response</param>
        /// <param name="message">Error message for Application-Error header</param>
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}