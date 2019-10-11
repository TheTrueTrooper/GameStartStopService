using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameStartStopService.TheServerClient.ClientModels;

namespace GameStartStopService
{
    public partial class AttendantConsole : Form
    {
        static string GUIDGameSelection;

        public AttendantConsole()
        {
            InitializeComponent();

            UpdateGameSelectorList();
        }
        
        public void UpdateGameSelectorList()
        {
            LisBox_GameSelectionList.Items.Clear();
            foreach(Games Games in ArcadeGameStartAndStopService.GameConfig.GamesRaw)
            {
                LisBox_GameSelectionList.Items.Add($"{Games.Name}:\r{Games.GUID}:\r{Games.Description}");
            }
            if (GUIDGameSelection != null && LisBox_GameSelectionList.Items.Count != 0)
            {
                LisBox_GameSelectionList.SelectedIndex = LisBox_GameSelectionList.Find(
                    x =>
                    {
                        string GUID = x.ToString().Split('\r')[1];
                        return GUID.Remove(GUID.Count() - 1) == GUIDGameSelection;
                    });
            }
            else
            {
                LisBox_GameSelectionList.SelectedIndex = 0;
                string GUID = LisBox_GameSelectionList.Items[LisBox_GameSelectionList.SelectedIndex].ToString().Split('\r')[1];
                GUIDGameSelection = GUID.Remove(GUID.Count() - 1);
            }
        }

        private void LisBox_GameSelectionList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string GUID = LisBox_GameSelectionList.Items[LisBox_GameSelectionList.SelectedIndex].ToString().Split('\r')[1];
            GUIDGameSelection = GUID.Remove(GUID.Count() - 1);

            Games Game = ArcadeGameStartAndStopService.GameConfig.GamesRaw.Find(x => x.GUID == GUIDGameSelection);
            PicBox_GameImage.ImageLocation = Game.ImagePath;
            RicTexBox_Description.Text = Game.Description;
            Lab_GameName.Text = $"{Game.Name}:{Game.GUID}";
            Lab_Cost.Text = $"Cost:{Game.CostToPlay}";
            Lab_PlayTime.Text = $"PlayTime(ms):{Game.PlayTime}";
        }

        private void MachineCheckHeartBeat_Tick(object sender, EventArgs e)
        {

        }
    }

    static class FinderExtentionFuctions
    {
        public static int Find(this ListBox This, Func<object, bool> func)
        {
            int j = -1;
            for (int i = 0; i < This.Items.Count; i ++)
            {
                if (func.Invoke(This.Items[i]))
                {
                    j = i;
                    break;
                }
            }
            return j;
        }
    }
}
