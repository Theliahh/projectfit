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
    [Activity(Label = "Workout Info")]
    public class WorkoutInfoActivity : Activity
    {
        private List<Exercise> AllExercises;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutInfo);

            var workoutId = Intent.GetIntExtra("workoutId", 0);

            Button btnStartButton = FindViewById<Button>(Resource.Id.btnWorkoutInfoStart);
            ListView exerciseListView = FindViewById<ListView>(Resource.Id.workoutInfoListView);

            btnStartButton.Click += BtnStartButton_Click;

            var sqliteFileName = "workoutDatabaseTest2.db3";
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

            this.Title = workoutToDisplay.Name;
            var adapter = new WorkoutStepListAdapter(this, displaySteps);

            exerciseListView.Adapter = adapter;

        }

        private void BtnStartButton_Click(object sender, EventArgs e)
        {
            var workoutActivity = new Intent(this, typeof(WorkoutActivity));

            StartActivity(workoutActivity);
        }
    }
}