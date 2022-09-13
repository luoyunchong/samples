namespace winforms_netframework
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBoxTodo = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateNotifictionTime = new System.Windows.Forms.DateTimePicker();
            this.checkedListTodo = new System.Windows.Forms.CheckedListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnUnSelect = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnFinish = new System.Windows.Forms.Button();
            this.hideId = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoxTodo
            // 
            this.txtBoxTodo.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBoxTodo.Location = new System.Drawing.Point(291, 12);
            this.txtBoxTodo.Margin = new System.Windows.Forms.Padding(10);
            this.txtBoxTodo.Multiline = true;
            this.txtBoxTodo.Name = "txtBoxTodo";
            this.txtBoxTodo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBoxTodo.Size = new System.Drawing.Size(434, 187);
            this.txtBoxTodo.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(538, 247);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 46);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "新增保存";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "待办事项";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 212);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "日程提醒";
            // 
            // dateNotifictionTime
            // 
            this.dateNotifictionTime.Checked = false;
            this.dateNotifictionTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateNotifictionTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateNotifictionTime.Location = new System.Drawing.Point(289, 205);
            this.dateNotifictionTime.Name = "dateNotifictionTime";
            this.dateNotifictionTime.ShowCheckBox = true;
            this.dateNotifictionTime.Size = new System.Drawing.Size(434, 25);
            this.dateNotifictionTime.TabIndex = 5;
            this.dateNotifictionTime.ValueChanged += new System.EventHandler(this.dateNotifictionTime_ValueChanged);
            // 
            // checkedListTodo
            // 
            this.checkedListTodo.CheckOnClick = true;
            this.checkedListTodo.FormattingEnabled = true;
            this.checkedListTodo.Location = new System.Drawing.Point(216, 374);
            this.checkedListTodo.Name = "checkedListTodo";
            this.checkedListTodo.Size = new System.Drawing.Size(507, 364);
            this.checkedListTodo.TabIndex = 6;
            this.checkedListTodo.SelectedIndexChanged += new System.EventHandler(this.checkedListTodo_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(428, 313);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(88, 43);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除选中";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(538, 313);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(88, 43);
            this.btnSelectAll.TabIndex = 8;
            this.btnSelectAll.Text = "全选";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnUnSelect
            // 
            this.btnUnSelect.Location = new System.Drawing.Point(635, 313);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new System.Drawing.Size(88, 43);
            this.btnUnSelect.TabIndex = 9;
            this.btnUnSelect.Text = "反选";
            this.btnUnSelect.UseVisualStyleBackColor = true;
            this.btnUnSelect.Click += new System.EventHandler(this.btnUnSelect_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(218, 313);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 43);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "刷新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(321, 313);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(88, 43);
            this.btnFinish.TabIndex = 11;
            this.btnFinish.Text = "完成任务";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // hideId
            // 
            this.hideId.Location = new System.Drawing.Point(196, 90);
            this.hideId.Name = "hideId";
            this.hideId.Size = new System.Drawing.Size(87, 25);
            this.hideId.TabIndex = 12;
            this.hideId.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(635, 247);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(88, 46);
            this.btnUpdate.TabIndex = 13;
            this.btnUpdate.Text = "更新";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 747);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.hideId);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUnSelect);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.checkedListTodo);
            this.Controls.Add(this.dateNotifictionTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtBoxTodo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxTodo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateNotifictionTime;
        private System.Windows.Forms.CheckedListBox checkedListTodo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnUnSelect;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.TextBox hideId;
        private System.Windows.Forms.Button btnUpdate;
    }
}

