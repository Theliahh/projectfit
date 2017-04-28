using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Widget;
using Android.OS;
using Environment = System.Environment;

namespace ProjectFit
{
    [Activity(Label = "Project Fit", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<string> mItems;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            ListView workoutListView = FindViewById<ListView>(Resource.Id.workoutListView);
            Button premadeWorkoutsButton = FindViewById<Button>(Resource.Id.premadeWorkoutsButton);
            Button customWorkoutsButton = FindViewById<Button>(Resource.Id.customWorkoutsButton);


            var sqliteFileName = "workoutDatabase.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);



            mItems = new List<string> {"one", "two", "three"};

            LoadPremadeWorkouts(workoutListView);

            premadeWorkoutsButton.Click += (sender, args) =>
            {
                LoadPremadeWorkouts(workoutListView);
            };

            customWorkoutsButton.Click += (sender, args) =>
            {
                workoutListView.Adapter = null;
            };

        }

        private void LoadPremadeWorkouts(ListView workoutListView)
        {
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, mItems);

            workoutListView.Adapter = adapter;
        }
    }
}

