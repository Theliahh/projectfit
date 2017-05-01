using System.IO;
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