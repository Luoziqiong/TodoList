using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;
using TodoList.Repository;
using TodoList.Service;
using TodoList.Service.Dto;
using TodoList.Service.Impl;

namespace TodoList.Web
{
    public class Startup
    {
       /*public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/
        /// <summary>
        /// 
        /// </summary>
        public Startup()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("luoziqiong.json", true, true);
            Configuration = builder.Build();
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //swagger
#if DEBUG
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TodoList.Web"
                });

                //Determine base path for the application.  
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                //Set the comments path for the swagger json and ui.  
                var xmlPath = Path.Combine(basePath, "TodoList.Web.xml");
                options.IncludeXmlComments(xmlPath);
            });
#endif

            //DbContext
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TodoListDbContext>(options => options.UseMySql(connection));

            services.AddScoped<IUserService,UserService>();
            services.AddScoped<ITodoListService, TodoListService>();
            services.AddScoped<IBookService, BookService>();

            //瞬时，作用域，单例服务注入
            services.AddTransient<ITransientService, TransientService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddSingleton<ISingletonService, SingletonService>();

            var provider = services.BuildServiceProvider();
            var transient1 = provider.GetService<ITransientService>();
            var transient2 = provider.GetService<ITransientService>();

            Console.WriteLine($"transient1 guid;{transient1.MyGuid}");
            Console.WriteLine($"transient2 guid;{transient2.MyGuid}");
            Console.WriteLine();

            var scoped1 = provider.GetService<IScopedService>();
            var scoped2 = provider.GetService<IScopedService>();

            Console.WriteLine($"scoped1 guid;{scoped1.MyGuid}");
            Console.WriteLine($"scoped2 guid;{scoped2.MyGuid}");
            Console.WriteLine();

            var singleton1 = provider.GetService<ISingletonService>();
            var singleton2 = provider.GetService<ISingletonService>();

            Console.WriteLine($"singleton1 guid;{singleton1.MyGuid}");
            Console.WriteLine($"singleton2 guid;{singleton2.MyGuid}");
            Console.WriteLine();

            //配置文件读取
            //字符串读取
            var NameStirng = Configuration.GetValue<string>("NameString");
            Console.WriteLine(NameStirng);
            Console.WriteLine();
            //列表读取
            var Books = Configuration.GetSection("Books").Get<List<BookDto>>();
            foreach(var b in Books)
            {
                Console.WriteLine($"Id :{ b.Id}");
                Console.WriteLine($"Name :{ b.Name}");
                Console.WriteLine($"Description :{ b.Description}");
                Console.WriteLine($"Price :${ b.Price}");
                Console.WriteLine($"Publisher :{ b.Publisher}");
                Console.WriteLine($"PublishDate :{ b.PublishDate}");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //swagger
#if DEBUG
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoList.Web");
            });

            app.UseStaticFiles();
#endif
        }
    }
}
