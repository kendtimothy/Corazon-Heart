using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides compression functionalities.
    /// </summary>
    public class CCompression
    {
        #region String Compression/De-Compression
        /// <summary>
        /// Compress string value.
        /// </summary>
        /// <param name="value">The string value to compress.</param>
        /// <returns>Returns the compressed string value if successful, null otherwise.</returns>
        public string Compress(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return Convert.ToBase64String(mso.ToArray());
            }
        }
        /// <summary>
        /// Uncompress a previously compressed string.
        /// </summary>
        /// <param name="compressed">The compressed string to uncompress.</param>
        /// <returns>Returns the uncompressed string if successful, null otherwise.</returns>
        public string Decompress(byte[] compressed)
        {
            using (var msi = new MemoryStream(compressed))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        /// <summary>
        /// Copy data from source stream to destination stream.
        /// </summary>
        /// <param name="source">The source stream to copy from.</param>
        /// <param name="destination">The destination stream to copy to.</param>
        private void CopyTo(Stream source, Stream destination)
        {
            // initialize a new byte array
            byte[] bytes = new byte[4096];

            // copy a chunk at a time (storing the chunk in temp everytime)
            int temp;
            while ((temp = source.Read(bytes, 0, bytes.Length)) != 0)
            {
                destination.Write(bytes, 0, temp);
            }
        }
        #endregion
    }
}