﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BottomTabbedPage : TabbedPage
    {
        public BottomTabbedPage()
        {
            InitializeComponent();
        }
    }
}