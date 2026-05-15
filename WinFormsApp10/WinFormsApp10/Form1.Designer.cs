namespace WinFormsApp10
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
            label1 = new Label();
            BTN_GUARDAR = new Button();
            BTN_RESTAURAR = new Button();
            ListVersiones = new ListBox();
            TEXTBOX = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(325, 31);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 0;
            label1.Text = "Pre parcial";
            // 
            // BTN_GUARDAR
            // 
            BTN_GUARDAR.Location = new Point(91, 134);
            BTN_GUARDAR.Name = "BTN_GUARDAR";
            BTN_GUARDAR.Size = new Size(75, 23);
            BTN_GUARDAR.TabIndex = 1;
            BTN_GUARDAR.Text = "Guardar";
            BTN_GUARDAR.UseVisualStyleBackColor = true;
            BTN_GUARDAR.Click += BTN_GUARDAR_Click;
            // 
            // BTN_RESTAURAR
            // 
            BTN_RESTAURAR.Location = new Point(91, 163);
            BTN_RESTAURAR.Name = "BTN_RESTAURAR";
            BTN_RESTAURAR.Size = new Size(75, 23);
            BTN_RESTAURAR.TabIndex = 2;
            BTN_RESTAURAR.Text = "Restaurar";
            BTN_RESTAURAR.UseVisualStyleBackColor = true;
            BTN_RESTAURAR.Click += BTN_RESTAURAR_Click;
            // 
            // ListVersiones
            // 
            ListVersiones.FormattingEnabled = true;
            ListVersiones.ItemHeight = 15;
            ListVersiones.Location = new Point(337, 92);
            ListVersiones.Name = "ListVersiones";
            ListVersiones.Size = new Size(120, 94);
            ListVersiones.TabIndex = 3;
            // 
            // TEXTBOX
            // 
            TEXTBOX.Location = new Point(117, 92);
            TEXTBOX.Name = "TEXTBOX";
            TEXTBOX.Size = new Size(100, 23);
            TEXTBOX.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(TEXTBOX);
            Controls.Add(ListVersiones);
            Controls.Add(BTN_RESTAURAR);
            Controls.Add(BTN_GUARDAR);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button BTN_GUARDAR;
        private Button BTN_RESTAURAR;
        private ListBox ListVersiones;
        private TextBox TEXTBOX;
    }
}
