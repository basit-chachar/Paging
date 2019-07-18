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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">current page number e.g. 1, 2, 3,...</param>
        /// <param name="s">page size, total displayed items in one page. e.g. 10</param>
        /// <remarks>
        ///     p and s are default parameter names in LazZiya.TagHelpers.PagingTagHelpers
        ///     if you are using different paramter names insterad of p and s then you have to
        ///     define them in the paging tag helper as described here:
        ///     http://demo.ziyad.info/en/paging#query-string-keys
        /// </remarks>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index(int p=1, int s = 10)
        {
            // PagingTagHelper will use totalRecords to calculate total pages required
            var totalRecords = _context.Questions.Count();
            var questions = new List<Questions>();

            if (totalRecords > 0)
            {
                questions =
                    _context.Questions

                    // we make sure to order the collection before paging
                    .OrderByDescending(x => x.Question)

                    // skip previously listed items
                    .Skip((p - 1) * s)

                    // take only defined page size
                    .Take(s)

                    // call ToList method to fetch the query and get the results
                    .ToList();
            }

            // create the model to be returned
            var model = new QuestionsList
            {
                PageNo = p,
                PageSize = s,
                TotalRecords = totalRecords,
                Items = questions
            };

            return View(model);
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
            if (ModelState.IsValid)
            {
                _context.Questions.Add(questions);
                _context.SaveChanges();
            }

            /* 
             * uncomment below lines to input sample data to questions table */

            var questionsList = new List<Questions>();
            for(int i = 0; i <= 150; i++)
            {
                questionsList.Add(new Questions
                {
                    Question = $"Question no. {i}",
                    Answer = $"Answer no. {i}"
                });
            }

            _context.AddRange(questionsList);
            _context.SaveChanges();
            

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
