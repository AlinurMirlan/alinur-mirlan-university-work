namespace ProcessTracker.Library
{
    public static class ExtensionMethods
    {
        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo directoryInfo in directory.GetDirectories())
            {
                directoryInfo.Delete();
            }
        }
    }
}
