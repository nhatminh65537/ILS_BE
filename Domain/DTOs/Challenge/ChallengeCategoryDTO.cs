// Domain/DTOs/Challenge/ChallengeCategoryDTO.cs
using System;

namespace ILS_BE.Domain.DTOs
{
    public class ChallengeCategoryDTO : BaseDTO<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
