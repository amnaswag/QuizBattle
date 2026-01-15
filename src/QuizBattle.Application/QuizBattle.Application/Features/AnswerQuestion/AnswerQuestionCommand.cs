namespace QuizBattle.Application.Features.AnswerQuestion;

public class AnswerQuestionCommand
{
    public Guid SessionId { get; set; }
    public string QuestionCode { get; set; }
    public string SelectedChoiceCode { get; set; }
}