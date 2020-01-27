using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chat_server.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace chat_server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR().AddAzureSignalR("Endpoint=https://testing77sr.service.signalr.net;AccessKey=Wx3KPM/gOFdNMarbXhNZcGxDDTRdi+U6s3Y4SxmyTGE=;Version=1.0;");
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options =>
            {
                options.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });

            app.UseFileServer();

            app.UseAzureSignalR(builder =>
            {
                builder.MapHub<Chat>("/chat");
            });

            app.UseRouting();

            
        }
    }
}
