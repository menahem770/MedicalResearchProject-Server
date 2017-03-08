using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRP.DAL.Services
{
    public class RandomPasswordGenerator
    {
        public static string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            Enumerable.Range(0, length).Select(n => res.Append(valid[rnd.Next(valid.Length)])).ToList();
            return res.ToString();
        }
    }
}
