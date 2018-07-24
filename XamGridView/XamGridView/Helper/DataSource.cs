using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace XamGridView.Helper
{
    public class DataSource
    {
        public static List<Dictionary<string, object>> GetOrders()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            string[] resources = assembly.GetManifestResourceNames();
            Stream stream = assembly.GetManifestResourceStream("XamGridView.sample_data.json");
            string json = string.Empty;

            using (var reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            Wraps ws = JsonConvert.DeserializeObject<Wraps>(json);
            return ws.wraper;
        }

        public class Wraps
        {
            public List<Dictionary<string, object>> wraper { get; set; }
        }
    }
}
