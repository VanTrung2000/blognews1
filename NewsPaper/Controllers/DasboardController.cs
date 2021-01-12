using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewsPaper.Helper;
using NewsPaper.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NewsPaper.Controllers
{
    public class DasboardController : Controller
    {
        private readonly ILogger<DasboardController> _logger;

        public DasboardController(ILogger<DasboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tables()
        {
            List<Category> categories = new List<Category>();
            categories = Helper.ApiHelper<List<Category>>.HttpGetAsync("api/category/gets");
            return View(categories);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategory model)
        {
            if (ModelState.IsValid)
            {
                var result = new CreateCategoryResult();
                result = Helper.ApiHelper<CreateCategoryResult>.HttpPostAsync("api/category/create", "POST", model);
                if(result.CategoryId > 0)
                {
                    return RedirectToAction("tables");
                }
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Update()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Update(UpdateCategory model)
        {
            if (ModelState.IsValid)
            {
                var result = new UpdateCategoryResult();
                result = Helper.ApiHelper<UpdateCategoryResult>.HttpPostAsync("api/category/update", "PUT", model);
                    if (result.CategoryId == 0)
                    {
                    return RedirectToAction("tables");
                }
                ModelState.AddModelError("", result.Message);
                return View(model);
            }
            return View(model);
        }


    }
}
