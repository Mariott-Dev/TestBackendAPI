using System;
using System.Net.Sockets;
using System.Text;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackendAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlockController : Controller
    {
        static byte[] GetLength(uint length)
        {
            return new byte[]
            {
                (byte)((length >> 24) & 0xFF),
                (byte)((length >> 16) & 0xFF),
                (byte)((length >> 8) & 0xFF),
                (byte)(length & 0xFF)
            };
        }

        [HttpGet]
        [EnableCors]
        public IActionResult Index()
        {
            // Use the provided JSON object to send
            //string jsonMessage = @"{
            //    ""header"": {
            //        ""name"": ""GetAllBlockDocs"",
            //        ""type"": ""command"",
            //        ""id"": ""0""
            //    }
            //}";
            JToken jAppSettings = JToken.Parse(
                System.IO.File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "appsettings.json"))
            );
            string jsonMessage = jAppSettings["JsonMessage"]!.ToString();  //will eventually need 'switch' for multiple commands

            // Connect to the TCP server
            string ipAddress = "127.0.0.1";
            int port = 30000;

            try
            {
                using (TcpClient client = new TcpClient(ipAddress, port))
                using (NetworkStream stream = client.GetStream())
                {
                    // Prepare the message with the header and length
                    byte[] header = Encoding.UTF8.GetBytes("BBSYNC");
                    byte[] length = GetLength((uint)jsonMessage.Length);
                    byte[] payload = Encoding.UTF8.GetBytes(jsonMessage);
                    byte[] message = new byte[header.Length + length.Length + payload.Length];
                    Buffer.BlockCopy(header, 0, message, 0, header.Length);
                    Buffer.BlockCopy(length, 0, message, header.Length, length.Length);
                    Buffer.BlockCopy(payload, 0, message, header.Length + length.Length, payload.Length);

                    // Send the message
                    stream.Write(message, 0, message.Length);
                    Console.WriteLine("JSON message sent!");

                    // Receive the response header and length
                    byte[] responseHeader = new byte[6];
                    byte[] responseLengthBytes = new byte[4];
                    stream.Read(responseHeader, 0, responseHeader.Length);
                    stream.Read(responseLengthBytes, 0, responseLengthBytes.Length);
                    uint responseLength = (uint)((responseLengthBytes[0] << 24) | (responseLengthBytes[1] << 16) | (responseLengthBytes[2] << 8) | responseLengthBytes[3]);

                    // Read the JSON payload based on the length
                    byte[] responseData = new byte[responseLength];
                    int bytesRead = 0;
                    while (bytesRead < responseLength)
                    {
                        bytesRead += stream.Read(responseData, bytesRead, (int)responseLength - bytesRead);
                    }

                    string response = Encoding.UTF8.GetString(responseData);
                    Console.WriteLine($"Received message: {response}");

                    return Ok(response);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");

                return StatusCode(500, e);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception: {e}");

                return StatusCode(500, e);
            }
        }
    }
}
