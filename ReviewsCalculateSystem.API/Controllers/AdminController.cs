using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ReviewsCalculateSystem.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminServices service;
        public AdminController()
        {
            service = new AdminServices();
        }

        [HttpPost]
        [Route("CreateAdmin")]
        public IHttpActionResult CreateAdmin(Admin admin)
        {
            return Ok(service.CreateAdmin(admin).Data);
        }

        [HttpGet]
        [Route("GetAllAdminList")]
        public IHttpActionResult GetAllAdminList()
        {
            return Ok(service.GetAllAdminList().Data);
        }

        [HttpGet]
        [Route("DatabaseBackUp")]
        public IHttpActionResult DatabaseBackUp()
        {
            // read connectionstring from config file
            var connectionString = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;

            // read backup folder from config file ("C:/temp/")
            var backupFolder = ConfigurationManager.AppSettings["BackupFolder"];

            var sqlConStrBuilder = new SqlConnectionStringBuilder(connectionString);

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}{1}-{2}.bak",
                backupFolder, sqlConStrBuilder.InitialCatalog,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(sqlConStrBuilder.ConnectionString))
            {
                /*Sql query like as:
                 * BACKUP DATABASE RCSDB TO DISK = 'C:/temp/RCSDB-2019-02-18.bak
                 */
                var query = String.Format("BACKUP DATABASE {0} TO DISK='{1}'",
                    sqlConStrBuilder.InitialCatalog, backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            return Ok("Datatabe backup successfully");
        }
    }
}
