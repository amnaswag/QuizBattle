namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionCommand
{
    public Guid SessionId { get; set; }
    public string QuestionCode { get; set; }
    public string SelectedChoiceCode { get; set; }

    public AnswerQuestionCommand(Guid sessionId, string questionCode, string selectedChoiceCode)
    {
        SessionId = sessionId;
        QuestionCode = questionCode;
        SelectedChoiceCode = selectedChoiceCode;
    }
}