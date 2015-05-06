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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.m_referencesTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.m_codeTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.m_clearBtn = new System.Windows.Forms.Button();
            this.m_runBtn = new System.Windows.Forms.Button();
            this.m_outputTB = new System.Windows.Forms.RichTextBox();
            this.m_bw = new System.ComponentModel.BackgroundWorker();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.m_outputTB);
            this.splitContainer1.Size = new System.Drawing.Size(1058, 490);
            this.splitContainer1.SplitterDistance = 297;
            this.splitContainer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.m_clearBtn);
            this.panel1.Controls.Add(this.m_runBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 297);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.splitContainer2);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1052, 262);
            this.panel2.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.m_referencesTB);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.m_codeTB);
            this.splitContainer2.Panel2.Controls.Add(this.label2);
            this.splitContainer2.Size = new System.Drawing.Size(1052, 262);
            this.splitContainer2.SplitterDistance = 350;
            this.splitContainer2.TabIndex = 0;
            // 
            // m_referencesTB
            // 
            this.m_referencesTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_referencesTB.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_referencesTB.Location = new System.Drawing.Point(6, 22);
            this.m_referencesTB.Name = "m_referencesTB";
            this.m_referencesTB.Size = new System.Drawing.Size(341, 237);
            this.m_referencesTB.TabIndex = 1;
            this.m_referencesTB.Text = "";
            this.m_referencesTB.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "References:";
            // 
            // m_codeTB
            // 
            this.m_codeTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.m_codeTB.Font = new System.Drawing.Font("Georgia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_codeTB.Location = new System.Drawing.Point(6, 22);
            this.m_codeTB.Multiline = true;
            this.m_codeTB.Name = "m_codeTB";
            this.m_codeTB.Size = new System.Drawing.Size(689, 237);
            this.m_codeTB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "F# code:";
            // 
            // m_clearBtn
            // 
            this.m_clearBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.m_clearBtn.Location = new System.Drawing.Point(12, 271);
            this.m_clearBtn.Name = "m_clearBtn";
            this.m_clearBtn.Size = new System.Drawing.Size(75, 23);
            this.m_clearBtn.TabIndex = 1;
            this.m_clearBtn.Text = "Clear output";
            this.m_clearBtn.UseVisualStyleBackColor = true;
            this.m_clearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // m_runBtn
            // 
            this.m_runBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.m_runBtn.Location = new System.Drawing.Point(971, 271);
            this.m_runBtn.Name = "m_runBtn";
            this.m_runBtn.Size = new System.Drawing.Size(75, 23);
            this.m_runBtn.TabIndex = 0;
            this.m_runBtn.Text = "Execute!";
            this.m_runBtn.UseVisualStyleBackColor = true;
            this.m_runBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // m_outputTB
            // 
            this.m_outputTB.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.m_outputTB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_outputTB.Font = new System.Drawing.Font("Georgia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.m_outputTB.Location = new System.Drawing.Point(0, 0);
            this.m_outputTB.Name = "m_outputTB";
            this.m_outputTB.ReadOnly = true;
            this.m_outputTB.Size = new System.Drawing.Size(1058, 189);
            this.m_outputTB.TabIndex = 0;
            this.m_outputTB.Text = "";
            // 
            // m_bw
            // 
            this.m_bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WorkerDoWork);
            this.m_bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkerCompleted);
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(FSCompileTestForm.Program);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 490);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "FS Compile Test";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button m_clearBtn;
        private System.Windows.Forms.Button m_runBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox m_referencesTB;
        private System.Windows.Forms.TextBox m_codeTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource programBindingSource;
        private System.Windows.Forms.RichTextBox m_outputTB;
        private System.ComponentModel.BackgroundWorker m_bw;
    }
}

