using Courses.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Common;


namespace Courses.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            CourseListViewModel vm = new CourseListViewModel();

            string Token = await CoursesApi.GetToken();
            vm.Courses = await CoursesApi.GetCourses(Token);
                       

            return View(vm);
        }

        public async Task<ActionResult> Details(int id)
        {
            string Token = await CoursesApi.GetToken();
            Course course = await CoursesApi.GetCourseById(Token, id);
            CourseDetailModel VM = new CourseDetailModel();
            VM.course = course;

            return View(VM);
        }


      
    }


}