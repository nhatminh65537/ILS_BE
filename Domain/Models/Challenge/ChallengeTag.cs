// Domain/Models/Challenge/ChallengeTag.cs
using System;
using System.Collections.Generic;

namespace ILS_BE.Domain.Models
{
    public class ChallengeTag : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
