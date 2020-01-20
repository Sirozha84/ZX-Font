namespace ZXFont
{
    partial class FormBorders
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
            this.numericUpDownLeft = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRight = new System.Windows.Forms.NumericUpDown();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownBottom = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDownTopP = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownTop = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTopP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTop)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownLeft
            // 
            this.numericUpDownLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownLeft.Location = new System.Drawing.Point(146, 90);
            this.numericUpDownLeft.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownLeft.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLeft.Name = "numericUpDownLeft";
            this.numericUpDownLeft.Size = new System.Drawing.Size(45, 26);
            this.numericUpDownLeft.TabIndex = 1;
            this.numericUpDownLeft.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numericUpDownRight
            // 
            this.numericUpDownRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownRight.Location = new System.Drawing.Point(280, 90);
            this.numericUpDownRight.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownRight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRight.Name = "numericUpDownRight";
            this.numericUpDownRight.Size = new System.Drawing.Size(45, 26);
            this.numericUpDownRight.TabIndex = 2;
            this.numericUpDownRight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOK.Location = new System.Drawing.Point(84, 185);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(126, 37);
            this.buttonOK.TabIndex = 4;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(216, 185);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(126, 37);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(74, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Слева:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(198, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Справа:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(142, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Снизу:";
            // 
            // numericUpDownBottom
            // 
            this.numericUpDownBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownBottom.Location = new System.Drawing.Point(214, 133);
            this.numericUpDownBottom.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownBottom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownBottom.Name = "numericUpDownBottom";
            this.numericUpDownBottom.Size = new System.Drawing.Size(45, 26);
            this.numericUpDownBottom.TabIndex = 15;
            this.numericUpDownBottom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(25, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 20);
            this.label4.TabIndex = 18;
            this.label4.Text = "Сверху до строчных:";
            // 
            // numericUpDownTopP
            // 
            this.numericUpDownTopP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownTopP.Location = new System.Drawing.Point(214, 44);
            this.numericUpDownTopP.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTopP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTopP.Name = "numericUpDownTopP";
            this.numericUpDownTopP.Size = new System.Drawing.Size(45, 26);
            this.numericUpDownTopP.TabIndex = 17;
            this.numericUpDownTopP.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(136, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Сверху:";
            // 
            // numericUpDownTop
            // 
            this.numericUpDownTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownTop.Location = new System.Drawing.Point(214, 12);
            this.numericUpDownTop.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownTop.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownTop.Name = "numericUpDownTop";
            this.numericUpDownTop.Size = new System.Drawing.Size(45, 26);
            this.numericUpDownTop.TabIndex = 19;
            this.numericUpDownTop.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FormBorders
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(354, 234);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDownTop);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numericUpDownTopP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericUpDownBottom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.numericUpDownRight);
            this.Controls.Add(this.numericUpDownLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBorders";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Параметры ограничивающих контуров";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLeft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTopP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTop)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownLeft;
        private System.Windows.Forms.NumericUpDown numericUpDownRight;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownBottom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDownTopP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDownTop;
    }
}