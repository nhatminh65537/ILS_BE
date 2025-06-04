// Domain/Models/Challenge/ChallengeWriteup.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class ChallengeWriteup : BaseEntity<int>
    {
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
