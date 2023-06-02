using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PierresTreats.Models
{
  public class Flavor
  {
    public int FlavorId { get; set; }
    [Required(ErrorMessage = "The flavor name can't be empty")]
    public string FlavorName { get; set; }
    public List<FlavorTreat> FlavorTreatJoinEntities { get;}
    public ApplicationUser User { get; set; }
  }
}