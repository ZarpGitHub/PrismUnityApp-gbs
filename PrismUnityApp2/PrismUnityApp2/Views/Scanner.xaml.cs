using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace PrismUnityApp2.Views
{
    public partial class Scanner : ContentPage
    {
        ZXingScannerView zxing= new ZXingScannerView();
        ZXingDefaultOverlay overlay;


        public Scanner() : base ()
        {

            try
            {
                zxing.OnScanResult += (result) =>  // => means what here ?
                        Device.BeginInvokeOnMainThread(async () =>
                        {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Show an alert
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Navigate away
                    await Navigation.PopAsync();
                        });
            }
            catch (System.Exception ex)
            {

                throw;
            }

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",      // this isnt shown
                BottomText = "Scanning will happen automatically",  //this isnt shown
                ShowFlashButton = zxing.HasTorch,  // this one is shown  also this doesnt work as we havnt open for flash yet but do we need it ?
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,

                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }

    }
}
 
