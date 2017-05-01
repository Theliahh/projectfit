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

namespace ProjectFit.Resources
{
    public class NewWorkoutStepListAdapter : BaseAdapter
    {

        Context context;

        public NewWorkoutStepListAdapter(Context context)
        {
            this.context = context;
        }

        private Activity activity;
        private List<WorkoutStepDisplay> Steps;

        public NewWorkoutStepListAdapter(Activity activity, List<WorkoutStepDisplay> steps)
        {
            this.activity = activity;
            this.Steps = steps;
        }

        public override int Count => Steps.Count;
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return Steps[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.NewWorkoutStepListLayout, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.newWorkoutStepListName);
            var txtReps = view.FindViewById<TextView>(Resource.Id.newWorkoutStepListReps);
            var txtSets = view.FindViewById<TextView>(Resource.Id.newWorkoutStepListSets);

            txtName.Text = Steps[position].Exercise.Name;
            txtReps.Text = Steps[position].Reps.ToString();
            txtSets.Text = Steps[position].Sets.ToString();

            return view;
        }
    }
}