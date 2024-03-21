using Microsoft.EntityFrameworkCore;
using Snowdeed.Tips.MinimalApis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ArticleDbContext>(opt => opt.UseInMemoryDatabase("ArticleList"));

var app = builder.Build();

app.MapGet("/articles", async (ArticleDbContext dbContext) =>
    await dbContext.Articles.ToListAsync());

app.MapGet("/articles/{id}", async (int id, ArticleDbContext dbContext) =>
    await dbContext.Articles.FindAsync(id) 
        is Article article
            ? Results.Ok(article)
            : Results.NotFound());

app.MapPost("/articles", async (Article article, ArticleDbContext dbContext) =>
{
    try
    {
        dbContext.Articles.Add(article);
        await dbContext.SaveChangesAsync();

        return Results.Created();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

app.MapPut("/articles/{id}", async (int id, Article articleUpdate, ArticleDbContext dbContext) =>
{
    Article? article = await dbContext.Articles.FindAsync(id);

    if (article is null) return Results.NotFound();

    article.Name = articleUpdate.Name;
    article.Price = articleUpdate.Price;

    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/articles/{id}", async (int id, ArticleDbContext dbContext) =>
{
    if(await dbContext.Articles.FindAsync(id) is Article article)
    {
        dbContext.Articles.Remove(article);
        await dbContext.SaveChangesAsync();

        return Results.NoContent();
    }

    return Results.NotFound();
});

app.Run();