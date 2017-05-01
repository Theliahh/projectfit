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
using Environment = System.Environment;

namespace ProjectFit
{
    public static class DbHelper
    {
        public static string GetLocalDbPath()
        {
            var sqliteFileName = "workoutDatabaseTest3.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);

            return path;
        }
    }
}