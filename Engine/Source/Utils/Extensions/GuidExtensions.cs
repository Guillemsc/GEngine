namespace GEngine.Utils.Extensions
{
    public static class GuidExtensions
    {
        /// <summary>
        /// Combines two <see cref="Guid"/> into one.
        /// </summary>
        public static Guid Combine(Guid guid1, Guid guid2)
        {
            const int bytecount = 16;

            byte[] destinationBytes = new byte[bytecount];
            byte[] guid1Bytes = guid1.ToByteArray();
            byte[] guid2Bytes = guid2.ToByteArray();

            for (int i = 0; i < bytecount; i++)
            {
                destinationBytes[i] = (byte)(guid1Bytes[i] ^ guid2Bytes[i]);
            }

            return new Guid(destinationBytes);
        }
    }
}
