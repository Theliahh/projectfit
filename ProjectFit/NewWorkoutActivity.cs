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
    [Activity(Label = "NewWorkoutActivity")]
    public class NewWorkoutActivity : Activity
    {
        private string newWorkoutName;
        private EditText txtNewWorkout;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewWorkout);

            Button continueButton = FindViewById<Button>(Resource.Id.btnNewWorkoutContinue);
            txtNewWorkout = FindViewById<EditText>(Resource.Id.txtNewWorkoutText);

            continueButton.Click += ContinueButton_Click;
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            newWorkoutName = txtNewWorkout.Text;
            var newWorkoutBase = new Intent(this, typeof(NewWorkoutBaseActivity));

            newWorkoutBase.PutExtra("workoutName", newWorkoutName);
            
            StartActivity(newWorkoutBase);
        }
    }
}