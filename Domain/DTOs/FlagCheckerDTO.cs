namespace ILS_BE.Domain.DTOs
{
    public class FlagCheckerSubmitDTO
    {
        public string Flag { get; set; } = string.Empty;
        public int challengeId { get; set; }
    }

    public class FlagCheckerResultDTO
    {
        public bool IsCorrect { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
