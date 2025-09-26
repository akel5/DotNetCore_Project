using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AkelTestingTool.Data;
using AkelTestingTool.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using TestingCoreIdentity.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Text.RegularExpressions;
using Hangfire;
using System.Data.Common;
using System.Collections.Generic;

namespace AkelTestingTool.Controllers
{
  
    [Authorize(Roles = "Testers,Admins,Dev,DemoTester")]
    public class BugsSummariesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _context2;
        private IHostingEnvironment _environment;
        private readonly UserManager<Models.ApplicationUser> _userManager;
       

        public BugsSummariesController(ApplicationDbContext context, IHostingEnvironment environment, UserManager<Models.ApplicationUser> userManager, ApplicationDbContext context2)
        {
            _context = context;
            _context2 = context2;
            _environment = environment;
            _userManager = userManager;
        }
        
        // GET: BugsSummaries
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {

            
                List<Status> StatusList= _context.Status.ToList(); //StatusList
            ViewBag.StatusList = new SelectList(StatusList, "STID", "Name");
            

            var bugs = from b in _context.BugsSummary.Where(b => !b.Status.Name.Contains("Archived"))
                    .Include(b => b.Projects).Include(b => b.Status)
                    
                       select b;
            {


                if (!String.IsNullOrEmpty(searchString))
                {
                    bugs = bugs.Where(b => b.Projects.PName.Contains(searchString)
                                           || b.Bug.Contains(searchString));
                }

                switch (sortOrder)
                {


                    case "name_desc":
                        bugs = bugs.OrderByDescending(b => b.Projects.PName);
                        break;
                    case "Date":
                        bugs = bugs.OrderBy(b => b.PublicationDate);
                        break;
                    case "date_desc":
                        bugs = bugs.OrderByDescending(b => b.PublicationDate);
                        break;
                    default:
                        bugs = bugs.OrderBy(b => b.Projects.PName);
                        break;

                }


                if (id > 0)
                {
                    bugs=bugs

                        .Where(b => b.ProjectsPId == id)
                        ;
                }
                else
                {
                    bugs = bugs.Include(b => b.User).Include(b => b.Projects);
                }



                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["CurrentFilter"] = searchString;
            }




                return View(await bugs.AsNoTracking().ToListAsync());

            




        }



        // GET: BugsSummaries
        public async Task<IActionResult> Index2(int? id, string sortOrder, string searchString)
        {

            //AJAX
            List<Status> StatusList = _context.Status.ToList();
            ViewBag.StatusList = new SelectList(StatusList, "STID", "Name");


            var bugs = from b in _context.BugsSummary.Where(b => b.Status.Name.Contains("Archived"))
                    .Include(b => b.Projects).Include(b => b.Status)
                       select b;
            {


                    if (!String.IsNullOrEmpty(searchString))
                {
                    bugs = bugs.Where(b => b.Projects.PName.Contains(searchString)
                                           || b.Bug.Contains(searchString));
                }

                switch (sortOrder)
                {


                    case "name_desc":
                        bugs = bugs.OrderByDescending(b => b.Projects.PName);
                        break;
                    case "Date":
                        bugs = bugs.OrderBy(b => b.PublicationDate);
                        break;
                    case "date_desc":
                        bugs = bugs.OrderByDescending(b => b.PublicationDate);
                        break;
                    default:
                        bugs = bugs.OrderBy(b => b.Projects.PName);
                        break;

                }


                if (id > 0)
                {
                    bugs = bugs

                        .Where(b => b.ProjectsPId == id)
                        ;
                }
                else
                {
                    bugs = bugs.Include(b => b.User).Include(b => b.Projects);
                }

                ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
                ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewData["CurrentFilter"] = searchString;
            }


            return View(await bugs.AsNoTracking().ToListAsync());
        }

       

        /* private void FillStatus(int statId)
         {
             ViewBag.StatusList = _context.Status
                                 .Where(g => g.STID == statId)
                                 .Select(f => new SelectListItem()
                                 {
                                     Value = f.STID.ToString(),
                                     Text = f.Name
                                 })
                                 .ToList();
         }*/

        // GET: BugsSummaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bugssummary = await _context.BugsSummary
                .Include(p => p.Projects)
                .Include(p => p.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bugssummary == null)
            {
                return NotFound();
            }

