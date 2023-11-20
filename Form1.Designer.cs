namespace WinFormsSnakeGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnStart = new Button();
            pcbGraphics = new PictureBox();
            lblScore = new Label();
            label1 = new Label();
            timer = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)pcbGraphics).BeginInit();
            SuspendLayout();
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnStart.Location = new Point(1405, 10);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(70, 50);
            btnStart.TabIndex = 0;
            btnStart.TabStop = false;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // pcbGraphics
            // 
            pcbGraphics.Location = new Point(0, 0);
            pcbGraphics.Margin = new Padding(0);
            pcbGraphics.Name = "pcbGraphics";
            pcbGraphics.Size = new Size(1400, 700);
            pcbGraphics.TabIndex = 1;
            pcbGraphics.TabStop = false;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lblScore.Location = new Point(1405, 98);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(28, 35);
            lblScore.TabIndex = 2;
            lblScore.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(1405, 66);
            label1.Name = "label1";
            label1.Size = new Size(61, 28);
            label1.TabIndex = 3;
            label1.Text = "Score";
            // 
            // timer
            // 
            timer.Tick += timer_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1482, 703);
            Controls.Add(label1);
            Controls.Add(lblScore);
            Controls.Add(pcbGraphics);
            Controls.Add(btnStart);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pcbGraphics).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStart;
        private PictureBox pcbGraphics;
        private Label lblScore;
        private Label label1;
        private System.Windows.Forms.Timer timer;
    }
}