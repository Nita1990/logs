using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace LogService.Controllers
{
    public class LogsController : ApiController
    {
        [Route("api/logerror")]
        public IHttpActionResult Post([FromBody]Error error)
        {
            var path = HttpContext.Current.Server.MapPath("~") + "\\logs.txt";
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"time of record:{DateTime.Now} error:{error.ProjectName} {error.Level} {error.Text}");
            }

            return Ok();
        }

        public class Error
        {
            public string ProjectName { get; set; }
            public string Text { get; set; }
            public string Level { get; set; }
        }
    }
}
