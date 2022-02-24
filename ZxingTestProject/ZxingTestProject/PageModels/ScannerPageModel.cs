using FreshMvvm;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace ZxingTestProject.PageModels
{
    internal class ScannerPageModel : FreshBasePageModel
    {
        public bool _isScanning;
        public bool IsScanning
        {
            get => _isScanning;
            set
            {
                _isScanning = value;
                RaisePropertyChanged(nameof(IsScanning));
            }
        }

        public bool _isAnalyzing;

        public bool IsAnalyzing
        {
            get => _isAnalyzing;
            set
            {
                _isAnalyzing = value;
                RaisePropertyChanged(nameof(IsAnalyzing));
            }
        }

        public ICommand OnScannedCommand { get; set; }

        Func<ZXing.Result, bool> _OnScanned;

        public ScannerPageModel()
        {
            IsScanning = true;
            IsAnalyzing = true;
            OnScannedCommand = new Command<Result>(async(v) =>
            {
                IsAnalyzing = false; // stop analyzing the images
                var isValid = _OnScanned?.Invoke(v);

                if (isValid is true)
                {
                    await CoreMethods.PopPageModel(true);
                }
                else
                {
                    IsAnalyzing = await MainThread.InvokeOnMainThreadAsync(async () =>
                    {
                        return await CoreMethods.DisplayAlert("Invalid QrCode", $@"This Qr code is invalid. A valid QrCode must contain ""{MainPageModel.ValidQrCodeMessage}""", "Scan Again", "Cancel");
                    });

                    if(IsAnalyzing is false)
                    {
                        await CoreMethods.PopPageModel(true);
                    }
                }
            });
        }

        public override void Init(object initData)
        {
            _OnScanned = initData as Func<ZXing.Result, bool>; ;
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            IsAnalyzing = false;
            base.ViewIsDisappearing(sender, e);
        }
    }
}
