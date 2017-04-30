using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjectFit.Resources;
using SQLite;
using Environment = System.Environment;

namespace ProjectFit
{
    [Activity(Label = "WorkoutInfoActivity")]
    public class WorkoutInfoActivity : Activity
    {
        private List<Exercise> AllExercises;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutInfo);

            var workoutId = Intent.GetIntExtra("workoutId", 0);

            Button btnStartButton = FindViewById<Button>(Resource.Id.btnWorkoutInfoStart);
            ListView exercsiseListView = FindViewById<ListView>(Resource.Id.workoutListView);

            var sqliteFileName = "workoutDatabaseTest1.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);

            var db = new SQLiteConnection(path);
            AllExercises = db.Table<Exercise>().ToList();
            var workoutToDisplay = db.Get<Workout>(workoutId);
            List<WorkoutStepDisplay> displaySteps = new List<WorkoutStepDisplay>();
            var stepsQuery = db.Table<WorkoutStep>();
            stepsQuery = stepsQuery.Where(c => c.WorkoutId == workoutToDisplay.Id);
            workoutToDisplay.Steps = stepsQuery.ToList();
            var workoutSteps = workoutToDisplay.Steps;
            foreach (var workoutStep in workoutSteps)
            {
                displaySteps.Add(new WorkoutStepDisplay
                {
                    Exercise = db.Get<Exercise>(workoutStep.ExerciseId),
                    Reps = workoutStep.Reps,
                    Sets = workoutStep.Sets
                });
            }

            var adapter = new WorkoutStepListAdapter(this, displaySteps);

            exercsiseListView.Adapter = adapter;



        }
    }
}