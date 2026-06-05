using Microsoft.AspNetCore.Components.Forms;

namespace SunsetSchedule.Services;

public class ImageService
{
    private readonly IWebHostEnvironment _env;

    public ImageService(IWebHostEnvironment env)
    {
        _env = env;
    }

    public async Task<string> SaveImageAsync(IBrowserFile file, string folder)
    {
        var uploadsPath = Path.Combine(_env.WebRootPath, folder.Replace("wwwroot/", ""));

        if (!Directory.Exists(uploadsPath))
            Directory.CreateDirectory(uploadsPath);

        var fileName = $"{Guid.NewGuid()}_{file.Name}";
        var filePath = Path.Combine(uploadsPath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.OpenReadStream(10 * 1024 * 1024).CopyToAsync(stream);

        return $"/images/uploads/{fileName}";
    }
}