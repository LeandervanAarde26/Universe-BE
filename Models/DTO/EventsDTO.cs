using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniVerServer.Models.DTO
{
    public class EventsDTO
    {

        public int event_id { get; set; }

        public string event_name { get; set; }

        public string event_description { get; set; }

        public string staff_organiser { get; set; }

        public string event_date { get; set; }
    }
}
