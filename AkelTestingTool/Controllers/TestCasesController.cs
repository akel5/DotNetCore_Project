using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkelTestingTool.Data;
using AkelTestingTool.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace AkelTestingTool.Controllers
{
    [Authorize(Roles = "Testers,Admins,Dev,DemoTester")]
    public class TestCasesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static int i4;

        public TestCasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestCases
        public async Task<IActionResult> Index(int? id, bool canDelete = true)
        {
            if (!canDelete)
            {
                ViewBag.iftestexeex = 1;
            }

            //ViewBag.iftestexeexstr = "Do you want to delete this test case ?";
            int k = 0;
            ViewBag.id14 = id;

            if (ViewBag.id14 != null)
            {
                i4 = ViewBag.id14;
            }

            var applicationDbContext = new object();




            //Back Button
            var ProjectTestss = _context.ProjectTests.FromSql($"SELECT * FROM ProjectTests Where TID='{id}'", id).ToList();
            foreach (var tes in ProjectTestss)
            {
                k = tes.ProjectsPId;
            }
            ViewBag.id170 = k;
            //Back Button


            





            if (id > 0)
            {
                applicationDbContext = _context.TestCases
                    .Include(n => n.ProjectTests)
                   
                    .Where(n => n.ProjectTestsTId == id)
                    .OrderBy(n => n.TestCaseNum);
            }
            else
            {
                applicationDbContext = _context.TestCases.Include(p => p.ProjectTests);
            }

            

            return View(await ((IQueryable<TestCases>)applicationDbContext).ToListAsync());

            //var applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.User);
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: TestCases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.id15 = i4;

            if (id == null)
            {
                return NotFound();
            }

            var testCases = await _context.TestCases
                .Include(t => t.ProjectTests)
                .SingleOrDefaultAsync(m => m.TCId == id);
            if (testCases == null)
            {
                return NotFound();
            }

            return View(testCases);
        }

        // GET: TestCases/Create
        public IActionResult Create(int? id)
        {
            ViewBag.id17 = id;
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id), "TId", "Test");
            return View();
        }

        // POST: TestCases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("TCId,ProjectTestsTId,TestCaseNum,TestCase,ExpectedResult")] TestCases testCases)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testCases);
                await _context.SaveChangesAsync();
                var ProjTestId = testCases.ProjectTestsTId;
                return RedirectToAction("Index", "TestCases", new { id = ProjTestId });


                //var productId = <code to get your product id>
                //return RedirectToAction("Details", "controller1", new { id = productId });


                // ("profile", "person", new { personID = Person.personID });
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id), "TId", "Test", testCases.ProjectTestsTId);
            return View(testCases);
        }

        // GET: TestCases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.id16 = i4;
            if (id == null)
            {
                return NotFound();
            }

            var testCases = await _context.TestCases.SingleOrDefaultAsync(m => m.TCId == id);
            if (testCases == null)
            {
                return NotFound();
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests, "TId", "TStatus", testCases.ProjectTestsTId);
            return View(testCases);
        }

        // POST: TestCases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TCId,ProjectTestsTId,TestCaseNum,TestCase,ExpectedResult")] TestCases testCases)
        {
            if (id != testCases.TCId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testCases);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestCasesExists(testCases.TCId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index/" + i4);
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests, "TId", "TStatus", testCases.ProjectTestsTId);
            return View(testCases);
        }

        // GET: TestCases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            int k = 0;
            



            //if test case exist in executions
            var testCases2 = await _context.TestCases
                .Include(t => t.ProjectTests)
                .SingleOrDefaultAsync(m => m.TCId == id);

            var TestsExeResultss = _context.TestsExeResults.ToList();
            


            foreach (var _cont2 in TestsExeResultss)
            {
                if(_cont2.TestCases != null)
                if (_cont2.TestCases.TestCase== testCases2.TestCase) { k = 1; }
            }
            ViewBag.iftestexeex = k;
            //if test case exist in executions

            if(k==1)
            {
                return RedirectToAction("Index/" + i4, new { canDelete= false });
            }




            ViewBag.id160 = i4;
            if (id == null)
            {
                return NotFound();
            }

            var testCases = await _context.TestCases
                .Include(t => t.ProjectTests)
                .SingleOrDefaultAsync(m => m.TCId == id);
            if (testCases == null)
            {
                return NotFound();
            }

            return View(testCases);
            
        }

        // POST: TestCases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testCases = await _context.TestCases.SingleOrDefaultAsync(m => m.TCId == id);
            _context.TestCases.Remove(testCases);
            await _context.SaveChangesAsync();
           // return RedirectToAction(nameof(Index));
            return RedirectToAction("Index/" + i4);
        }

        private bool TestCasesExists(int id)
        {
            return _context.TestCases.Any(e => e.TCId == id);
        }
    }
}
