using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string path);
    }

    class FileDownloader : IFileDownloader
    {
        private WebClient _webClient = new WebClient();

        public void DownloadFile(string url, string path)
        {
            var client = _webClient;

            client.DownloadFile(
                url,
                path);
        }
    }
}
