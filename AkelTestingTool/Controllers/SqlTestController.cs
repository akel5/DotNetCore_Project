using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AkelTestingTool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkelTestingTool.Controllers
{
    public class SqlTestController : Controller
    {

       private readonly ApplicationDbContext _contx;

        public SqlTestController(ApplicationDbContext contx)
        {
            _contx = contx;
        }
        public string Index()
        {
            /* var Projectss = _contx.Projects.FromSql("Select * from Projects").ToList();
             StringBuilder sb = new StringBuilder();

             foreach (var pro in Projectss)
             {
                 sb.Append(pro.PName + "\n");

             }
             return sb.ToString();*/




            /*
            int result = _contx.Database.ExecuteSqlCommand("DELETE FROM Projects WHERE PId=3");
                if (result>0)
                    return "Deleted";
                        else return "Not Found";*/

           // working Native SQL statement -Select
            DbConnection con = _contx.Database.GetDbConnection();
            con.Open();
            DbCommand com = con.CreateCommand();
            com.CommandText = "SELECT PName FROM Projects";
            com.CommandType = System.Data.CommandType.Text;
            DbDataReader dr = com.ExecuteReader();
            StringBuilder data = new StringBuilder();

            while (dr.Read())
            {
                data.AppendLine(dr.GetString(0));
            }

            return data.ToString();






        }

    }

}