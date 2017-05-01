using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
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
            Button doneNewExerciseButton = FindViewById<Button>(Resource.Id.btnDoneAddingExercise);
            TextView workoutBaseTextView = FindViewById<TextView>(Resource.Id.workoutBaseName);
            stepsListView = FindViewById<ListView>(Resource.Id.newWorkoutBaseExerciseList);
            currentSteps = new List<WorkoutStep>();

            workoutBaseTextView.Text = Intent.GetStringExtra("workoutName");

            db = new SQLiteConnection(DbHelper.GetLocalDbPath());

            db.CreateTable<Workout>();
            addNewExerciseButton.Click += AddNewExerciseButton_Click;
            doneNewExerciseButton.Click += DoneNewExerciseButtonOnClick;
        }

        private void DoneNewExerciseButtonOnClick(object sender, EventArgs eventArgs)
        {
            Workout finishedWorkout = new Workout
            {
                Name = Intent.GetStringExtra("workoutName"),
                IsCustom = true,
                Steps = currentSteps
            };
            var firstOrDefault = currentSteps.FirstOrDefault();
            if (firstOrDefault != null)
                finishedWorkout.MuscleGroup = db.Get<Exercise>(firstOrDefault.ExerciseId).MuscleGroup;
            //Add system to choose muscle group later I guess
            db.Insert(finishedWorkout);
            foreach (var step in currentSteps)
            {
                step.WorkoutId = finishedWorkout.Id;
                db.Insert(step);
            }

            db.Close();
            Finish();
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