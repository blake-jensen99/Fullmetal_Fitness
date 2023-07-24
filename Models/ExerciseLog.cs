#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;

namespace Fullmetal_Fitness.Models;

public class ExerciseLog 
{
    [Key]
    public int ExerciseLogId {get; set;}

    [Required]
    public string Name {get; set;}

    [Required (ErrorMessage = "Test")]
    public int Reps {get; set;}

    [Required (ErrorMessage = "Test")]
    public int Weight {get; set;}

    [Required (ErrorMessage = "Test")]
    public string Date {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int WorkoutId {get; set;}

    public Workout? Lift {get; set;}
}