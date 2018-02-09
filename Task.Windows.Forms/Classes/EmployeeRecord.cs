using System;
using System.ComponentModel;
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
            var service = new EmployeeService();
            var data = new BindingList<ResponseData>();
            try
            {
                var getResult = Awaitable.Task.Run(() => service.GetAsync());
                getResult.Wait();
                foreach (var item in getResult.Result.Result.ResponseData)
                    data.Add(item);
                return data;
            }
            catch
            {
                XtraMessageBox.Show("Error while sending request","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Composing request to the WebAPI
        /// </summary>
        /// <param name="uriRoute">Employee API route</param>
        /// <param name="responseData"> Response data that will be map to request data format </param>
        /// <param name="method"> POST, GET, PUT, DELETE </param>
        /// <returns>True if success else False </returns>
        public bool ServiceRequest(string urlRoute, ResponseData responseData, HttpRequestMethod method)
        {
            var mappedRequest = AutoMapper.Mapper.Map<EmployeeRequest>(responseData);
            var service = new EmployeeService();
            try
            {
                var getResult = Awaitable.Task.Run(() => service.SendingRequestAsync(urlRoute, mappedRequest, method));
                getResult.Wait();
                return true;
            }
            catch
            {
                XtraMessageBox.Show("Error while sending request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
