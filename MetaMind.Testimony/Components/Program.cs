/*
    Copyright (C) 2011 Vladimir Ivanovskiy <vivanovsky@gmail.com>.
    All rights reserved. 
    You can use this software under the terms of The Code Project Open License (CPOL) 1.02.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FSCompileTestForm
{
    static class Program
    {
        private static List<string> ReferenceList = new List<string>();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        public static List<string> References
        {
            get { return ReferenceList; }
        }
    }
}
