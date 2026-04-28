using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Board.Abstractions;
using TicTacToe.Domain.Game.Abstractions;
using TicTacToe.Infrastructure.Persistence;
using TicTacToe.Web;
using TicTacToe.Web.Board;
using TicTacToe.Web.Game;
using TicTacToe.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddTransient<IBoard, Board>();
builder.Services.AddSingleton<IGameService, GameService>();

builder.Services.AddTransient<Func<IBoard>>(sp =>
{
    return () => sp.GetRequiredService<IBoard>();
});

builder.Services.AddTransient<Func<IBoard, IGame>>(sp =>
{
    return (board) => ActivatorUtilities.CreateInstance<InMemoryGame>(sp, board);
});

builder.Services.AddSingleton<IGameFactory, GameFactory>();
var conectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");

builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseSqlServer(conectionString);
    options.LogTo(
       Console.WriteLine,
       new[] { DbLoggerCategory.Database.Command.Name },
       LogLevel.Information);

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
app.MapControllers();
app.Run();
