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
            components = new System.ComponentModel.Container();
            txtTopicName = new TextBox();
            btnTopicAdd = new Button();
            label1 = new Label();
            label2 = new Label();
            lstTopicList = new ListBox();
            timer_topicrefresh = new System.Windows.Forms.Timer(components);
            lblLastSync = new Label();
            label3 = new Label();
            numberPartition = new NumericUpDown();
            groupBox1 = new GroupBox();
            numberReplica = new NumericUpDown();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)numberPartition).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numberReplica).BeginInit();
            SuspendLayout();
            // 
            // txtTopicName
            // 
            txtTopicName.Location = new Point(103, 33);
            txtTopicName.Name = "txtTopicName";
            txtTopicName.Size = new Size(131, 25);
            txtTopicName.TabIndex = 0;
            // 
            // btnTopicAdd
            // 
            btnTopicAdd.Location = new Point(103, 156);
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
            label1.Location = new Point(12, 36);
            label1.Name = "label1";
            label1.Size = new Size(85, 17);
            label1.TabIndex = 2;
            label1.Text = "Topic Name: ";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(372, 25);
            label2.Name = "label2";
            label2.Size = new Size(69, 17);
            label2.TabIndex = 3;
            label2.Text = "Topic List: ";
            // 
            // lstTopicList
            // 
            lstTopicList.FormattingEnabled = true;
            lstTopicList.ItemHeight = 17;
            lstTopicList.Location = new Point(377, 45);
            lstTopicList.Name = "lstTopicList";
            lstTopicList.Size = new Size(409, 174);
            lstTopicList.TabIndex = 4;
            // 
            // timer_topicrefresh
            // 
            timer_topicrefresh.Enabled = true;
            timer_topicrefresh.Interval = 2000;
            timer_topicrefresh.Tick += timer_topicrefresh_Tick;
            // 
            // lblLastSync
            // 
            lblLastSync.AutoSize = true;
            lblLastSync.Location = new Point(532, 25);
            lblLastSync.Name = "lblLastSync";
            lblLastSync.Size = new Size(63, 17);
            lblLastSync.TabIndex = 5;
            lblLastSync.Text = "Last sync:";
            lblLastSync.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(38, 66);
            label3.Name = "label3";
            label3.Size = new Size(59, 17);
            label3.TabIndex = 7;
            label3.Text = "Partition:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // numberPartition
            // 
            numberPartition.Location = new Point(103, 64);
            numberPartition.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numberPartition.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberPartition.Name = "numberPartition";
            numberPartition.Size = new Size(67, 25);
            numberPartition.TabIndex = 8;
            numberPartition.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(numberReplica);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numberPartition);
            groupBox1.Controls.Add(btnTopicAdd);
            groupBox1.Controls.Add(txtTopicName);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(300, 214);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Add Topic";
            // 
            // numberReplica
            // 
            numberReplica.Location = new Point(103, 95);
            numberReplica.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numberReplica.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numberReplica.Name = "numberReplica";
            numberReplica.Size = new Size(67, 25);
            numberReplica.TabIndex = 10;
            numberReplica.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(48, 97);
            label4.Name = "label4";
            label4.Size = new Size(49, 17);
            label4.TabIndex = 9;
            label4.Text = "Relica: ";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 715);
            Controls.Add(groupBox1);
            Controls.Add(lblLastSync);
            Controls.Add(lstTopicList);
            Controls.Add(label2);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kafka Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numberPartition).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numberReplica).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTopicName;
        private Button btnTopicAdd;
        private Label label1;
        private Label label2;
        private ListBox lstTopicList;
        private System.Windows.Forms.Timer timer_topicrefresh;
        private Label lblLastSync;
        private Label label3;
        private NumericUpDown numberPartition;
        private GroupBox groupBox1;
        private NumericUpDown numberReplica;
        private Label label4;
    }
}