namespace EFOracle
{
    public static class Utility
    {
        public static void PrintTitle(string text, char separator)
        {
            string line = "";
            string border = "";

            for (int i = 0; i < 60; i++)
                line += separator;

            for (int i = 0; i < 3; i++)
                border += separator;

            int totalLength = line.Length - ((border.Length + 1) * 2);
            int leftLength = (totalLength / 2) + (text.Length / 2) - (border.Length + 1);

            if (totalLength < 0) totalLength = 0;
            if (leftLength < 0) leftLength = 0;

            Console.WriteLine(line);
            Console.WriteLine($"{border} {text.PadLeft(leftLength).PadRight(totalLength)} {border}");
            Console.WriteLine(line);
        }
    }
}
