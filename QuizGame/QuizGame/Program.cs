using QuizGame.Client.Pages;
using QuizGame.Components;
using Microsoft.EntityFrameworkCore;
using QuizGame.Data;
using QuizGame.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseNpgsql(connectionString)
           .UseSnakeCaseNamingConvention()); // 命名規則をPostgreSQLに合わせる

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.AddScoped<IQuizService, QuizService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<QuizDbContext>();

    // DBが起動するまで最大5回リトライする
    for (int i = 0; i < 5; i++)
    {
        try
        {
            Console.WriteLine($"Database connection attempt {i + 1}...");
            context.Database.EnsureCreated();
            Console.WriteLine("Database check completed successfully.");
            break; // 成功したらループを抜ける
        }
        catch (Exception ex)
        {
            if (i == 4) throw; // 5回ダメならエラーを出す
            Console.WriteLine("Database not ready yet... waiting 3 seconds.");
            Thread.Sleep(3000); // 3秒待ってから再チャレンジ
        }
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(QuizGame.Client._Imports).Assembly);

app.Run();
