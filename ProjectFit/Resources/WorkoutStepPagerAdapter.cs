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
using SQLite;
using Object = Java.Lang.Object;
using Android.Support.V4.App;

namespace ProjectFit.Resources
{
    public class WorkoutStepPagerAdapter : FragmentPagerAdapter
    {
        public List<WorkoutStep> workoutSteps;
        
        private SQLiteConnection db;

        public WorkoutStepPagerAdapter(Android.Support.V4.App.FragmentManager fm, List<WorkoutStep> workoutSteps)
            : base(fm)
        {
            this.workoutSteps = workoutSteps;
            db = new SQLiteConnection(DbHelper.GetLocalDbPath());
        }
        public override int Count => workoutSteps.Count;

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            var exerciseName = db.Get<Exercise>(workoutSteps[position].ExerciseId).Name;
            
            return WorkoutProcessFragment.newInstance(
                workoutSteps[position].Reps, exerciseName, workoutSteps[position].Sets);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
        {
            var exerciseName = db.Get<Exercise>(workoutSteps[position].ExerciseId).Name;
            return new Java.Lang.String(exerciseName);
        }
    }
}