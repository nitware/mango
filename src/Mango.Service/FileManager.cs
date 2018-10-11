using System;
using System.Net;

using System.IO;

namespace Mango.Service
{
    public class FileManager
    {
        private const string BACK_SLASH = @"\";
        private const string FORWARD_SLASH = "/";

        public FileManager(string rootFolderPath, string destinationFolderName)
        {
            RootFolderPath = rootFolderPath;
            DestinationFolderName = destinationFolderName;
        }

        public string RootFolderPath { get; set; }
        public string DestinationFolderName { get; set; }

        public string GetFileName(string fileName)
        {
            try
            {
                FileInfo fi = new FileInfo(fileName);
                return fi.Name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string UploadFiles(string fileName, byte[] bytes, string folderName)
        {
            try
            {
                if (bytes.Length > 0)
                {
                    //set folder and file path
                    string folderPath = RootFolderPath + DestinationFolderName + folderName;
                    string filePath = folderPath + BACK_SLASH + fileName;
                    string virtualFileName = CreateFileName(filePath, fileName);

                    //create folder
                    filePath = folderPath + BACK_SLASH + virtualFileName;
                    CreateFolder(folderPath);

                    //read file content into file stream object
                    FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();

                    return filePath;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpexted error occurred during file upload opeartion!" + ex);
            }
        }

        public string CreateFileName(string filePath, string fileName)
        {
            try
            {
                FileInfo fiObj = new FileInfo(filePath);
                string fileExtension = fiObj.Extension;
                fileName = fileName.Replace(fileExtension, "").Replace("/", "-").Replace(@"\", "-") + "-" + DateTime.Now.ToString().Replace("/", "").Replace(" ", "").Replace(":", "");
                return fileName + fileExtension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //this method creates a folder in a specified path if does not already exist
        public void CreateFolder(string fileFolderPath)
        {
            try
            {
                //create folder if it does not exist
                if (!Directory.Exists(fileFolderPath))
                {
                    Directory.CreateDirectory(fileFolderPath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string[] GetFilePathAndUrl(string fileName, string fileFolderName, bool isUniqueName)
        {
            try
            {
                // Get the name, file path and url of the file to upload.
                string fileFolderUrl = "~/" + fileFolderName.Replace(BACK_SLASH, FORWARD_SLASH);
                string fileFolderPath = RootFolderPath + fileFolderName.Replace(FORWARD_SLASH, BACK_SLASH);
                string filePath = fileFolderPath + @"\" + fileName;         //to be persisted
                string fileUrl = fileFolderUrl + "/" + fileName;            //to be persisted
                string uniqueFileName = "";

                //create unique file name if required
                if (isUniqueName)
                {
                    uniqueFileName = CreateFileName(filePath, fileName);    //create unique file name (using system date & time)
                    filePath = fileFolderPath + @"\" + uniqueFileName;      //to be persisted
                    fileUrl = fileFolderUrl + "/" + uniqueFileName;         //to be persisted
                }

                //return file path & url in an array
                return new string[] { filePath, fileUrl };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string[] GetFolderPathAndUrl(string folderName)
        {
            try
            {
                // Get folder path and url.
                string fileFolderUrl = "~/" + folderName.Replace(BACK_SLASH, FORWARD_SLASH);
                string fileFolderPath = RootFolderPath + folderName.Replace(FORWARD_SLASH, BACK_SLASH);

                //return folder path & url in an array
                return new string[] { fileFolderPath, fileFolderUrl };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }






    }
}
