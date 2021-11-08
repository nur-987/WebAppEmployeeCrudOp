using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAppEmployeeCrudOp.Data;
using WebAppEmployeeCrudOp.Models;

namespace WebAppEmployeeCrudOp.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        List<Employee> empList = new List<Employee>();
        FileManager fileManager = new FileManager();

        public EmployeeController()
        {       
            empList.Add(new Employee() { EmpId = 1, EmpName = "Mary" });
            empList.Add(new Employee() { EmpId = 2, EmpName = "John" });
            empList.Add(new Employee() { EmpId = 2, EmpName = "John" });

            var EmpListinString = JsonConvert.SerializeObject(empList);
            fileManager.WriteAllText("Employee.json", EmpListinString);

        }

        [HttpGet]
        [Route("empdetails/{id}")]
        public Employee GetSelected(int id)
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            return EmpListfromJson.Where(x => x.EmpId == id).FirstOrDefault();

        }

        [HttpGet]
        [Route("allemp")]
        public List<Employee> GetAll()
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            return EmpListfromJson;

        }

        [HttpPost]
        [Route("addemp")]
        public List<Employee> AddNewEmp(Employee newEmp)
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            EmpListfromJson.Add(newEmp);

            var EmpListinString = JsonConvert.SerializeObject(EmpListfromJson);
            fileManager.WriteAllText("Employee.json", EmpListinString);

            return EmpListfromJson;
        }

        [HttpPatch]
        [Route("updateemp")]
        public List<Employee> UpdateEmp(int id, string newName)
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            Employee EmpExist = EmpListfromJson.Where(x => x.EmpId == id).FirstOrDefault();
            if (EmpExist == null)
            {
                return null;
            }
            else
            {
                EmpListfromJson.Remove(EmpExist);
                EmpExist.EmpId = id;
                EmpExist.EmpName = newName;
                EmpListfromJson.Add(EmpExist);

                var EmpListinString = JsonConvert.SerializeObject(EmpListfromJson);
                fileManager.WriteAllText("Employee.json", EmpListinString);

                return EmpListfromJson;
            }


        }

        [HttpPut]
        [Route("putemp")]
        public List<Employee> PutEmployee(Employee emp)
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            Employee EmpExist = EmpListfromJson.Where(x => x.EmpId == emp.EmpId).FirstOrDefault();
            if (EmpExist == null)
            {
                return null;
            }
            else
            {
                EmpListfromJson.Remove(EmpExist);
                EmpListfromJson.Add(emp);

                var EmpListinString = JsonConvert.SerializeObject(EmpListfromJson);
                fileManager.WriteAllText("Employee.json", EmpListinString);

                return EmpListfromJson;
            }

        }

        [HttpDelete]
        [Route("deleteemp")]
        public List<Employee> Deleteemployee(Employee emp)
        {
            string EmpfromJson = fileManager.ReadAllText("Employee.json");
            List<Employee> EmpListfromJson = JsonConvert.DeserializeObject<List<Employee>>(EmpfromJson);

            Employee EmpExist = EmpListfromJson.Where(x => x.EmpId == emp.EmpId).FirstOrDefault();
            if (EmpExist != null)
            {
                EmpListfromJson.Remove(EmpExist);

                var EmpListinString = JsonConvert.SerializeObject(EmpListfromJson);
                fileManager.WriteAllText("Employee.json", EmpListinString);

                return EmpListfromJson;
            }
            else
            {
                return null;
            }
        }
    }
}
