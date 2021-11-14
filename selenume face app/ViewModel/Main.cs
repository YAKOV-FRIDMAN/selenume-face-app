using Microsoft.Edge.SeleniumTools;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.Edge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace selenume_face_app.ViewModel
{
    public class Main : INotifyPropertyChanged
    {
        public RelayCommand OpenFile { get; set; }
        private string jsonResult;
        public string JsonResoult
        {
            get { return jsonResult; }
            set { jsonResult = value; OnPropertyChanged(); }
        }
        private string imgSource;

        public string ImgSource
        {
            get { return imgSource; }
            set { imgSource = value; OnPropertyChanged(); }
        }
        private BitmapImage bitmapImage;

        public BitmapImage BitmapImage
        {
            get { return bitmapImage; }
            set { bitmapImage = value; OnPropertyChanged(); }
        }


        public Main()
        {
            OpenFile = new RelayCommand(OpenImage);
        }
        Bitmap bitmap;
        private void OpenImage(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Task.Run(() =>
                {
                    ImgSource = openFileDialog.FileName;
                    bitmap = new Bitmap(openFileDialog.FileName);
                    BitmapImage = ConvertBitmap(bitmap);
                }).Wait();
                Task.Run(() =>
                {
                    IWebDriver webDriver = new FirefoxDriver();
                    webDriver.Url = "https://face-api.sightcorp.com/demo-basic/";
                    Thread.Sleep(2000);
                    var input = webDriver.FindElement(By.Id("FACE_SRC_DISK"));
                    input.SendKeys(imgSource);
                    Thread.Sleep(2000);
                    webDriver.FindElement(By.Id("scan-image")).Click();
                    Thread.Sleep(3000);
                    string json = webDriver.FindElement(By.Id("FACE_JSON_response")).Text;
                    JsonResoult = json;
                    var face = JsonConvert.DeserializeObject<Face>(json);
                    foreach (var item in face.people[0].landmarks.maskpoints)
                    {

                        //for (int i = 1; i <= 5; i++)
                        //{
                            bitmap.SetPixel(item.x - 1, item.y, Color.FromArgb(255, 255, 0));
                           // if (i % 2 == 0)
                                bitmap.SetPixel(item.x - 1, item.y - 1, Color.FromArgb(255, 255, 0));
                            bitmap.SetPixel(item.x, item.y - 1, Color.FromArgb(255, 255, 0));
                            //if (i % 2 == 0)
                                bitmap.SetPixel(item.x -1, item.y + 1, Color.FromArgb(255, 255, 0));
                            bitmap.SetPixel(item.x, item.y + 1, Color.FromArgb(255, 255, 0));
                           // if (i % 2 == 0)
                                bitmap.SetPixel(item.x + 1, item.y + 1, Color.FromArgb(255, 255, 0));
                            bitmap.SetPixel(item.x + 1, item.y, Color.FromArgb(255, 255, 0));
                           // if (i % 2 == 0)
                                bitmap.SetPixel(item.x + 1, item.y - 1, Color.FromArgb(255, 255, 0));
                        //}
                        bitmap.SetPixel(item.x, item.y, Color.FromArgb(255, 255, 0));
                         //BitmapImage = ConvertBitmap(bitmap);

                    }
                    //var maskpoints = face.people[0].landmarks.maskpoints;
                    //for (int i = 0; i < maskpoints.Count; i +=2)
                    //{
                    //    // fix bug count => index
                    //    if (maskpoints[i] == null)
                    //        break;
                    //    Maskpoint maskpoint1 = maskpoints[i];
                    //    Maskpoint maskpoint2 = maskpoints[i +1];
                    //    int reX = maskpoint1.x - maskpoint2.x;
                    //    int reY = maskpoint1.y - maskpoint2.y;
                    //    int m = (int)Math.Sqrt((reY * reY) + (reX * reX));
                    //    for (int j = 0; j < m; j++)
                    //    {
                    //        bitmap.SetPixel(maskpoint1.x + j, maskpoint1.y + j, Color.FromArgb(255, 0, 0));

                    //    }

                    //}
                    bitmap.SetPixel(face.people[0].landmarks.lefteye.x-1, face.people[0].landmarks.lefteye.y, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(face.people[0].landmarks.lefteye.x, face.people[0].landmarks.lefteye.y, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(face.people[0].landmarks.lefteye.x, face.people[0].landmarks.lefteye.y-1, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(face.people[0].landmarks.lefteye.x, face.people[0].landmarks.lefteye.y+1, Color.FromArgb(255, 0, 0));
                    bitmap.SetPixel(face.people[0].landmarks.lefteye.x+1, face.people[0].landmarks.lefteye.y, Color.FromArgb(255, 0, 0));
                   
                    bitmap.SetPixel(face.people[0].landmarks.righteye.x-1, face.people[0].landmarks.righteye.y, Color.FromArgb(0, 0, 255));
                    bitmap.SetPixel(face.people[0].landmarks.righteye.x, face.people[0].landmarks.righteye.y, Color.FromArgb(0, 0, 255));
                    bitmap.SetPixel(face.people[0].landmarks.righteye.x, face.people[0].landmarks.righteye.y-1, Color.FromArgb(0, 0, 255));
                    bitmap.SetPixel(face.people[0].landmarks.righteye.x, face.people[0].landmarks.righteye.y+1, Color.FromArgb(0, 0, 255));
                    bitmap.SetPixel(face.people[0].landmarks.righteye.x+1, face.people[0].landmarks.righteye.y, Color.FromArgb(0, 0, 255));
                    
                    BitmapImage = ConvertBitmap(bitmap);
                });
            }
        }

        public BitmapImage ConvertBitmap(System.Drawing.Bitmap bitmap)
        {
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            ms.Seek(0, SeekOrigin.Begin);
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();
            return image;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
