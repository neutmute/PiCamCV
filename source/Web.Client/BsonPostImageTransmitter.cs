using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Emgu.CV;
using Emgu.CV.Structure;
using PiCamCV.Common.Interfaces;

namespace Web.Client
{
    public class BsonPostImageTransmitter : IImageTransmitter
    {
        public string RootUrl { get; set; }

        private Timer _retryTimer;
        private bool _enabled;
        private bool _transmitting;

        public BsonPostImageTransmitter()
        {
            RootUrl = $"http://{Config.ServerHost}:{Config.ServerPort}";
            _enabled = true;
            _transmitting = false;
        }
        
        /// <summary>
        /// http://www.asp.net/web-api/overview/formats-and-model-binding/bson-support-in-web-api-21
        /// </summary>
        public async Task Transmit(Image<Bgr, byte> image)
        {
            if (!_enabled)
            {
                return;
            }

            if (_transmitting)
            {
                return;
            }

            _transmitting = true;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RootUrl);

                // Set the Accept header for BSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/bson"));
                
                // POST using the BSON formatter.
                MediaTypeFormatter bsonFormatter = new BsonMediaTypeFormatter();
                try
                {
                    var result = await client.PostAsync("api/pi/PostImage", image, bsonFormatter);
                    result.EnsureSuccessStatusCode();
                }
                catch (Exception httpEx)
                {
                    const int retryMilliseconds = 20000;
                    Console.WriteLine($"BsonPost: {httpEx}. Retrying in {retryMilliseconds}");
                    _enabled = false;
                    _retryTimer = new Timer(retryMilliseconds);
                    _retryTimer.Elapsed += (sender, args) => _enabled = true;
                    _retryTimer.Start();
                }
                finally
                {
                    _transmitting = false;
                }
            }
        }
    }
}
