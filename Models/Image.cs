namespace portfolio_backend;
public class Image
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public byte[]? Data { get; set; }
    public string? ContentType { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
}