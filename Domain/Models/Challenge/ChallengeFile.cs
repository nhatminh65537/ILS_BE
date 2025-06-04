// Domain/Models/Challenge/ChallengeFile.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class ChallengeFile : BaseEntity<int>
    {
        public int ChallengeId { get; set; }
        public string FileName { get; set; } = null!;
        public string FilePath { get; set; } = null!;
    }
}
