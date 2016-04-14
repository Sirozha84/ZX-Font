namespace ZXFont
{
    partial class FormTextGenerator
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownCodes = new System.Windows.Forms.NumericUpDown();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.comboBoxSeparator = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxHex = new System.Windows.Forms.CheckBox();
            this.comboBoxStart = new System.Windows.Forms.ComboBox();
            this.comboBoxEnd = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCancel.Location = new System.Drawing.Point(606, 412);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(126, 37);
            this.buttonCancel.TabIndex = 15;
            this.buttonCancel.Text = "Закрыть";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSave.Location = new System.Drawing.Point(474, 412);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(126, 37);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(300, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 18;
            this.label2.Text = "Кодов на строке:";
            // 
            // numericUpDownCodes
            // 
            this.numericUpDownCodes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.numericUpDownCodes.Location = new System.Drawing.Point(461, 7);
            this.numericUpDownCodes.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownCodes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownCodes.Name = "numericUpDownCodes";
            this.numericUpDownCodes.Size = new System.Drawing.Size(74, 26);
            this.numericUpDownCodes.TabIndex = 20;
            this.numericUpDownCodes.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownCodes.ValueChanged += new System.EventHandler(this.numericUpDownCodes_ValueChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBox1.Location = new System.Drawing.Point(565, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(120, 24);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Построчно";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Начало строки:";
            // 
            // textBoxText
            // 
            this.textBoxText.AcceptsReturn = true;
            this.textBoxText.AcceptsTab = true;
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxText.Location = new System.Drawing.Point(12, 74);
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxText.Size = new System.Drawing.Size(720, 332);
            this.textBoxText.TabIndex = 24;
            this.textBoxText.WordWrap = false;
            // 
            // comboBoxSeparator
            // 
            this.comboBoxSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSeparator.FormattingEnabled = true;
            this.comboBoxSeparator.Items.AddRange(new object[] {
            ", ",
            "; "});
            this.comboBoxSeparator.Location = new System.Drawing.Point(461, 40);
            this.comboBoxSeparator.Name = "comboBoxSeparator";
            this.comboBoxSeparator.Size = new System.Drawing.Size(74, 28);
            this.comboBoxSeparator.TabIndex = 25;
            this.comboBoxSeparator.Text = ", ";
            this.comboBoxSeparator.TextChanged += new System.EventHandler(this.comboBoxSeparator_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(329, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "Разделитель:";
            // 
            // checkBoxHex
            // 
            this.checkBoxHex.AutoSize = true;
            this.checkBoxHex.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxHex.Location = new System.Drawing.Point(565, 42);
            this.checkBoxHex.Name = "checkBoxHex";
            this.checkBoxHex.Size = new System.Drawing.Size(63, 24);
            this.checkBoxHex.TabIndex = 27;
            this.checkBoxHex.Text = "HEX";
            this.checkBoxHex.UseVisualStyleBackColor = true;
            this.checkBoxHex.CheckedChanged += new System.EventHandler(this.checkBoxHex_CheckedChanged);
            // 
            // comboBoxStart
            // 
            this.comboBoxStart.AutoCompleteCustomSource.AddRange(new string[] {
            "",
            "DEFB ",
            "    DEFB ",
            "(",
            "{"});
            this.comboBoxStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxStart.FormattingEnabled = true;
            this.comboBoxStart.Items.AddRange(new object[] {
            "",
            "DEFB ",
            "    DEFB ",
            "(",
            "{"});
            this.comboBoxStart.Location = new System.Drawing.Point(158, 6);
            this.comboBoxStart.Name = "comboBoxStart";
            this.comboBoxStart.Size = new System.Drawing.Size(112, 28);
            this.comboBoxStart.TabIndex = 28;
            this.comboBoxStart.TextChanged += new System.EventHandler(this.comboBoxStart_TextChanged);
            // 
            // comboBoxEnd
            // 
            this.comboBoxEnd.AutoCompleteCustomSource.AddRange(new string[] {
            "",
            ")",
            "}"});
            this.comboBoxEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxEnd.FormattingEnabled = true;
            this.comboBoxEnd.Items.AddRange(new object[] {
            "",
            ",",
            ";",
            ")",
            "}"});
            this.comboBoxEnd.Location = new System.Drawing.Point(158, 40);
            this.comboBoxEnd.Name = "comboBoxEnd";
            this.comboBoxEnd.Size = new System.Drawing.Size(112, 28);
            this.comboBoxEnd.TabIndex = 30;
            this.comboBoxEnd.TextChanged += new System.EventHandler(this.comboBoxEnd_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(24, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Конец строки:";
            // 
            // FormTextGenerator
            // 
            this.AcceptButton = this.buttonSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(744, 461);
            this.Controls.Add(this.comboBoxEnd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxStart);
            this.Controls.Add(this.checkBoxHex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxSeparator);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.numericUpDownCodes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(760, 500);
            this.Name = "FormTextGenerator";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Текстовый генератор";
            this.Load += new System.EventHandler(this.FormASM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCodes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownCodes;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.ComboBox comboBoxSeparator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxHex;
        private System.Windows.Forms.ComboBox comboBoxStart;
        private System.Windows.Forms.ComboBox comboBoxEnd;
        private System.Windows.Forms.Label label4;
    }
}