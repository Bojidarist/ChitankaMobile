using ChitankaMobileUI.Models;
using ChitankaMobileUI.Services;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChitankaMobileUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailView : ContentPage
    {
        private ChitankaBookModel book;

        public BookDetailView(ChitankaBookModel book)
        {
            InitializeComponent();

            this.book = book;
            StaticFileDownloader.Downloader.OnFileDownloaded += Downloader_OnFileDownloaded;
            BookTitle.Text = book.Book.Title;
            BookAuthor.Text = book.Book.TitleAuthor;
            BookYear.Text = book.Book.Year.HasValue ? book.Book.Year.Value.ToString() : "";
            BookLanguage.Text = book.Book.Language.Name;
            BookDescription.Text = book.Book.Annotation;
            BookImage.Source = "https://assets.chitanka.info/" + book.Book.Cover;
        }

        private void Downloader_OnFileDownloaded(object sender, DownloadEventArgs e)
        {
            if (e.FileSaved)
            {
                DisplayAlert("File Download", "File saved", "Close");
                if (!StaticFileDownloader.DownloadPaths.Contains(e.Path))
                {
                    StaticDriveAPI.Instance.UploadFile(e.Path, Path.GetFileName(e.Path),
                        new List<string> { "FolderId" }, "Electronic Publication/epub");
                    StaticFileDownloader.DownloadPaths.Add(e.Path);
                }
            }
            else
            {
                DisplayAlert("File Download", "File not saved", "Close");
            }
        }

        private void DownloadButton_Clicked(object sender, System.EventArgs e)
        {
            StaticFileDownloader.Downloader.DownloadFile(book.DownloadURL, "ChitankaDownloads",
                $"{ book.Book.Id }-{ book.Book.Title }.epub");
            BookKeeperService.Books.Add(book.Book);
        }
    }
}