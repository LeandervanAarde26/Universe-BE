namespace UniVerServer.Models
{
    //This is a model that hides the password from the response on a request
    public class PersonDataObject
    {
        public int person_id { get; set; }
        public string person_system_identifier { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string person_email { get; set; } = string.Empty;
        public bool person_active { get; set; }
        public int role { get; set; }  = 0;
    }
}
