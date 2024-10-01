using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.VMs
{
    public class DTNationality
    {
        public int id { get; set; } 
        public string title { get; set; }

        public int sort { get; set; }

        public int status { get; set; }

        public DateTime created { get; set; }

        public Guid created_by { get; set; }

        public DateTime? modified { get; set; }

        public Guid? modified_by { get; set; }
    }
}
