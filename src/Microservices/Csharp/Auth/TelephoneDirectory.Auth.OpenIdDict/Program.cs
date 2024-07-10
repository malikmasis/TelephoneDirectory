using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TelephoneDirectory.Auth.OpenIdDict.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
       .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
       {
	       options.LoginPath = "/account/login";
       });

builder.Services.AddDbContext<OpenIdDictDbContext>(options =>
{
	// Configure the context to use an in-memory store.
	options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));

	// Register the entity sets needed by OpenIddict.
	options.UseOpenIddict();
});

builder.Services.AddOpenIddict()
        // Register the OpenIddict core components.
        .AddCore(options =>
        {
	        // Configure OpenIddict to use the EF Core stores/models.
	        options.UseEntityFrameworkCore()
	               .UseDbContext<DbContext>();
        })

        // Register the OpenIddict server components.
        .AddServer(options =>
        {
	        options.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();

	        options
		        .AllowAuthorizationCodeFlow()
		        .RequireProofKeyForCodeExchange()
		        .AllowClientCredentialsFlow()
		        .AllowRefreshTokenFlow();

	        options
		        .SetAuthorizationEndpointUris("/connect/authorize")
		        .SetTokenEndpointUris("/connect/token")
		        .SetUserinfoEndpointUris("/connect/userinfo");

	        // Encryption and signing of tokens
	        options
		        .AddEphemeralEncryptionKey()
		        .AddEphemeralSigningKey()
		        .DisableAccessTokenEncryption();

	        // Register scopes (permissions)
	        options.RegisterScopes("api");

	        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
	        options
		        .UseAspNetCore()
		        .EnableTokenEndpointPassthrough()
		        .EnableAuthorizationEndpointPassthrough()
		        .EnableUserinfoEndpointPassthrough();
        });

builder.Services.AddHostedService<TestData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapDefaultControllerRoute();
});

await app.RunAsync();