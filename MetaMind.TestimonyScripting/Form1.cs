/*
    Copyright (C) 2011 Vladimir Ivanovskiy <vivanovsky@gmail.com>.
    All rights reserved. 
    You can use this software under the terms of The Code Project Open License (CPOL) 1.02.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace FSCompileTestForm
{
    public partial class MainForm : Form
    {
        private StdStreamRedirector.Redirector m_r = null;


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Action<string> aOut = (text => AppendStdStreamText(text, Color.Black));
            Action<string> aErr = (text => AppendStdStreamText(text, Color.DarkRed));
            m_r = StdStreamRedirector.Init(aOut, aErr);
        }

        private void AppendStdStreamText(string text, Color c)
        {
            if(this.InvokeRequired)
            {
                MethodInvoker del = delegate
                {
                    AppendStdStreamText(text, c);
                };
                this.Invoke(del);
                return;
            }
            AppendColoredText(text, c);
            m_outputTB.AppendText("\n");
        }

        private List<string> GetReferences()
        {
            List<string> ls = new List<string>(
                m_referencesTB.Text.Split(m_lineSeparators, 
                    StringSplitOptions.RemoveEmptyEntries).AsEnumerable());
            return ls;
        }
        private void RunBtn_Click(object sender, EventArgs e)
        {
            m_references = GetReferences();
            m_code = m_codeTB.Text;
            m_runBtn.Enabled = false;
            m_bw.RunWorkerAsync();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            m_outputTB.Clear();
        }

        private void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            FSExecutor.CompileAndExecute(m_codeTB.Text, m_references);   
        }

        private void WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_runBtn.Enabled = true;
        }
    }
}
