using DemoApplication.Areas.Admin.ViewModels.Book.Add;
using DemoApplication.Areas.Admin.ViewModels.Slider;
using DemoApplication.Areas.Admin.ViewModels.User;
using DemoApplication.Contracts.FIle;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;


        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-slider-list")]

        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders
                .Select(u => new ListSliderViewModel(
                  u.Id, u.MainTitle, u.Content, u.ButtonName, u.ButtonRedirectUrl, u.Order, u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        [HttpGet("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync()
        {
         
            return View();
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> AddAsync(AddSliderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Slider);

           await AddSlider(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-slider-list");


           async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var slider = new Slider
                {
                    MainTitle = model.MainTitle,
                    Content = model.Content,
                    ButtonName = model.ButtonName,
                    ButtonRedirectUrl = model.ButtonRedirectUrl,
                    Order = model.Order,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

               await  _dataContext.Sliders.AddAsync(slider);
               await  _dataContext.SaveChangesAsync();
            }
        }

        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }

            var model = new AddSliderViewModel
            {
                Id = slider.Id,
                MainTitle= slider.MainTitle,
                Content = slider.Content,
                ButtonName= slider.ButtonName,
                ButtonRedirectUrl= slider.ButtonRedirectUrl,
                Order=slider.Order,
                ImageUrl = _fileService.GetFileUrl(slider.ImageNameInFileSystem, UploadDirectory.Slider)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(AddSliderViewModel model)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (slider is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
             await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);
             var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Slider);
             await UpdateSliderAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateSliderAsync(slider.ImageName, slider.ImageNameInFileSystem);
            }


            return RedirectToRoute("admin-slider-list");


            async Task UpdateSliderAsync(string imageName, string imageNameInFileSystem)
            {
                slider.MainTitle = model.MainTitle;
                slider.Content = model.Content;
                slider.ButtonName = model.ButtonName;
                slider.ButtonRedirectUrl = model.ButtonRedirectUrl;
                slider.Order = model.Order; 
                slider.ImageName = imageName;
                slider.ImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);

             _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
    }
}
