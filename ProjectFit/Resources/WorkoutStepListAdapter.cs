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
        public TextView txtReps { get; set; }
        public TextView txtSets { get; set; }

    }
    public class WorkoutStepListAdapter : BaseAdapter
    {
        private Activity activity;
        private List<WorkoutStepDisplay> workoutSteps;

        public WorkoutStepListAdapter(Activity activity, List<WorkoutStepDisplay> workout)
        {
            this.activity = activity;
            this.workoutSteps = workout;
        }

        public override int Count => workoutSteps.Count;
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return workoutSteps[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.WorkoutStepListLayout, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.workoutStepsListName);
            var txtGroup = view.FindViewById<TextView>(Resource.Id.workoutStepsListGroup);
            var txtSets = view.FindViewById<TextView>(Resource.Id.workoutStepsListSets);
            var txtReps = view.FindViewById<TextView>(Resource.Id.workoutStepsListReps);

            txtName.Text = workoutSteps[position].Exercise.Name;
            txtGroup.Text = workoutSteps[position].Exercise.MuscleGroup;
            txtSets.Text = "Sets: " + workoutSteps[position].Sets;
            txtReps.Text = "Reps: " + workoutSteps[position].Reps;



            return view;
        }
    }
}