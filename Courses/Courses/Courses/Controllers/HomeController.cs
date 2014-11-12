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

            using (HttpClient client = new HttpClient())
            {
                string TokenURL = "http://canvas-api.herokuapp.com/api/v1/tokens";
                
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                Token token = null;

                HttpResponseMessage response = await client.PostAsync(TokenURL, null);
                if (response.IsSuccessStatusCode)
                {
                    token =  JsonConvert.DeserializeObject<Token>( await response.Content.ReadAsStringAsync());

                }

                string CoursesUrl = "http://canvas-api.herokuapp.com/api/v1/courses";
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token);

                string JSONString = await client.GetStringAsync(CoursesUrl);

                List<Course> Courses = JsonConvert.DeserializeObject<List<Course>>(JSONString);

                vm.Courses = Courses;


            }

            return View(vm);
        }

      
    }

    public class Token
    {
        public string token { get; set; }
    }
}