using GameStartStopService.BasicConfig;
using GameStartStopService.ConfigLoaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameStartStopService
{
    public partial class PasswordCorrector : Form
    {
        internal PasswordCorrector()
        {
            InitializeComponent();
            TexBox_UserName.Text = ArcadeGameStartAndStopService.MainConfig.ServerCredential.UserName;
            TexBox_Password.Text = ArcadeGameStartAndStopService.MainConfig.ServerCredential.Password;
        }

        private void But_Save_Click(object sender, EventArgs e)
        {
            ArcadeGameStartAndStopService.MainConfig.ServerCredential.UserName = TexBox_UserName.Text;
            ArcadeGameStartAndStopService.MainConfig.ServerCredential.Password = TexBox_Password.Text;
            JSONServiceConfig.SaveJSONServiceConfig(ArcadeGameStartAndStopService.MainConfig);
            Close();
        }
    }
}
