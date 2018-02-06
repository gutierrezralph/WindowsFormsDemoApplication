using System;
using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task.DTO.Request;
using Task.DTO.Response;
using Task.Windows.Forms.Helper;
using Task.Windows.Forms.Service;
using DevExpress.XtraEditors;

namespace Task.Windows.Forms.Classes
{
    public class clsEmployeeRecord
    {
        public BindingList<ResponseData> GetEmployeeData()
        {
            var service = new EmployeeService();
            var data = new BindingList<ResponseData>();
            try
            {
                var getResult = System.Threading.Tasks.Task.Run(() => service.GetAsync(UrlRoute.GET.ToString()));
                getResult.Wait();
                foreach (var item in getResult.Result.Result.ResponseData)
                {
                    data.Add(item);
                }
                return data;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Error while sending request","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Composing request to the WebAPI
        /// </summary>
        /// <param name="route"> 
        /// URL Route such as Get , Edit/{id} , Remove/{id} , Add </param>
        /// <param name="data"> Request data </param>
        /// <param name="verb"> POST, GET, PUT, DELETE </param>
        /// <returns> Return TRUE if Success else False </returns>
        public bool ServiceRequestedData(string route, ResponseData data, HttpRequestVerb verb)
        {
            var mappedRequest = AutoMapper.Mapper.Map<EmployeeRequest>(data);
            var service = new EmployeeService();
            try
            {
                var getResult = System.Threading.Tasks.Task.Run(() => service.CreateServiceRequestAsync(route, mappedRequest, verb));
                getResult.Wait();
                return true;
            }
            catch (Exception e)
            {
                XtraMessageBox.Show("Error while sending request", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
