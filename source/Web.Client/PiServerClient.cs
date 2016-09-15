using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Web.Client
{
    public class PiServerClient
    {
        public string RootUrl { get; set; }

        public PiServerClient()
        {
            RootUrl = "http://localhost:4091";
        }

        /// <summary>
        /// http://www.asp.net/web-api/overview/formats-and-model-binding/bson-support-in-web-api-21
        /// </summary>
        public async Task PostBson(Image<Bgr, byte> image)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RootUrl);

                // Set the Accept header for BSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
                
                // POST using the BSON formatter.
                MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                var result = await client.PostAsync("api/pi/PostImage", image, bsonFormatter);
                result.EnsureSuccessStatusCode();
            }
        }
    }
}
