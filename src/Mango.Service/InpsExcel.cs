using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Data.OleDb;
using System.Data;
using Mango.Model.Model;
using Mango.Model;

namespace Mango.Service
{
    public class InpsExcel
    {
        private const string XLS_FILE_EXTENSION = ".xls";
        private const string XLSX_FILE_EXTENSION = ".xlsx";

        public List<Inps> Read(string filePath, Period period, InpsType inpsType)
        {
            try
            {
                DataTable excelData = ReadDataTable(filePath);

                if (excelData == null || excelData.Rows.Count == 0)
                {
                    //FileManager fm = new FileManager(;
                    throw new Exception("No data found on excel!");
                }

                List<Inps> npss = new List<Inps>();
                for (int i = 0; i < excelData.Rows.Count; i++)
                {
                    Inps nps = new Inps();
                    nps.Staff = new Model.Staff();
                    nps.ResponsibleDepartment = new Department();

                    //nps.Perspective = new MetricsPerspective() { Id = 1 };

                    nps.Type = inpsType;
                    nps.Staff.Id = excelData.Rows[i][0] == DBNull.Value ? null : Convert.ToString(excelData.Rows[i][0]);
                    nps.Kpi = excelData.Rows[i][1] == DBNull.Value ? null : Convert.ToString(excelData.Rows[i][1]);
                    nps.Measure = excelData.Rows[i][2] == DBNull.Value ? null : Convert.ToString(excelData.Rows[i][2]);
                    nps.DataSource = excelData.Rows[i][3] == DBNull.Value ? null : Convert.ToString(excelData.Rows[i][3]);
                    nps.ResponsibleDepartment.Id = excelData.Rows[i][4] == DBNull.Value ? null : Convert.ToString(excelData.Rows[i][4]);
                    nps.Target = excelData.Rows[i][5] == DBNull.Value ? 0 : Convert.ToDecimal(excelData.Rows[i][5]);
                    nps.Score = excelData.Rows[i][6] == DBNull.Value ? 0 : Convert.ToDecimal(excelData.Rows[i][6]);
                    nps.Period = period;

                    npss.Add(nps);
                }
                
                excelData.Clear();
                
                return npss;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetFileExtension(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                return fi.Extension;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private DataTable ReadDataTable(string filePath)
        {
            try
            {
                string connString = "";
                string fileExtension = GetFileExtension(filePath);

                if (fileExtension == XLS_FILE_EXTENSION)
                {
                    connString = string.Format(DataAccess.DataAccess.Excel03ConnString, filePath);
                }
                else if (fileExtension == XLSX_FILE_EXTENSION)
                {
                    connString = string.Format(DataAccess.DataAccess.Excel07ConnString, filePath);
                }

                //conStr = String.Format(conStr, FilePath, isHDR);
                OleDbConnection connExcel = new OleDbConnection(connString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable excelData = new DataTable("ExcelData");
                cmdExcel.Connection = connExcel;

                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                sheetName = sheetName.Replace("''", "'");
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(excelData);
                connExcel.Close();

                return excelData;
            }
            catch (Exception)
            {
                throw;
            }
        }







    }

}