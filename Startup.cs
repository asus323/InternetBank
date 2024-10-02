using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using InternetBank.Data;
using InternetBank.Data.User;
using InternetBank.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InternetBank.Interfaces;
namespace InternetBank
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InternetBankContext>(
                options =>options.UseNpgsql(Configuration.GetConnectionString("InternetBankConnectionString")));
            services.AddIdentity<ApplicationUser,IdentityRole>()
                    .AddEntityFrameworkStores<InternetBankContext>()
                    .AddDefaultTokenProviders();
            services.AddAuthentication  (options =>{
                options.DefaultAuthenticateScheme =JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme =JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt=>{
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters= new TokenValidationParameters(){
                    ValidateIssuer =true,
                    ValidIssuer =Configuration["JWT:ValidIssuer"],
                    ValidAudience =Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                };
            });
            services.AddControllers();
            services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new OpenApiInfo { Title = "Internetbank api", Version = "v0.1" });});
            services.AddTransient<IUserRepository,UserRepository>();
      
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>{c.SwaggerEndpoint("v1/swagger.json", "Internetbank api");});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSwagger();
                endpoints.MapControllers();
            });
        }
    }
}
