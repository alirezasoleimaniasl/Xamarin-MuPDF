using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Artifex.Mupdfdemo;
using Java.IO;
using File = Java.IO.File;

namespace Xamarin_MuPDF
{
    [Activity(Label = "Xamarin_MuPDF", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate {

                File file = (File)fileFromAsset(this, "test.pdf");
                var uri = Android.Net.Uri.Parse(file.AbsolutePath);
                var intent0 = new Intent(this, typeof(MuPDFActivity));
                intent0.SetFlags(ActivityFlags.NoHistory);
                intent0.SetAction(Intent.ActionView);
                intent0.SetData(uri);
                StartActivity(intent0);
            };

        }

        private object fileFromAsset(Context context, String assetName)
        {
            File outFile = new File(context.CacheDir, assetName + "-pdfview.pdf");
            copy(context.Assets.Open(assetName), outFile);
            return outFile;
        }

        private void copy(Stream inputStream, File output)
        {
            OutputStream outputStream = null;
            var bufferedInputStream = new BufferedInputStream(inputStream);
            try
            {
                outputStream = new FileOutputStream(output);
                int read = 0;
                byte[] bytes = new byte[1024];
                while ((read = bufferedInputStream.Read(bytes)) != -1)
                {
                    outputStream.Write(bytes, 0, read);
                }
            }
            finally
            {
                try
                {
                    if (inputStream != null)
                    {
                        inputStream.Close();
                    }
                }
                finally
                {
                    if (outputStream != null)
                    {
                        outputStream.Close();
                    }
                }
            }
        }

    }
}

