namespace MusicPlatformApi.Services
{
    public class FileHandler
    {
        public string ImageFilesPath { get; set; }
        public string SongFilesPath { get; set; }
        public FileHandler(string imageFilesPath, string songFilesPath)
        {
            ImageFilesPath = imageFilesPath;
            SongFilesPath = songFilesPath;
        }
        public void DeleteSong(string songFileName) => DeleteFile(SongFilesPath, songFileName);
        public void DeleteImage(string imageFileName) => DeleteFile(ImageFilesPath, imageFileName);
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            switch (fileExtension)
            {
                case ".png":
                case ".jpg":
                case ".jpeg":
                    break;
                default:
                    throw new ArgumentException($"Invalid file extension: {fileExtension}");
            }

            string imageFileName = await UploadFile(file, ImageFilesPath);
            return imageFileName;
        }

        public async Task<string> UploadSong(IFormFile file)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));

            string fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            switch (fileExtension)
            {
                case ".mp3":
                    break;
                default:
                    throw new ArgumentException($"Invalid file extension: {fileExtension}");
            }

            string songImageFile = await UploadFile(file, SongFilesPath);
            return songImageFile;
        }

        private async Task<string> UploadFile(IFormFile file, string basePath)
        {
            if (file is null)
                throw new ArgumentNullException(nameof(file));
            string fileName = file.FileName;
            string filePath = Path.Combine(basePath, fileName);
            using FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate);
            await file.CopyToAsync(stream);
            return fileName;
        }
        private void DeleteFile(string basePath, string fileName)
        {
            string filePath = Path.Combine(basePath, fileName);
            File.Delete(filePath);
        }
    }
}
