namespace NotificationCenterFE.Models
{
    public class Token
    {
        public string aud { get; set; }
        public string iss { get; set; }
        public int iat { get; set; }
        public int nbf { get; set; }
        public int exp { get; set; }
        public string aio { get; set; }
        public string azp { get; set; }
        public string azpacr { get; set; }
        public string name { get; set; }
        public string oid { get; set; }
        public string preferred_username { get; set; }
        public string rh { get; set; }
        public string scp { get; set; }
        public string sub { get; set; }
        public string tid { get; set; }
        public string uti { get; set; }
        public string ver { get; set; }


    }
}