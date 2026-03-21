using Microsoft.AspNetCore.Mvc;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/upload/kyc")]
public class KYCUploadController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<KYCUploadController> _logger;

    private static readonly HashSet<string> AllowedFolders =
    [
        "selfie", "dlfront", "dlback",
        "passportfront", "passportback",
        "otherfront", "otherback",
        "tinfront", "tinback",
        "sssfront", "sssback",
        "gsisfront", "gsisback"
    ];

    private static readonly HashSet<string> AllowedExtensions =
    [
        ".jpg", ".jpeg", ".png", ".webp"
    ];

    public KYCUploadController(IWebHostEnvironment env, ILogger<KYCUploadController> logger)
    {
        _env = env;
        _logger = logger;
    }

    // POST api/upload/kyc/{folder}/{customerId}
    // Returns: relative URL string e.g. "/uploads/kyc/selfie/12345_abc123.jpg"
    [HttpPost("{folder}/{customerId:long}")]
    [DisableRequestSizeLimit]
    [RequestFormLimits(MultipartBodyLengthLimit = 5_242_880)]
    public async Task<IActionResult> Upload(
        string folder,
        long customerId,
        IFormFile file)
    {
        // ── Validate folder ──────────────────────────────────────────
        if (!AllowedFolders.Contains(folder.ToLower()))
            return BadRequest($"Invalid folder '{folder}'.");

        // ── Validate file ────────────────────────────────────────────
        if (file == null || file.Length == 0)
            return BadRequest("No file received.");

        if (file.Length > 5 * 1024 * 1024)
            return BadRequest("File exceeds 5 MB limit.");

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(ext))
            return BadRequest($"File type '{ext}' not allowed. Use JPG, PNG, or WebP.");

        // ── Build target path ────────────────────────────────────────
        // wwwroot/uploads/kyc/{folder}/{customerId}_{guid}{ext}
        var targetFolder = Path.Combine(
            _env.WebRootPath, "uploads", "kyc", folder.ToLower());

        Directory.CreateDirectory(targetFolder); // safe if already exists

        var fileName = $"{customerId}_{Guid.NewGuid():N}{ext}";
        var filePath = Path.Combine(targetFolder, fileName);
        var relativeUrl = $"/uploads/kyc/{folder.ToLower()}/{fileName}";

        // ── Delete previous file for this customer in this folder ────
        // Keeps wwwroot clean — only one selfie / DL front etc. per customer
        foreach (var old in Directory.GetFiles(targetFolder, $"{customerId}_*"))
        {
            try { System.IO.File.Delete(old); }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not delete old KYC file: {Path}", old);
            }
        }

        // ── Save file ────────────────────────────────────────────────
        await using var stream = System.IO.File.Create(filePath);
        await file.CopyToAsync(stream);

        _logger.LogInformation(
            "KYC image saved: customer={CustomerId} folder={Folder} file={File}",
            customerId, folder, fileName);

        return Ok(relativeUrl);
    }
}