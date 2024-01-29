using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using API_ADO.Models;

namespace API_ADO.Controllers {
    public class StudentController : ApiController {
        private string conStr = @"Data Source=CYBER; Initial Catalog=StuFeeDB; Integrated Security=true";

        [HttpGet]
        public IHttpActionResult GetStudents() {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            List<Student> students = new List<Student>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Student", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read()) {
                students.Add(new Student {
                    rollno = int.Parse(sdr["rollno"].ToString()),
                    fullname = sdr["fullname"].ToString(),
                    @class = int.Parse(sdr["class"].ToString()),
                    year = int.Parse(sdr["year"].ToString()),
                    gender = char.Parse(sdr["gender"].ToString()),
                    job = bool.Parse(sdr["job"].ToString())
                }) ;
            }
            sdr.Close();
            con.Close();
            return Ok(students);
        }

        [HttpGet]
        public IHttpActionResult GetStudentById(int id) {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            SqlCommand cmd = new SqlCommand($"SELECT * FROM Student WHERE rollno = {id}", con);
            SqlDataReader sdr = cmd.ExecuteReader();
            sdr.Read();
            Student s = new Student {
                rollno = int.Parse(sdr["rollno"].ToString()),
                fullname = sdr["fullname"].ToString(),
                @class = int.Parse(sdr["class"].ToString()),
                year = int.Parse(sdr["year"].ToString()),
                gender = char.Parse(sdr["gender"].ToString()),
                job = bool.Parse(sdr["job"].ToString())
            };
            sdr.Close();
            con.Close();
            return Ok(s);
        }

        [HttpPost]
        public IHttpActionResult AddStudent([FromBody] Student student) {
            SqlConnection con = new SqlConnection(conStr);
            con.Open();
            string q = $"INSERT INTO Student VALUES('{student.rollno}', " +
                $"'{student.fullname}', '{student.@class}', '{student.year}'," +
                $"'{student.gender}', '{student.job}')";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.ExecuteNonQuery();

            con.Close();
            return Ok("Student Added!");
        }

    }
}