

namespace Shared.DTOs
{
    public record BasketDTO
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDTO> items { get; init; }
    }
}
