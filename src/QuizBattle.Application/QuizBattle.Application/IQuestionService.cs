using QuizBattle.Domain;

namespace QuizBattle.Application.Interfaces
{
    public interface IQuestionService
    {
        void DisplayQuestion(Question question, int number);
        Question GetRandomQuestion();
        List<Question> GetRandomQuestions(int count = 3);
        int PromptForAnswer(Question question);
    }
}