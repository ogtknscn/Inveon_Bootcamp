using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Week_1_Homework
{
    // This file demonstrates an API example with pagination implementation.
    public class PaginatedHttpServer
    {
        private readonly List<string> _data = new()
        {
            "Item 1", "Item 2", "Item 3", "Item 4", "Item 5",
            "Item 6", "Item 7", "Item 8", "Item 9", "Item 10"
        };

        public void Start()
        {
            HttpListener listener = new();
            listener.Prefixes.Add("http://localhost:5001/");
            listener.Start();
            Console.WriteLine("Server started. Listening on http://localhost:5001/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                if (request.Url.AbsolutePath == "/data" && request.HttpMethod == "GET")
                {
                    HandlePaginatedRequest(request, response);
                }
                else
                {
                    HandleNotFound(response);
                }
            }
        }

        private void HandlePaginatedRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            var queryParams = request.QueryString;
            if (!int.TryParse(queryParams["pageNumber"], out int pageNumber) || pageNumber <= 0)
            {
                pageNumber = 1; // Default to page 1 if not provided or invalid
            }

            if (!int.TryParse(queryParams["pageSize"], out int pageSize) || pageSize <= 0)
            {
                pageSize = 5; // Default to 5 items per page if not provided or invalid
            }

            var pagedData = _data.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            string jsonResponse = JsonSerializer.Serialize(pagedData);
            WriteResponse(response, jsonResponse, 200);
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
            PaginatedHttpServer server = new();
            server.Start();
        }
    }
}
