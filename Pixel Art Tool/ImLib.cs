using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Pixel_Art_Tool
{
    /// <summary>
    /// Image manipulation library
    /// </summary>
    class ImLib
    {
        /// <summary>
        /// The available blocks
        /// </summary>
        FormMain.Block[] blocks;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="blocks">Current database</param>
        public ImLib(FormMain.Block[] blocks)
        {
            this.blocks = blocks;
        }

        public async Task<ImLibResult> Generate(string imagePath, int maxHeight, int maxWidth, bool allowUpscale, string projectFolder,
                                                bool saveDownscale, bool savePixelated, bool saveFiltered, bool saveFilteredPixelated)
        {
            return await Task.Run(() =>
            {
                //=====Load image and scale it=====
                Bitmap scaledImage;
                try
                {
                    using (Image imgFromFile = Image.FromFile(imagePath))
                    {
                        //Check if origonal image is too small
                        if (imgFromFile.Height < maxHeight && !allowUpscale)
                        {
                            maxHeight = imgFromFile.Height;
                        }
                        if (imgFromFile.Width < maxWidth && !allowUpscale)
                        {
                            maxWidth = imgFromFile.Width;
                        }
                        //Scale image
                        ImLibResult result = ScaleImageWithInterpolation(imgFromFile, maxHeight, maxWidth, out Image tempImage);
                        //Check if we succeed
                        if (result == ImLibResult.Succeed)
                        {
                            //Copy the image and carry on
                            scaledImage = new Bitmap(tempImage); //This can throw, it will be catched below
                            tempImage.Dispose(); //imgFromFile will be disposed by the using statement
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
                catch (Exception err) when (err is OutOfMemoryException || err is ArgumentException)
                {
                    //Could not allocate memory
                    return ImLibResult.TooLarge;
                }
                catch (FileNotFoundException)
                {
                    //Could not read file
                    return ImLibResult.ReadError;
                }
                //=====Save the downscaled image=====
                if (saveDownscale)
                {
                    try
                    {
                        scaledImage.Save(Path.Combine(projectFolder, "Original Downscaled.png"), ImageFormat.Png);
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return ImLibResult.WriteError;
                    }
                }
                //=====Save pixelated image=====
                if (savePixelated)
                {
                    //Pixelate image
                    ImLibResult result = ScaleImageWithoutInterpolation(scaledImage, 10, out Bitmap pixelatedImage);
                    //Check if we succeed then save
                    if (result == ImLibResult.Succeed)
                    {
                        try
                        {
                            pixelatedImage.Save(Path.Combine(projectFolder, "Original Pixelated.png"), ImageFormat.Png);
                            pixelatedImage.Dispose();
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            pixelatedImage.Dispose();
                            return ImLibResult.WriteError;
                        }
                    }
                    else
                    {
                        //No need for dispose in this case, since it is only one pixel, we can wait for garbage collector
                        return result;
                    }

                }
                //=====Filter image=====
                Bitmap filteredImage = scaledImage;
                //This matrix will store the index of the Block that is needed for each pixel, will be used later
                int[,] constructionPlanMatrix = new int[filteredImage.Width, filteredImage.Height];
                //Loop though the whole image and convert it into colors that exists in-game
                for (int x = 0; x < filteredImage.Width; x++)
                {
                    for (int y = 0; y < filteredImage.Height; y++)
                    {
                        //For each pixel, we need to find the closest color and save it
                        int bestColorIndex = 0;
                        double currentDiff = double.MaxValue;
                        //Color of current pixel
                        Color currentPixelColor = filteredImage.GetPixel(x, y);
                        for (int i = 0; i < blocks.Count(); i++)
                        {
                            //See if we have a closer color
                            double cmpResult = ColorCmp(currentPixelColor, blocks[i].color);
                            if (cmpResult < currentDiff)
                            {
                                bestColorIndex = i;
                                currentDiff = cmpResult;
                            }
                        }
                        //Found best color, save it
                        filteredImage.SetPixel(x, y, blocks[bestColorIndex].color);
                        constructionPlanMatrix[x, y] = bestColorIndex;
                    }
                }
                //Save filtered image if needed
                if (saveFiltered)
                {
                    try
                    {
                        filteredImage.Save(Path.Combine(projectFolder, "Trovized Downscaled.png"), ImageFormat.Png);
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {
                        return ImLibResult.WriteError;
                    }
                }
                //=====Save filtered pixelated=====
                if (saveFilteredPixelated)
                {
                    ImLibResult result = ScaleImageWithoutInterpolation(filteredImage, 10, out Bitmap pixelatedImage);
                    {
                        try
                        {
                            pixelatedImage.Save(Path.Combine(projectFolder, "Trovized Pixelated.png"), ImageFormat.Png);
                            pixelatedImage.Dispose();
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            pixelatedImage.Dispose();
                            return ImLibResult.WriteError;
                        }
                    }
                }
                //=====Construction plan=====
                //Generate construction plan hexadecimal matrix
                string[,] constructionPlanHex = new string[filteredImage.Width, filteredImage.Height];
                //Loop through the whole image and convert indexes to hexadecimal
                for (int x = 0; x < filteredImage.Width; x++)
                {
                    for (int y = 0; y < filteredImage.Height; y++)
                    {
                        constructionPlanHex[x, y] = ToTwoDigitHex(constructionPlanMatrix[x, y]);
                    }
                }
                //Initialize the construction plan by scaling filtered image by 50 times
                ImLibResult cpInitResult = ScaleImageWithoutInterpolation(filteredImage, 50, out Bitmap constructionPlanImage);
                //Check if we succeed
                if (cpInitResult == ImLibResult.Succeed)
                {
                    using (Graphics constructionPlanGraphics = Graphics.FromImage(constructionPlanImage))
                    {
                        //Draw grid
                        using (Pen blackPen = new Pen(Color.Black, 2))
                        {
                            //Vertical lines
                            for (int x = 49; x < constructionPlanImage.Width - 5; x += 50)
                            {
                                constructionPlanGraphics.DrawLine(blackPen, new Point(x, 0), new Point(x, constructionPlanImage.Height));
                            }
                            //Horizontal lines
                            for (int y = 49; y < constructionPlanImage.Height - 5; y += 50)
                            {
                                constructionPlanGraphics.DrawLine(blackPen, new Point(0, y), new Point(constructionPlanImage.Width, y));
                            }
                        }
                        //Draw index numbers
                        using (Font consolasFont = new Font("Consolas", 24))
                        {
                            //The original y location to draw
                            const int yDrawOriginal = 5;
                            //Keep track of location to draw
                            int xDraw = 1;
                            int yDraw;
                            //Loop through each pixel
                            for (int x = 0; x < filteredImage.Width; x++)
                            {
                                //Reset y location
                                yDraw = yDrawOriginal;
                                for (int y = 0; y < filteredImage.Height; y++)
                                {
                                    //Draw index
                                    constructionPlanGraphics.DrawString(constructionPlanHex[x, y], consolasFont,
                                                                        new SolidBrush(ContrastColor(filteredImage.GetPixel(x, y))),
                                                                        xDraw, yDraw);
                                    //Update location to draw
                                    yDraw += 50;
                                }
                                //Update x location
                                xDraw += 50;
                            }
                        }
                        //Save image
                        try
                        {
                            constructionPlanImage.Save(Path.Combine(projectFolder, "Construction Plan.png"), ImageFormat.Png);
                        }
                        catch (System.Runtime.InteropServices.ExternalException)
                        {
                            return ImLibResult.WriteError;
                        }
                    }
                }
                else
                {
                    return cpInitResult;
                }
                //=====Save used index with used times count=====
                //Initialize counter
                int[] blocksCounter = Enumerable.Repeat(0, blocks.Count()).ToArray();
                //Loop through the construction plan matrix and count each index
                for (int x = 0; x < filteredImage.Width; x++)
                {
                    for (int y = 0; y < filteredImage.Height; y++)
                    {
                        blocksCounter[constructionPlanMatrix[x, y]]++;
                    }
                }
                //We are done with images, dispose
                //Since filteredImage references scaledImage, we don't need to dispose both
                filteredImage.Dispose();
                //Prepare lines to write
                string[] lines = new string[blocksCounter.Count(i => i != 0)];
                //Since we are not outputting all the lines, we need a separate counter
                int linesIndex = 0;
                //Loop though each counter and prepare lines to write
                for (int i = 0; i < blocksCounter.Count(); i++)
                {
                    //Check if the block is used
                    if (blocksCounter[i] > 0)
                    {
                        //Write line
                        lines[linesIndex++] = ToTwoDigitHex(i) + " - " + blocks[i].name + "  (" + blocksCounter[i] + ")";
                    }
                }
                //Write to file
                try
                {
                    File.WriteAllLines(Path.Combine(projectFolder, "Construction Block Index And Usage.txt"), lines);
                }
                catch (Exception err) when (err is PathTooLongException || err is IOException || err is UnauthorizedAccessException)
                {
                    return ImLibResult.WriteError;
                }
                return ImLibResult.Succeed;
            });
        }

        #region Helper Functions

        /// <summary>
        /// Compare two color and return a numeric relative difference
        /// </summary>
        /// <param name="col1">First color</param>
        /// <param name="col2">Second color</param>
        /// <returns>Relative color difference</returns>
        public static double ColorCmp(Color col1, Color col2)
        {
            return Math.Pow((col1.R - col2.R), 2) + Math.Pow((col1.G - col2.G), 2) + Math.Pow((col1.B - col2.B), 2);
        }

        /// <summary>
        /// Convert a number into two digit hexadecimal string
        /// </summary>
        /// <param name="i">The number to convert</param>
        /// <returns>Two deigit hexadecimal string</returns>
        public static string ToTwoDigitHex(int i)
        {
            //Convert into hexadecimal
            string temp = i.ToString("X");
            return (temp.Length == 1) ? "0" + temp : temp;
        }

        /// <summary>
        /// Find what should be the font color
        /// http://stackoverflow.com/questions/1855884/determine-font-color-based-on-background-color
        /// </summary>
        /// <param name="color">Background color</param>
        /// <returns>A good font color for</returns>
        public static Color ContrastColor(Color color)
        {
            int d = 0;
            //Counting the perceptive luminance, human eye favors green color
            double a = 1 - (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            if (a < 0.5)
            {
                d = 0; //Bright background, black font
            }
            else
            {
                d = 255; //Dark background, white font
            }
            return Color.FromArgb(d, d, d);
        }

        /// <summary>
        /// Scalling an image without interpolation but keeping ratio
        /// The built-in scalling functionality does not bahave the way we want,
        /// the border of the image will copy blank pixels outside the image
        /// </summary>
        /// <param name="originalImage">Original bitmap</param>
        /// <param name="ratio">Scalling ratio</param>
        /// <param name="scaledImage">Scaled bitmap, or an empty image if it failed</param>
        /// <returns>Processing result</returns>
        public static ImLibResult ScaleImageWithoutInterpolation(Bitmap originalImage, int ratio, out Bitmap scaledImage)
        {
            Bitmap newImage = new Bitmap(1, 1);
            try
            {
                //Initialize output bitmap
                newImage = new Bitmap(originalImage.Width * ratio, originalImage.Height * ratio);
                //Draw each pixel
                using (Graphics newImageGraphics = Graphics.FromImage(newImage))
                {
                    for (int x = 0; x < originalImage.Width; x++)
                    {
                        for (int y = 0; y < originalImage.Height; y++)
                        {
                            newImageGraphics.FillRectangle(new SolidBrush(originalImage.GetPixel(x, y)), x * ratio, y * ratio, x + ratio, y + ratio);
                        }
                    }
                }
                //Assign output argument
                scaledImage = newImage;
            }
            catch (ArgumentException)
            {
                //Too big
                scaledImage = newImage;
                return ImLibResult.TooLarge;
            }
            return ImLibResult.Succeed;
        }

        /// <summary>
        /// Scalling an image with interpolation and keeping ratio
        /// http://stackoverflow.com/questions/6501797/resize-image-proportionally-with-maxheight-and-maxwidth-constraints
        /// </summary>
        /// <param name="originalImage">Original image</param>
        /// <param name="maxHeight">Maximum desired height</param>
        /// <param name="maxWidth">Maximum desired width</param>
        /// <param name="scaledImage">Scaled image, or an empty image if it failed</param>
        /// <returns>Processing result</returns>
        public static ImLibResult ScaleImageWithInterpolation(Image originalImage, int maxHeight, int maxWidth, out Image scaledImage)
        {
            //Get the ratio to scale
            double ratioX = (double)maxWidth / originalImage.Width;
            double ratioY = (double)maxHeight / originalImage.Height;
            double ratio = Math.Min(ratioX, ratioY);
            //Calculate new dimentions
            int newWidth = (int)Math.Ceiling((originalImage.Width * ratio));
            int newHeight = (int)Math.Ceiling((originalImage.Height * ratio));
            //Scale the image
            Bitmap newImage = new Bitmap(1, 1);
            try
            {
                newImage = new Bitmap(newWidth, newHeight);
                using (Graphics graphics = Graphics.FromImage(newImage))
                {
                    graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                }
                scaledImage = newImage;
            }
            catch (ArgumentException)
            {
                //Too big
                scaledImage = newImage;
                return ImLibResult.TooLarge;
            }
            return ImLibResult.Succeed;
        }

        #endregion

        #region Other

        public enum ImLibResult
        {
            Succeed,
            TooLarge,
            ReadError,
            WriteError
        }

        #endregion

    }
}
