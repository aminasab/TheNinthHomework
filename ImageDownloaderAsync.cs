using System.Net;
using System.Threading;

namespace TheNinthProgram
{
    internal class ImageDownloaderAsync
    {
        public event EventHandler ImageStarted;
        public event EventHandler ImageCompleted;
        // Как назовем файл на диске
        string _fileName;
        int i = 1;
        List <Task> tasks = new List<Task>();
        List<string> _listOfUriOfImages;

        public ImageDownloaderAsync(List<string> listOfUriOfImages)
        {
            _listOfUriOfImages = listOfUriOfImages;
        }

        /// <summary>
        /// Метод, который скачивает изображения.
        /// </summary>
        public async Task DownloaderAsync(CancellationToken cancellationToken)
        {
            foreach (var url in _listOfUriOfImages)
            {
                _fileName = $"image{i}.jpg";
                i++;
                try
                {
                    tasks.Add(Task.Run(async () =>
                    {
                        using (var myWebClient = new WebClient())
                        {
                            ImageStarted?.Invoke(this, EventArgs.Empty);
                            await myWebClient.DownloadFileTaskAsync(url, _fileName);
                        }
                    }, cancellationToken));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                } 
            }
            
            
        }
        public bool IsCompletedAllTasks()
        {
            bool result = true;
            for (int i = 0; i < 10; i++)
            {
                tasks.ForEach(task =>
                {
                    if (!task.IsCompleted)
                    {
                        result = false;
                    }
                });
            }
            if (result == true)
            {
                ImageCompleted?.Invoke(this, EventArgs.Empty);
            }
            return result;
        }
    }
}
