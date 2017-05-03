using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ProjectFit.Resources;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "WorkoutProcess")]
    public class WorkoutProcessActivity : Activity
    {
        private Workout thisWorkout;
        private List<WorkoutStep> workoutSteps;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.WorkoutProcess);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var workoutId = Intent.GetIntExtra("workoutId", 1);
            var db  = new SQLiteConnection(DbHelper.GetLocalDbPath());
            thisWorkout = db.Get<Workout>(workoutId);

            workoutSteps = db.Table<WorkoutStep>().Where(x => x.WorkoutId == workoutId).ToList();

            viewPager.Adapter = new WorkoutStepPagerAdapter(this, workoutSteps);

        }
    }
}