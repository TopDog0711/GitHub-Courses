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

namespace Courses.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {

            CourseListViewModel vm = new CourseListViewModel();

            string Token = await GetToken();
            vm.Courses = await GetCourses(Token);
                       

            return View(vm);
        }

        public async Task<ActionResult> Details(int id)
        {
            string Token = await GetToken();
            Course course = await GetCourseById(Token, id);
            CourseDetailModel VM = new CourseDetailModel();
            VM.course = course;

            return View(VM);
        }


        private async Task<string> GetToken()
        {
            using (HttpClient client = new HttpClient())
            {
                string TokenURL = "http://canvas-api.herokuapp.com/api/v1/tokens";

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                Token token = null;

                HttpResponseMessage response = await client.PostAsync(TokenURL, null);
                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());

                    return token.token;
                }


            }

            return "";


        }

        private async Task<List<Course>> GetCourses(string Token)
        {
            using (HttpClient client = new HttpClient())
            {


                string CoursesUrl = "http://canvas-api.herokuapp.com/api/v1/courses";

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

                string JSONString = await client.GetStringAsync(CoursesUrl);

                List<Course> Courses = JsonConvert.DeserializeObject<List<Course>>(JSONString);

                return Courses;


            }
        }

        private async Task<Course> GetCourseById(string Token, int Id)
        {
            using (HttpClient client = new HttpClient())
            {


                string CoursesUrl = "http://canvas-api.herokuapp.com/api/v1/courses/" + Id;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Token);

                string JSONString = await client.GetStringAsync(CoursesUrl);

                Course course = JsonConvert.DeserializeObject<Course>(JSONString);

                return course;


            }
        }
      
    }

    public class Token
    {
        public string token { get; set; }
    }
}