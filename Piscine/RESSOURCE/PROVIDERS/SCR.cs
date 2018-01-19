using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.Extensions;
using RESSOURCE.CONFIGS;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using OpenQA.Selenium.Appium.iOS;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Linq;
using OpenQA.Selenium.PhantomJS;

namespace RESSOURCE.PROVIDERS
{
    public class SCR
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        #region VARIABLES
        string pathSCR, scrName;
        HNDLR _hndlr = new HNDLR();
        Tuple<string, string> _pngData;
        // Lors de la capture.
        List<Screenshot> _scrPreMade = new List<Screenshot>();
        // Pour la sauvegarde et recuperation.
        List<Image> _imagesPreMade = new List<Image>();
        // le Root en instant T.
        List<string> _pathBitmapPreMade = new List<string>();
        List<Bitmap> _bitmapPreMade = new List<Bitmap>();
        #endregion

        #region ANDROID
        public Image TakeScreenshot(AndroidDriver<AndroidElement> _driver, String _testID,
                                            string userName, string userLastName,
                                            string os, bool pgR)
        {
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);

            string _cheminCaptureEcran = _hndlr.PathProject() + "ANDROIDSCR\\" + userName + userLastName + os + DateTime.Now.ToString("MMdHHmmss") + _testID + ".jpg";
            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };

            _ceImg.Save(_cheminCaptureEcran, encoder, encParams);

            return _ceImg;
        }

        public Image GetEntireScreenshotMobile(AndroidDriver<AndroidElement> _driver, string deviceUDID)
        {
            // Get the total size of the page
            var totalWidth =
                (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.offsetWidth"); //documentElement.scrollWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return  document.body.parentNode.scrollHeight");
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.clientWidth"); //documentElement.scrollWidth");
            var viewportHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return window.innerHeight"); //documentElement.scrollWidth");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                var screenshot = _driver.TakeScreenshot();
                return ScreenshotToImage(screenshot);
            }
            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }
                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }
                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }
            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            var previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;

                    // Take Screenshot
                    var screenshotB = _driver.TakeScreenshot();
                    // Build an Image out of the Screenshot
                    var screenshotImageB = ScreenshotToImage(screenshotB);
                    pathSCR = _hndlr.PathProject() + "ANDROIDSCR\\" + deviceUDID + DateTime.Now.ToString("MMdHHmmss") + ".jpg";
                    _pathBitmapPreMade.Add(pathSCR);
                    var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                    screenshotImageB.Save(pathSCR, encoder, encParams);
                    // Scroll
                    ((IJavaScriptExecutor)_driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    System.Threading.Thread.Sleep(200);
                }
                // Take Screenshot
                var screenshot = _driver.TakeScreenshot();
                // Build an Image out of the Screenshot
                var screenshotImage = ScreenshotToImage(screenshot);
                // Calculate the source Rectangle
                var sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);
                // Copy the Image
                using (var graphics = Graphics.FromImage(stitchedImage))
                {
                    graphics.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
                // Set the Previous Rectangle
                previous = rectangle;
            }
            return Collage(_pathBitmapPreMade);
        }
        #endregion

        #region IOS
        public Image TakeScreenshot(IOSDriver<IOSElement> _driver, String _testID,
                                        string userName, string userLastName,
                                        string navg, bool pgR)
        {
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);

            string _cheminCaptureEcran = _hndlr.PathProject() + "IOSSCR\\" + userName + userLastName + navg + DateTime.Now.ToString("MMdHHmmss") + _testID + ".jpg";
            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
            _ceImg.Save(_cheminCaptureEcran, encoder, encParams);

            return _ceImg;
        }

        public Image GetEntireScreenshotMobile(IOSDriver<IOSElement> _driver, string deviceUDID)
        {
            // Get the total size of the page
            var totalWidth =
                (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.offsetWidth"); //documentElement.scrollWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return  document.body.parentNode.scrollHeight");
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.clientWidth"); //documentElement.scrollWidth");
            var viewportHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return window.innerHeight"); //documentElement.scrollWidth");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                var screenshot = _driver.TakeScreenshot();
                return ScreenshotToImage(screenshot);
            }
            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }
                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }
                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }
            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            var previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;

                    // Take Screenshot
                    var screenshotB = _driver.TakeScreenshot();
                    // Build an Image out of the Screenshot
                    var screenshotImageB = ScreenshotToImage(screenshotB);
                    var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                    _pathBitmapPreMade.Add(pathSCR);
                    screenshotImageB.Save(pathSCR, encoder, encParams);
                    // Scroll
                    ((IJavaScriptExecutor)_driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    System.Threading.Thread.Sleep(200);
                }
                // Take Screenshot
                var screenshot = _driver.TakeScreenshot();
                // Build an Image out of the Screenshot
                var screenshotImage = ScreenshotToImage(screenshot);
                // Calculate the source Rectangle
                var sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);
                // Copy the Image
                using (var graphics = Graphics.FromImage(stitchedImage))
                {
                    graphics.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
                // Set the Previous Rectangle
                previous = rectangle;
            }
            return Collage(_pathBitmapPreMade);
        }
        #endregion

        #region DESKTOP
        public Image TakeScreenshotDESKTOP(IWebDriver _driver, string testID,
                                        string userName, string userLastName,
                                        string navg, bool pgR)
        {
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);

            if (pgR == true)
            {
                string _cheminCaptureEcran = _hndlr.PathProject() + "DESKTOPSCR\\" + userName + userLastName + navg + DateTime.Now.ToString("MMdHHmmss") + testID + ".jpg";
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                _ceImg.Save(_cheminCaptureEcran, encoder, encParams);
            }
            else
            {
                string _cheminCaptureEcran = _hndlr.PathProject() + "DESKTOPSCR\\" + userName + userLastName + navg + DateTime.Now.ToString("MMdHHmmss") + testID + ".jpg";
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                _ceImg.Save(_cheminCaptureEcran, encoder, encParams);
            }
            return _ceImg;
        }
        public Image GetEntireScreenshot(IWebDriver _driver)
        {
            // Get the total size of the page
            var totalWidth =
                (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.offsetWidth");
            var totalHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return  document.body.parentNode.scrollHeight");
            // Get the size of the viewport
            var viewportWidth = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return document.body.clientWidth");
            var viewportHeight = (int)(long)((IJavaScriptExecutor)_driver).ExecuteScript("return window.innerHeight");

            // We only care about taking multiple images together if it doesn't already fit
            if (totalWidth <= viewportWidth && totalHeight <= viewportHeight)
            {
                var screenshot = _driver.TakeScreenshot();
                return ScreenshotToImage(screenshot);
            }
            // Split the screen in multiple Rectangles
            var rectangles = new List<Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                var newHeight = viewportHeight;
                // Fix if the height of the element is too big
                if (y + viewportHeight > totalHeight)
                {
                    newHeight = totalHeight - y;
                }
                // Loop until the totalWidth is reached
                for (var x = 0; x < totalWidth; x += viewportWidth)
                {
                    var newWidth = viewportWidth;
                    // Fix if the Width of the Element is too big
                    if (x + viewportWidth > totalWidth)
                    {
                        newWidth = totalWidth - x;
                    }
                    // Create and add the Rectangle
                    var currRect = new Rectangle(x, y, newWidth, newHeight);
                    rectangles.Add(currRect);
                }
            }
            // Build the Image
            var stitchedImage = new Bitmap(totalWidth, totalHeight);
            // Get all Screenshots and stitch them together
            var previous = Rectangle.Empty;
            foreach (var rectangle in rectangles)
            {
                // Calculate the scrolling (if needed)
                if (previous != Rectangle.Empty)
                {
                    var xDiff = rectangle.Right - previous.Right;
                    var yDiff = rectangle.Bottom - previous.Bottom;
                    // Scroll
                    ((IJavaScriptExecutor)_driver).ExecuteScript(String.Format("window.scrollBy({0}, {1})", xDiff, yDiff));
                    System.Threading.Thread.Sleep(500);
                }
                // Take Screenshot
                var screenshot = _driver.TakeScreenshot();
                // Build an Image out of the Screenshot
                var screenshotImage = ScreenshotToImage(screenshot);
                // Calculate the source Rectangle
                var sourceRectangle = new Rectangle(viewportWidth - rectangle.Width, viewportHeight - rectangle.Height, rectangle.Width, rectangle.Height);
                // Copy the Image
                using (var graphics = Graphics.FromImage(stitchedImage))
                {
                    graphics.DrawImage(screenshotImage, rectangle, sourceRectangle, GraphicsUnit.Pixel);
                }
                // Set the Previous Rectangle
                previous = rectangle;
            }
            return stitchedImage;
        }
        #endregion

        #region NOP NOP NOP & NOP!
        public Tuple<string, string> TakeScreenshotACTION(Image scrImage,
                                                        string testID, string userName,
                                                        string userLastName, string navg)
        {
            scrName = "LARGE" + userName + userLastName + navg + DateTime.Now.ToString("MMdHHmmss") + testID + ".jpg";

            if (navg == "Android")
            {
                Image _ce = scrImage;
                pathSCR = _hndlr.PathProject() + "ANDROIDSCR\\" + scrName;
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                _ce.Save(pathSCR, encoder, encParams);
            }
            else if (navg == "Ios")
            {
                Image _ce = scrImage;
                pathSCR = _hndlr.PathProject() + "IOSSCR\\" + scrName;
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                _ce.Save(pathSCR, encoder, encParams);
            }
            else
            {
                Image _ce = scrImage;
                pathSCR = _hndlr.PathProject() + "DESKTOPSCR\\" + scrName;
                var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };
                _ce.Save(pathSCR, encoder, encParams);
            }

            _pngData = new Tuple<string, string>(pathSCR, scrName);
            return _pngData;
        }

        private static Image ScreenshotToImage(Screenshot screenshot)
        {
            Image screenshotImage;
            using (var memStream = new MemoryStream(screenshot.AsByteArray))
            {
                screenshotImage = Image.FromStream(memStream);
            }
            return screenshotImage;
        }

        private Image Collage(List<string> files)
        {
            //read all images into memory
            List<Bitmap> images = new List<Bitmap>();
            Bitmap finalImage = null;

            try
            {
                int width = 0;
                int height = 0;
                foreach (string image in files)
                {
                    //create a Bitmap from the file and add it to the list
                    Bitmap bitmap = new Bitmap(image);

                    //update the size of the final bitmap
                    height += bitmap.Height;
                    width = bitmap.Width > width ? bitmap.Width : width;

                    images.Add(bitmap);
                }
                //create a bitmap to hold the combined image
                finalImage = new Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (Bitmap image in images)
                    {
                        g.DrawImage(image,
                          new Rectangle(0, offset, image.Width, image.Height));
                        offset += image.Height;
                    }
                }
                return (Image)finalImage;
            }
            catch (Exception)
            {
                if (finalImage != null)
                    finalImage.Dispose();
                //throw ex;
                throw;
            }
            finally
            {
                //clean up memory
                foreach (Bitmap image in images)
                {
                    image.Dispose();
                }
            }
        }
        #endregion

        #region GIF Handler
        public void GifCreator(List<Image> _images)
        {
            HNDLR _hndlr = new HNDLR();
            GifBitmapEncoder gEnc = new GifBitmapEncoder();

            foreach (Bitmap bmpImage in _images)
            {
                for (int i = 0; i < 5; i++)
                {
                    var bmp = bmpImage.GetHbitmap();
                    var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmp, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                    gEnc.Frames.Add(BitmapFrame.Create(src));
                    DeleteObject(bmp);
                }
            }
            using (FileStream fs = new FileStream(_hndlr.PathProject() + "DESKTOPSCR\\" + DateTime.Now.ToString("MMdHHmmss") + ".gif", FileMode.Create))
            {
                gEnc.Save(fs);
            }
        }
        #endregion

        #region Large v2
        public Image TakeScreenshotLarge(string url, String _testID,
                                    string userName, string userLastName,
                                    string os, bool pgR)
        {
            PhantomJSDriver _driver = new PhantomJSDriver(@"C:/WEBDRIVER");
            _driver.Manage().Window.Size = new System.Drawing.Size(1920, 1080);
            _driver.Navigate().GoToUrl(url);
            
            Screenshot _ce = _driver.TakeScreenshot();
            Image _ceImg = ScreenshotToImage(_ce);

            string _cheminCaptureEcran = _hndlr.PathProject() + "DESKTOPSCR\\LARGE" + userName + userLastName + os + DateTime.Now.ToString("MMdHHmmss") + _testID + ".jpg";
            var encoder = ImageCodecInfo.GetImageEncoders().First(c => c.FormatID == ImageFormat.Jpeg.Guid);
            var encParams = new EncoderParameters() { Param = new[] { new EncoderParameter(Encoder.Quality, 75L) } };

            _ceImg.Save(_cheminCaptureEcran, encoder, encParams);

            return _ceImg;
        }
        #endregion
    }
}
