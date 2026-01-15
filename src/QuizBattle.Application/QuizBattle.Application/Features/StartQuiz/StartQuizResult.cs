using QuizBattle.Domain;

namespace QuizBattle.Application.Features.StartQuiz;

public class StartQuizResult
{
    public Guid SessionId { get; set; }
    public IReadOnlyList<Question> Questions { get; set; }
}