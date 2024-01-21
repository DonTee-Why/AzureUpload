using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureUpload.Models;
using Azure.Storage.Blobs;

namespace AzureUpload.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel fileUploadViewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await UploadFile(fileUploadViewModel.File, fileUploadViewModel.File.FileName);
            }
            catch(Exception e){
                TempData["Admin Error"] = $"An error occured: {e.Message}. Please try again.";
                return View();
            }
        }
        return View("Index", fileUploadViewModel);
    }

    private async Task UploadFile(IFormFile file, string filename)
    {
        string? connectionString = _configuration.GetValue<string>("BlobConnectionString");
        string? containerName = _configuration.GetValue<string>("BlobContainerName");
        BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString, containerName);

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            stream.Position = 0;
            await blobContainerClient.UploadBlobAsync($"azure-upload/{filename}", stream);
        }
    }
}
