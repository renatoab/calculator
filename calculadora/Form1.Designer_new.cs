namespace calculadora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxEntrada = new System.Windows.Forms.RichTextBox();
            this.textBoxCalcOut = new System.Windows.Forms.RichTextBox();
            this.checkBoxAlwaysOnTop = new System.Windows.Forms.CheckBox();
            this.textBoxAscOut = new System.Windows.Forms.RichTextBox();
            this.textBoxLabelOut = new System.Windows.Forms.RichTextBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.checkBoxAddClipboard = new System.Windows.Forms.CheckBox();
            this.textBoxUnitIn = new System.Windows.Forms.RichTextBox();
            this.textBoxUnitOut = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBoxEntrada
            // 
            this.textBoxEntrada.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxEntrada.Location = new System.Drawing.Point(3, 4);
            this.textBoxEntrada.Name = "textBoxEntrada";
            this.textBoxEntrada.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxEntrada.Size = new System.Drawing.Size(197, 43);
            this.textBoxEntrada.TabIndex = 1;
            this.textBoxEntrada.Text = "";
            this.textBoxEntrada.Enter += new System.EventHandler(this.textBoxEntrada_Enter);
            this.textBoxEntrada.KeyUp += new System.Windows.Forms.KeyEventHandler(this.refreshOut);
            // 
            // textBoxCalcOut
            // 
            this.textBoxCalcOut.AutoWordSelection = true;
            this.textBoxCalcOut.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxCalcOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCalcOut.Location = new System.Drawing.Point(3, 53);
            this.textBoxCalcOut.Name = "textBoxCalcOut";
            this.textBoxCalcOut.ReadOnly = true;
            this.textBoxCalcOut.Size = new System.Drawing.Size(197, 43);
            this.textBoxCalcOut.TabIndex = 4;
            this.textBoxCalcOut.Text = "";
            this.textBoxCalcOut.Enter += new System.EventHandler(this.textBoxCalcOut_Enter);
            // 
            // checkBoxAlwaysOnTop
            // 
            this.checkBoxAlwaysOnTop.AutoSize = true;
            this.checkBoxAlwaysOnTop.Location = new System.Drawing.Point(4, 124);
            this.checkBoxAlwaysOnTop.Name = "checkBoxAlwaysOnTop";
            this.checkBoxAlwaysOnTop.Size = new System.Drawing.Size(92, 17);
            this.checkBoxAlwaysOnTop.TabIndex = 6;
            this.checkBoxAlwaysOnTop.Text = "Always on top";
            this.checkBoxAlwaysOnTop.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysOnTop.CheckedChanged += new System.EventHandler(this.setAlwaysOnTop);
            // 
            // textBoxAscOut
            // 
            this.textBoxAscOut.BackColor = System.Drawing.SystemColors.MenuBar;
            this.textBoxAscOut.Location = new System.Drawing.Point(122, 99);
            this.textBoxAscOut.Name = "textBoxAscOut";
            this.textBoxAscOut.Size = new System.Drawing.Size(78, 23);
            this.textBoxAscOut.TabIndex = 7;
            this.textBoxAscOut.Text = "";
            // 
            // textBoxLabelOut
            // 
            this.textBoxLabelOut.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.textBoxLabelOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLabelOut.Location = new System.Drawing.Point(45, 99);
            this.textBoxLabelOut.Name = "textBoxLabelOut";
            this.textBoxLabelOut.Size = new System.Drawing.Size(71, 23);
            this.textBoxLabelOut.TabIndex = 8;
            this.textBoxLabelOut.Text = "";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.Location = new System.Drawing.Point(156, 126);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(43, 16);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "V 6.1";
            // 
            // checkBoxAddClipboard
            // 
            this.checkBoxAddClipboard.AutoSize = true;
            this.checkBoxAddClipboard.Location = new System.Drawing.Point(4, 103);
            this.checkBoxAddClipboard.Name = "checkBoxAddClipboard";
            this.checkBoxAddClipboard.Size = new System.Drawing.Size(32, 17);
            this.checkBoxAddClipboard.TabIndex = 5;
            this.checkBoxAddClipboard.Text = "+";
            this.checkBoxAddClipboard.UseVisualStyleBackColor = true;
            this.checkBoxAddClipboard.CheckedChanged += new System.EventHandler(this.setAddClipboard);
            // 
            // textBoxUnitIn
            // 
            this.textBoxUnitIn.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnitIn.Location = new System.Drawing.Point(149, 4);
            this.textBoxUnitIn.Multiline = false;
            this.textBoxUnitIn.Name = "textBoxUnitIn";
            this.textBoxUnitIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxUnitIn.Size = new System.Drawing.Size(51, 43);
            this.textBoxUnitIn.TabIndex = 2;
            this.textBoxUnitIn.Text = "";
            this.textBoxUnitIn.Visible = false;
            this.textBoxUnitIn.TextChanged += new System.EventHandler(this.textBoxUnitIn_TextChanged);
            this.textBoxUnitIn.Enter += new System.EventHandler(this.textBoxUnitIn_Enter);
            this.textBoxUnitIn.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxUnit_KeyUp);
            // 
            // textBoxUnitOut
            // 
            this.textBoxUnitOut.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnitOut.Location = new System.Drawing.Point(149, 53);
            this.textBoxUnitOut.Multiline = false;
            this.textBoxUnitOut.Name = "textBoxUnitOut";
            this.textBoxUnitOut.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textBoxUnitOut.Size = new System.Drawing.Size(51, 43);
            this.textBoxUnitOut.TabIndex = 3;
            this.textBoxUnitOut.Text = "";
            this.textBoxUnitOut.Visible = false;
            this.textBoxUnitOut.TextChanged += new System.EventHandler(this.textBoxUnitOut_TextChanged);
            this.textBoxUnitOut.Enter += new System.EventHandler(this.textBoxUnitOut_Enter);
            this.textBoxUnitOut.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBoxUnit_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(203, 143);
            this.Controls.Add(this.textBoxUnitOut);
            this.Controls.Add(this.textBoxUnitIn);
            this.Controls.Add(this.checkBoxAddClipboard);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.checkBoxAlwaysOnTop);
            this.Controls.Add(this.textBoxLabelOut);
            this.Controls.Add(this.textBoxAscOut);
            this.Controls.Add(this.textBoxCalcOut);
            this.Controls.Add(this.textBoxEntrada);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Calculadora";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textBoxEntrada;
        private System.Windows.Forms.RichTextBox textBoxCalcOut;
        private System.Windows.Forms.CheckBox checkBoxAlwaysOnTop;
        private System.Windows.Forms.RichTextBox textBoxAscOut;
        private System.Windows.Forms.RichTextBox textBoxLabelOut;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.CheckBox checkBoxAddClipboard;
        private System.Windows.Forms.RichTextBox textBoxUnitIn;
        private System.Windows.Forms.RichTextBox textBoxUnitOut;

    }
}

