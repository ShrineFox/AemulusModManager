using System;
using System.IO;
using System.Security.Cryptography;

namespace AemulusModManager
{
    class ChecksumUtils
    {
        public string GetChecksumString(string filePath)
        {
            string checksumString = null;

            // get md5 checksum of file
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    // get hash
                    byte[] currentFileSum = md5.ComputeHash(stream);
                    // convert hash to string
                    checksumString = BitConverter.ToString(currentFileSum).Replace("-", "");
                }
            }

            return checksumString;
        }
    }
}
