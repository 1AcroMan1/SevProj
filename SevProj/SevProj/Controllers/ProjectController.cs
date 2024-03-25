using Microsoft.AspNetCore.Mvc;
using SevProj.Models;

namespace SevProj.Controllers
{
    public class ProjectController : Controller
    {
        private string _projectName = null;
        [HttpGet]
        public IActionResult ProjectPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ProjectPage(string projId)
        {
            return View(GetProjById(projId));
        }
        private Project GetProjById(string i)
        {
            Console.WriteLine(i);
            string projName = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT ProjName From MainProjects WHERE Id = {int.Parse(i)}");
            string projDesc = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT ProjDesc From MainProjects WHERE Id = {int.Parse(i)}");
            string dirId = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT DirectionId From MainProjects WHERE Id = {int.Parse(i)}");
            return new Project(i, projName, projDesc, dirId);
        }
    }
}
