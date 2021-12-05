using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GeekBrains.Lesson8.Models;
using GeekBrains.Lesson8.Data;
using Autofac;

namespace GeekBrains.Lesson8.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            
            var builder = new ContainerBuilder();
            builder.RegisterType<Repository>().As<IRepository>();
            IContainer container = builder.Build();
            IRepository service = container.Resolve<IRepository>();

            var Result = await service.GetStudents();
            var ResultMapped = Mapping.Mapper.Map<List<ListOfStudent>>(Result);
            return View(ResultMapped);
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
