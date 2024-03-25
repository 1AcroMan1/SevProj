using Microsoft.AspNetCore.Mvc;
using SevProj.Models;
using System;
using System.Diagnostics;

namespace SevProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private List<Project> GetProjList()
        {
            List<Project> proj = new List<Project>();
            List<object> a = Scr.DbHelper.ExecuteWithAnswer("SELECT Id FROM MainProjects ORDER BY Id");
            for (int i = 0; i < a.Count; i++)
            {
                string id = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT Id From MainProjects WHERE Id = {i}");
                string projName = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT ProjName From MainProjects WHERE Id = {i}");
                string projDesc = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT ProjDesc From MainProjects WHERE Id = {i}");
                string dirId = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT DirectionId From MainProjects WHERE Id = {i}");
                proj.Add(new Project(id, projName, projDesc,dirId));
                Console.WriteLine($"Project: {id}");
            }
            return proj;
        }
        private List<Direction> GetDirList()
        {
            List<Direction> dir = new List<Direction>();
            List<object> a = Scr.DbHelper.ExecuteWithAnswer("SELECT Id FROM Direction ORDER BY Id");
            for (int i = 0; i < a.Count; i++)
            {
                string id = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT Id From Direction WHERE Id = {i}");
                string dirName = Scr.DbHelper.ExecuteQueryWithAnswer($"SELECT DirName From Direction WHERE Id = {i}");
                dir.Add(new Direction(id, dirName));
                Console.WriteLine($"Direction: {id}");
            }
            return dir;
        }
        private List<Project> ProjDirSearch(string name)
        {
            if (name != null)
            {
                return GetProjList().Where(n => n.DirectionId.ToString().Contains(name)).ToList();
            }
            else
            {
                return GetProjList();
            }
        }
        private List<Project> ProjSearch(string search)
        {
            if (search != null)
            {
                return GetProjList().Where(n => n.Name.ToString().ToLower().Contains(search.ToLower())).ToList();
            }
            else
            {
                return GetProjList();
            }
        }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var tuple = new Tuple<List<Project>, List<Direction>>(GetProjList(), GetDirList());
            return View(tuple);
        }
        [HttpPost]
        public IActionResult Index(string name, string search)
        {
            Console.WriteLine($"name: {name}");
            Console.WriteLine($"search: {search}");
            if (search == null)
            {
                var tuple = new Tuple<List<Project>, List<Direction>>(ProjDirSearch(name), GetDirList());
                return View(tuple);
            }
            else
            {
                var tuple = new Tuple<List<Project>, List<Direction>>(ProjSearch(search), GetDirList());
                return View(tuple);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
