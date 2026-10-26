namespace test_connect
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ticketBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.filmhallBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.placetypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cinemaPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ticketBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filmhallBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.placetypeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ticketBindingSource
            // 
            this.ticketBindingSource.DataMember = "ticket";
            // 
            // cinemaDataSet
            // 
            // 
            // filmhallBindingSource
            // 
            this.filmhallBindingSource.DataMember = "film_hall";
            // 
            // film_hallTableAdapter
            // 
            // 
            // placetypeBindingSource
            // 
            this.placetypeBindingSource.DataMember = "place_type";
            // 
            // place_typeTableAdapter
            // 
            // 
            // ticketTableAdapter
            // 
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1138, 576);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // cinemaPanel
            // 
            this.cinemaPanel.AutoScroll = true;
            this.cinemaPanel.BackColor = System.Drawing.Color.DimGray;
            this.cinemaPanel.Location = new System.Drawing.Point(901, 12);
            this.cinemaPanel.Name = "cinemaPanel";
            this.cinemaPanel.Size = new System.Drawing.Size(140, 448);
            this.cinemaPanel.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 572);
            this.Controls.Add(this.cinemaPanel);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ticketBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filmhallBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.placetypeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource filmhallBindingSource;
        private System.Windows.Forms.BindingSource placetypeBindingSource;
        private System.Windows.Forms.BindingSource ticketBindingSource;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel cinemaPanel;
    }
}

