using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid.Styles;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid;
using System.Globalization;
using Syncfusion.WinForms.DataGrid.Renderers;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.GridCommon.ScrollAxis;

namespace ComboBoxColumn
{   
    public partial class Form1 : Form
    {
        #region Constructor
        public Form1()
        {
            InitializeComponent();
            sfDataGrid.AutoGenerateColumns = false;
            sfDataGrid.DataSource = new CountryInfoRepository().OrderDetails;
            GridSettings();
        }

        private void GridSettings()
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalDigits = 0;
            nfi.NumberGroupSizes = new int[] { };

            sfDataGrid.Columns.Add(new GridNumericColumn() { MappingName = "OrderID", HeaderText = "Order ID", NumberFormatInfo = nfi });
            sfDataGrid.Columns.Add(new GridTextColumn() { MappingName = "CustomerID", HeaderText = "Customer ID" });
            sfDataGrid.Columns.Add(new GridTextColumn() { MappingName = "ProductName", HeaderText = "Product Name" });
            sfDataGrid.Columns.Add(new GridNumericColumn() { MappingName = "NoOfOrders", HeaderText = "No Of Orders", NumberFormatInfo = nfi });
            sfDataGrid.Columns.Add(new GridTextColumn() { MappingName = "ShipCountry", HeaderText = "Ship Country" });

            //To add custom ComboBox renderer to DataGrid
            this.sfDataGrid.CellRenderers.Remove("ComboBox");
            this.sfDataGrid.CellRenderers.Add("ComboBox", new GridComboBoxCellRendererExt(sfDataGrid));
            //To add a combo box column in grid.
            sfDataGrid.Columns.Add(new GridComboBoxColumn() { MappingName = "ShipCityID", HeaderText = "Ship City", ValueMember = "ShipCityID", DisplayMember = "ShipCityName", IDataSourceSelector = new CustomSelector() });

        }


        #endregion

    }

    public class GridComboBoxCellRendererExt : GridComboBoxCellRenderer
    {
        SfDataGrid dataGrid;
        SfComboBox SfCombo;
        public GridComboBoxCellRendererExt(SfDataGrid sfDataGrid)
        {
            dataGrid = sfDataGrid;
        }
        protected override void OnInitializeEditElement(DataColumnBase column, RowColumnIndex rowColumnIndex, SfComboBox uiElement)
        {
            SfCombo = uiElement;
            base.OnInitializeEditElement(column, rowColumnIndex, uiElement);

        }

        protected override void OnKeyUp(DataColumnBase dataColumn, RowColumnIndex rowColumnIndex, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Tab)
            {
                dataGrid.CurrentCell.BeginEdit();
                //To open the dropdown
                SfCombo.ShowDropDown();
            }
        }
    }
}
