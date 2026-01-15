namespace QuizBattle.Application.Features.StartQuiz;

public class StartQuizCommand
{
    public int NumberOfQuestions { get; set; }
    public string? Category { get; set; }
    public int? Difficulty { get; set; }

    public StartQuizCommand(int count, string? category, int? difficulty)
    {
        NumberOfQuestions = count;
        Category = category;
        Difficulty = difficulty;
    }
}