using ILS_BE.Application.Interfaces;
using ILS_BE.Domain.DTOs;
using ILS_BE.Domain.Interfaces;
using ILS_BE.Domain.Models;

namespace ILS_BE.Application.Services
{
    public class FlagCheckerService
    {
        private readonly IAuthService _authService;
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<UserChallengeFinish> _userChallengeFinishReposity;
        private readonly IRepository<ChallengeProblem> _challengeRepository;

        public FlagCheckerService(
            IAuthService authService,
            IRepository<UserProfile> userProfileRepository,
            IRepository<UserChallengeFinish> userChallengeFinishReposity,
            IRepository<ChallengeProblem> challengeRepository
            )
        {
            _authService = authService;
            _userProfileRepository = userProfileRepository;
            _userChallengeFinishReposity = userChallengeFinishReposity;
            _challengeRepository = challengeRepository;
        }

        public async Task<FlagCheckerResultDTO> CheckFlagAsync(FlagCheckerSubmitDTO req)
        {
            var chall = await _challengeRepository.GetByIdAsync(req.challengeId) ?? throw new Exception("Challenge not found");
            var userId = _authService.GetUserId();

            var userChall = await _userChallengeFinishReposity.GetFirstWhereAsync(x => x.UserId == userId && x.ChallengeId == req.challengeId);
            if (userChall != null)
            {
                return new FlagCheckerResultDTO
                {
                    IsCorrect = true,
                    Message = "You have already completed this challenge."
                };
            }

            var isCorrect = CheckFlagInternalAsync(req.Flag, chall.Flag);

            if (isCorrect)
            {
                var userChallengeFinish = new UserChallengeFinish
                {
                    UserId = userId,
                    ChallengeId = req.challengeId,
                    FinishedAt = DateTime.UtcNow
                };
                await _userChallengeFinishReposity.AddAsync(userChallengeFinish);
                await _userChallengeFinishReposity.SaveAsync();

                var userProfile = await _userProfileRepository.GetByIdAsync(userId) ?? throw new Exception("User not found");
                userProfile.Xp += chall.Xp;
                await _userProfileRepository.UpdateAsync(userProfile);
                await _userProfileRepository.SaveAsync();
            }
            
            return new FlagCheckerResultDTO
            {
                IsCorrect = isCorrect,
                Message = isCorrect ? "Flag is correct!" : "Flag is incorrect."
            };
        }

        private bool CheckFlagInternalAsync(string flag, string challFlag)
        {
            return string.Equals(flag, challFlag);
        }
    }
}
