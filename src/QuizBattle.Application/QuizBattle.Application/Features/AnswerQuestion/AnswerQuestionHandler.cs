using QuizBattle.Application.Interfaces;
using QuizBattle.Domain;

namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionHandler
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IQuestionRepository _questionRepository;

    public AnswerQuestionHandler(ISessionRepository sessionRepository, IQuestionRepository questionRepository)
    {
        _sessionRepository = sessionRepository;
        _questionRepository = questionRepository;
    }

    public async Task<AnswerQuestionResult> Handle(AnswerQuestionCommand command, CancellationToken ct = default)
    {
        var session = await _sessionRepository.GetByIdAsync(command.SessionId, ct);
        var question = await _questionRepository.GetByCodeAsync(command.QuestionCode, ct);

        if (session == null || question == null)
        {
            throw new ArgumentException("Session eller fråga saknas.");
        }

        session.SubmitAnswer(question, command.SelectedChoiceCode, DateTime.UtcNow);
        await _sessionRepository.SaveAsync(session, ct);

        var lastAnswer = session.Answers.LastOrDefault();

        return new AnswerQuestionResult
        {
            IsCorrect = lastAnswer?.IsCorrect ?? false
        };
    }
}