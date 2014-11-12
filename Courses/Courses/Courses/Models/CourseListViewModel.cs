using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Courses.Models
{
    public class CourseListViewModel
    {
        public List<Course> Courses { get; set; }
    }

    public class CourseDetailModel
    {
        public Course course { get; set; }
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