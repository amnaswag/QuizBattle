using QuizBattle.Domain;
using QuizBattle.Application.Interfaces;
using System.Collections.ObjectModel;

namespace QuizBattle.Console
{
    public class InMemoryQuestionRepository : IQuestionRepository
    {
        public Task<Question?> GetByCodeAsync(string code, CancellationToken ct)
        {
            // seeda alla frågor
            var questions = QuestionUtils.SeedQuestions();

            // hämta den första frågan som matchar code
            var question = questions.FirstOrDefault(question => question.Code == code);

            // returner question asynkront
            return Task.FromResult(question);
        }

        // hämta en readonly-lista med exakt count antal frågor
        // Listan med frågor ska matcha category och difficulty om icke-null.
        public Task<IReadOnlyList<Question>> GetRandomAsync(
            string? category, 
            int? difficulty, 
            int count, 
            CancellationToken ct)
        {
            // seeda alla frågor
            var questions = QuestionUtils.SeedQuestions();

            // skapa slump-generatorn.
            var random = new Random();

            // välja slumpmässigt en fråga
            var randomizedQuestions = questions.OrderBy(question => random.NextInt64());

            // validera antalet matchande frågor.
            if (randomizedQuestions.Count() >= count)
            {
                throw new ArgumentOutOfRangeException(
                    $"Begärt antal frågor={count} men det fanns bara {randomizedQuestions.Count()} frågor.");
            }

            // returnerna count antal frågor. 
            return Task.FromResult<IReadOnlyList<Question>>(randomizedQuestions
                                        .Take(count)
                                        .ToList());
        }

        private static class QuestionUtils
        {
            public static List<Question> SeedQuestions()
            {
                return new List<Question>
                {
                    new Question(
                        "Q.CS.001",
                        "Vad gör 'using'-statement i C#?",
                        new[]
                        {
                            new Choice("Q.CS.001.A","Skapar en ny tråd"),
                            new Choice("Q.CS.001.B","Säkerställer korrekt Dispose av resurser"),
                            new Choice("Q.CS.001.C","Importerar ett NuGet-paket")
                        },
                        "Q.CS.001.B"
                    ),
                    new Question(
                        "Q.CS.002",
                        "Vad innebär 'var' i C#?",
                        new[]
                        {
                            new Choice("Q.CS.002.A","Dynamisk typ vid runtime"),
                            new Choice("Q.CS.002.B","Implicit, men statisk, typinferens vid compile-time"),
                            new Choice("Q.CS.002.C","Alias för object")
                        },
                        "Q.CS.002.B"
                    ),
                    new Question(
                        "Q.OOP.011",
                        "Vad beskriver inkapsling bäst?",
                        new[]
                        {
                            new Choice("Q.OOP.011.A","Göm implementation, exponera kontrollerat gränssnitt"),
                            new Choice("Q.OOP.011.B","Ärva från flera basklasser"),
                            new Choice("Q.OOP.011.C","Skapa statiska metoder")
                        },
                        "Q.OOP.011.A"
                    )
                };
            }
        }
    }
}
