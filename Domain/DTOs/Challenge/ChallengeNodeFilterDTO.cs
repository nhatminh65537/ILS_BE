// Domain/DTOs/Challenge/ChallengeNodeFilterDTO.cs
namespace ILS_BE.Domain.DTOs
{
    public class ChallengeNodeFilterDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<int>? TagIds { get; set; }
        public List<int>? CategoryIds { get; set; } 
        public List<int>? StateIds { get; set; }
        public string? SearchTerm { get; set; }
        public bool GetSolved { get; set; } = false;
        public bool IsProblem { get; set; } = false;
        public int ParentNodeId { get; set; } = 1;
    }
}
