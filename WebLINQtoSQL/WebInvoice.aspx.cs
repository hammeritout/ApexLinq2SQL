using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LINQPad;
using System.IO;

namespace WebLINQtoSQL
{
    public partial class WebInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            if (!IsPostBack)
            {
                calStart.Visible = false;
                calEnd.Visible = false;

            }


                        
           
        }

        protected void imgButton_Click(object sender, ImageClickEventArgs e)
        {
            calStart.Visible = true;
        }

        protected void imgeButton_Click(object sender, ImageClickEventArgs e)
        {
            calEnd.Visible = true;
        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
           

            getSaleData();

        }

        protected void fndFirstDayofMonth()
        {
            DateTime dCalcDate = calStart.SelectedDate;
            // changes month to previous month.
            dCalcDate = dCalcDate.AddMonths(-1);
            // changes date to first date value 01.  
            DateTime startDate = new DateTime(dCalcDate.Year, dCalcDate.Month, 1);
            //displays value of start date.
            //change value of calstart the startDate value was not found in getSalesData method.
            calStart.SelectedDate = startDate;
            txtStartDate.Text = startDate.ToShortDateString();


        }

        private void fndLastDayofMonth()
        {
            DateTime deCalcDate = calEnd.SelectedDate;
            // changes month to previous month.
            deCalcDate = deCalcDate.AddMonths(-1);
            // changes date to last day of month which uses DaysInMonth.  
            DateTime endDate = new DateTime(deCalcDate.Year, deCalcDate.Month, DateTime.DaysInMonth(deCalcDate.Year, deCalcDate.Month));
            //displays value of end date.
            //change value of calEnd to endDate. endDate was not found in getSaleData method.
            calEnd.SelectedDate = endDate;
            txtEndDate.Text = endDate.ToShortDateString();


        }

        private void getSaleData()
        {
            // creating an instance of the invoice datacontext naming it dbContext
            InvoiceDataContext dbContext = new InvoiceDataContext();
            // write a linq query 
            var linqQuery = from SalesOrderHeader in dbContext.SalesOrderHeaders
                            join SalesOrderDetail in dbContext.SalesOrderDetails
                            on SalesOrderHeader.SalesOrderID equals SalesOrderDetail.SalesOrderID
                            join SalesCustomer in dbContext.Customers
                            on SalesOrderHeader.CustomerID equals SalesCustomer.CustomerID
                            join ProductionProduct in dbContext.Products
                            on SalesOrderDetail.ProductID equals ProductionProduct.ProductID
                            join Person in dbContext.Persons
                            on SalesCustomer.PersonID equals Person.BusinessEntityID
                            join SalesStore in dbContext.Stores
                            on SalesCustomer.StoreID equals SalesStore.BusinessEntityID  
                            // need a date comparison here to select records that from Order Date compared to user entry. 
                            // purchase order number is not displaying.
                            where SalesOrderHeader.DueDate >= calStart.SelectedDate && SalesOrderHeader.DueDate <= calEnd.SelectedDate 
                           // where SalesOrderHeader.DueDate <= calEnd.SelectedDate
                           orderby SalesCustomer.AccountNumber

                            select new
                            {
                                    SoldAt = SalesStore.Name, 
                                    SoldTo = Person.FirstName + " " + Person.LastName,

                                   SalesCustomer.AccountNumber,
                                   SalesOrderHeader.SalesOrderNumber,
                                   SalesOrderHeader.PurchaseOrderNumber,
                                   OrderDate = SalesOrderHeader.OrderDate.ToShortDateString(),
                                   DueDate = SalesOrderHeader.DueDate.ToShortDateString(),
                                   InvoiceTotal = SalesOrderHeader.TotalDue, 
                                   
                                   ProductionProduct.ProductNumber,
                                   SalesOrderDetail.OrderQty,
                                   UnitNet= SalesOrderDetail.UnitPrice,
                                   SalesOrderDetail.LineTotal, 


               };
            GridView1.DataSource = linqQuery;
          //  GridView1.DataSource = linqQuery;

            GridView1.DataBind();
        }

        protected void calStart_SelectionChanged(object sender, EventArgs e)
        {
            //displays the selected calendar date
            txtStartDate.Text = calStart.SelectedDate.ToShortDateString();
            calStart.Visible = false;
        }

        protected void calEnd_SelectionChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = calEnd.SelectedDate.ToShortDateString();
            calEnd.Visible = false;
            fndFirstDayofMonth();

            fndLastDayofMonth();
        }

        private void export2Excel()
        {
            // exports to an Excel Spreadsheet.
            Response.ClearContent();
            Response.AppendHeader("content-disposition", "attachment, filename=InvoiceData.xls");
            Response.ContentType = "application/excel";


            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            GridView1.HeaderRow.Style.Add("background-color", "#FFFFFF");

            foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
            {
                tableCell.Style["backfround-color"] = "#A55129";

            }

            foreach (GridViewRow gridViewRow in GridView1.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFF7E7";
                }

            }

            GridView1.RenderControl(htmlTextWriter);
            Response.Write(stringWriter.ToString());
            Response.End();


        }

          


    protected void btnExport_Click(object sender, EventArgs e)

    {
        export2Excel();
    } 
          
        public override void VerifyRenderingInServerForm(Control control)
      {
            
       }
    }
}