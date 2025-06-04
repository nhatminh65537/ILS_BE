// Domain/Models/Challenge/UserChallengeFinish.cs
using System;

namespace ILS_BE.Domain.Models
{
    public class UserChallengeFinish
    {
        public int UserId { get; set; }
        public int ChallengeId { get; set; }
        public DateTime FinishedAt { get; set; } = DateTime.UtcNow;
    }
}
