﻿using DemoApplication.Areas.Client.ViewModels.Home.Contact;
using DemoApplication.Areas.Client.ViewModels.Home.Index;
using DemoApplication.Contracts.FIle;
using DemoApplication.Database;
using DemoApplication.Database.Models;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dbContext;

        public HomeController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpGet("~/", Name = "client-home-index")]
        //[HttpGet("index")]
        //public async Task<IActionResult> IndexAsync([FromServices] IFileService fileService)
        //{
        //    var model = new IndexViewModel
        //    {
        //        Books = await _dbContext.Books
        //        .Select(b => new BookListItemViewModel(
        //            b.Id,
        //            b.Title,
        //            $"{b.Author.FirstName} {b.Author.LastName}",
        //            b.Price,


        //            fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Book)
        //            ))
        //        .ToListAsync(),

        //        Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderLIstItemViewModel(
        //            b.MainTitle,
        //            b.Content,
        //            b.ButtonName,
        //            fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Slider),
        //            b.ButtonRedirectUrl,
        //            b.Order

        //            ))
        //        .ToListAsync()
        //    };

        //    return View(model);
        //}

        [HttpGet("contact")]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public ActionResult Contact([FromForm] CreateViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _dbContext.Contacts.Add(new Contact
            {
                Name = contactViewModel.Name,
                Email = contactViewModel.Email,
                Message = contactViewModel.Message,
                Phone = contactViewModel.PhoneNumber,
            });

            return RedirectToAction(nameof(Index));
        }
    }
}
