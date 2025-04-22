using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.RestApi.Common.Extension
{
    public sealed class SecurityMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // For Clickjacking, XSS ve MIME-type sniffing attacks
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");

            // For Clickjacking, XSS ve MIME-type sniffing attacks
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");

            // disable to get pages in iframe for attackers
            context.Response.Headers.Add("X-Frame-Options", "DENY");

            // disable the resource info for user
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");

            // Indicate that we will not use them
            context.Response.Headers.Add("Feature-Policy",
                                         "camera 'none'; " +
                                         "accelerometer 'none'; " +
                                         "geolocation 'none'; " +
                                         "magnetometer 'none'; " +
                                         "microphone 'none'; " +
                                         "usb 'none'");

            // change server name for success calls
            context.Response.Headers.SetCommaSeparatedValues("Server", "N/A");

            // cant bury the page in adobe reader or some else with this
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");

            await next.Invoke(context);
        }
    }
}
