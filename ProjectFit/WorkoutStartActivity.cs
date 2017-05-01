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

namespace ProjectFit
{
    [Activity(Label = "WorkoutStartActivity")]
    public class WorkoutStartActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutStartLayout);

            Button startButton = FindViewById<Button>(Resource.Id.btnWorkoutStartStart);
            Button editButton = FindViewById<Button>(Resource.Id.btnWorkoutStartEdit);
            TextView workoutNameText = FindViewById<TextView>(Resource.Id.workoutStartName);

            startButton.Click += StartButton_Click;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            var workoutProcess = new Intent(this,typeof(WorkoutProcess));

            StartActivity(workoutProcess);
        }
    }
}