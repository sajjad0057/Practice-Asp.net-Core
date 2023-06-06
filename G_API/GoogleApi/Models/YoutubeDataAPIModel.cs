using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;


namespace GoogleApi.Models
{
    public class YoutubeDataAPIModel
    {
        public YoutubeDataAPIModel()
        {             
        }

        public void Execute()
        {
            UserCredential credential;
            //using (var stream = new FileStream(@"C:/Users/sajjad/Downloads/client_secret.json", FileMode.Open, FileAccess.Read))
            //{
            //    string credPath = "token.json";
            //    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
            //        GoogleClientSecrets.FromStream(stream).Secrets,
            //        new[] { YouTubeService.Scope.YoutubeUpload },
            //        "user",
            //        CancellationToken.None,
            //        new FileDataStore(credPath, true)).Result;
            //}

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCsiJxSoCjWB87ogiS2c64cpoynPndS14U",
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var video = new Video();
            video.Snippet = new VideoSnippet();
            video.Snippet.Title = "My Test Video";
            video.Snippet.Description = "This is a test video.";
            video.Snippet.Tags = new string[] { "test", "video" };
            video.Snippet.CategoryId = "22"; // 22 represents the category ID for Entertainment
            video.Status = new Google.Apis.YouTube.v3.Data.VideoStatus();
            video.Status.PrivacyStatus = "public"; // Set as public, private, or unlisted

            var filePath = @"C:/Users/sajjad/Downloads/pexels-george-morina-2677752-1280x720-30fps.mp4";
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += VideosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += VideosInsertRequest_ResponseReceived;

                var uploadThread = new Thread(() =>
                {
                    videosInsertRequest.Upload();
                });

                uploadThread.Start();
                uploadThread.Join();
            }

            Console.WriteLine("Video uploaded successfully.");
            Console.ReadLine();
        }

        private static void VideosInsertRequest_ProgressChanged(IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Console.WriteLine($"{progress.BytesSent} bytes uploaded.");
                    break;
                case UploadStatus.Completed:
                    Console.WriteLine("Upload completed.");
                    break;
                case UploadStatus.Failed:
                    Console.WriteLine($"Upload failed. {progress.Exception.Message}");
                    break;
            }
        }

        private static void VideosInsertRequest_ResponseReceived(Google.Apis.YouTube.v3.Data.Video video)
        {
            Console.WriteLine($"Video ID: {video.Id}");
        }
    }
}
