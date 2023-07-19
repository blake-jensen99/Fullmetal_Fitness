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
    [Range(1, Double.PositiveInfinity, ErrorMessage = "Number cannot be negative or zero")]
    public int Sets {get; set;}

    [Required(ErrorMessage = "Reps are required")]
    [Range(1, Double.PositiveInfinity, ErrorMessage = "Number cannot be negative or zero")]
    public int Reps {get; set;}

    [MaxLength(200, ErrorMessage = "Notes cannot be more than 200 character long")]
    [Display(Name = "Description")]
    public string Notes {get; set;}

    public int WorkoutId {get; set;}

    public Workout? Routine {get; set;}
}



// TO-DO

// Make route for delete button
// Make single page for workout
// Style dash
// Add logic for favorite