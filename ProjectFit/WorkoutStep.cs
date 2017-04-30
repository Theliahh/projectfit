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
    [Table("WorkoutSteps")]
    public class WorkoutStep
    {
        public int WorkoutId { get; set; }
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }

    }
}