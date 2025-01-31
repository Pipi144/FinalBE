using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class GameAttempt
{
    [Key] public long AttemptId { get; set; }

    public int Score { get; set; } = 0;

    public DateTime AttemptedDate { get; set; } = DateTime.UtcNow;

    public long AttemptByUserId { get; set; }
    public User AttemptByUser { get; set; }
    public long GameId { get; set; }
    public Game Game { get; set; }

    public List<GameQuestion> GameQuestions { get; set; } = new List<GameQuestion>();


    public void CheckAnswers(long questionId, string userAnswer)
    {
        // var matchingQuestion = GameQuestions.FirstOrDefault(q=>q.Id == questionId);
        // if (matchingQuestion == null) throw new NullReferenceException($"Question {questionId} not found");
        //
        // var gameRules = Game.GameRules.ToList();
        // var matchingRule = gameRules.FirstOrDefault(r => r.DivisibleNumber == matchingQuestion.QuestionNumber);
        // if (matchingRule != null)
        // {
        //     if (userAnswer == matchingRule.ReplacedWord)
        //     {
        //         m
        //     }
        // }
    }
}