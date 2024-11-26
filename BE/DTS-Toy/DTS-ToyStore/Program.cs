using Business.Helper;
using Business.Profiles;
using Data.Models;
using DTS_ToyStore.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository.AddressRepository;
using Repository.BrandRepository;
using Repository.CategoryRepository;
using Repository.ContentRepository;
using Repository.Order;
using Repository.OrderRepository;
using Repository.FavoriteRepository;
using Repository.FlashSaleRepository;
using Repository.PermissionRepository;
using Repository.ProductRepository;
using Repository.RateRepository;
using Repository.RoleDetail;
using Repository.UsersRepository;
using System.Security;
using System.Text;
using Repository.PaymentRepository;
using Repository.TranslationRepository;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient<HttpClientWithToken>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        builder.Services.AddDistributedMemoryCache();
        //builder.Services.AddSession();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(5); // Thời gian tồn tại của Session (5phút)
            options.Cookie.HttpOnly = true; // Tăng bảo mật
            options.Cookie.IsEssential = true; // Bắt buộc cần thiết (cho GDPR hoặc luật khác)
        });
        builder.Services.AddControllers().AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }
        );
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ContentPageProfile).Assembly);
        builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ProductProfile).Assembly);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "DTS-ToyStore", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
            });
        });

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],

                ValidateAudience = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],

                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });





        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader());
        });

      


        string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        builder.Services.AddScoped<LanguageHelper>();
        builder.Services.AddSignalR();
        builder.Services.AddScoped<FileUpload>(provider => new FileUpload(uploadFolder));
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IContentRepository, ContentRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IBrandRepository, BrandRepository>();
        builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
        builder.Services.AddScoped<IRoleDetailRepository, RoleDetailRepository>();
        builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
        builder.Services.AddScoped<IAddressRepository, AddressRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
        builder.Services.AddScoped<IRateRepository, RateRepository>();
        builder.Services.AddScoped<IFlashSaleRepository, FlashSaleRepository>();
        builder.Services.AddScoped<IContentRepository, ContentRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<ITranslationRepository, TranslationRepository>();




        builder.Services.AddDbContext<ECommerceDBContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("MyCnn"))
        );

        var app = builder.Build();

        app.UseMiddleware<LanguageMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }



        app.UseStaticFiles();
        app.UseCors("AllowSpecificOrigin");
        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}