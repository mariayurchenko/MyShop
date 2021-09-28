using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SB.Actions.Senders.QueueMessageSender
{
    public sealed class HttpQueueSender : IHttpQueueSender, IDisposable
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public HttpQueueSender(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            var dictionary = connectionString
                .Split(';')
                .Select(kvp => kvp.Split(
                    new[]
                    {
                        '='
                    }, 2))
                .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim());

            var endpoint = dictionary["Endpoint"].Replace("sb://", "https://");
            var entityPass = dictionary["EntityPath"];
            var keyName = dictionary["SharedAccessKeyName"];
            var key = dictionary["SharedAccessKey"];
            var sasToken = GetSasToken(endpoint + entityPass, keyName, key);

            _httpClient.BaseAddress = new Uri(endpoint + entityPass + "/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("SharedAccessSignature", sasToken);
            _httpClient.DefaultRequestHeaders.Add("ContentType", "application/atom+xml;type=entry;charset=utf-8");
        }

        private static string GetSasToken(string uri, string keyName, string key)
        {
            var num = Convert.ToUInt32((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds) + 1200U;
            var s = Uri.EscapeDataString(uri) + "\n" + num;
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var base64String = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(s)));
                return $"sr={Uri.EscapeDataString(uri)}&sig={Uri.EscapeDataString(base64String)}&se={num}&skn={keyName}";
            }
        }

        public async Task<HttpResponseMessage> SendAsync(byte[] body)
        {
            if (body == null || body.Length <= 0)
                throw new ArgumentNullException(nameof(body));

            using (var content = new ByteArrayContent(body))
            {
                var response = await _httpClient.PostAsync("messages?timeout=60", content).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();

                return response;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}