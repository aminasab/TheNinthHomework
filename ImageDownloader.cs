using System.Net;

namespace TheNinthProgram
{
    internal class ImageDownloader
    {
        // Откуда будем качать
        string _remoteUri;
        // Как назовем файл на диске
        string _fileName;
        public ImageDownloader(string remoteUriOfImage)
        {
            _remoteUri = remoteUriOfImage;
            _fileName ="image.jpg";
        }

        /// <summary>
        /// Метод, который скачивает изображение.
        /// </summary>
        public void Downloader()
        {
            ImageStarted?.Invoke(this, EventArgs.Empty);
            WebClient myWebClient = new WebClient();
            Console.WriteLine("Качаю \"{0}\" из \"{1}\"", _fileName, _remoteUri);
            myWebClient.DownloadFile(_remoteUri, _fileName);
            Console.WriteLine("Успешно скачал \"{0}\" из \"{1}\"", _fileName, _remoteUri);
            ImageCompleted?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler ImageStarted;
        public event EventHandler ImageCompleted;
    }
}
