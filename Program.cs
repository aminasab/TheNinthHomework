namespace TheNinthProgram
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource source = new();
            CancellationToken token = source.Token;
            static void c_ImageStarted(object sender, EventArgs e) => Console.WriteLine("Скачивание файла началось");
            static void c_ImageCompleted(object sender, EventArgs e) => Console.WriteLine("Скачивание файла закончилось");
            string urlOfImage = "https://freelance.today/uploads/images/00/07/62/2017/06/13/14c404.jpg";
            List<string> listOfUrl = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                listOfUrl.Add(urlOfImage);
            }
            ImageDownloaderAsync asyncImageDowloader = new(listOfUrl);
            asyncImageDowloader.ImageStarted += c_ImageStarted;
            asyncImageDowloader.ImageCompleted += c_ImageCompleted;
            asyncImageDowloader.DownloaderAsync(token);
            Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания");
            while (Console.ReadKey().Key != ConsoleKey.A)
            {
                Console.WriteLine(asyncImageDowloader.IsCompletedAllTasks() ? "\n Все загружены" : "\n Не все загружены");
            }
        }
    }
}