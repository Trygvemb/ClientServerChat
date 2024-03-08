using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Client1
{
    static void Main()
    {
        string ipAd = "172.16.12.18";
        int port = 5001;

        Console.WriteLine("connecting...");

        TcpClient client = new TcpClient(ipAd, port);

        Console.WriteLine("Connected");

        // open network based on TcpClient
        NetworkStream networkStream = client.GetStream();
        StreamReader reader = new StreamReader(networkStream);
        StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

        string serverMessage = reader.ReadLine();
        Console.WriteLine($"from server: {serverMessage}");

        string clientMessage;

        // Send and receive data
        do
        {
            // Write and send message to server
            clientMessage = Console.ReadLine();
            writer.WriteLine(clientMessage);

            // Receive and read message from server
            serverMessage = reader.ReadLine();
            Console.WriteLine($"Server: {serverMessage}");
        }
        while (clientMessage.ToLower() != "quit" || serverMessage.ToLower() != "quit");

        // close connections
        reader.Close();
        writer.Close();
        networkStream.Close();
        client.Close();
    }
}
