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

    public class TestsExeResultsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _context2;
        private readonly ApplicationDbContext _context3;
        private readonly UserManager<Models.ApplicationUser> _userManager;
        private IHostingEnvironment _environment;
        public static int i1, i2, i3;
        public static string PPname2;



        public TestsExeResultsController(ApplicationDbContext context, ApplicationDbContext context2, ApplicationDbContext context3, UserManager<Models.ApplicationUser> userManager, IHostingEnvironment environment)
        {
            _context = context;
            _context2 = context2;
            _context3 = context3;
            _environment = environment;
            _userManager = userManager;


        }

        // GET: TestsExeResults
        public async Task<IActionResult> Index( int? id, int? id2)
        {


            int k = 0;
            List<StatusTER> StatusList3 = _context3.StatusTER.ToList();
            ViewBag.StatusList3 = new SelectList(StatusList3, "STTERID", "Name");

            var row2 = await _context.TestsExeResults.FirstOrDefaultAsync(m => m.ProjectTestsTId == id2 && m.TestExcutionsTEId == id);
            if (row2 == null)
                UpdateTERtable(id, id2);

            


            ViewBag.id2 = id2;
            ViewBag.id = id;

            if (ViewBag.id != null)
            {
                i1 = ViewBag.id;
                i2 = ViewBag.id2;
            }





            //Back Button
            var TestExcutionss = _context.TestExcutions.FromSql($"SELECT * FROM TestExcutions Where TEID='{id}'", id).ToList();
            foreach (var tes in TestExcutionss)
            {
                k = tes.ProjectTestsTId;
            }
            ViewBag.id172 = k;
            //Back Button





            var applicationDbContext = new object();


            if (id > 0)
            {
                applicationDbContext = _context.TestsExeResults
                    .Include(n => n.ProjectTests)
                    .Include(n => n.TestExcutions)
                    .Include(n => n.TestCases)
                    .Where(n => n.TestExcutionsTEId == id)
                    .OrderBy(n => n.TestCases.TestCaseNum);


            }
            else
            {
                applicationDbContext = _context.TestsExeResults.Include(p => p.TestExcutions).OrderBy(n => n.TestCases.TestCaseNum);
            }



            return View(await ((IQueryable<TestsExeResults>)applicationDbContext).ToListAsync());

            //  var applicationDbContext = _context.TestsExeResults.Include(p => p.TestExcutions).Include(p => p.TestCases);
            // return View(await applicationDbContext.ToListAsync());
        }


        public class ExeRStatus
        {
            public int ExeRId { get; set; }
            public int StatusId { get; set; }
        }

        // POST: BugsSummaries/UpdateExeStatus
        [HttpPost]
        public async Task<IActionResult> UpdateExeRStatus([FromBody]ExeRStatus exerStatus)
        {
            int id = exerStatus.ExeRId;

            var testExcutionsRes1 = await _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testExcutionsRes1 == null)
            {
                return NotFound();
            }



            if (exerStatus.ExeRId != testExcutionsRes1.TERId2)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    testExcutionsRes1.StatusSTTERID = exerStatus.StatusId;
                    _context.Update(testExcutionsRes1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExeResultsExists(testExcutionsRes1.TERId2))
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

        public class ResultCH
        {
            public int ExeRId2 { get; set; }
            public string Result2 { get; set; }
            
        }


        [HttpPost]
        public async Task<IActionResult> ResultChanged([FromBody]ResultCH Result3)
        {

            int id = Result3.ExeRId2;

            var testExcutionsRes1 = await _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testExcutionsRes1 == null)
            {
                return NotFound();
            }



            if (Result3.ExeRId2 != testExcutionsRes1.TERId2)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {


                    testExcutionsRes1.Result = Result3.Result2;
                    _context.Update(testExcutionsRes1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExeResultsExists(testExcutionsRes1.TERId2))
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


        // GET: TestsExeResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            ViewBag.id7 = i1;
            ViewBag.id8 = i2;

            if (id == null)
            {
                return NotFound();
            }

            var testsExeResults = await _context.TestsExeResults
                .Include(t => t.ProjectTests)
                .Include(t => t.TestCases)
                .Include(t => t.TestExcutions)
                .SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testsExeResults == null)
            {
                return NotFound();
            }

            return View(testsExeResults);
        }

        // GET: TestsExeResults/Create
        public IActionResult Create(int? id, int? id2)
        {

            ViewBag.id3 = id;
            ViewBag.id4 = id2;


            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id2), "TId", "Test");
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == id2), "TCId", "TestCase");
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == id), "TEId", "PPublicationDate");
            ViewData["Status"] = new SelectList("FP", "ff");

            //  new List<SelectListItem>
            // {
            //    new SelectListItem { Text = "Pass"},
            //     new SelectListItem { Text = "Fail"},
            // }, "Value", "Text");

            return View();
        }

        // POST: TestsExeResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int? id, int? id2, [Bind("TERId2,TestExcutionsTEId,TestCasesTCId,ProjectTestsTId,Result,PPublicationDate,Status,ImageUrl2,StatusSTTERID")] TestsExeResults testsExeResults, IFormFile myfile)
        {

            if (ModelState.IsValid)
            {

                 //  testsExeResults.ImageUrl2 = await UserFile.UploadeNewImageAsync(testsExeResults.ImageUrl2,
              //    myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

               // testsExeResults.PPublicationDate = DateTime.Today.Date;

                _context.Add(testsExeResults);


                lock (_context) { 
                 _context.SaveChangesAsync().Wait(); 
                }

                Console.WriteLine();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "TestsExeResults", new { id, id2 });

            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id2), "TId", "Test", testsExeResults.ProjectTestsTId);
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == id2), "TCId", "TestCase");
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == id), "TEId", "PPublicationDate", testsExeResults.TestExcutions.TEId);
            ViewData["Status"] = new SelectList("FP", "ff");
            // new List<SelectListItem>
            // {
            //      new SelectListItem { Text = "Pass"},
            //       new SelectListItem { Text = "Fail"},
            //  }, "Value", "Text");

            return View(testsExeResults);
        }









        // POST: BugsSummaries/UpdateBugStatus
        [HttpPost]
        public void UpdateTERtable(int? id, int? id2)
        {
            IFormFile myfile1 = null;


            foreach (var  row in (_context.TestCases.Where((testCase) => testCase.ProjectTestsTId == id2)).ToList())
            {

                TestsExeResults testsExeResults1 = new TestsExeResults
                {
                   
                    TestExcutionsTEId = id,
                    TestCasesTCId = row.TCId,
                    ProjectTestsTId = id2,
                    Result = "Default",
                    PPublicationDate = DateTime.Today.Date,
                    Status = "0",
                    ImageUrl2 = "camera3.png",
                    StatusSTTERID=1,
                };
               

                
                _ = Create(id, id2, testsExeResults1, myfile1);
            }

            

        }
















        // GET: TestsExeResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.id5 = i1;
            ViewBag.id6 = i2;

            if (id == null)
            {
                return NotFound();
            }

            var testsExeResults = await _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testsExeResults == null)
            {
                return NotFound();
            }
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == i2), "TId", "Test", testsExeResults.ProjectTestsTId);
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == i2), "TCId", "TestCase", testsExeResults.TestCasesTCId);
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == i1), "TEId", "PPublicationDate", testsExeResults.TestExcutionsTEId);
            ViewData["Status"] = new SelectList("FP", "ff");


            /*
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == id2), "TId", "Test");
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == id2), "TCId", "TestCase");
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == id), "TEId", "PPublicationDate");
            ViewData["Status"] = new SelectList("FP", "ff");*/

            return View(testsExeResults);
        }

        // POST: TestsExeResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("TERId2,TestExcutionsTEId,TestCasesTCId,ProjectTestsTId,Result,PPublicationDate,Status,ImageUrl2,StatusSTTERID")] TestsExeResults testsExeResults, IFormFile myfile)
        {

            int k1, k2;
            k1 = i1;
            k2 = i2;

            if (id != testsExeResults.TERId2)
            {
                return NotFound();
            }

            if ((ModelState.IsValid))
            {
                try
                {
                    // testsExeResults.ImageUrl2 = "";
                    testsExeResults.ImageUrl2 = await UserFile.UploadeNewImageAsync(testsExeResults.ImageUrl2,
                    myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

           

                    testsExeResults.PPublicationDate = DateTime.Today.Date;

                    _context.Update(testsExeResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExeResultsExists(testsExeResults.TERId2))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //  return RedirectToAction("Index", "TestsExeResults", new { k1, k2 });
                return RedirectToAction("Index/" + k1 + "/" + k2);

            }



            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == i2), "TId", "Test", testsExeResults.ProjectTestsTId);
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == i2), "TCId", "TestCase", testsExeResults.TestCasesTCId);
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == i1), "TEId", "PPublicationDate", testsExeResults.TestExcutionsTEId);
            ViewData["Status"] = new SelectList("FP", "ff");

            return View(testsExeResults);
        }



        // GET: TestsExeResults/Edit/5
        public async Task<IActionResult> EditPic(int? id)
        {

            ViewBag.id5 = i1;
            ViewBag.id6 = i2;

            if (id == null)
            {
                return NotFound();
            }

            var testsExeResults = await _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testsExeResults == null)
            {
                return NotFound();
            }


            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == i2), "TId", "Test", testsExeResults.ProjectTestsTId);
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == i2), "TCId", "TestCase", testsExeResults.TestCasesTCId);
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == i1), "TEId", "PPublicationDate", testsExeResults.TestExcutionsTEId);
            return View(testsExeResults);
        }

        // POST: TestsExeResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPic(int? id, [Bind("TERId2,TestExcutionsTEId,TestCasesTCId,ProjectTestsTId,Result,PPublicationDate,Status,ImageUrl2,StatusSTTERID")] TestsExeResults testsExeResults, IFormFile myfile)
        {

            int k1, k2;
            k1 = i1;
            k2 = i2;

            if (id != testsExeResults.TERId2)
            {
                return NotFound();
            }

            if ((ModelState.IsValid))
            {
                try
                {
                    // testsExeResults.ImageUrl2 = "";
                    testsExeResults.ImageUrl2 = await UserFile.UploadeNewImageAsync(testsExeResults.ImageUrl2,
                    myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);
                    testsExeResults.PPublicationDate = DateTime.Today.Date;
                    _context.Update(testsExeResults);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestsExeResultsExists(testsExeResults.TERId2))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                //  return RedirectToAction("Index", "TestsExeResults", new { k1, k2 });
                return RedirectToAction("Index/" + k1 + "/" + k2);

            }




            //  ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == i1), "TEId", "PPublicationDate", testsExeResults.TestExcutionsTEId);
            ViewData["ProjectTestsTId"] = new SelectList(_context.ProjectTests.Where(Test => Test.TId == i2), "TId", "Test", testsExeResults.ProjectTestsTId);
            ViewData["TestCasesTCId"] = new SelectList(_context.TestCases.Where(Test => Test.ProjectTests.TId == i2), "TCId", "TestCase", testsExeResults.TestCasesTCId);
            ViewData["TestExcutionsTEId"] = new SelectList(_context.TestExcutions.Where(Test => Test.TEId == i1), "TEId", "PPublicationDate", testsExeResults.TestExcutionsTEId);

            return View(testsExeResults);
        }



        // GET: TestsExeResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.id70 = i1;
            ViewBag.id80 = i2;
            if (id == null)
            {
                return NotFound();
            }

            var testsExeResults = await _context.TestsExeResults
                .Include(t => t.ProjectTests)
                .Include(t => t.TestCases)
                .Include(t => t.TestExcutions)
                .SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testsExeResults == null)
            {
                return NotFound();
            }

            return View(testsExeResults);
        }

        // POST: TestsExeResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testsExeResults = await _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            _context.TestsExeResults.Remove(testsExeResults);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index/" + i1 + "/" + i2);
        }

        private bool TestsExeResultsExists(int id)
        {
            return _context.TestsExeResults.Any(e => e.TERId2 == id);
        }


        public IActionResult BugExeCreate(int? id)
        {

            var testExcutionsRes2 =  _context.TestsExeResults.SingleOrDefaultAsync(m => m.TERId2 == id);
            if (testExcutionsRes2 == null)
            {
                return NotFound();
            }
            ViewBag.ImageUrl = testExcutionsRes2.Result.ImageUrl2; //
            ViewBag.bugg = testExcutionsRes2.Result.Result; //
          

            int? p1 = testExcutionsRes2.Result.ProjectTestsTId;
            var pt1 = _context.ProjectTests.SingleOrDefaultAsync(m => m.TId == p1);
            ViewBag.projecttest = pt1.Result.Test; //

            int? t1 = testExcutionsRes2.Result.TestCasesTCId;
            var tt1 = _context.TestCases.SingleOrDefaultAsync(m => m.TCId == t1);
            ViewBag.testcass = tt1.Result.TestCase; //

            int? c1 = testExcutionsRes2.Result.TestExcutionsTEId;
            var cc1 = _context.TestExcutions.SingleOrDefaultAsync(m => m.TEId == c1);
            int? c2 = cc1.Result.AssignedToAssignedToID; 
            var cc2 = _context.AssignedTo.SingleOrDefaultAsync(m => m.AssignedToID == c2);
            ViewBag.AssignedToTo = cc2.Result.Name; //


            ViewBag.id32 = i1;
            ViewBag.id33 = i2;

            

            DbConnection con2 = _context2.Database.GetDbConnection();
            con2.Open();
            DbCommand com = con2.CreateCommand();
            com.CommandText = "Select Projects.PName from Projects JOIN ProjectTests ON Projects.PId = ProjectTests.ProjectsPId JOIN TestsExeResults ON ProjectTests.TId = TestsExeResults.ProjectTestsTId where TestsExeResults.TERId2 =" + "'" + id + "'";
            com.CommandType = System.Data.CommandType.Text;
            DbDataReader dr2 = com.ExecuteReader();
            StringBuilder data2 = new StringBuilder();

            while (dr2.Read())
            {
                data2.AppendLine(dr2.GetString(0));

            }

            PPname2 = data2.ToString();
            PPname2 = PPname2.Substring(0, PPname2.Length - 2);





            if (id == null)
            {
                return NotFound();
            }



            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ProjectsPId"] = new SelectList(_context.Projects.Where(Test => Test.PName == PPname2), "PId", "PName");
            ViewData["StatusSTID"] = new SelectList(_context.Status, "STID", "Name");

            return View();
        }

       




        // POST: BugsSummaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Testers,Admins,Dev")]
        public async Task<IActionResult> BugExeCreate(int? id, [Bind("Idd,ProjectsPId,Bug,BugSummary,TesterName,ImageUrl,StatusSTID")] BugsSummary bugsSummary, IFormFile myfile)
        {

            string PPNme, HtPPNme;
            

            if (ModelState.IsValid)
            {

                bugsSummary.ImageUrl = await UserFile.UploadeNewImageAsync(bugsSummary.ImageUrl,
              myfile, _environment.WebRootPath, Properties.Resources.ImgFolder, 100, 100);

                bugsSummary.PublicationDate = DateTime.Today.Date;

                bugsSummary.TestsExeResultsTERID = id.Value;
                bugsSummary.UserId = _userManager.GetUserId(User);
                bugsSummary.StatusSTID = 1;
                
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

                PPNme = data.ToString();
                HtPPNme = "<b>" + data.ToString() + "- </b>";

                //-------------------------------------------------------------------------








                BackgroundJob.Schedule(() => SendEmail(PPNme, HtPPNme), TimeSpan.FromMinutes(1));


                return RedirectToAction("Index/" + i1 + "/" + i2);
            }



            ViewData["ProjectsPId"] = new SelectList(_context.Projects.Where(Test => Test.PName == PPname2), "PId", "PName", bugsSummary.ProjectsPId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", bugsSummary.UserId);
            ViewData["StatusSTID"] = new SelectList(_context.Status, "STID", "Name", bugsSummary.StatusSTID);
          



            return View(bugsSummary);
        }

        public void SendEmail(string Prname, string Prname2)

        {
            string htmlContent = "Akel found a new Bug related to " + Prname2 + " project please login the testing system and check it.";
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("akel.obied@outlook.com", "Akel -" + Prname + " Tests");
            var to = new EmailAddress("akel.obied@innovisec.com");
            var plainTextContent = Regex.Replace(htmlContent, "<[^>]*>", "");
            var msg = MailHelper.CreateSingleEmail(from, to, "Bug Added", plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);


        }


    }
}
