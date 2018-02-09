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
                {
                    gridView.ShowEditForm();
                }
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
                    if (XtraMessageBox.Show("Are you sure to delete selected record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var selectedRecordForDeletion = GetRowSelectedData();
                        if (selectedRecordForDeletion.Id == 0 || selectedRecordForDeletion == null) return;
                        var record = new clsEmployeeRecord();
                        record.ServiceRequestedData(
                            string.Format("{0}/{1}", UrlRoute.REMOVE.ToString(), selectedRecordForDeletion.Id), selectedRecordForDeletion, HttpRequestVerb.DELETE
                            );
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
                var IsCanel = EditFormCancelButtonWasClicked(sender);
                if (IsCanel) return;
                var data = GetRowSelectedData();
                if(!string.IsNullOrWhiteSpace(data.FirstName) && !string.IsNullOrWhiteSpace(data.LastName))
                {
                    CreateRequest(data);
                    gridView.CloseEditForm();
                    RefreshDataGrid();
                    XtraMessageBox.Show("Successfully saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
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
            var record = new clsEmployeeRecord();
            var data = record.GetEmployeeData();
            return data;
        }

        private void OnShowingPopupEditForm(object sender, DevExpress.XtraGrid.Views.Grid.ShowingPopupEditFormEventArgs e)
        {
            foreach (Control control in e.EditForm.Controls)
            {
                if (!(control is EditFormContainer))
                {
                    continue;
                }
                foreach (Control nestedControl in control.Controls)
                {
                    if (!(nestedControl is PanelControl))
                    {
                        continue;
                    }
                    foreach (Control button in nestedControl.Controls)
                    {
                        if (!(button is SimpleButton))
                        {
                            continue;
                        }
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
            var IsCanel = buttonType.Equals("EditFormCancelButton", StringComparison.CurrentCultureIgnoreCase);
            return IsCanel;
        }

        private void CreateRequest(ResponseData data)
        {
            var record = new clsEmployeeRecord();

            if(data.Id == 0)
                record.ServiceRequestedData(UrlRoute.ADD.ToString(), data, HttpRequestVerb.POST);
            else
                 record.ServiceRequestedData(string.Format("{0}/{1}", UrlRoute.EDIT.ToString(), data.Id), data, HttpRequestVerb.PUT);
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
