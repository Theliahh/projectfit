﻿using System;
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

            var db = new SQLiteConnection(DbHelper.GetLocalDbPath());

            SetTitle();

            var AllExercises = db.Table<Exercise>();

            mExercisesList = AllExercises.Where(x => x.ExerciseId > 0 ).ToList();

            var adapter = new ExerciseListAdapter(this, mExercisesList);

            mExercisesListView.Adapter = adapter;

            db.Close();

            mExercisesListView.ItemClick += MExercisesListView_ItemClick;
        }

        private void MExercisesListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var selectedItem = mExercisesList[e.Position];

            var selectedIntent = new Intent(this, typeof(SelectedExerciseActivity));
            selectedIntent.PutExtra("exerciseName", selectedItem.Name);
            selectedIntent.PutExtra("exerciseId", selectedItem.ExerciseId);

            StartActivityForResult(selectedIntent,3);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == 3)
            {
                if (resultCode == Result.Ok)
                {
                    Intent returnIntent = new Intent();

                    var reps = data.GetIntExtra("reps", 0);
                    var sets = data.GetIntExtra("sets", 1);
                    var exerciseId = data.GetIntExtra("exerciseId", 1);
                    returnIntent.PutExtra("reps", reps);
                    returnIntent.PutExtra("sets", sets);
                    returnIntent.PutExtra("exerciseId", exerciseId);
                    SetResult(Result.Ok, returnIntent);
                    Finish();
                    //Get reps and sets from Intent data
                    //Send back reps, sets, and id (workout page will pull Exercise with id)
                }
            }
        }

        private void SetTitle()
        {
            var pageTitle = Intent.GetStringExtra("workoutName");
            this.Title = pageTitle;
        }
    }
}