using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;

namespace GueuingSystem
{
    public partial class frmLogin : Form
    {
        private bool frmchange = false;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbUserName.Text.Trim() == "" && tbPwd.Text.Trim() == "")
            {
                MessageBox.Show("请将信息输入完整！");
            }
            else
            {
                OleDbConnection conn = new OleDbConnection(frmMain.odbcConnStr);
                conn.Open();

                string sql = "select filedUserName,filedUserRole from tbUserInfo where filedUserName='" + tbUserName.Text.Trim() + 
                    "' and filedUserPwd='" + tbPwd.Text.Trim() +"'";
                OleDbDataAdapter adp = new OleDbDataAdapter(sql,conn);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    frmMain.UserName = ds.Tables[0].Rows[0][0].ToString();
                    frmMain.UserRole = ds.Tables[0].Rows[0][1].ToString();
                    //if (comboPortName.Items.Count > 0)
                    //    comboPortName.Items.Clear();
                    //string[] ports = SerialPort.GetPortNames();
                    //Array.Sort(ports);
                    //comboPortName.Items.AddRange(ports);
                    //comboPortName.SelectedIndex = comboPortName.Items.Count > 0 ? 0 : -1;
                    //comboBaudrate.SelectedIndex = comboBaudrate.Items.IndexOf("9600");
                    ////初始化SerialPort对象  
                    //comm.NewLine = "/r/n";
                    //comm.RtsEnable = true;//根据实际情况吧。  
                    ////添加事件注册  
                    //comm.DataReceived += comm_DataReceived;
                    frmchange = true;
                    this.Close();
                    
                }
                else
                {
                    MessageBox.Show("用户名或者密码错误！");
                }
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!frmchange)
                Application.Exit();
        }
    }
}
