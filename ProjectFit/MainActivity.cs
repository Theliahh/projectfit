using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.App;
using Android.Widget;
using Android.OS;
using ProjectFit.Resources;
using Environment = System.Environment;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "Project Fit", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Workout> mPremadeWorkoutList;
        private List<Exercise> AllExercises;
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
            mPremadeWorkoutList = new List<Workout>();

            var db = new SQLiteConnection(path);
            //db.CreateTable<Workout>();


            //Add all possible exercsises now
            AllExercises = new List<Exercise>();
            Exercise BenchPress  = new Exercise("Bench Press",1,"Arms");
            AllExercises.Add(BenchPress);
            Exercise BarCurls = new Exercise("Bar Curls",2,"Arms");
            AllExercises.Add(BarCurls);
            Exercise DumbellCurls = new Exercise("Dumbell Curls",3,"Arms");
            AllExercises.Add(DumbellCurls);
            Exercise Pushups = new Exercise("Pushups",4,"Arms");
            AllExercises.Add(Pushups);
            Exercise InclineBench = new Exercise("Incline Bench Press", 5, "Arms");
            AllExercises.Add(InclineBench);
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
            mPremadeWorkoutList.Add(premadeUpperBodyWorkout);


            LoadPremadeWorkouts(workoutListView);

            workoutListView.ItemClick += WorkoutListViewOnItemClick;

            premadeWorkoutsButton.Click += (sender, args) =>
            {
                LoadPremadeWorkouts(workoutListView);
            };

            customWorkoutsButton.Click += (sender, args) =>
            {
                workoutListView.Adapter = null;
            };

        }

        private void WorkoutListViewOnItemClick(object o, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            Console.WriteLine(mPremadeWorkoutList[itemClickEventArgs.Position].Name);
        }

        private void LoadPremadeWorkouts(ListView workoutListView)
        {
            //ArrayAdapter<Workout> adapter = new ArrayAdapter<Workout>(this, Android.Resource.Layout.ListContent, mPremadeWorkoutList);
            var adapter = new WorkoutListAdapter(this,mPremadeWorkoutList);

            workoutListView.Adapter = adapter;
        }
    }
}

