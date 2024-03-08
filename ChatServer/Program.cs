using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        // Opret en TcpListener på port 5000
        TcpListener listener = new TcpListener(IPAddress.Any, 5001);
        listener.Start();

        Console.WriteLine("Venter på forbindelse...");

        // Accepter en forbindelse fra en klient
        TcpClient client = listener.AcceptTcpClient();

        Console.WriteLine("Forbindelse accepteret!");

        // Åbn en netværksstrøm baseret på klientens TcpClient
        NetworkStream networkStream = client.GetStream();
        StreamReader reader = new StreamReader(networkStream);
        StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

        // Send og modtag data
        writer.WriteLine("Velkommen til serveren!");

        string message;
        do
        {
            message = reader.ReadLine();
            Console.WriteLine($"Modtaget besked fra klient: {message}");

            writer.WriteLine($"Modtog besked ({message}) fra klient");
        }
        while(message.ToLower() != "exit");


        // Luk forbindelsen
        reader.Close();
        writer.Close();
        networkStream.Close();
        client.Close();

        // Stop lytning
        listener.Stop();
    }
}
