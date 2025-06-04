// Domain/Models/Challenge/ChallengeProblemTag.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class ChallengeProblemTag
    {
        public int ChallengeProblemId { get; set; }
        public int ChallengeTagId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
