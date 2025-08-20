namespace WFFiltragemSinal
{
    partial class SeismicPlot
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.plotView = new OxyPlot.WindowsForms.PlotView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLowPass = new System.Windows.Forms.TextBox();
            this.textBoxHighPass = new System.Windows.Forms.TextBox();
            this.trackBarLowPass = new System.Windows.Forms.TrackBar();
            this.trackBarHighPass = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLowPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHighPass)).BeginInit();
            this.SuspendLayout();
            // 
            // plotView
            // 
            this.plotView.Location = new System.Drawing.Point(194, 3);
            this.plotView.Name = "plotView";
            this.plotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.plotView.Size = new System.Drawing.Size(316, 405);
            this.plotView.TabIndex = 0;
            this.plotView.Text = "plotView1";
            this.plotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.plotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.plotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            this.plotView.Click += new System.EventHandler(this.plotView_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Low pass filter cut off Hz";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "High pass filter cut off Hz";
            // 
            // textBoxLowPass
            // 
            this.textBoxLowPass.Location = new System.Drawing.Point(21, 38);
            this.textBoxLowPass.Name = "textBoxLowPass";
            this.textBoxLowPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxLowPass.TabIndex = 3;
            // 
            // textBoxHighPass
            // 
            this.textBoxHighPass.Location = new System.Drawing.Point(21, 147);
            this.textBoxHighPass.Name = "textBoxHighPass";
            this.textBoxHighPass.Size = new System.Drawing.Size(100, 20);
            this.textBoxHighPass.TabIndex = 4;
            // 
            // trackBarLowPass
            // 
            this.trackBarLowPass.Location = new System.Drawing.Point(21, 64);
            this.trackBarLowPass.Name = "trackBarLowPass";
            this.trackBarLowPass.Size = new System.Drawing.Size(104, 45);
            this.trackBarLowPass.TabIndex = 5;
            this.trackBarLowPass.Scroll += new System.EventHandler(this.trackBarLowPass_Scroll);
            // 
            // trackBarHighPass
            // 
            this.trackBarHighPass.Location = new System.Drawing.Point(21, 173);
            this.trackBarHighPass.Name = "trackBarHighPass";
            this.trackBarHighPass.Size = new System.Drawing.Size(104, 45);
            this.trackBarHighPass.TabIndex = 6;
            this.trackBarHighPass.Scroll += new System.EventHandler(this.trackBarHighPass_Scroll);
            // 
            // SeismicPlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trackBarHighPass);
            this.Controls.Add(this.trackBarLowPass);
            this.Controls.Add(this.textBoxHighPass);
            this.Controls.Add(this.textBoxLowPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.plotView);
            this.Name = "SeismicPlot";
            this.Size = new System.Drawing.Size(513, 408);
            this.Load += new System.EventHandler(this.SeismicPlot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLowPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHighPass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OxyPlot.WindowsForms.PlotView plotView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLowPass;
        private System.Windows.Forms.TextBox textBoxHighPass;
        private System.Windows.Forms.TrackBar trackBarLowPass;
        private System.Windows.Forms.TrackBar trackBarHighPass;
    }
}
