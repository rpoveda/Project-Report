using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        dsViewVendasTableAdapters.VendasTableAdapter tableAdapter = new dsViewVendasTableAdapters.VendasTableAdapter();
        dsViewVendas.VendasDataTable dataTable = new dsViewVendas.VendasDataTable();
        dataTable.Merge(tableAdapter.MeuSelect());

        DataSet ds = new DataSet();
        DataTable t = dataTable;
        

        Microsoft.Reporting.WebForms.ReportDataSource item = new Microsoft.Reporting.WebForms.ReportDataSource();
        item.Name = "dsViewVendas_VendasTableAdapter";
        item.Value = dataTable;

        rv.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
        rv.LocalReport.ReportPath = Server.MapPath("~/Report2.rdlc");
        //rv.LocalReport.DataSources.Add(item);
        rv.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("dsViewVendas_VendasDataTable", (DataTable)dataTable));
        rv.DataBind();

        GridView1.DataSource = dataTable;
        GridView1.DataBind();
    }

    private DataSet GetVendas()
    {
        DataSet ds = new DataSet();
        
        SqlConnection conn = new SqlConnection("Data Source=POVEDA-PC\\SQLEXPRESS;Initial Catalog=TestingReport;Integrated Security=True");
        SqlCommand command = new SqlCommand("Select * From Vendas", conn);
        SqlDataAdapter daVendas = new SqlDataAdapter(command);
        daVendas.Fill(ds, "Vendas");
        return ds;
    }
}
