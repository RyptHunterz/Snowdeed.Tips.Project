namespace Snowdeed.Tips.MinimalApis;

public class Article
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required double Price { get; set; }
}