namespace winforms_core
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            var _fsql = DB.Sqlite;
            bool isok = _fsql.Ado.ExecuteConnectTest();
            MessageBox.Show(isok.ToString());
        }
    }
}