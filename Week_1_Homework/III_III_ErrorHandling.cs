using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Week_1_Homework
{
    // This file demonstrates error handling in an API example.
    public class ErrorHandlingHttpServer
    {
        public void Start()
        {
            HttpListener listener = new();
            listener.Prefixes.Add("http://localhost:5002/");
            listener.Start();
            Console.WriteLine("Server started. Listening on http://localhost:5002/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                try
                {
                    if (request.Url.AbsolutePath == "/error" && request.HttpMethod == "GET")
                    {
                        HandleErrorEndpoint(response);
                    }
                    else
                    {
                        HandleNotFound(response);
                    }
                }
                catch (Exception ex)
                {
                    HandleServerError(response, ex);
                }
            }
        }

        private void HandleErrorEndpoint(HttpListenerResponse response)
        {
            throw new InvalidOperationException("This is a simulated error.");
        }

        private void HandleNotFound(HttpListenerResponse response)
        {
            WriteResponse(response, "Endpoint not found", 404);
        }

        private void HandleServerError(HttpListenerResponse response, Exception ex)
        {
            string errorResponse = $"Server Error: {ex.Message}";
            WriteResponse(response, errorResponse, 500);
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
            ErrorHandlingHttpServer server = new();
            server.Start();
        }
    }
}
