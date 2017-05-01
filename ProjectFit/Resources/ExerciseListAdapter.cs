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
    public class ExerciseListAdapter : BaseAdapter
    {

        Context context;

        public ExerciseListAdapter(Context context)
        {
            this.context = context;
        }

        private Activity activity;
        private List<Exercise> exercises;

        public ExerciseListAdapter(Activity activity, List<Exercise> exercises)
        {
            this.activity = activity;
            this.exercises = exercises;
        }

        public override int Count => exercises.Count;
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return exercises[position].ExerciseId;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.ExerciseListLayout, parent, false);

            var txtName = view.FindViewById<TextView>(Resource.Id.exerciseListName);
            var txtGroup = view.FindViewById<TextView>(Resource.Id.exerciseListMuscleGroup);

            txtName.Text = exercises[position].Name;
            txtGroup.Text = exercises[position].MuscleGroup;

            return view;
        }

    }

    class ExerciseListAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}