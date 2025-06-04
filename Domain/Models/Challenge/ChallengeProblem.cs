// Domain/Models/Challenge/ChallengeProblem.cs
using System;
using System.Collections.Generic;

namespace ILS_BE.Domain.Models
{
    public class ChallengeProblem : BaseEntity<int>
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public int ChallengeStateId { get; set; }
        public string Flag { get; set; } = null!;
        public int CategoryId { get; set; }
        public int Xp { get; set; } = 0;

        public ChallengeState ChallengeState { get; set; } = null!;
        public ChallengeCategory Category { get; set; } = null!;
        public List<ChallengeFile> Files { get; set; } = [];
        public List<ChallengeTag> Tags { get; set; } = [];
    }
}
