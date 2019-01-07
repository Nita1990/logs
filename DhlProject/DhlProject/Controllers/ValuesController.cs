using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Targets.Wrappers;
using System;
using System.Web.Http;

namespace DhlProject.Controllers
{
    public class ValuesController : ApiController
    {
        private static Logger _logger;

        private const string WebServiceUrl = "http://localhost:49883/api/logerror";

        // GET api/values
        public string Get()
        {
            var config = new LoggingConfiguration();

            var webServiceTarget =
                new WebServiceTarget
                {
                    Url = new Uri(WebServiceUrl),
                    Protocol = WebServiceProtocol.JsonPost
                };

            AsyncTargetWrapper asyncWebServiceTarget = new AsyncTargetWrapper(webServiceTarget);
            config.AddTarget("asyncWebService", asyncWebServiceTarget);

            webServiceTarget.Parameters.Add(new MethodCallParameter("ProjectName", "Packman"));
            webServiceTarget.Parameters.Add(new MethodCallParameter("Level", "${level}"));
            webServiceTarget.Parameters.Add(new MethodCallParameter("Text", "blabla"));

            var rule = new LoggingRule("*", LogLevel.Debug, asyncWebServiceTarget);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            _logger = LogManager.GetCurrentClassLogger();

            _logger.Debug("Using programmatic config");

            return "Hello world!";
        }
    }
}
