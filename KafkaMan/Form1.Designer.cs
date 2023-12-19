namespace KafkaMan
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
            txtTopicName = new TextBox();
            btnTopicAdd = new Button();
            label1 = new Label();
            label2 = new Label();
            lstTopicList = new ListBox();
            SuspendLayout();
            // 
            // txtTopicName
            // 
            txtTopicName.Location = new Point(141, 45);
            txtTopicName.Name = "txtTopicName";
            txtTopicName.Size = new Size(131, 25);
            txtTopicName.TabIndex = 0;
            // 
            // btnTopicAdd
            // 
            btnTopicAdd.Location = new Point(278, 44);
            btnTopicAdd.Name = "btnTopicAdd";
            btnTopicAdd.Size = new Size(77, 25);
            btnTopicAdd.TabIndex = 1;
            btnTopicAdd.Text = "Add Topic";
            btnTopicAdd.UseVisualStyleBackColor = true;
            btnTopicAdd.Click += btnTopicAdd_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(50, 48);
            label1.Name = "label1";
            label1.Size = new Size(85, 17);
            label1.TabIndex = 2;
            label1.Text = "Topic Name: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(50, 94);
            label2.Name = "label2";
            label2.Size = new Size(69, 17);
            label2.TabIndex = 3;
            label2.Text = "Topic List: ";
            // 
            // lstTopicList
            // 
            lstTopicList.FormattingEnabled = true;
            lstTopicList.ItemHeight = 17;
            lstTopicList.Location = new Point(55, 114);
            lstTopicList.Name = "lstTopicList";
            lstTopicList.Size = new Size(300, 276);
            lstTopicList.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 715);
            Controls.Add(lstTopicList);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnTopicAdd);
            Controls.Add(txtTopicName);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kafka Manager";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTopicName;
        private Button btnTopicAdd;
        private Label label1;
        private Label label2;
        private ListBox lstTopicList;
    }
}