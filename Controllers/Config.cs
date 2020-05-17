namespace MyBicycle.Controllers
{
    public partial class Config
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string License { get; }

        public Config()
        {
            Name = Properties.Settings.Default["NAME"].ToString();
            Street = Properties.Settings.Default["STREET"].ToString();
            City = Properties.Settings.Default["CITY"].ToString();
            Post = Properties.Settings.Default["POST"].ToString();
            Phone = Properties.Settings.Default["PHONE"].ToString();
            Email = Properties.Settings.Default["EMAIL"].ToString();
            License = Properties.Settings.Default["SERIAL"].ToString();
            Properties.Settings.Default.Save();
        }
    }
}