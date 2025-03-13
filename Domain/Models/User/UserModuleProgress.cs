namespace ILS_BE.Domain.Models
{
    public class UserModuleProgress
    {
        public int UserId { get; set; }
        public int ModuleId { get; set; }
        public int ProgressStateId { get; set; }
    }
}

