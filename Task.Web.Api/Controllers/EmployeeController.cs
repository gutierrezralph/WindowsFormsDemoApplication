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
        private readonly IEmployeeBusinessLayer _employeeBusinessLayer;
        public EmployeeController(IEmployeeBusinessLayer employeeBusinessLayer)
        {
            _employeeBusinessLayer = employeeBusinessLayer;
        }

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
                    Message = nameof(this.GetAllEmployee),
                    Response = result
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("{0} ERROR {1}", nameof(this.GetAllEmployee), e.Message),
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
                    Message = nameof(this.AddEmployee),
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("{0} ERROR {1}", nameof(this.AddEmployee), e.Message),
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
                    Message = nameof(this.EditEmployee),
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("{0} ERROR {1}", nameof(this.EditEmployee), e.Message),
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
                await _employeeBusinessLayer.DeleteEmployee(id);
                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = nameof(this.RemoveEmployee)
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = string.Format("{0} ERROR {1}", nameof(this.RemoveEmployee), e.Message),
                    Exception = e
                });
            }
        }

    }
}