using System;
using System.ComponentModel;
// Define an alias for the nested namespace.
using Awaitable = System.Threading.Tasks;
using System.Windows.Forms;
using Task.DTO.Request;
using Task.DTO.Response;
using Task.Windows.Forms.Helper;
using Task.Windows.Forms.Service;
using DevExpress.XtraEditors;

namespace Task.Windows.Forms.Classes
{
    public class EmployeeRecord
    {
        public BindingList<ResponseData> GetEmployeeData()
        {
            var employeeService = new EmployeeService();
            var responseData = new BindingList<ResponseData>();
            try
            {
                var creatingRequest = Awaitable.Task.Run(() => employeeService.GetRequestAsync(EmployeeUriEnum.Get.ToString()));
                creatingRequest.Wait();

                foreach (var item in creatingRequest.Result.Results.ResponseData)
                    responseData.Add(item);

                creatingRequest.Dispose();
                return responseData;
            }
            catch
            {
                XtraMessageBox.Show("Error while sending request","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Composing request that will be sent to the WebAPI
        /// </summary>
        /// <param name="uriRoute"> 
        /// URL Route such as Get , Edit/{id} , Remove/{id} , Add </param>
        /// <param name="responseData"> 
        /// Request data that will be Map to EmployeeRequest class using AutoMapper library
        /// </param>
        /// <param name="requestVerb"> POST, GET, PUT, DELETE </param>
        /// <returns> Return TRUE if Success else False </returns>
        public bool SendingRequestedData(string uriRoute, ResponseData responseData, HttpRequestVerbEnum requestVerb)
        {
            var employeeRequest = AutoMapper.Mapper.Map<EmployeeRequest>(responseData);
            var employeeService = new EmployeeService();
            try
            {
                var creatingRequest = Awaitable.Task.Run(() => employeeService.CreateRequestAsync(uriRoute, employeeRequest, requestVerb));
                creatingRequest.Wait();
                creatingRequest.Dispose();
                return true;
            }
            catch
            {
                XtraMessageBox.Show("Error while sending request" , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
