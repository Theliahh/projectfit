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

namespace ProjectFit.Resources
{
    class WorkoutStepPagerAdapter : PagerAdapter
    {
        private List<WorkoutStep> workoutSteps;
        private Context context;
        private SQLiteConnection db;

        public WorkoutStepPagerAdapter(Context context, List<WorkoutStep> workoutSteps)
        {
            this.context = context;
            this.workoutSteps = workoutSteps;
            db = new SQLiteConnection(DbHelper.GetLocalDbPath());
        }
        public override bool IsViewFromObject(View view, Object objectValue)
        {
            return view == objectValue;
        }

        public override int Count => workoutSteps.Count;

        public override Java.Lang.Object InstantiateItem(View container, int position)
        {
            var textView = new TextView(context);
            var currentExercise = db.Get<Exercise>(workoutSteps[position].ExerciseId);
            textView.Text = currentExercise.Name;
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.AddView(textView);
            return textView;
        }

        public override void DestroyItem(View container, int position, Java.Lang.Object view)
        {
            var viewPager = container.JavaCast<ViewPager>();
            viewPager.RemoveView(view as View);
        }
    }
}