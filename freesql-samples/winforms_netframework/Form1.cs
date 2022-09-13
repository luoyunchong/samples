using System;
using System.Collections.Generic;
using System.Windows.Forms;
using winforms_netframework.Models;

namespace winforms_netframework
{
    public partial class Form1 : Form
    {
        private readonly IFreeSql _fsql = DB.Sqlite;
        public Form1()
        {

            InitializeComponent();
            this.checkedListTodo.ValueMember = "Id";
            this.checkedListTodo.DisplayMember = "Message";
            GetTodoList();
        }

        private void GetTodoList()
        {
            var tolist = _fsql.Select<Todo>().Where(u => !u.IsDone).ToList();
            checkedListTodo.Items.Clear();
            foreach (var item in tolist)
            {
                checkedListTodo.Items.Add(item);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveInsert();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SaveUpdate();
        }
        private void SaveInsert()
        {
            string message = txtBoxTodo.Text.Trim();
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("请输入待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime? notifictionTime =
                !string.IsNullOrWhiteSpace(dateNotifictionTime.Text) && dateNotifictionTime.Checked
                    ? DateTime.Parse(dateNotifictionTime.Text)
                    : (DateTime?)null;

            var todo = new Todo()
            {
                Message = message,
                IsDone = false,
                NotifictionTime = notifictionTime,
                CreateTime = DateTime.Now
            };


            _fsql.Insert<Todo>(todo).ExecuteAffrows();
            GetTodoList();
            MessageBox.Show("保存成功");
        }

        private void SaveUpdate()
        {
            string id = hideId.Text.ToString();
            string message = txtBoxTodo.Text.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("请选中一个待办事项后再修改", "提示", MessageBoxButtons.OK);
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageBox.Show("请输入待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            DateTime? notifictionTime =
                !string.IsNullOrWhiteSpace(dateNotifictionTime.Text) && dateNotifictionTime.Checked
                    ? DateTime.Parse(dateNotifictionTime.Text)
                    : (DateTime?)null;

            var todo = new Todo()
            {
                Id = long.Parse(id),
                Message = message,
                IsDone = false,
                NotifictionTime = notifictionTime,
            };

            _fsql.Update<Todo>()
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
            var checkedItems = checkedListTodo.CheckedItems;
            if (checkedItems.Count == 0)
            {
                MessageBox.Show("请选中要删除的待办事项", "提示", MessageBoxButtons.OK);
                return;
            }
            _fsql.Delete<Todo>(checkedItems).ExecuteAffrows();
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

        private void btnFinish_Click(object sender, EventArgs e)
        {
            var checkedItems = checkedListTodo.CheckedItems;
            if (checkedItems.Count == 0)
            {
                MessageBox.Show("请选中要完成的待办事项", "提示", MessageBoxButtons.OK);
                return;
            }

            var todolist = new List<Todo>();
            foreach (var item in checkedItems)
            {
                Todo todo = (Todo)item;
                todo.IsDone = true;
                todo.DoneTime = DateTime.Now;
                todolist.Add(todo);
            }
            _fsql.Update<Todo>()
                .SetSource(todolist)
                .UpdateColumns(u => new { u.IsDone, u.DoneTime })
                .ExecuteAffrows();
            GetTodoList();
            MessageBox.Show("任务成功");
        }

        private void checkedListTodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var checkedItem = checkedListTodo.SelectedItem;
            if (checkedItem == null) return;
            Todo todo = (Todo)checkedItem;
            hideId.Text = todo.Id.ToString();
            txtBoxTodo.Text = todo.Message.ToString();
            if (todo.NotifictionTime != null)
            {
                dateNotifictionTime.Checked = true;
                dateNotifictionTime.Text = todo.NotifictionTime.ToString();
            }
            else
            {
                dateNotifictionTime.Checked = false;
                dateNotifictionTime.Text = null;
            }
        }

    }
}
