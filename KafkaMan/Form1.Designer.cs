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
            btnReferesh = new Button();
            numberReplica = new NumericUpDown();
            label4 = new Label();
            groupBox2 = new GroupBox();
            btnProduce = new Button();
            cboTopicProducer = new ComboBox();
            label6 = new Label();
            label5 = new Label();
            txtProduce = new TextBox();
            groupBox3 = new GroupBox();
            btnNewConsumer = new Button();
            lblLog = new Label();
            btnConsume = new Button();
            cbotopicConsumer = new ComboBox();
            label7 = new Label();
            label8 = new Label();
            txtConsume = new TextBox();
            ((System.ComponentModel.ISupportInitialize)numberPartition).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numberReplica).BeginInit();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // txtTopicName
            // 
            txtTopicName.Location = new Point(103, 33);
            txtTopicName.Name = "txtTopicName";
            txtTopicName.Size = new Size(108, 25);
            txtTopicName.TabIndex = 0;
            // 
            // btnTopicAdd
            // 
            btnTopicAdd.Location = new Point(103, 135);
            btnTopicAdd.Name = "btnTopicAdd";
            btnTopicAdd.Size = new Size(108, 25);
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
            label2.Location = new Point(7, 173);
            label2.Name = "label2";
            label2.Size = new Size(75, 17);
            label2.TabIndex = 3;
            label2.Text = "Topic List - ";
            // 
            // lstTopicList
            // 
            lstTopicList.FormattingEnabled = true;
            lstTopicList.ItemHeight = 17;
            lstTopicList.Location = new Point(7, 193);
            lstTopicList.Name = "lstTopicList";
            lstTopicList.Size = new Size(311, 242);
            lstTopicList.TabIndex = 4;
            // 
            // timer_topicrefresh
            // 
            timer_topicrefresh.Interval = 15000;
            // 
            // lblLastSync
            // 
            lblLastSync.AutoSize = true;
            lblLastSync.Location = new Point(77, 173);
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
            groupBox1.Controls.Add(btnReferesh);
            groupBox1.Controls.Add(numberReplica);
            groupBox1.Controls.Add(lblLastSync);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(lstTopicList);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(numberPartition);
            groupBox1.Controls.Add(btnTopicAdd);
            groupBox1.Controls.Add(txtTopicName);
            groupBox1.Controls.Add(label3);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(324, 453);
            groupBox1.TabIndex = 9;
            groupBox1.TabStop = false;
            groupBox1.Text = "Topic";
            // 
            // btnReferesh
            // 
            btnReferesh.BackgroundImage = Properties.Resources.gui_refresh_icon_157047;
            btnReferesh.BackgroundImageLayout = ImageLayout.Stretch;
            btnReferesh.Location = new Point(294, 169);
            btnReferesh.Name = "btnReferesh";
            btnReferesh.Size = new Size(24, 24);
            btnReferesh.TabIndex = 11;
            btnReferesh.UseVisualStyleBackColor = true;
            btnReferesh.Click += btnReferesh_Click_1;
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
            // groupBox2
            // 
            groupBox2.Controls.Add(btnProduce);
            groupBox2.Controls.Add(cboTopicProducer);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(txtProduce);
            groupBox2.Location = new Point(362, 15);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(470, 187);
            groupBox2.TabIndex = 10;
            groupBox2.TabStop = false;
            groupBox2.Text = "Produce";
            // 
            // btnProduce
            // 
            btnProduce.Location = new Point(224, 149);
            btnProduce.Name = "btnProduce";
            btnProduce.Size = new Size(121, 25);
            btnProduce.TabIndex = 13;
            btnProduce.Text = "Produce ...";
            btnProduce.UseVisualStyleBackColor = true;
            btnProduce.Click += btnProduce_Click;
            // 
            // cboTopicProducer
            // 
            cboTopicProducer.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTopicProducer.FormattingEnabled = true;
            cboTopicProducer.Location = new Point(97, 150);
            cboTopicProducer.Name = "cboTopicProducer";
            cboTopicProducer.Size = new Size(121, 25);
            cboTopicProducer.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 153);
            label6.Name = "label6";
            label6.Size = new Size(85, 17);
            label6.TabIndex = 11;
            label6.Text = "Topic Name: ";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 29);
            label5.Name = "label5";
            label5.Size = new Size(68, 17);
            label5.TabIndex = 4;
            label5.Text = "Message: ";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtProduce
            // 
            txtProduce.Location = new Point(97, 26);
            txtProduce.Multiline = true;
            txtProduce.Name = "txtProduce";
            txtProduce.Size = new Size(367, 117);
            txtProduce.TabIndex = 3;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(btnNewConsumer);
            groupBox3.Controls.Add(lblLog);
            groupBox3.Controls.Add(btnConsume);
            groupBox3.Controls.Add(cbotopicConsumer);
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(txtConsume);
            groupBox3.Location = new Point(362, 208);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(470, 257);
            groupBox3.TabIndex = 14;
            groupBox3.TabStop = false;
            groupBox3.Text = "Consume";
            // 
            // btnNewConsumer
            // 
            btnNewConsumer.Location = new Point(214, 226);
            btnNewConsumer.Name = "btnNewConsumer";
            btnNewConsumer.Size = new Size(121, 25);
            btnNewConsumer.TabIndex = 15;
            btnNewConsumer.Text = "new consume";
            btnNewConsumer.UseVisualStyleBackColor = true;
            btnNewConsumer.Click += btnNewConsumer_Click;
            // 
            // lblLog
            // 
            lblLog.AutoSize = true;
            lblLog.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lblLog.Location = new Point(6, 210);
            lblLog.Name = "lblLog";
            lblLog.Size = new Size(0, 15);
            lblLog.TabIndex = 14;
            // 
            // btnConsume
            // 
            btnConsume.Location = new Point(238, 17);
            btnConsume.Name = "btnConsume";
            btnConsume.Size = new Size(121, 25);
            btnConsume.TabIndex = 13;
            btnConsume.Text = "Consume ...";
            btnConsume.UseVisualStyleBackColor = true;
            btnConsume.Click += btnConsume_Click;
            // 
            // cbotopicConsumer
            // 
            cbotopicConsumer.DropDownStyle = ComboBoxStyle.DropDownList;
            cbotopicConsumer.FormattingEnabled = true;
            cbotopicConsumer.Location = new Point(97, 18);
            cbotopicConsumer.Name = "cbotopicConsumer";
            cbotopicConsumer.Size = new Size(121, 25);
            cbotopicConsumer.TabIndex = 12;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 21);
            label7.Name = "label7";
            label7.Size = new Size(85, 17);
            label7.TabIndex = 11;
            label7.Text = "Topic Name: ";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(23, 52);
            label8.Name = "label8";
            label8.Size = new Size(68, 17);
            label8.TabIndex = 4;
            label8.Text = "Message: ";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtConsume
            // 
            txtConsume.Location = new Point(97, 49);
            txtConsume.Multiline = true;
            txtConsume.Name = "txtConsume";
            txtConsume.ReadOnly = true;
            txtConsume.Size = new Size(367, 132);
            txtConsume.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(844, 479);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Kafka Manager";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numberPartition).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numberReplica).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
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
        private GroupBox groupBox2;
        private ComboBox cboTopicProducer;
        private Label label6;
        private Label label5;
        private TextBox txtProduce;
        private Button btnProduce;
        private GroupBox groupBox3;
        private ComboBox cbotopicConsumer;
        private Label label7;
        private Label label8;
        private TextBox txtConsume;
        private Button btnReferesh;
        private Label lblLog;
        private Button btnNewConsumer;
        private Button btnConsume;
    }
}