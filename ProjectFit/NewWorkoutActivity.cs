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
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewWorkout);

            Button continueButton = FindViewById<Button>(Resource.Id.btnNewWorkoutContinue);
            EditText txtNewWorkout = FindViewById<EditText>(Resource.Id.txtNewWorkoutText);
        }
    }
}