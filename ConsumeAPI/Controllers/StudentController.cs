using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using ConsumeApI.Models;
using Newtonsoft.Json;

namespace ConsumeApI.Controllers {
    public class StudentController : Controller {
        // GET: Student
        public ActionResult StudentList() {
            List<Student> ls = new List<Student>();
            using (HttpClient cli = new HttpClient()) {
                cli.BaseAddress = new Uri("https://localhost:44354/api/Student/");
                var responseTask = cli.GetAsync("GetStudents");
                responseTask.Wait();

                var response = responseTask.Result;
                if (response.IsSuccessStatusCode) {
                    var data = response.Content.ReadAsStringAsync();
                    data.Wait();
                    ls = JsonConvert.DeserializeObject<List<Student>>(data.Result);
                }
            }
            return View(ls);
        }

        public ActionResult Create() {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student s) {
            using (HttpClient cli  = new HttpClient()) {
                cli.BaseAddress = new Uri("https://localhost:44354/api/Student/");
                var postTask = cli.PostAsJsonAsync<Student>("AddStudent", s);
                postTask.Wait();

                var responseTask = postTask.Result; 
                if (responseTask.IsSuccessStatusCode) {
                    return RedirectToAction("StudentList");
                }
            }

            return View();
        }

        public ActionResult Detail(int id) { 
            Student s = new Student();
            using (HttpClient cli  = new HttpClient()) {
                cli.BaseAddress = new Uri("https://localhost:44354/api/Student/");
                var responseTask = cli.GetAsync($"GetStudentById/{id}");
                responseTask.Wait();
                var response = responseTask.Result;
                if (response.IsSuccessStatusCode) {
                    var data = response.Content.ReadAsStringAsync();
                    data.Wait();
                    s = JsonConvert.DeserializeObject<Student>(data.Result);
                }
            }
            return View(s);
        }
    }
}