using MvcMovie.Models;
namespace MvcMovie.Models
{
public class Person
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public Person(){
            FullName = "Ho ten mac dinh";
            Address = "Dia chi mac dinh";
            Age = 20;
        }

        public void NhapThongTin(){
            System.Console.Write("Full name: ");
            FullName = Console.ReadLine();
            System.Console.Write("Address: ");
            Address = Console.ReadLine();
            System.Console.Write("Age: ");
            try{
                Age = Convert.ToInt32(Console.ReadLine());
            }catch(Exception e)
            {
                Age = 0;
            }
            
        }
        public void HienThi(){
            System.Console.Write(FullName + " - " + Address + " - " + Age);
        }

        public void Display(string ten, int tuoi){
            System.Console.WriteLine("{0}-{1} tuoi", ten, tuoi);
        }
        public int GetYearOfBirthDay(int year){
            int yearOfBirthDay = 2023 - year;
            return yearOfBirthDay;
        }
    }
}
   