using BlazorApp.Components;
using BlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<StudentsApiClient>(client =>
{
    client.BaseAddress = new Uri(
        builder.Configuration["ApiBaseUrl"] ?? "http://localhost:6001");
});

builder.Services.AddScoped<ObservedStudentsState>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
  
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
