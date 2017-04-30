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
    [Table("exercises")]
    public class Exercise
    {
        [Unique]
        public string Name { get; set; }
        //TODO: steps for each exercise - private List<ExerciseStep> Steps;
        [PrimaryKey]
        public int ExerciseId { get; set; }
        public string MuscleGroup { get; set; }

        public Exercise(string name, int id, string muscleGroup)
        {
            Name = name;
            ExerciseId = id;
            MuscleGroup = muscleGroup;
            //Steps = steps;
        }

        public Exercise()
        {
            
        }
    }
}