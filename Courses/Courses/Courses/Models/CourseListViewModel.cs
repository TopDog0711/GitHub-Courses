using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Common;

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

    
}