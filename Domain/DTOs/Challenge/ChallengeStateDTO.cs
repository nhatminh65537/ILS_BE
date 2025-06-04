// Domain/DTOs/Challenge/ChallengeStateDTO.cs
using System;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeStateDTO : BaseDTO<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
