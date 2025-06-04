// Domain/DTOs/Challenge/ChallengeProblemDTO.cs
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeProblemDTO : BaseDTO<int>
    {
        public string Title { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public ChallengeStateDTO ChallengeState { get; set; } = null!;
        public int Xp { get; set; } = 0;
        public ChallengeCategoryDTO Category { get; set; } = null!;
        public List<ChallengeFileDTO> Files { get; set; } = [];
        public List<ChallengeTagDTO> Tags { get; set; } = [];

        // User specific information
        public bool IsSolved { get; set; } = false;
    }

    public class ChallengeProblemCreateOrUpdateDTO : BaseDTO<int>
    {
        public int ParentNodeId { get; set; } = 0;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public int ChallengeStateId { get; set; }
        public string Flag { get; set; } = null!;
        public int Xp { get; set; } = 0;
        public int CategoryId { get; set; }
        public List<int> TagIds { get; set; } = [];
    }
}
