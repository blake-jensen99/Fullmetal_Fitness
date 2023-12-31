﻿using System.Diagnostics;
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

    [HttpGet("/workouts/{id}")]
    public IActionResult OneWorkout(int id)
    {
        Workout? oneWorkout = _context.Workouts.Include(e => e.MyExercises).FirstOrDefault(w => w.WorkoutId == id);
        ViewBag.log = _context.ExerciseLogs.OrderBy(e => e.CreatedAt).Where(e => e.WorkoutId == id).ToList();
        return View("OneWorkout", oneWorkout);
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

    [HttpPost("/exercise/log/{id}")]
    public IActionResult LogExercise(ExerciseLog newExerciseLog, int id) 
    {
        if (!ModelState.IsValid)
        {
            Workout? oneWorkout = _context.Workouts.Include(e => e.MyExercises).FirstOrDefault(w => w.WorkoutId == id);
            return View("OneWorkout", oneWorkout);
        }
        else 
        {
            _context.Add(newExerciseLog);
            _context.SaveChanges();
            return RedirectToAction ("OneWorkout", new {id = id});
        }
    }

    [HttpPost("/exercise/destroy/{id}")]
    public IActionResult DestroyExercise(int id)
    {
        Exercise? exToDelete = _context.Exercises.SingleOrDefault(e => e.ExerciseId == id);
        int workId = exToDelete.WorkoutId;
        _context.Exercises.Remove(exToDelete);
        _context.SaveChanges();
        return RedirectToAction("EditWorkout", new {id = workId});
    }

    [HttpPost("/exercise/log/destroy/{id}")]
    public IActionResult DestroyExerciseLog(int id, int hidden)
    {
        ExerciseLog? exLogToDelete = _context.ExerciseLogs.SingleOrDefault(e => e.ExerciseLogId == hidden);
        int workId = exLogToDelete.WorkoutId;
        _context.ExerciseLogs.Remove(exLogToDelete);
        _context.SaveChanges();
        return RedirectToAction("OneWorkout", new {id = workId});
    }

    [HttpPost("/workouts/destroy/{id}")]
    public IActionResult DestroyWorkout(int id)
    {
        Workout? workToDelete = _context.Workouts.SingleOrDefault(w => w.WorkoutId == id);
        List<Exercise> exesToDelete = _context.Exercises.Where(e => e.WorkoutId == id).ToList();
        int workId = workToDelete.WorkoutId;
        _context.Workouts.Remove(workToDelete);
        _context.Exercises.RemoveRange(exesToDelete);
        _context.SaveChanges();
        return RedirectToAction("Dashboard", "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
