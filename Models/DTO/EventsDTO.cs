using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UniVerServer.Models.DTO
{
    public class EventsDTO
    {
        public
         int event_id { get; set; }

         string event_name { get; set; }

         string event_description { get; set; }

         string staff_organiser { get; set; }

         string event_date { get; set; }
    }
}
