namespace Neurotec.Samples
{
	partial class CreateTemplate
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
			this.panel_Top = new System.Windows.Forms.FlowLayoutPanel();
			this.button_RefreshList = new System.Windows.Forms.Button();
			this.comboBox_Source = new System.Windows.Forms.ComboBox();
			this.button_Capture = new System.Windows.Forms.Button();
			this.button_Stop = new System.Windows.Forms.Button();
			this.checkBox_ManualCapture = new System.Windows.Forms.CheckBox();
			this.checkBox_ICAO = new System.Windows.Forms.CheckBox();
			this.checkBox_Liveness = new System.Windows.Forms.CheckBox();
			this.comboBox_LivenessModes = new System.Windows.Forms.ComboBox();
			this.label_qualityThreshold = new System.Windows.Forms.Label();
			this.textBox_QualityThreshold = new System.Windows.Forms.TextBox();
			this.button_defaultQualityThreshold = new System.Windows.Forms.Button();
			this.panel_bottom = new System.Windows.Forms.TableLayoutPanel();
			this.button_SaveTemplate = new System.Windows.Forms.Button();
			this.button_SaveImage = new System.Windows.Forms.Button();
			this.label_Status = new System.Windows.Forms.Label();
			this.button_Force = new System.Windows.Forms.Button();
			this.button_SaveTokenImage = new System.Windows.Forms.Button();
			this.panel_middle = new System.Windows.Forms.TableLayoutPanel();
			this.panel_main.SuspendLayout();
			this.panel_Top.SuspendLayout();
			this.panel_bottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_main
			// 
			this.panel_main.AutoSize = true;
			this.panel_main.ColumnCount = 1;
			this.panel_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_main.Controls.Add(this.panel_Top, 0, 0);
			this.panel_main.Controls.Add(this.panel_bottom, 0, 2);
			this.panel_main.Controls.Add(this.panel_middle, 1, 1);
			this.panel_main.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_main.Location = new System.Drawing.Point(0, 0);
			this.panel_main.Name = "panel_main";
			this.panel_main.RowCount = 3;
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
			this.panel_main.Size = new System.Drawing.Size(1095, 597);
			this.panel_main.TabIndex = 0;
			// 
			// panel_Top
			// 
			this.panel_Top.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_Top.AutoSize = true;
			this.panel_Top.Controls.Add(this.button_RefreshList);
			this.panel_Top.Controls.Add(this.comboBox_Source);
			this.panel_Top.Controls.Add(this.button_Capture);
			this.panel_Top.Controls.Add(this.button_Stop);
			this.panel_Top.Controls.Add(this.checkBox_ManualCapture);
			this.panel_Top.Controls.Add(this.checkBox_ICAO);
			this.panel_Top.Controls.Add(this.checkBox_Liveness);
			this.panel_Top.Controls.Add(this.comboBox_LivenessModes);
			this.panel_Top.Controls.Add(this.label_qualityThreshold);
			this.panel_Top.Controls.Add(this.textBox_QualityThreshold);
			this.panel_Top.Controls.Add(this.button_defaultQualityThreshold);
			this.panel_Top.Location = new System.Drawing.Point(3, 3);
			this.panel_Top.Name = "panel_Top";
			this.panel_Top.Size = new System.Drawing.Size(1089, 29);
			this.panel_Top.TabIndex = 0;
			// 
			// button_RefreshList
			// 
			this.button_RefreshList.Location = new System.Drawing.Point(3, 3);
			this.button_RefreshList.Name = "button_RefreshList";
			this.button_RefreshList.Size = new System.Drawing.Size(75, 23);
			this.button_RefreshList.TabIndex = 9;
			this.button_RefreshList.Text = "Refresh list";
			this.button_RefreshList.UseVisualStyleBackColor = true;
			this.button_RefreshList.Click += new System.EventHandler(this.BtnRefreshListClick);
			// 
			// comboBox_Source
			// 
			this.comboBox_Source.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Source.FormattingEnabled = true;
			this.comboBox_Source.Location = new System.Drawing.Point(84, 4);
			this.comboBox_Source.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.comboBox_Source.Name = "comboBox_Source";
			this.comboBox_Source.Size = new System.Drawing.Size(150, 21);
			this.comboBox_Source.TabIndex = 0;
			this.comboBox_Source.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSourceSelectedIndexChanged);
			// 
			// button_Capture
			// 
			this.button_Capture.Location = new System.Drawing.Point(240, 3);
			this.button_Capture.Name = "button_Capture";
			this.button_Capture.Size = new System.Drawing.Size(75, 23);
			this.button_Capture.TabIndex = 1;
			this.button_Capture.Text = "Capture";
			this.button_Capture.UseVisualStyleBackColor = true;
			this.button_Capture.Click += new System.EventHandler(this.BtnCaptureClick);
			// 
			// button_Stop
			// 
			this.button_Stop.Enabled = false;
			this.button_Stop.Location = new System.Drawing.Point(321, 3);
			this.button_Stop.Name = "button_Stop";
			this.button_Stop.Size = new System.Drawing.Size(75, 23);
			this.button_Stop.TabIndex = 10;
			this.button_Stop.Text = "Stop";
			this.button_Stop.UseVisualStyleBackColor = true;
			this.button_Stop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// checkBox_ManualCapture
			// 
			this.checkBox_ManualCapture.AutoSize = true;
			this.checkBox_ManualCapture.Location = new System.Drawing.Point(402, 7);
			this.checkBox_ManualCapture.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
			this.checkBox_ManualCapture.Name = "checkBox_ManualCapture";
			this.checkBox_ManualCapture.Size = new System.Drawing.Size(61, 17);
			this.checkBox_ManualCapture.TabIndex = 2;
			this.checkBox_ManualCapture.Text = "Manual";
			this.checkBox_ManualCapture.UseVisualStyleBackColor = true;
			this.checkBox_ManualCapture.CheckedChanged += new System.EventHandler(this.CheckBoxManualCaptureCheckedChanged);
			// 
			// checkBox_ICAO
			// 
			this.checkBox_ICAO.AutoSize = true;
			this.checkBox_ICAO.Location = new System.Drawing.Point(469, 7);
			this.checkBox_ICAO.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
			this.checkBox_ICAO.Name = "checkBox_ICAO";
			this.checkBox_ICAO.Size = new System.Drawing.Size(85, 17);
			this.checkBox_ICAO.TabIndex = 3;
			this.checkBox_ICAO.Text = "Check ICAO";
			this.checkBox_ICAO.UseVisualStyleBackColor = true;
			this.checkBox_ICAO.CheckedChanged += new System.EventHandler(this.CheckBoxICAOCheckedChanged);
			// 
			// checkBox_Liveness
			// 
			this.checkBox_Liveness.AutoSize = true;
			this.checkBox_Liveness.Location = new System.Drawing.Point(560, 7);
			this.checkBox_Liveness.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
			this.checkBox_Liveness.Name = "checkBox_Liveness";
			this.checkBox_Liveness.Size = new System.Drawing.Size(102, 17);
			this.checkBox_Liveness.TabIndex = 4;
			this.checkBox_Liveness.Text = "Check Liveness";
			this.checkBox_Liveness.UseVisualStyleBackColor = true;
			this.checkBox_Liveness.CheckedChanged += new System.EventHandler(this.CheckBoxLivenessCheckedChanged);
			// 
			// comboBox_LivenessModes
			// 
			this.comboBox_LivenessModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_LivenessModes.Enabled = false;
			this.comboBox_LivenessModes.FormattingEnabled = true;
			this.comboBox_LivenessModes.Location = new System.Drawing.Point(668, 4);
			this.comboBox_LivenessModes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.comboBox_LivenessModes.Name = "comboBox_LivenessModes";
			this.comboBox_LivenessModes.Size = new System.Drawing.Size(121, 21);
			this.comboBox_LivenessModes.TabIndex = 5;
			// 
			// label_qualityThreshold
			// 
			this.label_qualityThreshold.AutoSize = true;
			this.label_qualityThreshold.Location = new System.Drawing.Point(800, 7);
			this.label_qualityThreshold.Margin = new System.Windows.Forms.Padding(8, 7, 3, 0);
			this.label_qualityThreshold.Name = "label_qualityThreshold";
			this.label_qualityThreshold.Size = new System.Drawing.Size(88, 13);
			this.label_qualityThreshold.TabIndex = 6;
			this.label_qualityThreshold.Text = "Quality threshold:";
			// 
			// textBox_QualityThreshold
			// 
			this.textBox_QualityThreshold.Location = new System.Drawing.Point(894, 4);
			this.textBox_QualityThreshold.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
			this.textBox_QualityThreshold.MaxLength = 3;
			this.textBox_QualityThreshold.Name = "textBox_QualityThreshold";
			this.textBox_QualityThreshold.Size = new System.Drawing.Size(30, 20);
			this.textBox_QualityThreshold.TabIndex = 7;
			this.textBox_QualityThreshold.TextChanged += new System.EventHandler(this.TextBoxQualityThresholdTextChanged);
			// 
			// button_defaultQualityThreshold
			// 
			this.button_defaultQualityThreshold.AutoSize = true;
			this.button_defaultQualityThreshold.Location = new System.Drawing.Point(930, 3);
			this.button_defaultQualityThreshold.Name = "button_defaultQualityThreshold";
			this.button_defaultQualityThreshold.Size = new System.Drawing.Size(75, 23);
			this.button_defaultQualityThreshold.TabIndex = 8;
			this.button_defaultQualityThreshold.Text = "Default";
			this.button_defaultQualityThreshold.UseVisualStyleBackColor = true;
			this.button_defaultQualityThreshold.Click += new System.EventHandler(this.BtnDefaultQualityThresholdClick);
			// 
			// panel_bottom
			// 
			this.panel_bottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel_bottom.AutoSize = true;
			this.panel_bottom.ColumnCount = 3;
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_bottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.panel_bottom.Controls.Add(this.button_SaveTemplate, 0, 0);
			this.panel_bottom.Controls.Add(this.button_SaveImage, 1, 0);
			this.panel_bottom.Controls.Add(this.label_Status, 3, 0);
			this.panel_bottom.Controls.Add(this.button_Force, 4, 0);
			this.panel_bottom.Controls.Add(this.button_SaveTokenImage, 2, 0);
			this.panel_bottom.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
			this.panel_bottom.Location = new System.Drawing.Point(3, 565);
			this.panel_bottom.Name = "panel_bottom";
			this.panel_bottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.panel_bottom.Size = new System.Drawing.Size(1089, 29);
			this.panel_bottom.TabIndex = 1;
			// 
			// button_SaveTemplate
			// 
			this.button_SaveTemplate.AutoSize = true;
			this.button_SaveTemplate.Enabled = false;
			this.button_SaveTemplate.Location = new System.Drawing.Point(3, 3);
			this.button_SaveTemplate.Name = "button_SaveTemplate";
			this.button_SaveTemplate.Size = new System.Drawing.Size(85, 23);
			this.button_SaveTemplate.TabIndex = 0;
			this.button_SaveTemplate.Text = "Save template";
			this.button_SaveTemplate.UseVisualStyleBackColor = true;
			this.button_SaveTemplate.Click += new System.EventHandler(this.BtnSaveTemplateClick);
			// 
			// button_SaveImage
			// 
			this.button_SaveImage.AutoSize = true;
			this.button_SaveImage.Enabled = false;
			this.button_SaveImage.Location = new System.Drawing.Point(94, 3);
			this.button_SaveImage.Name = "button_SaveImage";
			this.button_SaveImage.Size = new System.Drawing.Size(73, 23);
			this.button_SaveImage.TabIndex = 1;
			this.button_SaveImage.Text = "Save image";
			this.button_SaveImage.UseVisualStyleBackColor = true;
			this.button_SaveImage.Click += new System.EventHandler(this.BtnSaveImageClick);
			// 
			// label_Status
			// 
			this.label_Status.AutoSize = true;
			this.label_Status.Location = new System.Drawing.Point(282, 7);
			this.label_Status.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
			this.label_Status.Name = "label_Status";
			this.label_Status.Size = new System.Drawing.Size(35, 13);
			this.label_Status.TabIndex = 2;
			this.label_Status.Text = "status";
			// 
			// button_Force
			// 
			this.button_Force.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button_Force.Enabled = false;
			this.button_Force.Location = new System.Drawing.Point(1011, 3);
			this.button_Force.Name = "button_Force";
			this.button_Force.Size = new System.Drawing.Size(75, 23);
			this.button_Force.TabIndex = 3;
			this.button_Force.Text = "Force";
			this.button_Force.UseVisualStyleBackColor = true;
			this.button_Force.Click += new System.EventHandler(this.BtnForceClick);
			// 
			// button_SaveTokenImage
			// 
			this.button_SaveTokenImage.AutoSize = true;
			this.button_SaveTokenImage.Enabled = false;
			this.button_SaveTokenImage.Location = new System.Drawing.Point(173, 3);
			this.button_SaveTokenImage.Name = "button_SaveTokenImage";
			this.button_SaveTokenImage.Size = new System.Drawing.Size(103, 23);
			this.button_SaveTokenImage.TabIndex = 4;
			this.button_SaveTokenImage.Text = "Save token image";
			this.button_SaveTokenImage.UseVisualStyleBackColor = true;
			this.button_SaveTokenImage.Click += new System.EventHandler(this.BtnSaveTokenImageClick);
			// 
			// panel_middle
			// 
			this.panel_middle.ColumnCount = 2;
			this.panel_middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.panel_middle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_middle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_middle.Location = new System.Drawing.Point(3, 43);
			this.panel_middle.Name = "panel_middle";
			this.panel_middle.RowCount = 1;
			this.panel_middle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.panel_middle.Size = new System.Drawing.Size(1089, 516);
			this.panel_middle.TabIndex = 2;
			// 
			// CreateTemplate
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.panel_main);
			this.Name = "CreateTemplate";
			this.Size = new System.Drawing.Size(1095, 597);
			this.Load += new System.EventHandler(this.CreateTemplateLoad);
			this.panel_main.ResumeLayout(false);
			this.panel_main.PerformLayout();
			this.panel_Top.ResumeLayout(false);
			this.panel_Top.PerformLayout();
			this.panel_bottom.ResumeLayout(false);
			this.panel_bottom.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel panel_main;
		private System.Windows.Forms.FlowLayoutPanel panel_Top;
		private System.Windows.Forms.ComboBox comboBox_Source;
		private System.Windows.Forms.Button button_Capture;
		private System.Windows.Forms.CheckBox checkBox_ManualCapture;
		private System.Windows.Forms.CheckBox checkBox_ICAO;
		private System.Windows.Forms.CheckBox checkBox_Liveness;
		private System.Windows.Forms.ComboBox comboBox_LivenessModes;
		private System.Windows.Forms.Label label_qualityThreshold;
		private System.Windows.Forms.TextBox textBox_QualityThreshold;
		private System.Windows.Forms.TableLayoutPanel panel_bottom;
		private System.Windows.Forms.Button button_SaveTemplate;
		private System.Windows.Forms.Button button_SaveImage;
		private System.Windows.Forms.Label label_Status;
		private System.Windows.Forms.Button button_defaultQualityThreshold;
		private System.Windows.Forms.Button button_RefreshList;
		private System.Windows.Forms.TableLayoutPanel panel_middle;
		private System.Windows.Forms.Button button_Stop;
		private System.Windows.Forms.Button button_Force;
		private System.Windows.Forms.Button button_SaveTokenImage;
	}
}
