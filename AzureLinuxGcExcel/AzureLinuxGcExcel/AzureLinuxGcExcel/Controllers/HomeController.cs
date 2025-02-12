﻿using AzureLinuxGcExcel.Library;
using AzureLinuxGcExcel.Models;
using AzureLinuxGcExcel.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AzureLinuxGcExcel.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private string OutFilePath = Path.Combine("Files", "Output", "GcExcel");

        public HomeController(
            ILogger<HomeController> logger
            )
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IndexViewModel vm = new IndexViewModel();

            return View(vm);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            if (files == null)
            {
                return BadRequest("Cannot Read files");
            }

            IndexViewModel vm = new IndexViewModel();

            foreach (var file in files)
            {

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", file.FileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }


                MyGcExcel gcExcel = new MyGcExcel(filePath);
                MySSG ssg = new MySSG(filePath);
                MyAspose aspose = new MyAspose(filePath);

                object[,] chartData = new object[,]
                {
                    { "Library","GcExcel","SSG","Aspose" },
                    { nameof(MyGcExcel.OpenTime), gcExcel.OpenTime,ssg.OpenTime,aspose.OpenTime },
                    { nameof(MyGcExcel.SaveTime), gcExcel.SaveTime,ssg.SaveTime,aspose.SaveTime },
                    { nameof(MyGcExcel.CalcTime), gcExcel.CalcTime,ssg.CalcTime,aspose.CalcTime },
                    { nameof(MyGcExcel.MemSizeAfterOpen), gcExcel.MemSizeAfterOpen,ssg.MemSizeAfterOpen,aspose.MemSizeAfterOpen },
                    { nameof(MyGcExcel.MemSizeAfterCalc), gcExcel.MemSizeAfterCalc,ssg.MemSizeAfterCalc,aspose.MemSizeAfterCalc }
                };

                vm.ChartDatas.Add(new IndexViewModel._ChartData(file.FileName, chartData));

            }


            return View(vm);
        }

        public IActionResult Aspose()
        {

            AsposeViewModel vm = new AsposeViewModel();

            return View(vm);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Aspose(List<IFormFile> files)
        {
            if (files == null)
            {
                return BadRequest("Cannot Read files");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", files[0].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await files[0].CopyToAsync(stream);
            }

            AsposeViewModel vm = new AsposeViewModel(filePath);

            return View(vm);
        }

        public IActionResult GrapeCity()
        {
            GcExcelViewModel vm = new GcExcelViewModel();

            return View(vm);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> GrapeCity(List<IFormFile> files)
        {
            if (files == null)
            {
                return BadRequest("Cannot Read files");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", files[0].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await files[0].CopyToAsync(stream);
            }

            GcExcelViewModel vm = new GcExcelViewModel(filePath);

            return View(vm);
        }

        public IActionResult SpreadsheetGear()
        {
            SSGViewModel vm = new SSGViewModel();

            return View(vm);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> SpreadsheetGear(List<IFormFile> files)
        {
            if (files == null)
            {
                return BadRequest("Cannot Read files");
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", files[0].FileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await files[0].CopyToAsync(stream);
            }

            SSGViewModel vm = new SSGViewModel(filePath);

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}