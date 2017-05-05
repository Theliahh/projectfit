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
    [Activity(Label = "WorkoutStartActivity")]
    public class WorkoutStartActivity : Activity
    {
        private Workout thisWorkout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutStartLayout);

            Button startButton = FindViewById<Button>(Resource.Id.btnWorkoutStartStart);
            Button editButton = FindViewById<Button>(Resource.Id.btnWorkoutStartEdit);
            TextView workoutNameText = FindViewById<TextView>(Resource.Id.workoutStartName);

            var db = new SQLiteConnection(DbHelper.GetLocalDbPath());

            thisWorkout = db.Get<Workout>(Intent.GetIntExtra("workoutId", 1));
            db.Close();
            workoutNameText.Text = thisWorkout.Name;
            this.Title = thisWorkout.Name;
            startButton.Click += StartButton_Click;
            editButton.Click += EditButton_Click;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            var workoutEditActivity = new Intent(this, typeof(EditWorkoutActivity));
            workoutEditActivity.PutExtra("workoutId", thisWorkout.Id);
            StartActivity(workoutEditActivity);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var workoutProcess = new Intent(this,typeof(WorkoutProcessActivity));
            workoutProcess.PutExtra("workoutId", Intent.GetIntExtra("workoutId", 0));

            StartActivity(workoutProcess);
        }
    }
}