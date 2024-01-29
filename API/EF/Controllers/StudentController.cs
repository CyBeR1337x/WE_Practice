using System.Linq;
using System.Web.Http;


namespace API.Controllers {
    public class StudentController : ApiController {

        StuFeeDBEntities1 db = new StuFeeDBEntities1();

        [HttpGet]
        public IHttpActionResult GetStudents() { 
            return Ok(db.Students); 
        }
        [HttpGet]
        public IHttpActionResult GetStudentById(int id) {
            return Ok(db.Students.Where(i => i.rollno == id).FirstOrDefault());
			//OR
            //return Ok(db.Students.FirstOrDefault(i => i.rollno == id));
        }

        [HttpPost]
        public IHttpActionResult AddStudent([FromBody] Student student) { 
            db.Students.Add(student);
            db.SaveChanges();
            return Ok("Student Added!");   
        }
    }
}
