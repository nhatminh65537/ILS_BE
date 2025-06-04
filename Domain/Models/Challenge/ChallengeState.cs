// Domain/Models/Challenge/ChallengeState.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class ChallengeState : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
