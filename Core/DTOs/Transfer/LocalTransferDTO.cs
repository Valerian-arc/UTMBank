using Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Transfer
{
    public class LocalTransferDTO
    {
        [Required]
        public int SourceCardId { get; set; }
        [Required]
        public int TargetCardId { get; set; }
        [Required]
        public double Amount { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
