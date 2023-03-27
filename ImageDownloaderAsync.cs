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
        List<string> _listOfUriOfImages;
        public bool IsCompleted { get; private set; }
        CancellationTokenSource? _cancellTokenSource;

        public ImageDownloaderAsync(List<string> listOfUriOfImages)
        {
            _listOfUriOfImages = listOfUriOfImages;
        }

        /// <summary>
        /// Метод, который скачивает изображение.
        /// </summary>
        public async Task DownloaderAsync()
        {
            var tasks = new List<Task>();
            _cancellTokenSource = new CancellationTokenSource();
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
                    }, _cancellTokenSource.Token));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            try
            {
                await Task.WhenAll(tasks);
                ImageCompleted?.Invoke(this, EventArgs.Empty);
                IsCompleted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка скачивания");
                Console.WriteLine(ex.Message);
            }
        }

        public void Cancel()
        {
            _cancellTokenSource?.Cancel();
        }
    }
}
