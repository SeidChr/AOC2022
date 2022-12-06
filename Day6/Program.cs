var content = File.ReadAllText("input.txt");
var foundStartOfPacket = false;
for (var i = 3; i < content.Length; i++)
{
    if (!foundStartOfPacket && content.Substring(i - 3, 4).Distinct().Count() == 4)
    {
        Console.WriteLine($"Packet: {i + 1} - {content[i - 3]}{content[i - 2]}{content[i - 1]}{content[i]} ");
        foundStartOfPacket = true;
    }

    if (i >= 14 && content.Substring(i - 13, 14).Distinct().Count() == 14)
    {
        Console.WriteLine($"Message: {i + 1}");
        break;
    }
}