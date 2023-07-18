#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Fullmetal_Fitness.Models;
public class Exercise 
{
    [Key]
    public int ExerciseId {get; set;}

    [Required(ErrorMessage = "Exercise name is required")]
    public string Name {get; set;}

    [Required (ErrorMessage = "Sets are required")]
    public int Sets {get; set;}

    [Required(ErrorMessage = "Reps are required")]
    public int Reps {get; set;}

    [MaxLength(200, ErrorMessage = "Notes cannot be more than 200 character long")]
    public string Notes {get; set;}

    public int WorkoutId {get; set;}

    public Workout? Routine {get; set;}
}