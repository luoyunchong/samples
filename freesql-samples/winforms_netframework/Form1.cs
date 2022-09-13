using System;
using System.Windows.Forms;
using winforms_netframework.Models;

namespace winforms_netframework
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();
            this.checkedListTodo.ValueMember = "Id";
            this.checkedListTodo.DisplayMember = "Message";
            GetTodoList();
        }

        private void GetTodoList()
        {
            var fsql = DB.Sqlite;
            var tolist = fsql.Select<Todo>().ToList();
            checkedListTodo.Items.Clear();
            foreach (var item in tolist)
            {
                checkedListTodo.Items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            string message = txtBoxTodo.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("请输入待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime? notifictionTime = string.IsNullOrWhiteSpace(dateNotifictionTime.Text) && dateNotifictionTime.Checked ? (DateTime?)null : DateTime.Parse(dateNotifictionTime.Text);

            var todo = new Todo()
            {
                Message = message,
                IsDone = false,
                NotifictionTime = notifictionTime,
                CreateTime = DateTime.Now
            };

            var fsql = DB.Sqlite;
            fsql.Insert<Todo>(todo).ExecuteAffrows();
            GetTodoList();
            MessageBox.Show("保存成功");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string message = txtBoxTodo.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("请输入待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime? notifictionTime = string.IsNullOrWhiteSpace(dateNotifictionTime.Text) && dateNotifictionTime.Checked ? (DateTime?)null : DateTime.Parse(dateNotifictionTime.Text);

            var todo = new Todo()
            {
                Message = message,
                IsDone = false,
                NotifictionTime = notifictionTime,
            };

            var fsql = DB.Sqlite;

            fsql.Update<Todo>()
                .SetSource(todo)
                .UpdateColumns(r => new { r.Message, r.NotifictionTime, r.IsDone })
                .ExecuteAffrows();
            GetTodoList();
            MessageBox.Show("保存修改成功");
        }

        private void dateNotifictionTime_ValueChanged(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var fsql = DB.Sqlite;

            var checkedItems = checkedListTodo.CheckedItems;
            if (checkedItems.Count == 0)
            {
                MessageBox.Show("请选中要删除的待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            fsql.Delete<Todo>(checkedItems).ExecuteAffrows();
            GetTodoList();
            MessageBox.Show("删除成功");

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.checkedListTodo.Items.Count; i++)
            {
                this.checkedListTodo.SetItemChecked(i, true);
            }
        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListTodo.Items.Count; i++)
            {
                if (checkedListTodo.GetItemChecked(i))
                {
                    checkedListTodo.SetItemChecked(i, false);
                }
                else
                {
                    checkedListTodo.SetItemChecked(i, true);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetTodoList();
        }
    }
}
