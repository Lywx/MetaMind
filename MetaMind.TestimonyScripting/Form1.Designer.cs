namespace FSCompileTestForm
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                ((System.IDisposable)m_r).Dispose();
                m_r = null;

                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerDoWork);
            this.m_bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerCompleted);
        }

        #endregion
    }
}

