using System.Net.WebSockets;
using System.Net;
using System.Text;
using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;

namespace Moodle.API
    
{
    public class WebSocketServer
    {
        
        private readonly string _ipAddress;
        private readonly int _port;

        public WebSocketServer(string ipAddress, int port)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public async Task Run()
        {
            var host = new WebHostBuilder()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Parse(_ipAddress), _port);
                })
                .Configure(app =>
                {
                    app.UseWebSockets();
                    app.Use(async (HttpContext context, Func<Task> next) =>
                    {
                        if (context.WebSockets.IsWebSocketRequest)
                        {
                            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                            await HandleWebSocket(webSocket);
                        }
                        else
                        {
                            context.Response.StatusCode = 400;
                        }
                    });
                })
                .Build();

            await host.RunAsync();

            Console.WriteLine($"WebSocket server is up and running on {_ipAddress}:{_port}");
        }



        private async Task ReceiveMessagesAsync(WebSocket webSocket, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    // Extract the message from the buffer
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received: {message}");

                    // Broadcast the message to all connected clients
                    foreach (var client in _clients.Values)
                    {
                        if (client.State == WebSocketState.Open)
                        {
                            await client.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    // If the client sends a close message, break out of the loop
                    break;
                }
            }
        }


        private readonly ConcurrentDictionary<Guid, WebSocket> _clients = new ConcurrentDictionary<Guid, WebSocket>();

        private async Task HandleWebSocket(WebSocket webSocket)
        {
            try
            {
                // Create a cancellation token source for handling client disconnections
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                // Generate a new Guid for the client
                Guid clientId = Guid.NewGuid();

                // Add the WebSocket to the clients dictionary
                _clients[clientId] = webSocket;

                // Start a new task to handle messages from this WebSocket connection
                Task receiveTask = ReceiveMessagesAsync(webSocket, cancellationTokenSource.Token);

                // Wait for the receive task to complete or for the client to disconnect
                await receiveTask;

                // Remove the WebSocket from the clients dictionary
                _clients.TryRemove(clientId, out _);

                // Close the WebSocket connection
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "WebSocket connection closed", CancellationToken.None);
            }
            catch (WebSocketException ex)
            {
                // Handle WebSocket exceptions
                Console.WriteLine($"WebSocket exception: {ex.Message}");
            }
        }




















        /*

        private async Task HandleWebSocket(WebSocket webSocket)
        {
            try
            {
                // Create a cancellation token source for handling client disconnections
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                // Start a new task to handle messages from this WebSocket connection
                Task receiveTask = ReceiveMessagesAsync(webSocket, cancellationTokenSource.Token);

                // Wait for the receive task to complete or for the client to disconnect
                await receiveTask;
            }
            catch (WebSocketException ex)
            {
                // Handle WebSocket exceptions
                Console.WriteLine($"WebSocket exception: {ex.Message}");
            }
            finally
            {
                // Close the WebSocket connection gracefully
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "WebSocket connection closed", CancellationToken.None);
            }
        }






        private async Task ReceiveMessagesAsync(WebSocket webSocket, CancellationToken cancellationToken)
        {
            byte[] buffer = new byte[1024 * 4];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Console.WriteLine($"Received: {message}");

                    byte[] responseBuffer = Encoding.UTF8.GetBytes($"Echo: {message}");
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer), WebSocketMessageType.Text, true, CancellationToken.None);
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
                else
                {
                    // Handle unexpected message types
                    Console.WriteLine($"Unexpected message type: {result.MessageType}");
                    await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes("Error: Unexpected message type")), WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }




        */













    }

}
