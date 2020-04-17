namespace WebServiceClient
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
            this.btn_GO = new System.Windows.Forms.Button();
            this.com_Currency = new System.Windows.Forms.ComboBox();
            this.tbx_Value = new System.Windows.Forms.TextBox();
            this.lbl_Result = new System.Windows.Forms.Label();
            this.lbl_wert = new System.Windows.Forms.Label();
            this.lbl_wechselnin = new System.Windows.Forms.Label();
            this.btn_Initialize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_GO
            // 
            this.btn_GO.Location = new System.Drawing.Point(138, 12);
            this.btn_GO.Name = "btn_GO";
            this.btn_GO.Size = new System.Drawing.Size(97, 26);
            this.btn_GO.TabIndex = 0;
            this.btn_GO.Text = "GO";
            this.btn_GO.UseVisualStyleBackColor = true;
            this.btn_GO.Click += new System.EventHandler(this.btn_GO_Click);
            // 
            // com_Currency
            // 
            this.com_Currency.FormattingEnabled = true;
            this.com_Currency.Location = new System.Drawing.Point(19, 67);
            this.com_Currency.Name = "com_Currency";
            this.com_Currency.Size = new System.Drawing.Size(66, 21);
            this.com_Currency.TabIndex = 1;
            // 
            // tbx_Value
            // 
            this.tbx_Value.Location = new System.Drawing.Point(138, 67);
            this.tbx_Value.Name = "tbx_Value";
            this.tbx_Value.Size = new System.Drawing.Size(100, 20);
            this.tbx_Value.TabIndex = 2;
            // 
            // lbl_Result
            // 
            this.lbl_Result.AutoSize = true;
            this.lbl_Result.Location = new System.Drawing.Point(270, 70);
            this.lbl_Result.Name = "lbl_Result";
            this.lbl_Result.Size = new System.Drawing.Size(10, 13);
            this.lbl_Result.TabIndex = 3;
            this.lbl_Result.Text = "-";
            // 
            // lbl_wert
            // 
            this.lbl_wert.AutoSize = true;
            this.lbl_wert.Location = new System.Drawing.Point(135, 51);
            this.lbl_wert.Name = "lbl_wert";
            this.lbl_wert.Size = new System.Drawing.Size(33, 13);
            this.lbl_wert.TabIndex = 4;
            this.lbl_wert.Text = "Wert:";
            // 
            // lbl_wechselnin
            // 
            this.lbl_wechselnin.AutoSize = true;
            this.lbl_wechselnin.Location = new System.Drawing.Point(16, 51);
            this.lbl_wechselnin.Name = "lbl_wechselnin";
            this.lbl_wechselnin.Size = new System.Drawing.Size(69, 13);
            this.lbl_wechselnin.TabIndex = 5;
            this.lbl_wechselnin.Text = "Wechseln in:";
            // 
            // btn_Initialize
            // 
            this.btn_Initialize.Location = new System.Drawing.Point(19, 12);
            this.btn_Initialize.Name = "btn_Initialize";
            this.btn_Initialize.Size = new System.Drawing.Size(97, 26);
            this.btn_Initialize.TabIndex = 6;
            this.btn_Initialize.Text = "Initialize";
            this.btn_Initialize.UseVisualStyleBackColor = true;
            this.btn_Initialize.Click += new System.EventHandler(this.btn_Initialize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 111);
            this.Controls.Add(this.btn_Initialize);
            this.Controls.Add(this.lbl_wechselnin);
            this.Controls.Add(this.lbl_wert);
            this.Controls.Add(this.lbl_Result);
            this.Controls.Add(this.tbx_Value);
            this.Controls.Add(this.com_Currency);
            this.Controls.Add(this.btn_GO);
            this.Name = "Form1";
            this.Text = "SOAP Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_GO;
        private System.Windows.Forms.ComboBox com_Currency;
        private System.Windows.Forms.TextBox tbx_Value;
        private System.Windows.Forms.Label lbl_Result;
        private System.Windows.Forms.Label lbl_wert;
        private System.Windows.Forms.Label lbl_wechselnin;
        private System.Windows.Forms.Button btn_Initialize;
    }
}

