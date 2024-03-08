using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Server
{
    static void Main()
    {
        IPAddress ipAd = IPAddress.Parse("172.16.12.18");
        int port = 5001;

        TcpListener listener = new TcpListener(ipAd, port);
        listener.Start();

        Console.WriteLine($"The server is running at port {port}...");
        Console.WriteLine("The local End point is  :" + listener.LocalEndpoint);
        Console.WriteLine("Waiting for a connection.....");

        // Creates and accept Tcp client
        TcpClient client = listener.AcceptTcpClient();

        Console.WriteLine("Connection accepted!");

        // open network based on TcpClient
        NetworkStream networkStream = client.GetStream();
        StreamReader reader = new StreamReader(networkStream);
        StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

        writer.WriteLine("Welcome to the chat write quit to exit!");
        Console.WriteLine("Write quit to exit chat");
        
        string client1Message = "";
        string serverMessage = "";

        // Send and receive data
        do
        {
            // Receive and read message from Client1
            client1Message = reader.ReadLine();
            Console.WriteLine($"Client1: {client1Message}");

            // Write and send message to Client1
            serverMessage = Console.ReadLine();
            writer.WriteLine(serverMessage);
        }
        while (client1Message.ToLower() != "quit" || serverMessage.ToLower() != "quit");


        // Close connections and stop listening
        reader.Close();
        writer.Close();
        networkStream.Close();
        client.Close();
        listener.Stop();
    }
}
