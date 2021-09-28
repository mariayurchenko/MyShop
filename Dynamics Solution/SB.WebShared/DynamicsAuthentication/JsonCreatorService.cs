using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace SB.WebShared.DynamicsAuthentication
{
    public static class JsonCreatorService
    {
        public static StringContent FormStringContent<T>(T obj, string actionName)
        {
            //var json = JsonConvert.SerializeObject(obj);
            var inputParameters = new InputParameters
            {
                ActionName = actionName,
                Parameters = obj is string ? Convert.ToString(obj): JsonConvert.SerializeObject(obj)
            };
            var data = JsonConvert.SerializeObject(inputParameters);
            var result = new StringContent(data, Encoding.UTF8, "application/json");

            return result;
        }
        private class InputParameters
        {
            public string ActionName { get; set; }
            public string Parameters { get; set; }
        }
    }
}