using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;


namespace ManaBob.Windows
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            //LoadApplication(new ManaBob.AppCore());
        }

        async void button_Click(object sender, RoutedEventArgs e)
        {

            // Delete the file if it exists.


            //String filepath = @"C:\test.txt";
            //StreamWriter str = new StreamWriter();


            //char[] ch = text.ToCharArray();
            //for (int i = 0; i < ch.Length; i++)
            //{
            //   str.AsStreamForWrite(ch[i]);
            //}
            //str.Dispose();

            
            String text = textBox.Text;
            StorageFolder current = ApplicationData.Current.LocalFolder;
            StorageFile file = await current.CreateFileAsync(text, CreationCollisionOption.ReplaceExisting);
            IRandomAccessStream writeStream = await file.OpenAsync(FileAccessMode.ReadWrite);
            IOutputStream outputStream = writeStream.GetOutputStreamAt(0);
            DataWriter dataWriter = new DataWriter(outputStream);
            dataWriter.WriteString(" ");
            await dataWriter.StoreAsync();
            await outputStream.FlushAsync();
            
            
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            string filename = @"c:\Users\exampleuser\Documents\userinputlog.txt";
            
            using (FileStream SourceStream = File.Open(filename, FileMode.OpenOrCreate))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                await SourceStream.WriteAsync(result, 0, result.Length);
            }
            
            string path = "C:\\ListColorRGB.txt";
            System.Environment.
            StreamReader sr = new StreamReader();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
