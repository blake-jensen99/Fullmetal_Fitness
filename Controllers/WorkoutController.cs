using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Fullmetal_Fitness.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

#pragma warning disable CS8629

namespace Fullmetal_Fitness.Controllers;

[SessionCheck]
public class WorkoutController : Controller
{
    private readonly ILogger<WorkoutController> _logger;

    private MyContext _context; 

    public WorkoutController(ILogger<WorkoutController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("/workouts/add")]
    public IActionResult NameWorkout()
    {
        return View("NameWorkout");
    }

    [HttpPost("/wokrouts/create")]
    public IActionResult CreateWorkout(Workout newWorkout) 
    {
        if (!ModelState.IsValid) 
        {
            return View("NameWorkout");
        }
        else 
        {
            newWorkout.UserId = (int)HttpContext.Session.GetInt32("UserId");
            _context.Add(newWorkout);
            _context.SaveChanges();
            return RedirectToAction("EditWorkout", new {id = newWorkout.WorkoutId});
        }
    }

    [HttpGet("/workouts/edit/{id}")]
    public IActionResult EditWorkout(int id) 
    {

        Workout? oneWorkout = _context.Workouts.Include(e => e.MyExercises).FirstOrDefault(w => w.WorkoutId == id);
        return View("EditWorkout", oneWorkout);
    }

    [HttpPost("/exercise/create")]
    public IActionResult AddExercise(Exercise newExercise) 
    {
        int id = newExercise.WorkoutId;

        if (!ModelState.IsValid)
        {
            Workout? oneWorkout = _context.Workouts.Include(e => e.MyExercises).FirstOrDefault(w => w.WorkoutId == id);
            return View("EditWorkout", oneWorkout);
        }
        else 
        {
            _context.Add(newExercise);
            _context.SaveChanges();
            return RedirectToAction ("EditWorkout", new {id = newExercise.WorkoutId});
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
