// Domain/DTOs/Challenge/ChallengeTagDTO.cs
using System;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeTagDTO : BaseDTO<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
