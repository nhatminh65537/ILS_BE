// Domain/DTOs/Challenge/ChallengeFileDTO.cs
using System;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeFileDTO : BaseDTO<int>
    {
        public int ChallengeId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
