﻿using ChitankaDriveAPI;
using ChitankaMobileUI.Configuration;
using ChitankaMobileUI.Services;
using ChitankaMobileUI.ViewModels.Base;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ChitankaMobileUI.ViewModels
{
    public class GDriveFolderViewModel : BaseViewModel
    {
        private ObservableCollection<DriveFile> files;

        public ObservableCollection<DriveFile> Files
        {
            get => files;
            set
            {
                files = value; OnPropertyChanged(nameof(Files));
            }
        }

        public Command RefreshCommand { get; set; }

        public GDriveFolderViewModel(FileListResponse files = null)
        {
            InitCommands();
            if (files == null)
            {
                Init();
            }
            else
            {
                Files = new ObservableCollection<DriveFile>(files.Files);
            }
        }

        public void Init()
        {
            if (!GlobalConfig.Instance.Properties.ContainsKey(GlobalConfig.Options.DFoldersList)
                || string.IsNullOrWhiteSpace(GlobalConfig.Instance.Properties[GlobalConfig.Options.DFoldersList].ToString())
                || GlobalConfig.Instance.Properties[GlobalConfig.Options.DFoldersList].GetType() != typeof(FileListResponse))
            {
                RefreshCommand.Execute(null);
            }
            else
            {
                var driveFiles = GlobalConfig.Instance.Properties[GlobalConfig.Options.DFoldersList] as FileListResponse;
                Files = new ObservableCollection<DriveFile>(driveFiles.Files);
            }
        }

        public void InitCommands()
        {
            RefreshCommand = new Command(async () =>
            {
                GlobalConfig.Instance.Properties[GlobalConfig.Options.DFoldersList] =
                    await StaticDriveAPI.Instance.GetFiles("mimeType = 'application/vnd.google-apps.folder'", 100, "viewedByMeTime desc");
                GlobalConfig.Instance.Save();
                var driveFiles = GlobalConfig.Instance.Properties[GlobalConfig.Options.DFoldersList] as FileListResponse;
                Files = new ObservableCollection<DriveFile>(driveFiles.Files);
            });
        }
    }
}
