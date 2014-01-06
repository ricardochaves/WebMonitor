namespace WebMonitor
{
    partial class FormSettings
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
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnSaveClose = new System.Windows.Forms.Button();
            this.lblKey = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImpJson = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(98, 20);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(146, 20);
            this.txtKey.TabIndex = 0;
            // 
            // btnSaveClose
            // 
            this.btnSaveClose.Location = new System.Drawing.Point(15, 69);
            this.btnSaveClose.Name = "btnSaveClose";
            this.btnSaveClose.Size = new System.Drawing.Size(103, 53);
            this.btnSaveClose.TabIndex = 1;
            this.btnSaveClose.Text = "Save and Close";
            this.btnSaveClose.UseVisualStyleBackColor = true;
            this.btnSaveClose.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(12, 23);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(80, 13);
            this.lblKey.TabIndex = 2;
            this.lblKey.Text = "Application Key";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Imprimir Jsons";
            // 
            // txtImpJson
            // 
            this.txtImpJson.Location = new System.Drawing.Point(98, 43);
            this.txtImpJson.Name = "txtImpJson";
            this.txtImpJson.Size = new System.Drawing.Size(43, 20);
            this.txtImpJson.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(167, 81);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 198);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtImpJson);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.btnSaveClose);
            this.Controls.Add(this.txtKey);
            this.Name = "FormSettings";
            this.Text = "FormSettings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnSaveClose;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtImpJson;
        private System.Windows.Forms.Button button1;
    }
}