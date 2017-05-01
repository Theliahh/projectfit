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
using ProjectFit.Resources;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "New Workout")]
    public class NewWorkoutBaseActivity : Activity
    {
        private List<WorkoutStep> currentSteps;
        private WorkoutStep newWorkoutStep;
        private ListView stepsListView;
        private SQLiteConnection db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewWorkoutBase);

            Button addNewExerciseButton = FindViewById<Button>(Resource.Id.btnAddNewExercise);
            TextView workoutBaseTextView = FindViewById<TextView>(Resource.Id.workoutBaseName);
            stepsListView = FindViewById<ListView>(Resource.Id.newWorkoutBaseExerciseList);
            currentSteps = new List<WorkoutStep>();

            workoutBaseTextView.Text = Intent.GetStringExtra("workoutName");

            db = new SQLiteConnection(DbHelper.GetLocalDbPath());


            addNewExerciseButton.Click += AddNewExerciseButton_Click;
        }

        private void AddNewExerciseButton_Click(object sender, EventArgs e)
        {
            var selectExercise = new Intent(this, typeof(SelectExercisesActivity));

            var workoutName = Intent.GetStringExtra("workoutName");

            selectExercise.PutExtra("workoutName", workoutName);

            StartActivityForResult(selectExercise, 1);

        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 1)
            {
                if (resultCode == Result.Ok)
                {
                    var reps = data.GetIntExtra("reps", 0);
                    var sets = data.GetIntExtra("sets", 1);
                    var exerciseId = data.GetIntExtra("exerciseId",0);

                    currentSteps.Add(new WorkoutStep
                    {
                        ExerciseId = exerciseId,
                        Reps = reps,
                        Sets = sets,
                    });

                    NewStepAdded();
                }
            }
        }

        private void NewStepAdded()
        {

            List<WorkoutStepDisplay> displaySteps = new List<WorkoutStepDisplay>();
            foreach (var workoutStep in currentSteps)
            {
                displaySteps.Add(new WorkoutStepDisplay
                {
                    Exercise = db.Get<Exercise>(workoutStep.ExerciseId),
                    Reps = workoutStep.Reps,
                    Sets = workoutStep.Sets
                });
            }

            var adapter = new NewWorkoutStepListAdapter(this, displaySteps);
            stepsListView.Adapter = adapter;
        }
    }
}