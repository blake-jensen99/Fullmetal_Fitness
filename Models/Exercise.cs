#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Fullmetal_Fitness.Models;
public class Exercise 
{
    [Key]
    public int ExerciseId {get; set;}

    [Required(ErrorMessage = "Exercise name is required")]
    public string Name {get; set;}

    [MaxLength(200, ErrorMessage = "Description cannot be more than 200 character long")]
    public string Description {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int WorkoutId {get; set;}

    public Workout? Routine {get; set;}

    
}


