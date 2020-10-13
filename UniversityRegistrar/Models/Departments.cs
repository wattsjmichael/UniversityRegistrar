using System.Collections.Generic;
namespace UniversityRegistrar.Models
{
  public class Department
  {
    public Department()
    {
      this.Courses = new HashSet<Course>();
      this.Students = new HashSet<Student>();
    }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public string DepartmentHead { get; set; }
    public virtual ICollection<Course> Courses { get; set; }
    public virtual ICollection<Student> Students { get; set; }
  }
}