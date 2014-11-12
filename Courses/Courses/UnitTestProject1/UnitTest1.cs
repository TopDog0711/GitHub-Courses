using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TokenTest()
        {
            try
            {
                string token = await CoursesApi.GetToken();
                Assert.IsNotNull(token);

            }
            catch (Exception)
            {
                Assert.Fail();
                
            }
        }

        [TestMethod]
        public async Task CoursesTest()
        {
            try
            {
                string token = await CoursesApi.GetToken();
                List<Course> courses = await CoursesApi.GetCourses(token);
                Assert.IsNotNull(courses);
                Assert.IsTrue(courses.Count > 0);



            }
            catch(Exception)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        public async Task SingleCourseTest()
        {
            try
            {
                string token = await CoursesApi.GetToken();
                Course course = await CoursesApi.GetCourseById(token, 1);
                Assert.IsNotNull(course);
                
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
