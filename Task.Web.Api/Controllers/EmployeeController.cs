using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Task.BusinessLayer.Implementation;
using Task.BusinessLayer.Interface;
using Task.DTO.Request;
using Task.Web.Api.Infrastructure.Filter;
using Task.Web.Api.Infrastructure.Response;

namespace Task.Web.Api.Controllers
{

    [RoutePrefix("api")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeBusinessLayer _employeeBusinessLayer = new EmployeeBusinessLayer();

        [HttpGet]
        [Route("employee")]
        public async Task<IHttpActionResult> GetAllEmployee()
        {
            try
            {
                this.ModelState.Clear();
                var result = await _employeeBusinessLayer.GetAllEmployee();

                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "GetAllEmployee",
                    Response = result.ToList()
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("GetAllEmployee ERROR {0}", e.Message),
                    Exception = e
                });
            }

        }
             
        [HttpPost]
        [ValidateModelStateFilter]
        [Route("employee/add")]
        public async Task<IHttpActionResult> AddEmployee([FromBody] EmployeeRequest employee)
        {
            try
            {
                this.ModelState.Clear();
                await _employeeBusinessLayer.InsertEmployee(employee);
                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "AddEmployee",
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("AddEmployee ERROR {0}", e.Message),
                    Exception = e
                });
            }
        }

        [HttpPut]
        [ValidateModelStateFilter]
        [Route("employee/edit/{id}")]
        public async Task<IHttpActionResult> EditEmployee(int id, [FromBody] EmployeeRequest employee)
        {

            try
            {
                this.ModelState.Clear();
                 await _employeeBusinessLayer.UpdateEmployee(id,employee);

                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "EditEmployee",
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("EditEmployee ERROR {0}", e.Message),
                    Exception = e
                });
            }
        }

        
        [HttpDelete]
        [ValidateModelStateFilter]
        [Route("employee/remove/{id}")]
        public async Task<IHttpActionResult> RemoveEmployee(int id)
        {
            try
            {
                this.ModelState.Clear();
                int affectedRow = await _employeeBusinessLayer.DeleteEmployee(id);
                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "RemoveEmployee"
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("RemoveEmployee ERROR {0}", e.Message),
                    Exception = e
                });
            }
        }

    }
}