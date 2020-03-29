using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.Helpers;
using ChitankaMobileUI.Models;
using ChitankaMobileUI.Services;
using ChitankaMobileUI.ViewModels.Base;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

namespace ChitankaMobileUI.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        private bool isDownloadable = true;

        public ChitankaBookModel Book { get; set; }
        public string BookCover
        {
            get
            {
                string val = "";
                if (Book.Book.HasCover.HasValue)
                {
                    if (Book.Book.HasCover.Value)
                    {
                        val = "https://assets.chitanka.info/" + Book.Book.Cover;
                    }
                    else
                    {
                        val = "https://assets.chitanka.info/thumb/?book-cover/00/0.2500.png";
                    }
                }
                else
                {
                    val = "https://assets.chitanka.info/thumb/?book-cover/00/0.2500.png";
                }

                return val;
            }
        }
        public string BookYear { get { return Book.Book.Year.HasValue ? Book.Book.Year.Value.ToString() : ""; } }
        public bool IsDownloadable
        {
            get => isDownloadable;
            set
            {
                isDownloadable = value;
                OnPropertyChanged(nameof(IsDownloadable));
            }
        }

        public Command DownloadBookCommand { get; set; }

        public BookDetailViewModel(ChitankaBookModel book)
        {
            Book = book;
            StaticFileDownloader.Downloader.OnFileDownloaded += Downloader_OnFileDownloaded;
            InitCommands();
        }

        public void InitCommands()
        {
            DownloadBookCommand = new Command(() =>
            {
                StaticFileDownloader.Downloader.DownloadFile(Book.DownloadURL, "ChitankaDownloads",
                    $"{ Book.Book.Id }-{ Book.Book.Title }.epub");
            });
        }

        private async void Downloader_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            string folderId = GlobalConfig.Instance.Properties[GlobalConfig.Options.DSaveFolderId] as string;
            bool isFolderUsed = StaticFileDownloader.DownloadPaths.ContainsKey(folderId);
            if (!isFolderUsed)
            {
                StaticFileDownloader.DownloadPaths.Add(folderId, new List<string>());
            }

            if (!StaticFileDownloader.DownloadPaths[folderId].Contains(e.Path))
            {
                if (e.FileSaved)
                {
                    StaticFileDownloader.DownloadPaths[folderId].Add(e.Path);
                    IsDownloadable = false;
                    StaticDriveAPI.Instance.UploadFile(e.Path, Path.GetFileName(e.Path),
                        new List<string> { folderId },
                        "Electronic Publication/epub");
                    await PopupHelper.DisplayAlert("File Download", "File saved", "Close");
                }
                else
                {
                    await PopupHelper.DisplayAlert("File Download", "File not saved", "Close");
                }
            }
            else
            {
                IsDownloadable = false;
            }
        }
    }
}
