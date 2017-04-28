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
        private string MuscleGroup { get; set; }
        private string Name { get; set; }
        [PrimaryKey,AutoIncrement]
        private int Id { get; set; }
        private List<WorkoutStep> steps;
        private bool isCustom;

        public Workout(string muscleGroup, string name, bool custom)
        {
            isCustom = custom;
            MuscleGroup = muscleGroup;
            Name = name;
        }

    }
}