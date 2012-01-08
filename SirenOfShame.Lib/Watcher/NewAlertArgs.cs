using System;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace SirenOfShame.Lib.Watcher
{
    public class NewAlertArgs
    {
        public string Message { get; set; }
        public string Url { get; set; }
        public int SoftwareInstanceId { get; set; }

        private static readonly ILog _log = MyLogManager.GetLogger(typeof(RulesEngine));

        public bool Instantiate(string result)
        {
            List<string> results = result
                .Split('\n', '\r')
                .Where(i => !string.IsNullOrEmpty(i))
                .ToList();
            if (results.Count < 3)
            {
                _log.Error("Unable to parse alert response: " + result);
                return false;
            }

            try
            {
                SoftwareInstanceId = int.Parse(results.ElementAt(0));
                Url = results.ElementAtOrDefault(1);
                Message = results.ElementAtOrDefault(2);

                return true;
            } 
            catch (Exception ex)
            {
                _log.Error("Error while parsing alert response: " + result, ex);
                return false;
            }
        }
    }
}