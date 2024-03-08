using System;
using System.IO;
using System.Net.Sockets;

class Client2
{
    static void Main()
    {
        Console.WriteLine("Opretter forbindelse ");
        // string serverIp = Console.ReadLine();

        // Opret en TcpClient og forbind til serveren
        TcpClient client = new TcpClient("localhost", 5001);


        // Åbn en netværksstrøm baseret på TcpClient
        NetworkStream networkStream = client.GetStream();
        StreamReader reader = new StreamReader(networkStream);
        StreamWriter writer = new StreamWriter(networkStream) { AutoFlush = true };

        // Modtag velkomstbesked fra serveren
        string welcomeMessage = reader.ReadLine();
        Console.WriteLine($"Modtaget besked fra server: {welcomeMessage}");

        // Indtast og send beskeder til serveren
        Console.WriteLine("Skriv 'exit' for at afslutte.");

        string message;
        do
        {
            Console.Write("Skriv en besked: ");
            message = Console.ReadLine();

            // Send besked til serveren
            writer.WriteLine(message);
        }
        while(message.ToLower() != "exit");

        // Luk forbindelsen
        reader.Close();
        writer.Close();
        networkStream.Close();
        client.Close();
    }
}
