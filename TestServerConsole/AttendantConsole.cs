using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestServerConsole
{
    public partial class AttendantConsole : Form
    {
        ServiceJSONGamesConfig GameConfig;

        internal static string GUIDGameSelection;

        MasterServer OwningServer;

        List<string> RunningMachines = new List<string>();

        internal AttendantConsole(MasterServer Owner)
        {
            InitializeComponent();
            GameConfig = new ServiceJSONGamesConfig();
            UpdateGameSelectorList();
            BuildMachineList();
            OwningServer = Owner;
        }

        void BuildMachineList()
        {
            lock (MasterServer.Connections)
                foreach (KeyValuePair<string, SlaveInfo> Slave in MasterServer.Connections)
                {
                    CheLisBox_ConnectedMachines.Items.Add(Slave.ToString());
                }
        }

        void UpdateGameSelectorList()
        {
            LisBox_GameSelectionList.Items.Clear();
            foreach(Games Games in GameConfig.GamesRaw)
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

            Games Game = GameConfig.GamesRaw.Find(x => x.GUID == GUIDGameSelection);
            PicBox_GameImage.ImageLocation = $"{Environment.CurrentDirectory}\\VRMenu\\GameSelector_Data\\StreamingAssets\\{Game.ImagePath}.png";
            RicTexBox_Description.Text = Game.Description;
            Lab_GameName.Text = $"{Game.Name}:{Game.GUID}";
            Lab_Cost.Text = $"Cost:{Game.CostToPlay}";
            Lab_PlayTime.Text = $"PlayTime(ms):{Game.PlayTime}";
        }

        internal void AddToMachineList(SlaveInfo Slave)
        {
            lock (MasterServer.Connections)
                try
                {
                        Invoke(new Action(() => CheLisBox_ConnectedMachines.Items.Add(Slave.ToString())));
                }
                catch { }
        }

        internal void RemoveFromMachineList(SlaveInfo Slave)
        {
            lock (MasterServer.Connections)
                try
                {
                    Invoke(new Action(() => CheLisBox_ConnectedMachines.Items.Remove(Slave.ToString())));
                }
                catch { }
        }

        internal void MarkGameAsStoped(SlaveInfo Slave)
        {
            lock (MasterServer.Connections)
                try
                {
                    Invoke(new Action(() =>
                    {
                        RunningMachines.Remove(Slave.ToString());
                        LisBox_RunningMachines.Items.Remove(Slave.ToString());
                        if(RunningMachines.Count == 0)
                        {
                            Bu_Start.Enabled = true;
                            Bu_Stop.Enabled = false;
                        }
                    }));
                }
                catch { }
        }

        internal void ActivateForTappedCard(SlaveInfo Slave)
        {
            lock (MasterServer.Connections)
                try
                {
                    Invoke(new Action(() => CheLisBox_ConnectedMachines.SetItemChecked(CheLisBox_ConnectedMachines.Items.IndexOf(Slave.ToString()), true)));
                }
                catch { }
        }

        public void Log(string Message)
        {
            try
            {
                Invoke(new Action(() => { RicTexBox_Log.AppendText($"{Message}\n"); RicTexBox_Log.SelectionStart = RicTexBox_Log.Text.Length; RicTexBox_Log.ScrollToCaret(); }));
            }
            catch { }
            Console.WriteLine(Message);
        }

        private void Bu_Start_Click(object sender, EventArgs e)
        {
            RunningMachines = new List<string>();
            foreach (string Obj in CheLisBox_ConnectedMachines.CheckedItems)
            {
                LisBox_RunningMachines.Items.Add(Obj);
                RunningMachines.Add(Obj);
            }
            OwningServer.StartGames(RunningMachines, GUIDGameSelection);
            Bu_Start.Enabled = false;
            Bu_Stop.Enabled = true;
        }

        private void Bu_Stop_Click(object sender, EventArgs e)
        {
            OwningServer.StopGames(RunningMachines);
            RunningMachines = new List<string>();
            LisBox_RunningMachines.Items.Clear();
            Bu_Start.Enabled = true;
            Bu_Stop.Enabled = false;
        }

        private void CheLisBox_ConnectedMachines_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                OwningServer.NotifyOfUncheck(CheLisBox_ConnectedMachines.Items[e.Index].ToString());
                Log($"{CheLisBox_ConnectedMachines.Items[e.Index].ToString()} Has been unchecked please ask them to tap again for them to be charged for the ride");
            }
        }

        private void AttendantConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            Bu_Stop_Click(sender, e);
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
