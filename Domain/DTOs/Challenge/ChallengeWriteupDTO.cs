// Domain/DTOs/Challenge/ChallengeWriteupDTO.cs
using System;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeWriteupDTO : BaseDTO<int>
    {
        public int ChallengeId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
