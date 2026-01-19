using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.FinishQuiz;

public class FinishQuizHandler
{
    private readonly ISessionRepository _sessionRepository;

    public FinishQuizHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<FinishQuizResult> Handle(FinishQuizCommand command, CancellationToken ct = default)
    {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);

        if (session == null)
        {
            throw new ArgumentException("Session saknas.");
        }

        session.Finish(DateTime.UtcNow);

        return new FinishQuizResult
        {
            Score = session.Score,
            AnsweredCount = session.Answers.Count
        };
    }
}