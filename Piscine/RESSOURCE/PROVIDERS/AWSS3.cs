using Amazon.S3;
using RESSOURCE.CONFIGS;
using System;
using System.Configuration;
using System.IO;

namespace RESSOURCE.PROVIDERS
{
    public class AWSS3 : BASE
    {
        #region CREDENTIALS
        string serverName = ConfigurationManager.AppSettings["AWSSERVERREGION"];
        string bucket = ConfigurationManager.AppSettings["BUCKET"];
        string awsId = ConfigurationManager.AppSettings["AWSID"];
        string awsSecretId = ConfigurationManager.AppSettings["AWSIDSECRET"];
        string screenType, strPath;
        AmazonS3Client S3Client = null;
        HNDLR _hndlr = new HNDLR();

        #endregion

        public AWSS3()
        {
            AmazonS3Config s3Config = new AmazonS3Config();
            s3Config.ServiceURL = serverName;

            this.S3Client = new AmazonS3Client(awsId, awsSecretId, s3Config);
        }

        #region UPLOAD S3
        public string ImagesURL(Tuple<string, string> pathScreenshot, string campaign, string userName, string userLastName, string platform)
        {
            // PATH A RECUPERER
            if (pathScreenshot.Item1.Contains("LARGE"))
            {
                screenType = "Large";
            }
            else screenType = "Simple";
            strPath = pathScreenshot.Item2;
            var s3Uri = serverName + "/" + bucket + "/" + _hndlr.GetStringSha256Hash(userName + userLastName) + "/" + _hndlr.GetStringSha256Hash(campaign) + "/" + platform + "/" + screenType + "/" + strPath;
            return s3Uri;
        }

        public void UploadFile(bool deleteLocalFileOnSuccess, Tuple<string, string> pathScreenshot, string campaign, string userName, string userLastName, string platform)
        {
            // PATH A RECUPERER
            if (pathScreenshot.Item1.Contains("LARGE"))
            {
                screenType = "Large";
            }
            else screenType = "Simple";
            string s3Bucket = bucket + "/" + _hndlr.GetStringSha256Hash(userName + userLastName) + "/" + _hndlr.GetStringSha256Hash(campaign) + "/" + platform + "/" + screenType;
            //sauvegarde S3
            Amazon.S3.Model.PutObjectRequest s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest = new Amazon.S3.Model.PutObjectRequest();
            s3PutRequest.FilePath = pathScreenshot.Item1;
            s3PutRequest.BucketName = s3Bucket;
            s3PutRequest.CannedACL = S3CannedACL.PublicRead;
            //Clef - nouveau nom de fichier
            if (!string.IsNullOrWhiteSpace(pathScreenshot.Item1))
            {
                s3PutRequest.Key = pathScreenshot.Item2;
            }
            s3PutRequest.Headers.Expires = new DateTime(2020, 1, 1);
            try
            {
                Amazon.S3.Model.PutObjectResponse s3PutResponse = this.S3Client.PutObject(s3PutRequest);
                if (deleteLocalFileOnSuccess)
                {
                    //Delete local file
                    if (File.Exists(pathScreenshot.Item2))
                    {
                        File.Delete(pathScreenshot.Item2);
                    }
                }
            }
            catch (Exception ex)
            {
                //handle exceptions
            }
        }
        #endregion
    }
}