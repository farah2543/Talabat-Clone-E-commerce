

using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public record BasketItemDTO
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string PictureUrl { get; init; }


        [Range(1, 99)]
        public int Quantity { get; init; }

        [Range(1, double.MaxValue)]

        public decimal Price { get; init; }

        public string Brand { get; init; }

        public string Category { get; init; } 
    }
}
