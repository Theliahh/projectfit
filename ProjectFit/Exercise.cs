﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProjectFit
{
    public class Exercise
    {
        private string Name;
        private List<ExerciseStep> Steps;
        private int ExerciseId;
        private string MuscleGroup;
    }
}