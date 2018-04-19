using CreateThumbnail.DataAccess;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateThumbnail.Service
{
    public class PictureService : IPictureService
    {
        #region Const

        private const int MULTIPLE_THUMB_DIRECTORIES_LENGTH = 3;

        #endregion

        #region Fields


        #endregion

        #region Ctor

        public PictureService()
        {
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Calculates picture dimensions whilst maintaining aspect
        /// </summary>
        /// <param name="originalSize">The original picture size</param>
        /// <param name="targetSize">The target picture size (longest side)</param>
        /// <param name="resizeType">Resize type</param>
        /// <param name="ensureSizePositive">A value indicatingh whether we should ensure that size values are positive</param>
        /// <returns></returns>
        protected virtual Size CalculateDimensions(Size originalSize, int targetSize,
            ResizeType resizeType = ResizeType.LongestSide, bool ensureSizePositive = true)
        {
            float width, height;

            switch (resizeType)
            {
                case ResizeType.LongestSide:
                    if (originalSize.Height > originalSize.Width)
                    {
                        // portrait
                        width = originalSize.Width * (targetSize / (float)originalSize.Height);
                        height = targetSize;
                    }
                    else
                    {
                        // landscape or square
                        width = targetSize;
                        height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    }
                    break;
                case ResizeType.Width:
                    width = targetSize;
                    height = originalSize.Height * (targetSize / (float)originalSize.Width);
                    break;
                case ResizeType.Height:
                    width = originalSize.Width * (targetSize / (float)originalSize.Height);
                    height = targetSize;
                    break;
                default:
                    throw new Exception("Not supported ResizeType");
            }

            if (ensureSizePositive)
            {
                if (width < 1)
                    width = 1;
                if (height < 1)
                    height = 1;
            }

            //we invoke Math.Round to ensure that no white background is rendered - http://www.nopcommerce.com/boards/t/40616/image-resizing-bug.aspx
            return new Size((int)Math.Round(width), (int)Math.Round(height));
        }

        /// <summary>
        /// Returns the file extension from mime type.
        /// </summary>
        /// <param name="mimeType">Mime type</param>
        /// <returns>File extension</returns>
        protected virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
                return null;

            //also see System.Web.MimeMapping for more mime types

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }
            return lastPart;
        }

        /// <summary>
        /// Loads a picture from file
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="mimeType">MIME type</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureFromFile(int pictureId, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            var filePath = GetPictureLocalPath(fileName);
            if (!File.Exists(filePath))
                return new byte[0];
            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Save picture on file system
        /// </summary>
        /// <param name="pictureId">Picture identifier</param>
        /// <param name="pictureBinary">Picture binary</param>
        /// <param name="mimeType">MIME type</param>
        protected virtual void SavePictureInFile(int pictureId, byte[] pictureBinary, string mimeType)
        {
            string lastPart = GetFileExtensionFromMimeType(mimeType);
            string fileName = string.Format("{0}_0.{1}", pictureId.ToString("0000000"), lastPart);
            File.WriteAllBytes(GetPictureLocalPath(fileName), pictureBinary);
        }

        /// <summary>
        /// Delete a picture on file system
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureOnFileSystem(Picture picture)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
            string fileName = string.Format("{0}_0.{1}", picture.Id.ToString("0000000"), lastPart);
            string filePath = GetPictureLocalPath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>
        /// Delete picture thumbs
        /// </summary>
        /// <param name="picture">Picture</param>
        protected virtual void DeletePictureThumbs(Picture picture)
        {
            string filter = string.Format("{0}*.*", picture.Id.ToString("0000000"));
            var thumbDirectoryPath = CommonHelper.MapPath("~/content/images/thumbs");
            string[] currentFiles = System.IO.Directory.GetFiles(thumbDirectoryPath, filter, SearchOption.AllDirectories);
            foreach (string currentFileName in currentFiles)
            {
                var thumbFilePath = GetThumbLocalPath(currentFileName);
                File.Delete(thumbFilePath);
            }
        }

        /// <summary>
        /// Get picture (thumb) local path
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbLocalPath(string thumbFileName)
        {
            var thumbsDirectoryPath = CommonHelper.MapPath("~/content/images/thumbs");
            var thumbFilePath = Path.Combine(thumbsDirectoryPath, thumbFileName);
            return thumbFilePath;
        }

        /// <summary>
        /// Get picture (thumb) URL 
        /// </summary>
        /// <param name="thumbFileName">Filename</param>
        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
        /// <returns>Local picture thumb path</returns>
        protected virtual string GetThumbUrl(string thumbFileName, string storeLocation = null)
        {
            storeLocation = !String.IsNullOrEmpty(storeLocation)
                                    ? storeLocation
                                    : Configs.Instance.LocalPath;
            var url = storeLocation + "content/images/thumbs/";

            url = url + thumbFileName;
            return url;
        }

        /// <summary>
        /// Get picture local path. Used when images stored on file system (not in the database)
        /// </summary>
        /// <param name="fileName">Filename</param>
        /// <returns>Local picture path</returns>
        protected virtual string GetPictureLocalPath(string fileName)
        {
            return Path.Combine(CommonHelper.MapPath("~/content/images/"), fileName);
        }

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <param name="fromDb">Load from database; otherwise, from file system</param>
        /// <returns>Picture binary</returns>
        protected virtual byte[] LoadPictureBinary(Picture picture, bool fromDb)
        {
            if (picture == null)
                throw new ArgumentNullException("picture");

            var result = fromDb
                ? picture.PictureBinary
                : LoadPictureFromFile(picture.Id, picture.MimeType);
            return result;
        }

        /// <summary>
        /// Get a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <returns>Result</returns>
        protected virtual bool GeneratedThumbExists(string thumbFilePath, string thumbFileName)
        {
            return File.Exists(thumbFilePath);
        }

        /// <summary>
        /// Save a value indicating whether some file (thumb) already exists
        /// </summary>
        /// <param name="thumbFilePath">Thumb file path</param>
        /// <param name="thumbFileName">Thumb file name</param>
        /// <param name="mimeType">MIME type</param>
        /// <param name="binary">Picture binary</param>
        protected virtual void SaveThumb(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary)
        {
            File.WriteAllBytes(thumbFilePath, binary);
            Console.Write("Success - " + thumbFileName);
        }

        protected virtual string GetPictureSeNameTicks(Picture picture)
        {

            string seoFilename = "";
            if (!String.IsNullOrEmpty(picture.SeoFilename) && !String.IsNullOrEmpty(picture.UpdatedToTicks))
                seoFilename = string.Format("{0}-{1}", picture.SeoFilename, picture.UpdatedToTicks);
            else if (!String.IsNullOrEmpty(picture.SeoFilename) && String.IsNullOrEmpty(picture.UpdatedToTicks))
                seoFilename = string.Format("{0}", picture.SeoFilename);
            else if (String.IsNullOrEmpty(picture.SeoFilename) && !String.IsNullOrEmpty(picture.UpdatedToTicks))
                seoFilename = string.Format("{0}", picture.UpdatedToTicks);
            return seoFilename;
        }
        #endregion

        /// <summary>
        /// Gets the loaded picture binary depending on picture storage settings
        /// </summary>
        /// <param name="picture">Picture</param>
        /// <returns>Picture binary</returns>
        public virtual byte[] LoadPictureBinary(Picture picture)
        {
            return LoadPictureBinary(picture, false);
        }

        public void CreateThumbnail(Picture picture, int[] sizeThumbs, int quantity)
        {
            DeletePictureThumbs(picture);
            if (picture == null)
                throw new ArgumentNullException("picture");
            picture.UpdatedToTicks = DateTime.UtcNow.Ticks.ToString();
            picture.SizeThumbs = String.Join(",", sizeThumbs);
            var picThumbs = string.IsNullOrEmpty(picture.SizeThumbs) ? new List<string>() : picture.SizeThumbs.Split(',').ToList();
            var seoFileName = GetPictureSeNameTicks(picture);
            foreach (var targetSize in sizeThumbs)
            {
                string lastPart = GetFileExtensionFromMimeType(picture.MimeType);
                string thumbFileName;
                if (targetSize == 0)
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName)
                        ? string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), seoFileName, lastPart)
                        : string.Format("{0}.{1}", picture.Id.ToString("0000000"), lastPart);
                }
                else
                {
                    thumbFileName = !String.IsNullOrEmpty(seoFileName)
                        ? string.Format("{0}_{1}_{2}.{3}", picture.Id.ToString("0000000"), seoFileName, targetSize, lastPart)
                        : string.Format("{0}_{1}.{2}", picture.Id.ToString("0000000"), targetSize, lastPart);
                }
                string thumbFilePath = GetThumbLocalPath(thumbFileName);
                byte[] pictureBinary = null;
                if (picture != null)
                    pictureBinary = LoadPictureBinary(picture);
                if (pictureBinary != null && pictureBinary.Length > 0)
                {
                    byte[] pictureBinaryResized;

                    using (var stream = new MemoryStream(pictureBinary))
                    {
                        Bitmap b = null;
                        try
                        {
                            //try-catch to ensure that picture binary is really OK. Otherwise, we can get "Parameter is not valid" exception if binary is corrupted for some reasons
                            b = new Bitmap(stream);
                        }
                        catch (ArgumentException exc)
                        {
                            Console.WriteLine(string.Format("Error generating picture thumb. ID={0}", picture.Id),
                                exc);
                        }

                        if (b == null)
                        {
                            //bitmap could not be loaded for some reasons
                            return;
                        }
                        using (var destStream = new MemoryStream())
                        {
                            if (targetSize != 0)
                            {
                                var newSize = CalculateDimensions(b.Size, targetSize);
                                ImageBuilder.Current.Build(b, destStream, new ResizeSettings
                                {
                                    Width = newSize.Width,
                                    Height = newSize.Height,
                                    Scale = ScaleMode.Both,
                                    Quality = quantity
                                });
                            }
                            else
                            {
                                ImageBuilder.Current.Build(b, destStream, new ResizeSettings
                                {
                                    Width = b.Width,
                                    Height = b.Height,
                                    Scale = ScaleMode.Both,
                                    Quality = quantity
                                });
                            }
                            Console.WriteLine(quantity);
                            pictureBinaryResized = destStream.ToArray();
                            b.Dispose();
                        }

                    }
                    //resizing required

                    try
                    {
                        SaveThumb(thumbFilePath, thumbFileName, picture.MimeType, pictureBinaryResized);
                        if (!picThumbs.Any(x => x.ToLower() == targetSize.ToString().ToLower()))
                        {
                            picThumbs.Add(targetSize.ToString().ToLower());
                        }
                    }
                    catch { }

                }
                else
                {
                    Console.WriteLine("No Size");
                }

            }

        }
    }
}
