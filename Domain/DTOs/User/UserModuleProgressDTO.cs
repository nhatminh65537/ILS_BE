namespace ILS_BE.Domain.DTOs
{
    public class UserModuleProgressDTO
    {
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public LearnProgressStateDTO ProgressState { get; set; } = null!;
        public float ProgressPercentage { get; set; }
    }

    public class UserModuleProgressCreateOrUpdateDTO
    {
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public int ProgressStateId { get; set; }
    }
}
