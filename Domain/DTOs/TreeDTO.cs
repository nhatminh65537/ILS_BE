namespace ILS_BE.Domain.DTOs
{
    public class TreeDTO<NDTO>
    {
        public NDTO Item { get; set; } = default!;
        public List<TreeDTO<NDTO>> Children { get; set; } = default!;
    }
}
