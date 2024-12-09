using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Week_1_Homework
{
    public class SimpleHttpServer
    {
        private readonly List<string> _books = new()
        {
            "Book 1", "Book 2", "Book 3", "Book 4", "Book 5"
        };

        public void Start()
        {
            HttpListener listener = new();
            listener.Prefixes.Add("http://localhost:5000/");
            listener.Start();
            Console.WriteLine("Server started. Listening on http://localhost:5000/");

            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;

                if (request.Url.AbsolutePath == "/books" && request.HttpMethod == "GET")
                {
                    HandleGetAllBooks(response);
                }
                else if (request.Url.AbsolutePath.StartsWith("/books/") && request.HttpMethod == "GET")
                {
                    HandleGetBookById(request, response);
                }
                else if (request.Url.AbsolutePath == "/books" && request.HttpMethod == "POST")
                {
                    HandleAddBook(request, response);
                }
                else
                {
                    HandleNotFound(response);
                }
            }
        }

        private void HandleGetAllBooks(HttpListenerResponse response)
        {
            string json = JsonSerializer.Serialize(_books);
            WriteResponse(response, json, 200);
        }

        private void HandleGetBookById(HttpListenerRequest request, HttpListenerResponse response)
        {
            string[] segments = request.Url.AbsolutePath.Split('/');
            if (segments.Length < 3 || !int.TryParse(segments[2], out int id) || id < 0 || id >= _books.Count)
            {
                WriteResponse(response, "Book not found", 404);
                return;
            }

            string book = _books[id];
            WriteResponse(response, book, 200);
        }

        private void HandleAddBook(HttpListenerRequest request, HttpListenerResponse response)
        {
            using var reader = new System.IO.StreamReader(request.InputStream, request.ContentEncoding);
            string newBook = reader.ReadToEnd();

            if (string.IsNullOrWhiteSpace(newBook))
            {
                WriteResponse(response, "Invalid book name", 400);
                return;
            }

            _books.Add(newBook);
            WriteResponse(response, "Book added", 201);
        }

        private void HandleNotFound(HttpListenerResponse response)
        {
            WriteResponse(response, "Not Found", 404);
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
            SimpleHttpServer server = new();
            server.Start();
        }
    }
}
