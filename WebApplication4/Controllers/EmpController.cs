using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication4.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace WebApplication4.Controllers
{
    public class EmpController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
          select EmpId,EmpName,Department,
         convert(varchar(10),Dateofjoining,120)as Dateofjoining,
              PhotoFilename
           from
           dbo.Emp
            ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpAppDB"].ConnectionString))


            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Emp emp)
        {
            try
            {
                string query = @"
               insert into dbo.Emp values
                  (
                '" + emp.EmpName + @"'
                   ,'" + emp.Department + @"'
                , '" + emp.Dateofjoining + @"'
                  ,'" + emp.PhotoFileName + @"'

                   )";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpAppDB"].ConnectionString))


                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Added successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Add!!";
            }
        }

        public string Put(Emp emp)
        {
            try
            {
                string query = @"
               update dbo.Emp set
                   EmpName='" + emp.EmpName + @"'
                  , Department='" + emp.Department + @"'
                   , DateOfJoining='" + emp.Dateofjoining + @"'
                    , PhotoFileName='" + emp.PhotoFileName + @"'
                where EmpId=" + emp.EmpId + @"
                       ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpAppDB"].ConnectionString))


                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Update successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Update!!";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"
               delete from dbo.Emp 
                 where EmpId=" + id + @"
                       ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpAppDB"].ConnectionString))


                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);

                }
                return "Delete successfully!!";
            }
            catch (Exception)
            {

                return "Failed to Delete!!";
            }
        }

        [Route("api/Emp/GetAllDepartmentName")]
        [HttpGet]
        public HttpResponseMessage GetAllEmpName()
        {
            string query = @"
               select DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["EmpAppDB"].ConnectionString))


            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);

            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    

        [Route("api/Emp/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photo/" + filename);
                postedFile.SaveAs(physicalPath);
                return filename;
            }
            catch(Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
