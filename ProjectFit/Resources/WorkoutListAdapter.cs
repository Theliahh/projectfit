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

namespace ProjectFit.Resources
{
    partial class ViewHolder : Java.Lang.Object
    {
        public TextView txtName { get; set; }
        public TextView txtGroup { get; set; }

    }
    public class WorkoutListAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Workout> workouts;

        public WorkoutListAdapter(Activity activity, List<Workout> workout)
        {
            this.activity = activity;
            this.workouts = workout;
        }

        public override int Count => workouts.Count;
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return workouts[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.WorkoutListLayout, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.workoutListName);
            var txtGroup = view.FindViewById<TextView>(Resource.Id.workoutListGroup);

            txtName.Text = workouts[position].Name;
            txtGroup.Text = workouts[position].MuscleGroup;

            return view;
        }
    }
}