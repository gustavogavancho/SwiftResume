﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SwiftResume.WPF.Views
{
    /// <summary>
    /// Interaction logic for ResumeView.xaml
    /// </summary>
    public partial class ResumeView : UserControl
    {
        public ResumeView()
        {
            InitializeComponent();
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var checkSender = sender;
            var checkE = sender;
        }
    }
}
