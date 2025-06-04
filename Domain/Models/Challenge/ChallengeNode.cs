// Domain/Models/Challenge/ChallengeNode.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class ChallengeNode : NodeEntity<int>
    {
        public string? Title { get; set; }
        public bool IsProblem { get; set; } = false;
        public int? ProblemId { get; set; }
        public string Description { get; set; } = string.Empty;

        public ChallengeProblem? Problem { get; set; }
    }
}
