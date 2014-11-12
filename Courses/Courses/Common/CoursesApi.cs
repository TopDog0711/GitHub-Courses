using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class CoursesApi
    {
        public static async Task<string> GetToken()
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

        public static async Task<List<Course>> GetCourses(string Token)
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

        public static async Task<Course> GetCourseById(string Token, int Id)
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

    public class Course
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public DateTime? start_at { get; set; }
        public DateTime? end_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
