namespace Neurotec.Samples
{
	partial class Verify
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel_main = new System.Windows.Forms.TableLayoutPanel();
			this.panel_Bottom = new System.Windows.Forms.TableLayoutPanel();
			this.button_verify = new System.Windows.Forms.Button();
			this.label_status = new System.Windows.Forms.Label();
			this.button_Stop = new System.Windows.Forms.Button();
			this.panel_Middle = new System.Windows.Forms.TableLayoutPanel();
			this.label_viewStatusLeft = new System.Windows.Forms.Label();
			this.label_viewStatusRight = new System.Windows.Forms.Label();
			this.panel_Top = new System.Windows.Forms.TableLayoutPanel();
			this.button_openfvtemplate = new System.Windows.Forms.Button();
			this.button_defaultFAR = new System.Windows.Forms.Button();
			this.panel_Sources = new System.Windows.Forms.FlowLayoutPanel();
			this.button_RefreshList = new System.Windows.Forms.Button();
			this.comboBox_sourceSelection = new System.Windows.Forms.ComboBox();
			this.panel_FAR = new System.Windows.Forms.FlowLayoutPanel();
			this.comboBox_FAR = new System.Windows.Forms.ComboBox();
			this.label_FAR = new System.Windows.Forms.Label();
			this.panel_main.SuspendLayout();
			this.panel_Bottom.SuspendLayout();
			this.panel_Middle.SuspendLayout();
			this.panel_Top.SuspendLayout();
			this.panel_Sources.SuspendLayout();
			this.panel_FAR.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_main
			// 
			this.panel_main.ColumnCount = 1;
			this.panel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_main.Controls.Add(this.panel_Bottom, 0, 2);
			this.panel_main.Controls.Add(this.panel_Middle, 0, 1);
			this.panel_main.Controls.Add(this.panel_Top, 0, 0);
			this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_main.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.panel_main.Location = new System.Drawing.Point(0, 0);
			this.panel_main.Name = "panel_main";
			this.panel_main.RowCount = 3;
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.panel_main.Size = new System.Drawing.Size(1028, 644);
			this.panel_main.TabIndex = 3;
			// 
			// panel_Bottom
			// 
			this.panel_Bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panel_Bottom.AutoSize = true;
			this.panel_Bottom.ColumnCount = 3;
			this.panel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_Bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_Bottom.Controls.Add(this.button_verify, 0, 0);
			this.panel_Bottom.Controls.Add(this.label_status, 2, 0);
			this.panel_Bottom.Controls.Add(this.button_Stop, 1, 0);
			this.panel_Bottom.Location = new System.Drawing.Point(3, 612);
			this.panel_Bottom.Name = "panel_Bottom";
			this.panel_Bottom.RowCount = 1;
			this.panel_Bottom.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.panel_Bottom.Size = new System.Drawing.Size(209, 29);
			this.panel_Bottom.TabIndex = 5;
			// 
			// button_verify
			// 
			this.button_verify.Location = new System.Drawing.Point(3, 3);
			this.button_verify.Name = "button_verify";
			this.button_verify.Size = new System.Drawing.Size(75, 23);
			this.button_verify.TabIndex = 0;
			this.button_verify.Text = "Verify";
			this.button_verify.UseVisualStyleBackColor = true;
			this.button_verify.Click += new System.EventHandler(this.BtnVerifyClick);
			// 
			// label_status
			// 
			this.label_status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.label_status.AutoSize = true;
			this.label_status.Location = new System.Drawing.Point(165, 3);
			this.label_status.Margin = new System.Windows.Forms.Padding(3);
			this.label_status.Name = "label_status";
			this.label_status.Padding = new System.Windows.Forms.Padding(3);
			this.label_status.Size = new System.Drawing.Size(41, 23);
			this.label_status.TabIndex = 1;
			this.label_status.Text = "status";
			// 
			// button_Stop
			// 
			this.button_Stop.Enabled = false;
			this.button_Stop.Location = new System.Drawing.Point(84, 3);
			this.button_Stop.Name = "button_Stop";
			this.button_Stop.Size = new System.Drawing.Size(75, 23);
			this.button_Stop.TabIndex = 2;
			this.button_Stop.Text = "Stop";
			this.button_Stop.UseVisualStyleBackColor = true;
			this.button_Stop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// panel_Middle
			// 
			this.panel_Middle.BackColor = System.Drawing.SystemColors.Control;
			this.panel_Middle.ColumnCount = 2;
			this.panel_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.panel_Middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.panel_Middle.Controls.Add(this.label_viewStatusLeft, 0, 1);
			this.panel_Middle.Controls.Add(this.label_viewStatusRight, 1, 1);
			this.panel_Middle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Middle.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.panel_Middle.Location = new System.Drawing.Point(3, 44);
			this.panel_Middle.Name = "panel_Middle";
			this.panel_Middle.RowCount = 2;
			this.panel_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_Middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.panel_Middle.Size = new System.Drawing.Size(1022, 562);
			this.panel_Middle.TabIndex = 4;
			// 
			// label_viewStatusLeft
			// 
			this.label_viewStatusLeft.AutoSize = true;
			this.label_viewStatusLeft.Location = new System.Drawing.Point(3, 542);
			this.label_viewStatusLeft.Name = "label_viewStatusLeft";
			this.label_viewStatusLeft.Size = new System.Drawing.Size(65, 13);
			this.label_viewStatusLeft.TabIndex = 0;
			this.label_viewStatusLeft.Text = "viewStatus1";
			// 
			// label_viewStatusRight
			// 
			this.label_viewStatusRight.AutoSize = true;
			this.label_viewStatusRight.Location = new System.Drawing.Point(514, 542);
			this.label_viewStatusRight.Name = "label_viewStatusRight";
			this.label_viewStatusRight.Size = new System.Drawing.Size(65, 13);
			this.label_viewStatusRight.TabIndex = 1;
			this.label_viewStatusRight.Text = "viewStatus2";
			// 
			// panel_Top
			// 
			this.panel_Top.AutoSize = true;
			this.panel_Top.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel_Top.ColumnCount = 4;
			this.panel_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.panel_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.panel_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.panel_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.panel_Top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.panel_Top.Controls.Add(this.button_openfvtemplate, 0, 0);
			this.panel_Top.Controls.Add(this.button_defaultFAR, 2, 0);
			this.panel_Top.Controls.Add(this.panel_Sources, 3, 0);
			this.panel_Top.Controls.Add(this.panel_FAR, 1, 0);
			this.panel_Top.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_Top.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.panel_Top.Location = new System.Drawing.Point(3, 3);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.RowCount = 1;
			this.panel_Top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.panel_Top.Size = new System.Drawing.Size(1022, 35);
			this.panel_Top.TabIndex = 3;
			// 
			// button_openfvtemplate
			// 
			this.button_openfvtemplate.AutoSize = true;
			this.button_openfvtemplate.Location = new System.Drawing.Point(5, 5);
			this.button_openfvtemplate.Margin = new System.Windows.Forms.Padding(5);
			this.button_openfvtemplate.Name = "button_openfvtemplate";
			this.button_openfvtemplate.Size = new System.Drawing.Size(147, 23);
			this.button_openfvtemplate.TabIndex = 0;
			this.button_openfvtemplate.Text = "Open FVTemplate or Image";
			this.button_openfvtemplate.UseVisualStyleBackColor = true;
			this.button_openfvtemplate.Click += new System.EventHandler(this.BtnOpenfvtemplateClick);
			// 
			// button_defaultFAR
			// 
			this.button_defaultFAR.AutoSize = true;
			this.button_defaultFAR.Location = new System.Drawing.Point(515, 5);
			this.button_defaultFAR.Margin = new System.Windows.Forms.Padding(5);
			this.button_defaultFAR.Name = "button_defaultFAR";
			this.button_defaultFAR.Size = new System.Drawing.Size(51, 23);
			this.button_defaultFAR.TabIndex = 2;
			this.button_defaultFAR.Text = "Default";
			this.button_defaultFAR.UseVisualStyleBackColor = true;
			this.button_defaultFAR.Click += new System.EventHandler(this.BtnDefaultFARClick);
			// 
			// panel_Sources
			// 
			this.panel_Sources.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_Sources.AutoSize = true;
			this.panel_Sources.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel_Sources.Controls.Add(this.button_RefreshList);
			this.panel_Sources.Controls.Add(this.comboBox_sourceSelection);
			this.panel_Sources.Location = new System.Drawing.Point(778, 1);
			this.panel_Sources.Margin = new System.Windows.Forms.Padding(1);
			this.panel_Sources.Name = "panel_Sources";
			this.panel_Sources.Size = new System.Drawing.Size(243, 31);
			this.panel_Sources.TabIndex = 4;
			this.panel_Sources.WrapContents = false;
			// 
			// button_RefreshList
			// 
			this.button_RefreshList.Location = new System.Drawing.Point(4, 4);
			this.button_RefreshList.Margin = new System.Windows.Forms.Padding(4);
			this.button_RefreshList.Name = "button_RefreshList";
			this.button_RefreshList.Size = new System.Drawing.Size(75, 23);
			this.button_RefreshList.TabIndex = 0;
			this.button_RefreshList.Text = "Refresh list";
			this.button_RefreshList.UseVisualStyleBackColor = true;
			this.button_RefreshList.Click += new System.EventHandler(this.BtnRefreshListClick);
			// 
			// comboBox_sourceSelection
			// 
			this.comboBox_sourceSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_sourceSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_sourceSelection.DropDownWidth = 1;
			this.comboBox_sourceSelection.FormattingEnabled = true;
			this.comboBox_sourceSelection.Location = new System.Drawing.Point(88, 5);
			this.comboBox_sourceSelection.Margin = new System.Windows.Forms.Padding(5);
			this.comboBox_sourceSelection.Name = "comboBox_sourceSelection";
			this.comboBox_sourceSelection.Size = new System.Drawing.Size(150, 21);
			this.comboBox_sourceSelection.TabIndex = 3;
			// 
			// panel_FAR
			// 
			this.panel_FAR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_FAR.AutoSize = true;
			this.panel_FAR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel_FAR.Controls.Add(this.comboBox_FAR);
			this.panel_FAR.Controls.Add(this.label_FAR);
			this.panel_FAR.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.panel_FAR.Location = new System.Drawing.Point(256, 1);
			this.panel_FAR.Margin = new System.Windows.Forms.Padding(1);
			this.panel_FAR.Name = "panel_FAR";
			this.panel_FAR.Size = new System.Drawing.Size(253, 31);
			this.panel_FAR.TabIndex = 5;
			this.panel_FAR.WrapContents = false;
			// 
			// comboBox_FAR
			// 
			this.comboBox_FAR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.comboBox_FAR.DropDownWidth = 50;
			this.comboBox_FAR.FormattingEnabled = true;
			this.comboBox_FAR.Location = new System.Drawing.Point(173, 5);
			this.comboBox_FAR.Margin = new System.Windows.Forms.Padding(3, 5, 5, 5);
			this.comboBox_FAR.Name = "comboBox_FAR";
			this.comboBox_FAR.Size = new System.Drawing.Size(75, 21);
			this.comboBox_FAR.TabIndex = 1;
			this.comboBox_FAR.Leave += new System.EventHandler(this.ComboBoxFARLeave);
			// 
			// label_FAR
			// 
			this.label_FAR.AutoSize = true;
			this.label_FAR.Location = new System.Drawing.Point(92, 9);
			this.label_FAR.Margin = new System.Windows.Forms.Padding(3, 9, 0, 0);
			this.label_FAR.Name = "label_FAR";
			this.label_FAR.Size = new System.Drawing.Size(78, 13);
			this.label_FAR.TabIndex = 2;
			this.label_FAR.Text = "Matching FAR:";
			// 
			// Verify
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel_main);
			this.Name = "Verify";
			this.Size = new System.Drawing.Size(1028, 644);
			this.Load += new System.EventHandler(this.VerifyLoad);
			this.panel_main.ResumeLayout(false);
			this.panel_main.PerformLayout();
			this.panel_Bottom.ResumeLayout(false);
			this.panel_Bottom.PerformLayout();
			this.panel_Middle.ResumeLayout(false);
			this.panel_Middle.PerformLayout();
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			this.panel_Sources.ResumeLayout(false);
			this.panel_FAR.ResumeLayout(false);
			this.panel_FAR.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.TableLayoutPanel panel_main;
		private System.Windows.Forms.TableLayoutPanel panel_Bottom;
		private System.Windows.Forms.Button button_verify;
		private System.Windows.Forms.Label label_status;
		private System.Windows.Forms.TableLayoutPanel panel_Middle;
		private System.Windows.Forms.Label label_viewStatusLeft;
		private System.Windows.Forms.Label label_viewStatusRight;
		private System.Windows.Forms.TableLayoutPanel panel_Top;
		private System.Windows.Forms.Button button_openfvtemplate;
		private System.Windows.Forms.ComboBox comboBox_FAR;
		private System.Windows.Forms.Button button_defaultFAR;
		private System.Windows.Forms.ComboBox comboBox_sourceSelection;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.FlowLayoutPanel panel_Sources;
		private System.Windows.Forms.Button button_RefreshList;
		private System.Windows.Forms.FlowLayoutPanel panel_FAR;
		private System.Windows.Forms.Label label_FAR;
	}
}
