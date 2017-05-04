using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;
using ProjectFit.Resources;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "Project Fit", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Workout> mPremadeWorkoutList;
        private List<Workout> mCustomWorkoutList;
        private List<Exercise> AllExercises;
        private Button newWorkoutButton;
        private SQLiteConnection db;
        private ListView workoutListView;
        private Workout premadeUpperBodyWorkout;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            workoutListView = FindViewById<ListView>(Resource.Id.workoutListView);
            Button premadeWorkoutsButton = FindViewById<Button>(Resource.Id.premadeWorkoutsButton);
            Button customWorkoutsButton = FindViewById<Button>(Resource.Id.customWorkoutsButton);
            newWorkoutButton = FindViewById<Button>(Resource.Id.btnNewWorkout);
            newWorkoutButton.Visibility = Android.Views.ViewStates.Gone;

            mPremadeWorkoutList = new List<Workout>();

            db = new SQLiteConnection(DbHelper.GetLocalDbPath());
            
            db.CreateTable<Workout>();
            db.CreateTable<Exercise>();
            db.CreateTable<WorkoutStep>();

            db.DeleteAll<Exercise>();
            //db.DeleteAll<WorkoutStep>();

            //Add all possible exercsises now
            AllExercises = new List<Exercise>();
            Exercise WorkoutDone = new Exercise("Workout Finished",0,"None");
            AllExercises.Add(WorkoutDone);
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
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 6,
                Name = "Sit-Ups"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 7,
                Name = "Front Planks"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 8,
                Name = "Side Planks"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 9,
                Name = "Russian Twists"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 10,
                Name = "Russian Twists w/ Medicine Ball"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 11,
                Name = "Russian Twists w/ Plates"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 12,
                Name = "Leg Lifts"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Core",
                ExerciseId = 13,
                Name = "Crunches"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Arms",
                ExerciseId = 14,
                Name = "Overhead Press"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Back",
                ExerciseId = 15,
                Name = "Lat Pulldown"
            });
            AllExercises.Add(new Exercise
            {
                MuscleGroup = "Cardio",
                ExerciseId = 16,
                Name = "Running"
            });
            /*
             * MuscleGroup can be Arms, Legs, Core, Shoulders, back (for now)
             */

            var allWorkouts = db.Table<Workout>();

            List<WorkoutStep> upperBodySteps = new List<WorkoutStep>();
            premadeUpperBodyWorkout = new Workout("Arms", "Basic Upperbody", false,upperBodySteps);
            var workoutId = db.Insert(premadeUpperBodyWorkout);
            premadeUpperBodyWorkout.Id = workoutId;
            try{
                db.Get<WorkoutStep>(workoutId);
            }
            catch
            {
                upperBodySteps = new List<WorkoutStep>
                {
                    new WorkoutStep()
                    {
                        Reps = 8,
                        Sets = 3,
                        ExerciseId = 1,
                        WorkoutId = workoutId
                    },
                    new WorkoutStep()
                    {
                        Reps = 20,
                        Sets = 3,
                        ExerciseId = 4,
                        WorkoutId = workoutId
                    },
                    new WorkoutStep()
                    {
                        Reps = 8,
                        Sets = 3,
                        ExerciseId = 5,
                        WorkoutId = workoutId
                    },
                    new WorkoutStep()
                    {
                        Reps = 10,
                        Sets = 4,
                        ExerciseId = 2,
                        WorkoutId = workoutId
                    },
                    new WorkoutStep()
                    {
                        Reps = 0,
                        Sets = 0,
                        ExerciseId = 0,
                        WorkoutId = workoutId
                    }
                };
            }


            foreach (var exercise in AllExercises)
            {
                try
                {
                    db.Insert(exercise);
                }
                catch (Exception e)
                {
                    break;
                }
            }
            db.InsertAll(upperBodySteps);
            
            
            mPremadeWorkoutList.Add(premadeUpperBodyWorkout);


            LoadPremadeWorkouts();

            newWorkoutButton.Click += NewWorkoutButton_Click;
            workoutListView.ItemClick += WorkoutListViewOnItemClick;

            premadeWorkoutsButton.Click += (sender, args) =>
            {
                LoadPremadeWorkouts();
                newWorkoutButton.Visibility = Android.Views.ViewStates.Gone;
            };

            customWorkoutsButton.Click += (sender, args) =>
            {
                workoutListView.Adapter = null;
                newWorkoutButton.Visibility = Android.Views.ViewStates.Visible;
                LoadCustomWorkouts();
            };

        }

        private void NewWorkoutButton_Click(object sender, EventArgs e)
        {
            var newWorkoutActivity = new Intent(this, typeof(NewWorkoutActivity));
            newWorkoutActivity.SetFlags(ActivityFlags.ClearTop);
            StartActivity(newWorkoutActivity);

        }

        private void WorkoutListViewOnItemClick(object o, AdapterView.ItemClickEventArgs itemClickEventArgs)
        {
            var infoActivity = new Intent(this, typeof(WorkoutInfoActivity));
            if (mCustomWorkoutList.Count > 0)
            {
                infoActivity.PutExtra("workoutId", mCustomWorkoutList[itemClickEventArgs.Position].Id);
            }
            else
            {
                infoActivity.PutExtra("workoutId", mPremadeWorkoutList[itemClickEventArgs.Position].Id);
            }
            

            StartActivity(infoActivity);

        }

        private void LoadPremadeWorkouts()
        {
            //ArrayAdapter<Workout> adapter = new ArrayAdapter<Workout>(this, Android.Resource.Layout.ListContent, mPremadeWorkoutList);
            if (mPremadeWorkoutList.Count == 0)
            {
                mPremadeWorkoutList.Add(premadeUpperBodyWorkout);
            }
            var adapter = new WorkoutListAdapter(this,mPremadeWorkoutList);
            newWorkoutButton.Visibility = ViewStates.Gone;

            workoutListView.Adapter = adapter;
            mCustomWorkoutList = new List<Workout>();
        }

        private void LoadCustomWorkouts()
        {
            newWorkoutButton.Visibility = ViewStates.Visible;
            try
            {
                mCustomWorkoutList = db.Table<Workout>().Where(c => c.IsCustom).ToList();
            }
            catch (Exception e)
            {
                return;
            }
            var adapter = new WorkoutListAdapter(this, mCustomWorkoutList);

            workoutListView.Adapter = adapter;
            mPremadeWorkoutList = new List<Workout>();
        }

        protected override void OnRestart()
        {
            base.OnRestart();
            LoadPremadeWorkouts();
            
        }
    }
}

