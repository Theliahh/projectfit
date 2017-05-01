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
    [Activity(Label = "NewWorkoutBaseActivity")]
    public class NewWorkoutBaseActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewWorkoutBase);

            Button addNewExerciseButton = FindViewById<Button>(Resource.Id.btnAddNewExercise);

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
                    var test = data.Data;
                    
                }
            }
        }
    }
}