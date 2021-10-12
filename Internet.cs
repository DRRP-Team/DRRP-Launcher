using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DRRP_Launcher {
    class Internet {
        public static string Get(string url) {
            using (var client = new WebClient()) {
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                client.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36");
                return client.DownloadString(url);
            }
        }

        public static object GetJson(string url) {
            var rnd = new Random();
            return JsonConvert.DeserializeObject(Get(url + "?seed=" + rnd.Next().ToString()));
        }

        public static JArray GetJsonArray(string url) {
            return JArray.Parse(Get(url));
        }

        public static JObject GetJsonObject(string url) {
            return JObject.Parse(Get(url));
        }

        public static void Download(string url, string destination) {
            var client = new WebClient();
            client.DownloadFile(url, destination);
        }
    }
}
