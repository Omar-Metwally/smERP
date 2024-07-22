namespace smERP.Application.DTOs.Rquests.Image;

public class ImagePersistenceData
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public MemoryStream Content { get; set; }
}
