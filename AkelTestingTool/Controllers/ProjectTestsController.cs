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

namespace AkelTestingTool.Controllers
{
    [Authorize(Roles = "Testers,Admins,Dev,DemoTester")]
    public class ProjectTestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public static int i3;

        public ProjectTestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTests
        public async Task<IActionResult> Index(int? id)
        {

            ViewBag.id10 = id;


            if (ViewBag.id10 != null)
            {
                i3 = ViewBag.id10;
            }


            var applicationDbContext = new object();
            /*   var Projectss = _context2.Projects.FromSql("SELECT * FROM Projects").ToList();
               StringBuilder sb = new StringBuilder();

               foreach (var pro in Projectss)
               {
                   sb.Append(pro.PName + "\n");

               }

               return ToString(sb);
               */

            if (id > 0)
            {
                applicationDbContext = _context.ProjectTests
                    .Include(n => n.Projects)
                   
                    .Where(n => n.ProjectsPId == id)
                    .OrderBy(n => n.PPublicationDate);
            }
            else
            {
                applicationDbContext = _context.ProjectTests.Include(p => p.Projects);
                
            }

            Dictionary<int, bool> hasTestCases = new Dictionary<int, bool>();

            foreach (var _cont in (IQueryable<ProjectTests>)applicationDbContext)
            {
                bool temp = true;
                var row3 = await _context.TestCases.FirstOrDefaultAsync(m => m.ProjectTestsTId == _cont.TId);
                if (row3 == default)
                    hasTestCases.Add(_cont.TId, false);
                else
                    hasTestCases.Add(_cont.TId, true);
                
            }

            ViewBag.id100 = hasTestCases;


            return View(await ((IQueryable<ProjectTests>)applicationDbContext).ToListAsync());






           // var applicationDbContext = _context.ProjectTests.Include(p => p.Projects);
           // return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProjectTests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.id12 = i3;

            if (id == null)
            {
                return NotFound();
            }

            var projectTests = await _context.ProjectTests
                .Include(p => p.Projects)
                .SingleOrDefaultAsync(m => m.TId == id);
            if (projectTests == null)
            {
                return NotFound();
            }

            return View(projectTests);
        }

        // GET: ProjectTests/Create
        public IActionResult Create(int? id)
        {
            ViewBag.id11 = id;

            ViewData["ProjectsPId"] = new SelectList(_context.Projects.Where(Test => Test.PId == id), "PId", "PName");
            return View();
        }

        // POST: ProjectTests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id,[Bind("TId,ProjectsPId,Test,TStatus,TestedBy")] ProjectTests projectTests)
        {

            projectTests.PPublicationDate = DateTime.Today.Date;
            
            if (ModelState.IsValid)
            {
                _context.Add(projectTests);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index/" + id );
            }
            
            ViewData["ProjectsPId"] = new SelectList(_context.Projects.Where(Test => Test.PId == id), "PId", "PName", projectTests.ProjectsPId);
            return View(projectTests);
        }

        // GET: ProjectTests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.id13 = i3;

            if (id == null)
            {
                return NotFound();
            }

            var projectTests = await _context.ProjectTests.SingleOrDefaultAsync(m => m.TId == id);
            if (projectTests == null)
            {
                return NotFound();
            }
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName", projectTests.ProjectsPId);
            return View(projectTests);
        }

        // POST: ProjectTests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TId,ProjectsPId,Test,TStatus,TestedBy")] ProjectTests projectTests)
        {
            if (id != projectTests.TId)
            {
                return NotFound();
            }

            projectTests.PPublicationDate = DateTime.Today.Date;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTests);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTestsExists(projectTests.TId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
              //  return RedirectToAction(nameof(Index));
                return RedirectToAction("Index/" + i3);
            }
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName", projectTests.ProjectsPId);
            return View(projectTests);
        }

        // GET: ProjectTests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.id130 = i3;
            if (id == null)
            {
                return NotFound();
            }

            var projectTests = await _context.ProjectTests
                .Include(p => p.Projects)
                .SingleOrDefaultAsync(m => m.TId == id);
            if (projectTests == null)
            {
                return NotFound();
            }

            return View(projectTests);
        }

        // POST: ProjectTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTests = await _context.ProjectTests.SingleOrDefaultAsync(m => m.TId == id);
            _context.ProjectTests.Remove(projectTests);
            await _context.SaveChangesAsync();
           // return RedirectToAction(nameof(Index));
            return RedirectToAction("Index/" + i3);
        }

        private bool ProjectTestsExists(int id)
        {
            return _context.ProjectTests.Any(e => e.TId == id);
        }
    }
}
