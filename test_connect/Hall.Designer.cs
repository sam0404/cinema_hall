namespace test_connect
{
    partial class Hall
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
            this.Buy = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Buy
            // 
            this.Buy.BackColor = System.Drawing.Color.DodgerBlue;
            this.Buy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Buy.ForeColor = System.Drawing.Color.White;
            this.Buy.Location = new System.Drawing.Point(932, 10);
            this.Buy.Name = "Buy";
            this.Buy.Size = new System.Drawing.Size(129, 43);
            this.Buy.TabIndex = 0;
            this.Buy.Text = "КУПИТЬ";
            this.Buy.UseVisualStyleBackColor = false;
            this.Buy.Click += new System.EventHandler(this.buy_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Goldenrod;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(932, 99);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 59);
            this.button2.TabIndex = 1;
            this.button2.Text = "Отменить выбор";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(932, 205);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 55);
            this.button3.TabIndex = 2;
            this.button3.Text = "Подтвертить отмену";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.AcceptCancel_Click);
            // 
            // clear
            // 
            this.clear.BackColor = System.Drawing.Color.Sienna;
            this.clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clear.Location = new System.Drawing.Point(932, 375);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(129, 43);
            this.clear.TabIndex = 3;
            this.clear.Text = "Очистить";
            this.clear.UseVisualStyleBackColor = false;
            this.clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Hall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 624);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Buy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Hall";
            this.Text = "Hall";
            this.Load += new System.EventHandler(this.Hall_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Buy;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button clear;
    }
}