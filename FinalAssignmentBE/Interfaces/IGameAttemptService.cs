using FinalAssignmentBE.Dto;

namespace FinalAssignmentBE.Interfaces;

public interface IGameAttemptService
{
    public Task<GameAttemptDto> CreateGameAttempt(AddGameAttemptDto payload);
    public Task<GameAttemptDto> GetGameAttempt(int id);
}