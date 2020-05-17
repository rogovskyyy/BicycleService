using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyBicycle.Model;

namespace MyBicycle.Controllers
{
    public class Activate
    {
        private readonly string FILENAME = "APPLICATION32_x32.BIN";
        private readonly string FILENAMEWITHHASH = "APPLICATION64_x64.BIN";
        private static readonly HttpClient CLIENT = new HttpClient();

        public async Task<string> GetResponse(string myText)
        {
            var values = new Dictionary<string, string>
            {
                { "product_key_min", myText },
            };

            var content = new FormUrlEncodedContent(values);
            var response = await CLIENT.PostAsync("http://api.vaside.ct8.pl/index.php", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;

        }

        public async Task<bool> MakeRequest(string myText)
        {
            var response = await GetResponse(myText);
            Product deserializeData = JsonConvert.DeserializeObject<Product>(response);
            return await CheckIfFine(deserializeData, myText);
        }

        public async Task<bool> CheckIfFine(Product account, string securedKey)
        {
            if (account.Status == 1)
            {
                MessageBox.Show(account.Comment, "Informacja", MessageBoxButtons.OK);
                CreateFile(securedKey);
                CreateFileWithHash(securedKey);
                return true;
            }
            else
            {
                MessageBox.Show(account.Comment, "Informacja", MessageBoxButtons.OK);
                return false;
            }
        }

        public string GetMacAddress()
        {
            string macAddress = NetworkInterface
                            .GetAllNetworkInterfaces()
                            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                            .Select(nic => nic.GetPhysicalAddress().ToString())
                            .FirstOrDefault();

            return macAddress;
        }

        public string BuildCode(string key)
        {
            string buildCode = key + "(!)FAS)!_!@#faso)" + GetMacAddress();
            return buildCode;
        }

        public void CreateFile(string key)
        {
            if (!File.Exists(FILENAME))
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(FILENAME, FileMode.Create)))
                {
                    writer.Write(key);
                }
            }
        }

        public void CreateFileWithHash(string key)
        {
            if (!File.Exists(FILENAMEWITHHASH))
            {
                var data = System.Text.Encoding.UTF8.GetBytes(BuildCode(key));

                using (BinaryWriter writer = new BinaryWriter(File.Open(FILENAMEWITHHASH, FileMode.Create)))
                {
                    writer.Write(GetHash(data));
                }
            }

        }

        public string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                byte[] fileBytes = File.ReadAllBytes(path).Skip(1).ToArray();
                return System.Text.Encoding.UTF8.GetString(fileBytes);
            }

            return "";
        }

        public string GetHash(byte[] data)
        {
            using (SHA512 SHA = new SHA512Managed())
            {
                return System.Text.Encoding.UTF8.GetString(SHA.ComputeHash(data));
            }
        }

        public bool IsProductValidAndActivated()
        {
            string key = ReadFile(FILENAME);
            string hash = ReadFile(FILENAMEWITHHASH);
            string getH = GetHash(System.Text.Encoding.UTF8.GetBytes(BuildCode(key)));

            if (getH == hash)
            {
                return true;
            }
            return false;
        }
    }
}