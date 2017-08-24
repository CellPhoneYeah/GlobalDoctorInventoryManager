using BusinessLayer.Entities;
using Common;
using CommonUtility;
using GlobalDoctorInventoryManager.TestSvc;
using ServerCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlobalDoctorInventoryManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Test test = new Test();
                string temp = test.DoWork();
                Func<GDIM_User, bool> conditions = delegate(GDIM_User user) { return true; };
                string remoteAddress = ConfigHelper.GetSettingByName("RemoteServer");
                ITest testSvc = WcfChannelFactory.GetRemoteInstance<ITest>(BindingTypes.Basic, remoteAddress);
                string tempStr = testSvc.DoWork();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

    }
}
