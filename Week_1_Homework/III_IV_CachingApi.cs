using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Week_1_Homework
{
    // This file demonstrates API response caching using a simple in-memory approach.
    public class CachingHttpServer
    {
        private readonly List<string> _data = new()
        {
            "Cached Item 1", "Cached Item 2", "Cached Item 3", "Cached Item 4", "Cached Item 5"
        };

        private string _cachedResponse = null;
        private DateTime _cacheExpirationTime;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromSeconds(30);

        public void Start()
        {
            HttpListener listener = new();
            listener.Prefixes.Add("http://localhost:5003/");
            listener.Start();
            Console.WriteLine("Server started. Listening on http://localhost:5003/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                if (request.Url.AbsolutePath == "/cached-data" && request.HttpMethod == "GET")
                {
                    HandleCachedRequest(response);
                }
                else
                {
                    HandleNotFound(response);
                }
            }
        }

        private void HandleCachedRequest(HttpListenerResponse response)
        {
            if (_cachedResponse == null || DateTime.Now > _cacheExpirationTime)
            {
                Console.WriteLine("Cache expired or not set. Generating new response.");

                // Simulate a delay, e.g., fetching data from a database
                Thread.Sleep(1000);

                _cachedResponse = JsonSerializer.Serialize(_data);
                _cacheExpirationTime = DateTime.Now.Add(_cacheDuration);
            }
            else
            {
                Console.WriteLine("Returning cached response.");
            }

            WriteResponse(response, _cachedResponse, 200);
        }

        private void HandleNotFound(HttpListenerResponse response)
        {
            WriteResponse(response, "Endpoint not found", 404);
        }

        private void WriteResponse(HttpListenerResponse response, string content, int statusCode)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.StatusCode = statusCode;
            using var output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CachingHttpServer server = new();
            server.Start();
        }
    }
}
