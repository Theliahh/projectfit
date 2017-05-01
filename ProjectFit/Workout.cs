using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace ProjectFit
{
    [Table("workouts")]
    public class Workout
    {
        public string MuscleGroup { get; set; }
        public string Name { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public bool IsCustom { get; set; }
        public List<WorkoutStep> Steps;
        
        public Workout(string muscleGroup, string name, bool custom,List<WorkoutStep> newSteps)
        {
            IsCustom = custom;
            MuscleGroup = muscleGroup;
            Name = name;
            Steps = newSteps;
        }

        public Workout()
        {
            
        }

    }
}