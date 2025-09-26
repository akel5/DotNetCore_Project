using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkelTestingTool.Data;
using AkelTestingTool.Models;

namespace AkelTestingTool.Controllers
{
    public class TestExcutionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static int i5;

        public TestExcutionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestExcutions
        public async Task<IActionResult> Index(int? id)
        {
            int k = 0;
            List<StatusTE> StatusList2 = _context.StatusTE.ToList();
            ViewBag.StatusList2 = new SelectList(StatusList2, "STTEID", "Name");

            List<AssignedTo> StatusListAssignedTo = _context.AssignedTo.ToList();
            ViewBag.StatusListAssignedTo= new SelectList(StatusListAssignedTo, "AssignedToID", "Name");

            ViewBag.id18 = id;

            if (ViewBag.id18 != null)
            {
                i5 = ViewBag.id18;
            }






            var applicationDbContext = new object();



            //Back Button
            var ProjectTestss = _context.ProjectTests.FromSql($"SELECT * FROM ProjectTests Where TID='{id}'", id).ToList();
            foreach (var tes in ProjectTestss)
            {
                k = tes.ProjectsPId;
            }
            ViewBag.id171 = k;
            //Back Button



            if (id > 0)
            {
                applicationDbContext = _context.TestExcutions
                    .Include(n => n.ProjectTests)

                    .Where(n => n.ProjectTestsTId == id)
                    .OrderBy(n => n.PPublicationDate);
            }
            else
            {
                applicationDbContext = _context.TestExcutions.Include(p => p.ProjectTests);
            }



            return View(await ((IQueryable<TestExcutions>)applicationDbContext).ToListAsync());

            //var applicationDbContext = _context.Posts.Include(p => p.Category).Include(p => p.User);
            //return View(await applicationDbContext.ToListAsync());
        }

        public class ExeStatus
        {
            public int ExeId { get; set; }
            public int StatusId { get; set; }
        }

        // POST: BugsSummaries/UpdateExeStatus
        [HttpPost]
        public async Task<IActionResult> UpdateExeStatus([FromBody]ExeStatus exeStatus)
        {
            int id = exeStatus.ExeId;

            var testExcutions1 = await _context.TestExcutions.SingleOrDefaultAsync(m => m.TEId == id);
            if (testExcutions1 == null)
            {
                return NotFound();
            }



            if (exeStatus.ExeId != testExcutions1.TEId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    testExcutions1.StatusSTTEID = exeStatus.StatusId;
                    _context.Update(testExcutions1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExcutionsExists(testExcutions1.TEId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok();
        }



        //UpdateExeAssignedTo



        public class ExeAssigned
        {
            public int ExeId { get; set; }
            public int AssignedId { get; set; }
        }

        // POST: BugsSummaries/UpdateExeStatus
        [HttpPost]
        public async Task<IActionResult> UpdateExeAssignedTo([FromBody]ExeAssigned exeAssigned)
        {
            int id = exeAssigned.ExeId;

            var testExcutions1 = await _context.TestExcutions.SingleOrDefaultAsync(m => m.TEId == id);
            if (testExcutions1 == null)
            {
                return NotFound();
            }



            if (exeAssigned.ExeId != testExcutions1.TEId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    testExcutions1.AssignedToAssignedToID = exeAssigned.AssignedId;
                    _context.Update(testExcutions1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExcutionsExists(testExcutions1.TEId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return Ok();
        }












        // GET: TestExcutions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.id19 = i5;

            if (id == null)
            {
                return NotFound();
            }

            var testExcutions = await _context.TestExcutions
                .Include(t => t.ProjectTests)
                .SingleOrDefaultAsync(m => m.TEId == id);
            if (testExcutions == null)
            {
                return NotFound();
            }

            return View(testExcutions);
        }

        // GET: TestExcutions/Create
        public IActionResult Create(int? id)
        {
            List<AssignedTo> AssignedToList = _context.AssignedTo.ToList();
            ViewBag.AssignedTo22 = new SelectList(AssignedToList, "AssignedToID", "Name");

            ViewBag.id21 = id;
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id), "TId", "Test");
            return View();
        }

        // POST: TestExcutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,[Bind("TEId,ProjectTestsTId,PPublicationDate,AssignedToAssignedToID")] TestExcutions testExcutions)
        {
            if (ModelState.IsValid)
            {
                testExcutions.StatusSTTEID = 1; //
                _context.Add(testExcutions);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index/" + id);
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id), "TId", "Test", testExcutions.ProjectTestsTId);
            return View(testExcutions);
        }

        // GET: TestExcutions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.id20 = i5;
            if (id == null)
            {
                return NotFound();
            }

            var testExcutions = await _context.TestExcutions.SingleOrDefaultAsync(m => m.TEId == id);
            if (testExcutions == null)
            {
                return NotFound();
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests, "TId", "Test", testExcutions.ProjectTestsTId);
            return View(testExcutions);
        }

        // POST: TestExcutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TEId,ProjectTestsTId,PPublicationDate")] TestExcutions testExcutions)
        {                                   //TEId,ProjectTestsTId,PPublicationDate
            if (id != testExcutions.TEId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testExcutions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestExcutionsExists(testExcutions.TEId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               // return RedirectToAction(nameof(Index));
                return RedirectToAction("Index/" + i5);
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests, "TId", "Test", testExcutions.ProjectTestsTId);
            return View(testExcutions);
        }

        


            // GET: TestExcutions/Delete/5
            public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.id23 = i5;
            if (id == null)
            {
                return NotFound();
            }

            var testExcutions = await _context.TestExcutions
                .Include(t => t.ProjectTests)
                .SingleOrDefaultAsync(m => m.TEId == id);
            if (testExcutions == null)
            {
                return NotFound();
            }

            return View(testExcutions);
        }

        // POST: TestExcutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testExcutions = await _context.TestExcutions.SingleOrDefaultAsync(m => m.TEId == id);

            _context.TestExcutions.Remove(testExcutions);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index/" + i5);
        }



        private bool TestExcutionsExists(int id)
        {
            return _context.TestExcutions.Any(e => e.TEId == id);
        }
    }
}
