﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using ProjectFit.Resources;
using SQLite;
using Android.Support.V4.App;

namespace ProjectFit
{
    [Activity(Label = "WorkoutProcess")]
    public class WorkoutProcessActivity : FragmentActivity
    {
        private Workout thisWorkout;
        private List<WorkoutStep> workoutSteps;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.WorkoutProcess);
            ViewPager viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);

            var workoutId = Intent.GetIntExtra("workoutId", 1);
            var db  = new SQLiteConnection(DbHelper.GetLocalDbPath());
            thisWorkout = db.Get<Workout>(workoutId);

            workoutSteps = db.Table<WorkoutStep>().Where(x => x.WorkoutId == workoutId).ToList();

            WorkoutStepPagerAdapter adapter = new WorkoutStepPagerAdapter(SupportFragmentManager, workoutSteps);
            
            viewPager.Adapter = adapter;
            viewPager.AddView(new Button(this)
            {
                Text = "Test"
            });

        }


        private void WorkoutFinishedButton_Click(object sender, EventArgs e)
        {
            var workoutFinished = new Intent(this, typeof(MainActivity));
            workoutFinished.SetFlags(ActivityFlags.ClearTop);

            StartActivity(workoutFinished);
        }
    }
}