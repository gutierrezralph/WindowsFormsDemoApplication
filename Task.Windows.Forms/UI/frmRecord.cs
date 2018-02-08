using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task.DTO.Response;
using Task.Windows.Forms.Service;
using DevExpress.XtraBars;
using Task.Windows.Forms.Classes;
using DevExpress.XtraGrid.EditForm.Helpers.Controls;
using DevExpress.XtraEditors;
using Task.DTO.Request;
using Task.Windows.Forms.Helper;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace Task.Windows.Forms.Forms
{
    public partial class frmRecord : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmRecord()
        {
            InitializeComponent();
            RefreshDataGrid();
            gridRecord.FindForm().Visible = true;
        }

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView.AddNewRow();
                gridView.ShowEditForm();
            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gridView.SelectedRowsCount > 0)
                    gridView.ShowEditForm();
            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (gridView.SelectedRowsCount > 0)
                {
                    var dialogResultIsYes = XtraMessageBox.Show("Selected record will be deleted?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    if (dialogResultIsYes)
                    {
                        var selectedRecordForDeletion = GetRowSelectedData();
                        if (selectedRecordForDeletion.Id == 0 || selectedRecordForDeletion == null)
                            return;

                        var record = new EmployeeRecord();
                        record.SendingRequestedData(
                            string.Format("{0}/{1}", EmployeeUriEnum.Remove.ToString(), selectedRecordForDeletion.Id), 
                            selectedRecordForDeletion, HttpRequestVerbEnum.Delete);

                        gridView.DeleteSelectedRows();
                    }
                }
            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void OnClickEditFormUpdateButton(object sender, EventArgs e)
        {
            try
            {            
                var isFromCanel = EditFormCancelButtonWasClicked(sender);
                if (isFromCanel)
                    return;

                var data = GetRowSelectedData();
                if (string.IsNullOrWhiteSpace(data.FirstName) && string.IsNullOrWhiteSpace(data.LastName))
                    return;

                CreateRequest(data);
                gridView.CloseEditForm();
                RefreshDataGrid();
            }
            catch (Exception x)
            {
                XtraMessageBox.Show(x.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void RefreshDataGrid()
        {
            gridRecord.DataSource = null;
            gridRecord.DataSource = GridDataSource();
            gridRecord.RefreshDataSource();
        }

        private BindingList<ResponseData> GridDataSource()
        {
            var employeeRecord = new EmployeeRecord();
            var employeeData = employeeRecord.GetEmployeeData();
            return employeeData;
        }

        private void OnShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {
            foreach (Control control in e.EditForm.Controls)
            {
                if (!(control is EditFormContainer))
                    continue;
                foreach (Control nestedControl in control.Controls)
                {
                    if (!(nestedControl is PanelControl))
                        continue;
                    foreach (Control button in nestedControl.Controls)
                    {
                        if (!(button is SimpleButton))
                            continue;

                        var updateBotton = button as SimpleButton;
                        updateBotton.Click -= OnClickEditFormUpdateButton;
                        updateBotton.Click += OnClickEditFormUpdateButton;
                    }
                }
            }
        }

        private bool EditFormCancelButtonWasClicked(object sender)
        {
            var buttonType = sender.GetType().Name;
            // Check if the sender is cancel button from editForm
            var isCanel = buttonType.Equals("EditFormCancelButton", StringComparison.CurrentCultureIgnoreCase);
            return isCanel;
        }

        private void CreateRequest(ResponseData responseData)
        {
            var record = new EmployeeRecord();

            if(responseData.Id == 0)
                record.SendingRequestedData(EmployeeUriEnum.Add.ToString(), responseData, HttpRequestVerbEnum.Post);
            else
                 record.SendingRequestedData(string.Format("{0}/{1}", EmployeeUriEnum.Edit.ToString(), responseData.Id), responseData, HttpRequestVerbEnum.Put);
        }

        private ResponseData GetRowSelectedData()
        {
            var selectedRowIndex = gridView.GetSelectedRows().FirstOrDefault();
            var rowValue = gridRecord.FocusedView.GetRow(selectedRowIndex);
            return (ResponseData)rowValue;
        }

        private void gridView_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            if (!(column.Name.Equals("FirstName", StringComparison.CurrentCultureIgnoreCase)
                || column.Name.Equals("LastName", StringComparison.CurrentCultureIgnoreCase)))
                return;           

            var inputValue = Convert.ToString(e.Value);
            var inputValueArray = inputValue.ToCharArray();

            if (!(inputValue.All(c => Char.IsLetter(c) || c == ' ')))
            {
                e.ErrorText = "The text entered contains invalid character.";
                e.Valid = false;
            }

            if (inputValue.Length > 50)
            {
                e.ErrorText = "The text entered exceeds the maximum length.";
                e.Valid = false;
            }

            if (inputValue.Length == 0)
            {
                e.ErrorText = "Zero.";
                e.Valid = false;
            }

        }
    }
}
