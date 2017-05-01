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
using Java.Lang;

namespace ProjectFit
{
    [Activity(Label = "SelectedExerciseActivity")]
    public class SelectedExerciseActivity : Activity
    {
        private EditText textSets;
        private EditText textReps;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectedExercise);

            Button confirmButton = FindViewById<Button>(Resource.Id.btnSelectedExerciseContinue);
            textSets = FindViewById<EditText>(Resource.Id.selectedExerciseEditSets);
            textReps = FindViewById<EditText>(Resource.Id.selectedExerciseEditReps);

            confirmButton.Click += ConfirmButton_Click;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            int sets = -1;
            int reps = -1;
            try
            {
                sets = Integer.ParseInt(textSets.Text);

                reps = Integer.ParseInt(textReps.Text);
            }
            catch (System.Exception exception)
            {
                Console.WriteLine(exception);
                sets = 0;
                reps = 0;
            }

            if (sets > 0 && reps > 0)
            {
                Intent returnIntent = new Intent();
                returnIntent.PutExtra("reps", reps);
                returnIntent.PutExtra("sets", sets);
                returnIntent.PutExtra("exerciseId", Intent.GetIntExtra("exerciseId",0));

                SetResult(Result.Ok,returnIntent);

                Finish();
            }
            else
            {
                Toast.MakeText(ApplicationContext, "Reps and Sets must be greater than 0",ToastLength.Short).Show();
            }
        }
    }
}