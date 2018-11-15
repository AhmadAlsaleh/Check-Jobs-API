using CheckJobsAPI.Data;
using CheckJobsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckJobsAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ReportController : Controller
    {
        ApplicationDbContext _context { get; set; }
        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult AddReport([FromBody] Report report)
        {
            try
            {
                report.ID = Guid.NewGuid();
                _context.Reports.Add(report);
                _context.SaveChanges();
                return Json(Ok());
            }
            catch (Exception e)
            {
                return Json(BadRequest());
            }
        }

    }
}
