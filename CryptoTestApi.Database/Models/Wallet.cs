using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoTestApi.Database.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public decimal? Balance { get; set; }
    }
}
