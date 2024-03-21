using Microsoft.EntityFrameworkCore;

namespace Snowdeed.Tips.MinimalApis;

public class ArticleDbContext : DbContext
{
    public ArticleDbContext(DbContextOptions<ArticleDbContext> options) : base(options) { }

    public DbSet<Article> Articles { get; set; }
}