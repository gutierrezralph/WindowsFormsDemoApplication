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
        private readonly IEmployeeBusinessLayer empBL = new EmployeeBusinessLayer();

        [HttpGet]
        [Route("employee/get")]
        public async Task<IHttpActionResult> GetAll()
        {
            if (ModelState.IsValid == false)
                return BadRequest(ModelState);

            try
            {
                var result = await empBL.GetAllEmployee();

                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "GetAllPerson",
                    Response = result.ToList()
                });

            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = "GetAllPerson ERROR " + e.Message,
                    Exception = e
                });
            }

        }

        
        [HttpGet]
        [ValidateModelStateFilter]
        [Route("employee/get/{id}")]
        public async Task<IHttpActionResult> GetEmployeeById(int id)
        {
            try
            {
                this.ModelState.Clear();
                var result = await empBL.GetEmployeeById(id);
                       
                return Ok(new BasicResponse()
                {
                    Status = true,
                    Message = "GetEmployeeById",
                    Response = result
                });
            }
            catch (Exception e)
            {
                return Ok(new BasicResponse()
                {
                    Status = false,
                    Message = "GetEmployeeById ERROR " + e.Message,
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

                    var affectedRow = await empBL.InsertEmployee(employee);
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
                    Message = "AddEmployee ERROR " + e.Message,
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
                int affectedRow = await empBL.UpdateEmployee(id,employee);

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
                    Message = "EditEmployee ERROR " + e.Message,
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
                int affectedRow = await empBL.DeleteEmployee(id);
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
                    Message = "RemoveEmployee ERROR " + e.Message,
                    Exception = e
                });
            }
        }

    }
}