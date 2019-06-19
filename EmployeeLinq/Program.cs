using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            var employees = new[] {
                      new { Name="Andras", Salary=420},
                      new { Name="Bela", Salary=400},
                      new { Name="Csaba", Salary=250},
                      new { Name="David", Salary=300},
                      new { Name="Endre", Salary=620},
                      new { Name="Ferenc", Salary=350},
                      new { Name="Gabor", Salary=410},
                      new { Name="Hunor", Salary=500},
                      new { Name="Imre", Salary=900},
                      new { Name="Janos", Salary=600},
                      new { Name="Karoly", Salary=700},
                      new { Name="Laszlo", Salary=400},
                      new { Name="Marton", Salary=500}
                                    };


            //Display the name of the employee who earn the most
            Console.WriteLine(employees.OrderByDescending(e => e.Salary).Select(e => e.Name).First());

            //Display the name of the employees who earn less than the company average.
            double salaryAvarage = employees.Average(e => e.Salary);
            var employeesLessThatAvarageSalary = employees.Where(e => e.Salary < salaryAvarage).ToList() ;

            //Sort the employees by their salaries in an ascending order    
            var sortedEmployees = employees.OrderBy(e => e.Salary).Select(e => e).ToList();

            //Display the name of employees who earn the same amount and sort the result by salaries then names in an ascending order
            var result = employees.GroupBy(item => item.Salary)
                               .Where(group => group.Count() >= 2)
                               .Select(group => group.OrderBy(emp => emp.Salary)
                               .ThenBy(emp => emp.Name));

            result.ToList().ForEach(i => i.ToList().ForEach(item => Console.WriteLine(item.Name +" earns " + item.Salary + "$")));


            //Group the employees in the following salary ranges: 200-399, 400-599, 600-799, 800-999

            var ranges = new[] { 200, 399, 599, 799, 999 };

            var grouping = employees.GroupBy(x => ranges.FirstOrDefault(r => r >= x.Salary));
            grouping.ToList().ForEach(i => i.ToList()
                             .ForEach(item => Console.WriteLine(ranges[Array.IndexOf(ranges, i.Key) - 1] + "-" + i.Key + "   " + item.Name + " earns " + item.Salary + "$")));



            Console.ReadKey();
        }
    }
}
