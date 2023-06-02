using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models
{
  public class Treat
    {
        public int TreatId { get; set; }
        [Required(ErrorMessage = "The treat name can't be empty")]
        public string TreatName { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Your price must be at least 1")]
        public int TreatPrice { get; set; }
        public List<FlavorTreat> FlavorTreatJoinEntities { get;}
        public ApplicationUser User { get; set; }

    }
}