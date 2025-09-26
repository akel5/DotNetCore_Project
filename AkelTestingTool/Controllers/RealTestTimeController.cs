using System;
using Microsoft.AspNetCore.Mvc;
using AkelTestingTool.Models;
using Omu.AwesomeMvc;
using System.Linq;
using AkelTestingTool.Data;
using Omu.Awem.Utils;

namespace AkelTestingTool.Controllers
{
    /*
        [Route("[controller]")]

        public class RealTestTimeController : Controller
        {
            InMemoryEmployeesDataContext _data;
            public RealTestTimeController(IHttpContextAccessor httpContextAccessor, IMemoryCache memoryCache)
            {
                _data = new InMemoryEmployeesDataContext(httpContextAccessor, memoryCache);
            }



            [HttpGet]
            public object Get(DataSourceLoadOptions loadOptions)
            {
                return DataSourceLoader.Load(_data.TestsExeResults, loadOptions);
            }


            [HttpPost]
            public IActionResult Post(string values)
            {
                var newTestsExeResult = new TestsExeResults();
                JsonConvert.PopulateObject(values, newTestsExeResult);

                if (!TryValidateModel(newTestsExeResult))
                    return BadRequest(ModelState.GetFullErrorMessage());

                _data.TestsExeResults.Add(newTestsExeResult);
                _data.SaveChanges();

                return Ok();
            }

            [HttpPut]
            public IActionResult Put(int key, string values)
            {
                var testsExeResult = _data.TestsExeResults.First(a => a.TERId2 == key);
                JsonConvert.PopulateObject(values, testsExeResult);

                if (!TryValidateModel(testsExeResult))
                    return BadRequest(ModelState.GetFullErrorMessage());

                _data.SaveChanges();

                return Ok();
            }

            [HttpDelete]
            public void Delete(int key)
            {
                var testsExeResult = _data.TestsExeResults.First(a => a.TERId2 == key);
                _data.TestsExeResults.Remove(testsExeResult);
                _data.SaveChanges();
            }
        }
    }




    /*  public async Task<IActionResult> Index(int? id, int? id2)
             {

                 ViewBag.id2 = id2;
                 ViewBag.id = id;
                 var applicationDbContext = new object();


                 if (id > 0)
                 {
                     applicationDbContext = _context.TestsExeResults
                         .Include(n => n.ProjectTests)
                         .Include(n => n.TestExcutions)
                         .Include(n => n.TestCases)
                         .Where(n => n.TestExcutionsTEId == id)
                         .OrderBy(n => n.PPublicationDate);


                 }
                 else
                 {
                     applicationDbContext = _context.TestsExeResults.Include(p => p.TestExcutions);
                 }


                 list = await ((IQueryable<TestsExeResults>)applicationDbContext).ToListAsync();
                 return View(await ((IQueryable<TestsExeResults>)applicationDbContext).ToListAsync());


             }


         */

    public class RealTestTimeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RealTestTimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConditionalDemo()
        {
            return View();
        }

        public ActionResult MultiEditorsDemo()
        {
            return View();
        }

        private object MapToGridModel(TestsExeResults ttt)
        {
            return new
            {
                ttt.TERId2,
                ttt.TestCases.TestCaseNum,
                ttt.TestCases.TestCase,
                ttt.Result,
                ttt.Status,
                Date = ttt.PPublicationDate.ToShortDateString(),

                // below properties used for inline editing only
                TestCaseIds = ttt.TestCases.TCId,
                TestExcutionIds = ttt.TestExcutions.TEId,
                BonusMealId = ttt.ProjectTests.TId,

                // for conditional demo
                Editable = ttt.TestCases.TestCase.Count() > 1,
                DateReadOnly = ttt.PPublicationDate.Date.Year < 2012
            };
        }



        public ActionResult GridGetItems(GridParams g, string search)
        {

            search = (search ?? "").ToLower();
            var items = _context.TestsExeResults.Where(o => o.TestCases.TestCase.ToLower().Contains(search)).AsQueryable();

            var model = new GridModelBuilder<TestsExeResults>(items, g)
            {
                Key = "Id", // needed for api select, update, tree, nesting, EF
                GetItem = () => Db.Get<TestsExeResults>(Convert.ToInt32(g.Key)), // called by the grid.api.update
                Map = MapToGridModel,
            }.Build();

            return Json(model);
        }

        [HttpPost]
        public ActionResult Create(TestsExeResults input)
        {
            if (ModelState.IsValid)
            {
                var TestsExeResult = new TestsExeResults
                {
                    TestCases = input.TestCases,

                    Result = input.Result,
                    Status = input.Status,
                    PPublicationDate = input.PPublicationDate
                };

                Db.Insert(TestsExeResult);

                return Json(new { Item = MapToGridModel(TestsExeResult) });
            }

            return Json(ModelState.GetErrorsInline());
        }

        [HttpPost]
        public ActionResult Edit(TestsExeResults input)
        {
            if (ModelState.IsValid)
            {
                var TestsExeResult = Db.Get<TestsExeResults>(input.TERId2);
                TestsExeResult.TestCases.TestCaseNum = input.TestCases.TestCaseNum;
                TestsExeResult.PPublicationDate = input.PPublicationDate.Date;
                TestsExeResult.TestCases.TestCase = input.TestCases.TestCase;
                TestsExeResult.Result = input.Result;
                TestsExeResult.Status = input.Status;

                Db.Update(TestsExeResult);

                return Json(new { });
            }

            return Json(ModelState.GetErrorsInline());
        }

        

       

        public ActionResult Popup()
        {
            return PartialView();
        }
    }
}