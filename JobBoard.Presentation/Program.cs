namespace JobBoard.Presentation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            UI UI = new UI();
            await UI.Print();
        }
    }
}