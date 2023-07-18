#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace Fullmetal_Fitness.Models;
public class Workout 
{
    [Key]
    public int WorkoutId {get; set;}

    [Required(ErrorMessage = "Workout name is Required")]
    [MinLength(2, ErrorMessage = "Workout name must be at least 2 characters long")]
    [MaxLength(55, ErrorMessage = "Workout name cannot be more than 55 chaacters long")]
    public string Name {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public int UserId {get; set;}
    public User? Creator {get; set;}

    public List<Exercise> MyExercises {get; set;} = new List<Exercise>();

}
