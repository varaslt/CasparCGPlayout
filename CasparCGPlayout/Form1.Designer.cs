namespace CasparCGPlayout
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolstriplabelAMCPConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstriplabelOSCConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolstriplabelDBConnected = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1FileLabel = new System.Windows.Forms.ToolStripLabel();
            this.tslAbout = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsLabel_STOPALL = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pgbCountup = new System.Windows.Forms.ProgressBar();
            this.lblPastDesc = new System.Windows.Forms.Label();
            this.lblCountUp = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pgbCountdown = new System.Windows.Forms.ProgressBar();
            this.lblRemainDesc = new System.Windows.Forms.Label();
            this.lblCountDown = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblLengthDesc = new System.Windows.Forms.Label();
            this.lblLength = new System.Windows.Forms.Label();
            this.TmrNoItemDuration = new System.Windows.Forms.Timer(this.components);
            this.tmrLastSeconds = new System.Windows.Forms.Timer(this.components);
            this.btnGoNext = new System.Windows.Forms.Button();
            this.tmrConnectionCheck = new System.Windows.Forms.Timer(this.components);
            this.tmrClockStarts = new System.Windows.Forms.Timer(this.components);
            this.btnHold = new System.Windows.Forms.Button();
            this.cmsHold = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiLatchHold = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrCGTiming = new System.Windows.Forms.Timer(this.components);
            this.tmrNTPUpdate = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            //this.vuMeter2 = new VU_MeterLibrary.VuMeter();
            //this.vuMeter1 = new VU_MeterLibrary.VuMeter();
            this.digitalStudioClock1 = new CasparCGPlayout.DigitalStudioClock.DigitalStudioClock();
            this.holdlatchingguitimer = new System.Windows.Forms.Timer(this.components);
            this.ListRunningOrder = new CasparCGPlayout.Components.ExtendedListBox.ExtendedListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tmrCheckAssetOnDisk = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.cmsHold.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowMerge = false;
            this.statusStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolstriplabelAMCPConnected,
            this.toolstriplabelOSCConnected,
            this.toolstriplabelDBConnected});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.SizingGrip = false;
            // 
            // toolstriplabelAMCPConnected
            // 
            this.toolstriplabelAMCPConnected.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.toolstriplabelAMCPConnected, "toolstriplabelAMCPConnected");
            this.toolstriplabelAMCPConnected.Name = "toolstriplabelAMCPConnected";
            this.toolstriplabelAMCPConnected.Spring = true;
            // 
            // toolstriplabelOSCConnected
            // 
            this.toolstriplabelOSCConnected.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.toolstriplabelOSCConnected, "toolstriplabelOSCConnected");
            this.toolstriplabelOSCConnected.Name = "toolstriplabelOSCConnected";
            this.toolstriplabelOSCConnected.Spring = true;
            // 
            // toolstriplabelDBConnected
            // 
            this.toolstriplabelDBConnected.BackColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.toolstriplabelDBConnected, "toolstriplabelDBConnected");
            this.toolstriplabelDBConnected.Name = "toolstriplabelDBConnected";
            this.toolstriplabelDBConnected.Spring = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip1FileLabel,
            this.tslAbout,
            this.toolStripSeparator1,
            this.tsLabel_STOPALL,
            this.toolStripSeparator2});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // toolStrip1FileLabel
            // 
            this.toolStrip1FileLabel.Name = "toolStrip1FileLabel";
            resources.ApplyResources(this.toolStrip1FileLabel, "toolStrip1FileLabel");
            // 
            // tslAbout
            // 
            this.tslAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tslAbout.Name = "tslAbout";
            resources.ApplyResources(this.tslAbout, "tslAbout");
            this.tslAbout.Click += new System.EventHandler(this.TslAboutClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsLabel_STOPALL
            // 
            this.tsLabel_STOPALL.Name = "tsLabel_STOPALL";
            resources.ApplyResources(this.tsLabel_STOPALL, "tsLabel_STOPALL");
            this.tsLabel_STOPALL.Click += new System.EventHandler(this.TsLabelStopallClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.pgbCountup);
            this.panel2.Controls.Add(this.lblPastDesc);
            this.panel2.Controls.Add(this.lblCountUp);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pgbCountup
            // 
            resources.ApplyResources(this.pgbCountup, "pgbCountup");
            this.pgbCountup.Name = "pgbCountup";
            // 
            // lblPastDesc
            // 
            resources.ApplyResources(this.lblPastDesc, "lblPastDesc");
            this.lblPastDesc.Name = "lblPastDesc";
            // 
            // lblCountUp
            // 
            resources.ApplyResources(this.lblCountUp, "lblCountUp");
            this.lblCountUp.Name = "lblCountUp";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel3.Controls.Add(this.pgbCountdown);
            this.panel3.Controls.Add(this.lblRemainDesc);
            this.panel3.Controls.Add(this.lblCountDown);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // pgbCountdown
            // 
            resources.ApplyResources(this.pgbCountdown, "pgbCountdown");
            this.pgbCountdown.Name = "pgbCountdown";
            // 
            // lblRemainDesc
            // 
            resources.ApplyResources(this.lblRemainDesc, "lblRemainDesc");
            this.lblRemainDesc.Name = "lblRemainDesc";
            // 
            // lblCountDown
            // 
            resources.ApplyResources(this.lblCountDown, "lblCountDown");
            this.lblCountDown.Name = "lblCountDown";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel4.Controls.Add(this.lblLengthDesc);
            this.panel4.Controls.Add(this.lblLength);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // lblLengthDesc
            // 
            resources.ApplyResources(this.lblLengthDesc, "lblLengthDesc");
            this.lblLengthDesc.Name = "lblLengthDesc";
            // 
            // lblLength
            // 
            this.lblLength.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.lblLength, "lblLength");
            this.lblLength.Name = "lblLength";
            // 
            // TmrNoItemDuration
            // 
            this.TmrNoItemDuration.Interval = 500;
            this.TmrNoItemDuration.Tick += new System.EventHandler(this.tmrNoItemDurationTick);
            // 
            // tmrLastSeconds
            // 
            this.tmrLastSeconds.Interval = 500;
            this.tmrLastSeconds.Tick += new System.EventHandler(this.tmrLastSecondsTick);
            // 
            // btnGoNext
            // 
            this.btnGoNext.BackColor = System.Drawing.Color.Lime;
            resources.ApplyResources(this.btnGoNext, "btnGoNext");
            this.btnGoNext.Name = "btnGoNext";
            this.btnGoNext.UseVisualStyleBackColor = false;
            this.btnGoNext.Click += new System.EventHandler(this.btnGoNextClick);
            // 
            // tmrConnectionCheck
            // 
            this.tmrConnectionCheck.Tick += new System.EventHandler(this.tmrConnectionCheckTick);
            // 
            // btnHold
            // 
            this.btnHold.BackColor = System.Drawing.Color.Silver;
            //this.btnHold.ContextMenuStrip = this.cmsHold;
            resources.ApplyResources(this.btnHold, "btnHold");
            this.btnHold.Name = "btnHold";
            this.btnHold.UseVisualStyleBackColor = false;
            this.btnHold.Click += new System.EventHandler(this.BtnHoldClick);
            // 
            // cmsHold
            // 
            this.cmsHold.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLatchHold});
            this.cmsHold.Name = "cmsHold";
            resources.ApplyResources(this.cmsHold, "cmsHold");
            this.cmsHold.Opening += new System.ComponentModel.CancelEventHandler(this.cmsHold_Opening);
            // 
            // tsmiLatchHold
            // 
            this.tsmiLatchHold.Name = "tsmiLatchHold";
            resources.ApplyResources(this.tsmiLatchHold, "tsmiLatchHold");
            // 
            // tmrCGTiming
            // 
            this.tmrCGTiming.Interval = 1000;
            // 
            // tmrNTPUpdate
            // 
            this.tmrNTPUpdate.Interval = 1024000;
            this.tmrNTPUpdate.Tick += new System.EventHandler(this.tmrNtpUpdateTick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::CasparCGPlayout.Properties.Resources.media_seek_forward;
            resources.ApplyResources(this.pictureBox3, "pictureBox3");
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CasparCGPlayout.Properties.Resources.media_seek_backward;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
//            this.pictureBox1.Image = global::CasparCGPlayout.Properties.Resources.BBC_One1;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel5.Controls.Add(this.label1);
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Name = "panel5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            /*
            // 
            // vuMeter2
            // 
            this.vuMeter2.AnalogMeter = false;
            this.vuMeter2.BackColor = System.Drawing.Color.White;
            this.vuMeter2.DialBackground = System.Drawing.Color.White;
            this.vuMeter2.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter2.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter2.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter2.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter2.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter2.Led1Count = 18;
            this.vuMeter2.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter2.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter2.Led2Count = 16;
            this.vuMeter2.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter2.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter2.Led3Count = 8;
            this.vuMeter2.LedSize = new System.Drawing.Size(16, 6);
            this.vuMeter2.LedSpace = 1;
            this.vuMeter2.Level = 0;
            this.vuMeter2.LevelMax = 65535;
            resources.ApplyResources(this.vuMeter2, "vuMeter2");
            this.vuMeter2.MeterScale = VU_MeterLibrary.MeterScale.Log10;
            this.vuMeter2.Name = "vuMeter2";
            this.vuMeter2.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter2.PeakHold = true;
            this.vuMeter2.Peakms = 1000;
            this.vuMeter2.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter2.ShowDialOnly = false;
            this.vuMeter2.ShowLedPeak = true;
            this.vuMeter2.ShowTextInDial = false;
            this.vuMeter2.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter2.UseLedLight = false;
            this.vuMeter2.VerticalBar = true;
            this.vuMeter2.VuText = "VU";
            // 
            // vuMeter1
            // 
            this.vuMeter1.AnalogMeter = false;
            this.vuMeter1.BackColor = System.Drawing.Color.White;
            this.vuMeter1.DialBackground = System.Drawing.Color.White;
            this.vuMeter1.DialTextNegative = System.Drawing.Color.Red;
            this.vuMeter1.DialTextPositive = System.Drawing.Color.Black;
            this.vuMeter1.DialTextZero = System.Drawing.Color.DarkGreen;
            this.vuMeter1.Led1ColorOff = System.Drawing.Color.DarkGreen;
            this.vuMeter1.Led1ColorOn = System.Drawing.Color.LimeGreen;
            this.vuMeter1.Led1Count = 18;
            this.vuMeter1.Led2ColorOff = System.Drawing.Color.Olive;
            this.vuMeter1.Led2ColorOn = System.Drawing.Color.Yellow;
            this.vuMeter1.Led2Count = 16;
            this.vuMeter1.Led3ColorOff = System.Drawing.Color.Maroon;
            this.vuMeter1.Led3ColorOn = System.Drawing.Color.Red;
            this.vuMeter1.Led3Count = 8;
            this.vuMeter1.LedSize = new System.Drawing.Size(16, 6);
            this.vuMeter1.LedSpace = 1;
            this.vuMeter1.Level = 0;
            this.vuMeter1.LevelMax = 65535;
            resources.ApplyResources(this.vuMeter1, "vuMeter1");
            this.vuMeter1.MeterScale = VU_MeterLibrary.MeterScale.Log10;
            this.vuMeter1.Name = "vuMeter1";
            this.vuMeter1.NeedleColor = System.Drawing.Color.Black;
            this.vuMeter1.PeakHold = true;
            this.vuMeter1.Peakms = 1000;
            this.vuMeter1.PeakNeedleColor = System.Drawing.Color.Red;
            this.vuMeter1.ShowDialOnly = false;
            this.vuMeter1.ShowLedPeak = true;
            this.vuMeter1.ShowTextInDial = false;
            this.vuMeter1.TextInDial = new string[] {
        "-40",
        "-20",
        "-10",
        "-5",
        "0",
        "+6"};
            this.vuMeter1.UseLedLight = false;
            this.vuMeter1.VerticalBar = true;
            this.vuMeter1.VuText = "VU";
            */
            // 
            // digitalStudioClock1
            // 
            this.digitalStudioClock1.BackColor = System.Drawing.Color.Black;
            this.digitalStudioClock1.DigitColor = System.Drawing.Color.Red;
            resources.ApplyResources(this.digitalStudioClock1, "digitalStudioClock1");
            this.digitalStudioClock1.Name = "digitalStudioClock1";
            // 
            // holdlatchingguitimer
            // 
            this.holdlatchingguitimer.Interval = 750;
            this.holdlatchingguitimer.Tick += new System.EventHandler(this.holdlatchingguitimer_Tick);
            // 
            // ListRunningOrder
            // 
            this.ListRunningOrder.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(this.ListRunningOrder, "ListRunningOrder");
            this.ListRunningOrder.Name = "ListRunningOrder";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tmrCheckAssetOnDisk
            // 
            this.tmrCheckAssetOnDisk.Tick += new System.EventHandler(this.tmrCheckAssetOnDisk_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.digitalStudioClock1);
//            this.Controls.Add(this.vuMeter1);
//            this.Controls.Add(this.vuMeter2);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.ListRunningOrder);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnHold);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.btnGoNext);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Tag = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.form1FormClosed);
            this.Load += new System.EventHandler(this.form1Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.cmsHold.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStrip1FileLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblPastDesc;
        private System.Windows.Forms.Label lblCountUp;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRemainDesc;
        private System.Windows.Forms.Label lblCountDown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblLengthDesc;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.Timer TmrNoItemDuration;
        private System.Windows.Forms.Timer tmrLastSeconds;
        private System.Windows.Forms.Button btnGoNext;
        private System.Windows.Forms.ToolStripLabel tslAbout;
        private System.Windows.Forms.Timer tmrConnectionCheck;
        private System.Windows.Forms.Timer tmrClockStarts;
        private System.Windows.Forms.ToolStripStatusLabel toolstriplabelAMCPConnected;
        private System.Windows.Forms.ToolStripStatusLabel toolstriplabelOSCConnected;
        private System.Windows.Forms.ToolStripStatusLabel toolstriplabelDBConnected;
        private System.Windows.Forms.Button btnHold;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tsLabel_STOPALL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.Timer tmrCGTiming;
        private System.Windows.Forms.Timer tmrNTPUpdate;
        private System.Windows.Forms.ProgressBar pgbCountdown;
        private System.Windows.Forms.ProgressBar pgbCountup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private Components.ExtendedListBox.ExtendedListBox ListRunningOrder;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
       // private VU_MeterLibrary.VuMeter vuMeter2;
       // private VU_MeterLibrary.VuMeter vuMeter1;
        private DigitalStudioClock.DigitalStudioClock digitalStudioClock1;
        private System.Windows.Forms.Timer holdlatchingguitimer;
        private System.Windows.Forms.ContextMenuStrip cmsHold;
        private System.Windows.Forms.ToolStripMenuItem tsmiLatchHold;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer tmrCheckAssetOnDisk;
        
    }
}