            // Execute Stored procedure in the database (Delete, Update)
            var con = _context.Database.GetDbConnection();
            con.Open();
            var comm = con.CreateCommand();
            comm.CommandText = "IncreaseReaders"; //Stored procedure name
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.Parameters.Add(new SqlParameter("Id", id));
            comm.ExecuteNonQuery();
            //End of SP code 

            
            return View(bugssummary);
        }

        // GET: BugsSummaries/Create
        public IActionResult Create()
        {
            List<Status> StatusList = _context.Status.ToList();
            ViewBag.StatusList2 = new SelectList(StatusList, "STID", "Name");

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName");
            ViewData["StatusSTID"] = new SelectList(_context.Status, "STID", "Name");

            return View();
        }

        // POST: BugsSummaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectsPId,Bug,BugSummary,TesterName,ImageUrl,StatusSTID")] BugsSummary bugsSummary, IFormFile myfile) 
        {
            string PPNme, HtPPNme;
            
            if (ModelState.IsValid)
            {

                bugsSummary.ImageUrl = await UserFile.UploadeNewImageAsync(bugsSummary.ImageUrl,
              myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

                bugsSummary.PublicationDate = DateTime.Today.Date;
                bugsSummary.UserId = _userManager.GetUserId(User);

                _context.Add(bugsSummary);
                await _context.SaveChangesAsync();


         //---------------------------------------------------------------------------------------
                DbConnection con = _context2.Database.GetDbConnection();
                con.Open();
                DbCommand com = con.CreateCommand();
                com.CommandText = "SELECT PName FROM Projects WHERE PId=" + "'" + bugsSummary.ProjectsPId + "'";
                com.CommandType = System.Data.CommandType.Text;
                DbDataReader dr = com.ExecuteReader();
                StringBuilder data = new StringBuilder();

                while (dr.Read())
                {
                    data.AppendLine(dr.GetString(0));
                }

                PPNme =  data.ToString() ;
                HtPPNme = "<b>" + data.ToString() + "- </b>";

         //-------------------------------------------------------------------------








                BackgroundJob.Schedule(() => SendEmail(PPNme, HtPPNme), TimeSpan.FromMinutes(1));

                return RedirectToAction(nameof(Index));
            }
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName", bugsSummary.ProjectsPId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bugsSummary.UserId);
            ViewData["StatusSTID"] = new SelectList(_context.Status, "STID", "Name", bugsSummary.StatusSTID);


            return View(bugsSummary); 
        }


        public void SendEmail(string Prname, string Prname2)

        {
            string htmlContent = "Akel found a new Bug related to "+ Prname2+" project please login the testing system and check it.";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("akel.obied@outlook.com", "Akel -" + Prname+" Tests");
            var to = new EmailAddress("akel.obied@innovisec.com");
            var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, "Bug Added", plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);


        }

        // GET: BugsSummaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var bugsSummary = await _context.BugsSummary.SingleOrDefaultAsync(m => m.Id == id);
            List<Status>  StatusList = _context.Status.ToList();
            

            if (id == null)
            {
                return NotFound();
            }

         
            if (bugsSummary == null)
            {
                return NotFound();
            }
            ViewData["StatusList"] = new SelectList(_context.Status, "STID", "Name", bugsSummary.StatusSTID);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bugsSummary.UserId);
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName", bugsSummary.ProjectsPId);

           


            return View(bugsSummary);
        }

        // POST: BugsSummaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bug,BugSummary,ImageUrl,ProjectsPId,TesterName,StatusSTID")]  BugsSummary bugsSummary, IFormFile myfile)
        {
            if (id != bugsSummary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    
              bugsSummary.ImageUrl = await UserFile.UploadeNewImageAsync(bugsSummary.ImageUrl,
              myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

                    bugsSummary.PublicationDate = DateTime.Today.Date;
                    bugsSummary.UserId = _userManager.GetUserId(User);


                    _context.Update(bugsSummary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugsSummaryExists(bugsSummary.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bugsSummary.UserId);
            ViewData["ProjectsPId"] = new SelectList(_context.Projects, "PId", "PName", bugsSummary.ProjectsPId);

            return View(bugsSummary);
        }

        public class ResultCH
        {
            public int ExeRId2 { get; set; }
            public string Result2 { get; set; }

        }


        [HttpPost]
        public async Task<IActionResult> ResultChanged([FromBody]ResultCH Result3)
        {

            int id = Result3.ExeRId2;

            var bs = await _context.BugsSummary.SingleOrDefaultAsync(m => m.Id == id);
            if (bs == null)
            {
                return NotFound();
            }



            if (Result3.ExeRId2 != bs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    bs.BugSummary = Result3.Result2;
                    _context.Update(bs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugsSummaryExists(bs.Id))
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

        public class BugStatus
        {
            public int BugId { get; set; }
            public int StatusId { get; set; }
        }

        // POST: BugsSummaries/UpdateBugStatus
        [HttpPost]
        public async Task<IActionResult> UpdateBugStatus([FromBody]BugStatus bugStatus)
        {
            int id = bugStatus.BugId;

            var bugsSummary1 = await _context.BugsSummary.SingleOrDefaultAsync(m => m.Id == id);
            if (bugsSummary1 == null)
            {
                return NotFound();
            }

           

            if (bugStatus.BugId != bugsSummary1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    bugsSummary1.StatusSTID = bugStatus.StatusId;
                    _context.Update(bugsSummary1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugsSummaryExists(bugsSummary1.Id))
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


            //Add To Archive

            // GET: BugsSummaries/MoveToArchive/5
            public  IActionResult MoveToArchive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bugsSummary = new BugsSummary { Id = id ?? 1 };
            
            //bugsSummary.Status.Name = "Archived";
            //bugsSummary.Status.STID = 3;
            bugsSummary.StatusSTID = 3;

            //SqlCommand command = new SqlCommand($"UPDATE BugsSummary SET StatusSTID = 3 WHERE Id = {Request.Query["id"]}", connection);
           // SqlCommand command = new SqlCommand($"UPDATE BugsSummary SET StatusSTID = 3 WHERE Id ="+ id);
           // command.();

            _context.Entry(bugsSummary).Property("StatusSTID").IsModified = true;
            _context.SaveChanges();


            return Redirect("/BugsSummaries");//(bugsSummary);
        }

        









        // GET: BugsSummaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            
            

            if (id == null)
            {
                return NotFound();
            }

            var bugsSummary = await _context.BugsSummary
                .Include(b => b.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bugsSummary == null)
            {
                return NotFound();
            }
            var projects2 = await _context.Projects.SingleOrDefaultAsync(m => m.PId == bugsSummary.ProjectsPId);
            ViewBag.project = projects2.PName;

            var Status2 = await _context.Status.SingleOrDefaultAsync(m => m.STID == bugsSummary.StatusSTID);
            ViewBag.Status2 = Status2.Name;

            return View(bugsSummary);
        }

        // POST: BugsSummaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bugsSummary = await _context.BugsSummary.SingleOrDefaultAsync(m => m.Id == id);
            _context.BugsSummary.Remove(bugsSummary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugsSummaryExists(int id)
        {
            return _context.BugsSummary.Any(e => e.Id == id);
        }
    }
}
