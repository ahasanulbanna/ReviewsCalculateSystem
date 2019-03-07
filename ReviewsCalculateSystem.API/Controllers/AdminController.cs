using ReviewsCalculateSystem.Models.Models;
using ReviewsCalculateSystem.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace ReviewsCalculateSystem.API.Controllers
{
    [RoutePrefix("api/admin")]
    public class AdminController : ApiController
    {
        private readonly IAdminServices service;
        private readonly IRegistrationService registrationService;
        public AdminController()
        {
            service = new AdminServices();
            registrationService = new RegistrationService();
        }

        [HttpPost]
        [Route("Login")]
        public HttpResponseMessage Login(Admin admin)
        {         
            string role = "Admin";
            var token = TokenManager.CreateJwtToken(admin.Name, role);
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ObjectContent<object>(new
                {
                    UserName = admin.Name,
                    Roles = role,
                    AccessToken = token
                }, Configuration.Formatters.JsonFormatter)
            };
        }

        [Authorize(Roles = "Admin")]
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


        [HttpPost]
        [Route("UserRegistration")]
        public IHttpActionResult UserRegistration(Registration model)
        {

            return Ok(registrationService.UserRegistration(model).Data);
        }

        [HttpGet]
        [Route("RegistrationConfirm")]
        public IHttpActionResult RegistrationConfirm(int Id, string Key)
        {
            return Ok(registrationService.UserRegistrationConfirm(Id, Key).Data);
        }




        [HttpPost]
        [Route("UploadImage")]
        public async Task<HttpResponseMessage> ImageUpload()
        {

            //HttpRequestHeaders headers = this.Request.Headers;
            //string Name = string.Empty;
            //string Phone = string.Empty;
            //if (headers.Contains("Phone"))
            //{
            //    Phone = headers.GetValues("Phone").First();
            //    Name = headers.GetValues("Name").First();

            //}


            // Check whether the POST operation is MultiPart?
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            // Prepare CustomMultipartFormDataStreamProvider in which our multipart form
            // data will be loaded.
            string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);
            List<string> files = new List<string>();

            try
            {
                // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.
                await Request.Content.ReadAsMultipartAsync(provider);
                var ObjectValue = provider.FormData;
                foreach (MultipartFileData file in provider.FileData)
                {
                    files.Add(Path.GetFileName(file.LocalFileName));
                }

                // Send OK Response along with saved file names to the client.
                return Request.CreateResponse(HttpStatusCode.OK, files);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }

    // We implement MultipartFormDataStreamProvider to override the filename of File which
    // will be stored on server, or else the default name will be of the format like Body-
    // Part_{GUID}. In the following implementation we simply get the FileName from 
    // ContentDisposition Header of the Request Body.
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        }

    }

    public class KeyGenerator
    {
        public static string GetUniqueKey()
        {
            int size = 10;
            char[] chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[size];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
    public class TestClass
    {
        public string name { get; set; }
        public int count { get; set; }
    }
}



