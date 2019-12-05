using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Utility
{
    public class Util
    {
        public static string ReadAllBytes(string Path)
        {
            // byte[] bytes = System.IO.File.ReadAllBytes(response.Result.ToString());
            string data = string.Empty;
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                data = Convert.ToBase64String(buffer);
            }
            return data;

        }
    }
}
