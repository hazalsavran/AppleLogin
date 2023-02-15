using Microsoft.IdentityModel.Logging;
using SignInWithApple.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "apple";
})
    .AddCookie("cookie")
    .AddOpenIdConnect("apple", options =>
    {
        options.Authority = "https://appleid.apple.com"; // disco doc: https://appleid.apple.com/.well-known/openid-configuration

                    options.ClientId = "6QP7GK5R6A"; // Service ID
                    options.CallbackPath = "/signin-apple"; // corresponding to your redirect URI

                    options.ResponseType = "code id_token"; // hybrid flow due to lack of PKCE support
                    options.ResponseMode = "form_post"; // form post due to prevent PII in the URL
                    options.DisableTelemetry = true;

        options.Scope.Clear(); // apple does not support the profile scope
                    options.Scope.Add("openid");
        options.Scope.Add("email");
        options.Scope.Add("name");

                    // custom client secret generation - secret can be re-used for up to 6 months
                    options.Events.OnAuthorizationCodeReceived = context =>
        {
            context.TokenEndpointRequest.ClientSecret = TokenGenerator.CreateNewToken();
            return Task.CompletedTask;
        };

        options.UsePkce = false; // apple does not currently support PKCE (April 2021)
                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
