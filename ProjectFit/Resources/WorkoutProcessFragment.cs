using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace ProjectFit.Resources
{
    public class WorkoutProcessFragment : Android.Support.V4.App.Fragment
    {

        public WorkoutProcessFragment()
        {
            
        }
        public static WorkoutProcessFragment newInstance(int numReps, string exerciseName, int numSets)
        {
            WorkoutProcessFragment fragment = new WorkoutProcessFragment();
            Bundle args = new Bundle();
            args.PutInt("numReps", numReps);
            args.PutString("exerciseName", exerciseName);
            args.PutInt("numSets", numSets);
            fragment.Arguments = args;

            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            string exerciseString = Arguments.GetString("exerciseName", "");
            int reps = Arguments.GetInt("numReps", 8);
            int sets = Arguments.GetInt("numSets", 3);

            View view = inflater.Inflate(Resource.Layout.WorkoutProcessPageLayout, container, false);
            TextView exerciseName = (TextView)view.FindViewById(Resource.Id.workoutProcessExerciseName);
            TextView numReps = (TextView)view.FindViewById(Resource.Id.workoutProcessNumReps);
            TextView numSets = (TextView)view.FindViewById(Resource.Id.workoutProcessNumSets);
            TextView setsTextView = (TextView) view.FindViewById(Resource.Id.workoutProcessSets);
            TextView repsTextView = (TextView) view.FindViewById(Resource.Id.workoutProcessReps);

            if (exerciseString == "Workout Finished")
            {
                numReps.Visibility = ViewStates.Gone;
                numSets.Visibility = ViewStates.Gone;
                setsTextView.Visibility = ViewStates.Gone;
                repsTextView.Visibility = ViewStates.Gone;
            }
            else
            {
                numReps.Text = reps.ToString();
                numSets.Text = sets.ToString();
            }
            exerciseName.Text = exerciseString;
            
            return view;
        }
    }
}