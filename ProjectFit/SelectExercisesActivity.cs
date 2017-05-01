using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ProjectFit.Resources;
using SQLite;

namespace ProjectFit
{
    [Activity(Label = "SelectExercisesActivity")]
    public class SelectExercisesActivity : Activity
    {
        private List<Exercise> mExercisesList;
        private ListView mExercisesListView;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SelectExercises);

            mExercisesListView = FindViewById<ListView>(Resource.Id.selectExercisesListView);

            var sqliteFileName = "workoutDatabaseTest2.db3";
            string libraryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);

            var db = new SQLiteConnection(path);

            var AllExercises = db.Table<Exercise>();

            mExercisesList = AllExercises.ToList();

            var adapter = new ExerciseListAdapter(this, mExercisesList);

            mExercisesListView.Adapter = adapter;

            db.Close();

            mExercisesListView.ItemClick += MExercisesListView_ItemClick;
        }

        private void MExercisesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedItem = mExercisesList[e.Position];

            var testActivity = new Intent(this, typeof(NewWorkoutActivity));

            StartActivity(testActivity);
        }
    }
}