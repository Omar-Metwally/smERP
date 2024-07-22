namespace smERP.Application.DTOs.Image;

public class ImageProcessingInput
{
    public string FileName { get; set; }
    public string Type { get; set; }
    public string Path { get; set; }
    public Stream Content { get; set; }
}
