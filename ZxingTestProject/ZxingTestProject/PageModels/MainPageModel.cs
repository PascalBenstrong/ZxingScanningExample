using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZxingTestProject.PageModels
{
    internal class MainPageModel : FreshBasePageModel
    {
        public const string ValidQrCodeMessage = "Hello World";
        public FreshAwaitCommand ScanCommand { get; set; }

        private string _decodedMessage = string.Empty;
        public string DecodedMessage 
        { 
            get => _decodedMessage; 
            set
            {
                _decodedMessage = value;
                RaisePropertyChanged();
            }
        }
        
        public MainPageModel()
        {
            ScanCommand = new FreshAwaitCommand(async (t) =>
            {
                Func<ZXing.Result, bool> onScanned = OnFinishedScanning;
                await CoreMethods.PushPageModelWithNewNavigation<ScannerPageModel>(onScanned);
                t.SetResult(true);
            });

        }

        public bool OnFinishedScanning(ZXing.Result data)
        {
            DecodedMessage = data.Text;
            return !string.IsNullOrWhiteSpace(DecodedMessage) && DecodedMessage.Contains(ValidQrCodeMessage);
        }
    }
}
