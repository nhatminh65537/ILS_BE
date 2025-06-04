// Domain/DTOs/Challenge/ChallengeNodeDTO.cs
using System;
using System.Collections.Generic;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeNodeDTO : BaseDTO<int>
    {
        public string? Title { get; set; }
        public bool IsProblem { get; set; }
        public string Description { get; set; } = string.Empty;

        public ChallengeProblemDTO? Problem { get; set; }
    }

    public class ChallengeNodeCreateOrUpdateDTO : BaseDTO<int>
    {
        public int ParentNodeId { get; set; }
        public string? Title { get; set; }
        public bool IsProblem { get; set; }
        public int? ProblemId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
