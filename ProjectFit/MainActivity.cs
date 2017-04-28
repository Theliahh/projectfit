using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using Environment = System.Environment;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "Project Fit", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<string> mItems;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            ListView workoutListView = FindViewById<ListView>(Resource.Id.workoutListView);
            Button premadeWorkoutsButton = FindViewById<Button>(Resource.Id.premadeWorkoutsButton);
            Button customWorkoutsButton = FindViewById<Button>(Resource.Id.customWorkoutsButton);


            var sqliteFileName = "workoutDatabase.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);

            var db = new SQLiteConnection(path);
            db.CreateTable<Workout>();


            //Add all possible exercsises now
            Exercise BenchPress  = new Exercise("Bench Press",1,"Arms");
            Exercise BarCurls = new Exercise("Bar Curls",2,"Arms");
            Exercise DumbellCurls = new Exercise("Dumbell Curls",3,"Arms");
            Exercise Pushups = new Exercise("Pushups",4,"Arms");
            Exercise InclineBench = new Exercise("Incline Bench Press", 5, "Arms");
            /*
             * MuscleGroup can be Arms, Legs, Core, Shoulders (for now)
             */

            List<WorkoutStep> upperBodySteps = new List<WorkoutStep>
            {
                new WorkoutStep()
                {
                    Reps = 8,
                    Sets = 3,
                    ExerciseId = 1
                },
                new WorkoutStep()
                {
                    Reps = 20,
                    Sets = 3,
                    ExerciseId = 4
                },
                new WorkoutStep()
                {
                    Reps = 8,
                    Sets = 3,
                    ExerciseId = 5
                },
                new WorkoutStep()
                {
                    Reps = 10,
                    Sets = 4,
                    ExerciseId = 2
                }
            };
            Workout premadeUpperBodyWorkout = new Workout("Arms", "Basic Upperbody", false,upperBodySteps);


            mItems = new List<string> {"one", "two", "three"};

            LoadPremadeWorkouts(workoutListView);

            premadeWorkoutsButton.Click += (sender, args) =>
            {
                LoadPremadeWorkouts(workoutListView);
            };

            customWorkoutsButton.Click += (sender, args) =>
            {
                workoutListView.Adapter = null;
            };

        }

        private void LoadPremadeWorkouts(ListView workoutListView)
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            workoutListView.Adapter = adapter;
        }
    }
}

