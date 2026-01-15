namespace QuizBattle.Application.Features.FinishQuiz;

public class FinishQuizCommand
{
    public Guid SessionId { get; set; }

    public FinishQuizCommand(Guid sessionId)
    {
        SessionId = sessionId;
    }
}