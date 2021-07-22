using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibraryNuGet
{
    using Microsoft.AspNetCore.Routing;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using TestApp.Model;

    public class Startup
    {
        public class Response
        {
            [JsonConverter(typeof(StringEnumConverter))]
            public ServerState State { get; set; }
            public string Message { get; set; }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private void AddHealthChecker(IEndpointRouteBuilder endpoints, Func<string> response)
        {
            endpoints.MapGet("/health", async context =>
            {
                await context.Response.WriteAsync(response());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                var response = new Response() { State = ServerState.Idle };
                response.AddMessage("Hello");
                endpoints.UseHealthChecker(() => JsonConvert.SerializeObject(response));
            });
        }
    }
}
