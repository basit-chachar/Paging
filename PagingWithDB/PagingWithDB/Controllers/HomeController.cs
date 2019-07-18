using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PagingWithDB.Data;
using PagingWithDB.Models;

namespace PagingWithDB.Controllers
{
    public class HomeController : Controller
    {

        public readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {

            return View(_context.Questions.OrderByDescending(x => x.Question));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AddData()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddData(Questions questions)
        {
            if(ModelState.IsValid)
            {
                _context.Questions.Add(questions);
                _context.SaveChanges();
                return RedirectToAction("AddData");
            }
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
