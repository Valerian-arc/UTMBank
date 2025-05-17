using Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.CardActions
{
    public class DepositTransferDTO
    {
        [Required]
        public int CardId { get; set; }
        [Required]
        public double Amount { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string Cvv { get; set; }

        public List<Card> Cards { get; set; }
    }
}
