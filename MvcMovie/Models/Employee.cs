using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models;
public class Employee : Person
{
    [Key]
    public string EmployeeID { get; set; }
    public int Age { get; set; }

}