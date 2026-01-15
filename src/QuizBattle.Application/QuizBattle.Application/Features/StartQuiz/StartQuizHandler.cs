using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartQuiz;

public class StartQuizHandler
{
    private readonly IQuestionRepository _questionRepository;
    private readonly ISessionRepository _sessionRepository;

    public StartQuizHandler(IQuestionRepository questionRepository, ISessionRepository sessionRepository)
    {
        _questionRepository = questionRepository;
        _sessionRepository = sessionRepository;
    }

    public async Task<StartQuizResult> Handle(StartQuizCommand command, CancellationToken ct = default)
    {
        if (command.NumberOfQuestions <= 0)
        {
            throw new ArgumentException("Antal frågor måste vara mer än 0.");
        }

        var questions = await _questionRepository.GetRandomAsync(null, null, command.NumberOfQuestions, ct);
        var session = QuizSession.Create(command.NumberOfQuestions);

        await _sessionRepository.SaveAsync(session, ct);

        return new StartQuizResult
        {
            SessionId = session.Id,
            Questions = questions
        };
    }
}