namespace GestionOT5.Utilities
{
    public static class StreamExtensions
    {
        public static async Task<byte[]> ReadAllBytes(this Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                await instream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
