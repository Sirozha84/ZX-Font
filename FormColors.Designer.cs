namespace ZXFont
{
    partial class FormColors
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxThemes = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxInk = new System.Windows.Forms.ComboBox();
            this.comboBoxPaper = new System.Windows.Forms.ComboBox();
            this.labelStupid = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Цвет букв:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цвет фона:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(262, 169);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 37);
            this.button2.TabIndex = 13;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(130, 169);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 37);
            this.button1.TabIndex = 12;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxThemes
            // 
            this.comboBoxThemes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxThemes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxThemes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxThemes.FormattingEnabled = true;
            this.comboBoxThemes.Items.AddRange(new object[] {
            "Светлая",
            "Тёмная",
            "Голубая",
            "Старый компьютер",
            "Страшная",
            "Кислотная",
            "Пользовательская"});
            this.comboBoxThemes.Location = new System.Drawing.Point(161, 12);
            this.comboBoxThemes.Name = "comboBoxThemes";
            this.comboBoxThemes.Size = new System.Drawing.Size(227, 28);
            this.comboBoxThemes.TabIndex = 15;
            this.comboBoxThemes.SelectedIndexChanged += new System.EventHandler(this.comboBoxThemes_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Цветовая тема:";
            // 
            // comboBoxInk
            // 
            this.comboBoxInk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxInk.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxInk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxInk.FormattingEnabled = true;
            this.comboBoxInk.Items.AddRange(new object[] {
            "Чёрный",
            "Синий",
            "Красный",
            "Пурпурный",
            "Зелёный",
            "Бирюзовый",
            "Жёлтый",
            "Белый"});
            this.comboBoxInk.Location = new System.Drawing.Point(161, 70);
            this.comboBoxInk.Name = "comboBoxInk";
            this.comboBoxInk.Size = new System.Drawing.Size(227, 28);
            this.comboBoxInk.TabIndex = 17;
            this.comboBoxInk.SelectedIndexChanged += new System.EventHandler(this.comboBoxInk_SelectedIndexChanged);
            // 
            // comboBoxPaper
            // 
            this.comboBoxPaper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPaper.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxPaper.FormattingEnabled = true;
            this.comboBoxPaper.Items.AddRange(new object[] {
            "Чёрный",
            "Синий",
            "Красный",
            "Пурпурный",
            "Зелёный",
            "Бирюзовый",
            "Жёлтый",
            "Белый"});
            this.comboBoxPaper.Location = new System.Drawing.Point(161, 104);
            this.comboBoxPaper.Name = "comboBoxPaper";
            this.comboBoxPaper.Size = new System.Drawing.Size(227, 28);
            this.comboBoxPaper.TabIndex = 18;
            this.comboBoxPaper.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaper_SelectedIndexChanged);
            // 
            // labelStupid
            // 
            this.labelStupid.AutoSize = true;
            this.labelStupid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStupid.Location = new System.Drawing.Point(12, 35);
            this.labelStupid.Name = "labelStupid";
            this.labelStupid.Size = new System.Drawing.Size(77, 20);
            this.labelStupid.TabIndex = 19;
            this.labelStupid.Text = "(глупая)";
            this.labelStupid.Visible = false;
            // 
            // FormColors
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(400, 218);
            this.Controls.Add(this.labelStupid);
            this.Controls.Add(this.comboBoxPaper);
            this.Controls.Add(this.comboBoxInk);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBoxThemes);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormColors";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Цвета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxThemes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxInk;
        private System.Windows.Forms.ComboBox comboBoxPaper;
        private System.Windows.Forms.Label labelStupid;
    }
}