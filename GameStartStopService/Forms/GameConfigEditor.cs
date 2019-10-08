using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameInstancerNS;
using GameStartStopService.TheServerClient.ClientModels;
using GameStartStopService.UtilitiesFolder;
using Newtonsoft.Json;

namespace GameStartStopService
{
    public partial class GameConfigEditor : Form
    {
        readonly ReadOnlyDictionary<string, int> GameMemoryMap = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>()
            {
                //"VRGameID": "1316E2E1-E19B-4E49-BDF1-CA4A24BF609B",
                {"VRGameID",0},
                //"MinNumberShares": null,
                {"MinNumberShares",1},
                //"MaxNumberShares": null,
                {"MaxNumberShares",2},
                //"Price": "25",
                {"Price",3},
                //"IsActive": true,
                {"IsActive",4},
                //"IsDeleted": false,
                {"IsDeleted",5},
                //"CreatedBy": null,
                {"CreatedBy",6},
                //"CreatedAt": "0001-01-01T00:00:00",
                {"CreatedAt",7},
                //"UpdatedBy": null,
                {"UpdatedBy",8},
                //"UpdatedAt": null,
                {"UpdatedAt",9},
                //"Name": "Phantom Breaker: Battle Grounds",  
                {"Name",10},
                //"StartOptions": "",
                {"StartOptions",11},
                //"ImagePath": "PutPathToImageHere",
                {"ImagePath",12},
                //"Path": "I:\\Games\\Steam\\steamapps\\common\\Phantom Breaker Battle Grounds\\bin\\pbbg_win32.exe",  
                {"Path",13},
                //"PlayTime": 0,
                {"PlayTime",14},
                //"Description": "This is a Indie title from japan", 
                {"Description",15},
                { "ExeStarts" , 16 },
            });

        readonly IDictionary<string, int> ExeMemoryMap = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>()
            {
                { "GUID" , 0},
                { "Path" , 1 },
                { "Delay" , 2 },
                { "StartOptions" , 3 },
                { "IsDeleted" , 4 },
            });

        const string GUID = "GUID:";
        const string TheName = "Name:";
        const string Path = "Path:";
        const string ImagePath = "ImagePath:";
        const string PlayTime = "PlayTime:";
        const string CostToPlay = "CostToPlay:";
        const string Delay = "Delay:";
        const string StartOptions = "StartOptions:";
        const string OptionalAdditionalExeStarts = "Optional Additional ExeStarts";
        const string OptionalAdditionalExeStartsIndividual = "Optional Exe #";
        const string IsDeleted = "IsDeleted:";
        const string Description = "Description:";
        const string IsActive = "IsActive:";
        const string MinNumberShares = "MinNumberShares:";
        const string MaxNumberShares = "MaxNumberShares:";
        const string CreatedBy = "CreatedBy:";
        const string CreatedAt = "CreatedAt:";
        const string UpdatedBy = "UpdatedBy:";
        const string UpdatedAt = "UpdatedAt:";

        const string ConfigLoc = "GameInstancerConfig.json";

        ulong LastPlayTime = 0;
        double LastCostToPlay = 0;
        int LastDelay = 0;
        byte LastMaxNumberOfShares = 0;
        byte LastMinNumberOfShares = 0;

        TreeNode SelectedNode = null;

        internal GameConfigEditor()
        {
            InitializeComponent();
            int CurrentGameNode = 0;
            foreach (Games Game in ArcadeGameStartAndStopService.GameConfig.GamesRaw)
            {
                int CurrentOptionalNode = 0;
                TreVie_GamesView.Nodes.Add(Game.Name + ":" + Game.GUID);
                //"VRGameID": "1316E2E1-E19B-4E49-BDF1-CA4A24BF609B",
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(GUID + Game.GUID);
                //"MinNumberShares": null,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(MinNumberShares + (Game.MaxNumberShares != null ? Game.MaxNumberShares.ToString() : "0"));
                //"MaxNumberShares": null,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(MaxNumberShares + (Game.MaxNumberShares != null ? Game.MaxNumberShares.ToString() : "0"));
                //"Price": "25",
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(CostToPlay + Game.CostToPlay.ToString());
                //"IsActive": true,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(IsActive + (Game.IsActive != null ? Game.IsActive.ToString() : false.ToString()));
                //"IsDeleted": false,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(IsDeleted + Game.IsDeleted);
                //"CreatedBy": null,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(CreatedBy + (Game.CreatedBy != null ? Game.CreatedBy : "NULL"));
                //"CreatedAt": "0001-01-01T00:00:00",
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(CreatedAt + (Game.CreatedAt != null ? Game.CreatedAt.ToString() : "NULL"));
                //"UpdatedBy": null,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(UpdatedBy + (Game.UpdatedBy != null ? Game.UpdatedBy : "NULL"));
                //"UpdatedAt": null,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(UpdatedAt + (Game.UpdatedAt != null ? Game.UpdatedAt.ToString() : "NULL"));
                //"Name": "Phantom Breaker: Battle Grounds",  
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(TheName + Game.Name);
                //"StartOptions": "",
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(StartOptions + Game.StartOptions);
                //"ImagePath": "PutPathToImageHere",
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(ImagePath + Game.ImagePath);
                //"Path": "I:\\Games\\Steam\\steamapps\\common\\Phantom Breaker Battle Grounds\\bin\\pbbg_win32.exe",  
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(Path + Game.Path);
                //"PlayTime": 0,
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(PlayTime + Game.PlayTime.ToString());
                //"Description": "This is a Indie title from japan", 
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(Description + Game.Description);
                //optional starts
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(OptionalAdditionalExeStarts);
                foreach (OptionalExeView OptionalExe in Game.OptionalExes)
                {
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes.Add(OptionalAdditionalExeStartsIndividual + CurrentOptionalNode);
                    //"ID": "",
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(GUID + OptionalExe.ID);
                    //"Path": "C:\\Windows\\System32\\notepad.exe",
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(Path + OptionalExe.Path);
                    //"Delay": "0",
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(Delay + OptionalExe.Delay.ToString());
                    //"StartOptions": "",
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(StartOptions + OptionalExe.StartOptions);
                    //"IsDeleted": false
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(IsDeleted + OptionalExe.IsDeleted);
                    CurrentOptionalNode++;
                }
                TreVie_GamesView.ExpandAll();
                CurrentGameNode++;
            }
        }

        private void TreVie_GamesView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (TreVie_GamesView.SelectedNode.Level == 1 || TreVie_GamesView.SelectedNode.Level == 3)
                TreVie_GamesView.SelectedNode = TreVie_GamesView.SelectedNode.Parent;
            if (SelectedNode != null)
            {
                ChangeBackGroundRecurive(SelectedNode, Color.White);
            }
            SelectedNode = TreVie_GamesView.SelectedNode;
            ChangeBackGroundRecurive(SelectedNode, Color.LightBlue);
            if (TreVie_GamesView.SelectedNode.Level == 0)
            {
                TexBox_MaxNumberOfShares.Text = SelectedNode.Nodes[GameMemoryMap["MaxNumberShares"]].Text.Remove(0, MaxNumberShares.Count());
                TexBox_MinNumberOfShares.Text = SelectedNode.Nodes[GameMemoryMap["MinNumberShares"]].Text.Remove(0, MinNumberShares.Count());
                CheBox_IsDeleted.Checked = bool.Parse(SelectedNode.Nodes[GameMemoryMap["IsDeleted"]].Text.Remove(0, IsDeleted.Count()));
                CheBox_IsActive.Checked = bool.Parse(SelectedNode.Nodes[GameMemoryMap["IsActive"]].Text.Remove(0, IsActive.Count()));
                RicTexBox_Description.Text = SelectedNode.Nodes[GameMemoryMap["Description"]].Text.Remove(0, Description.Count());

                TexBox_GUID.Text = SelectedNode.Nodes[GameMemoryMap["VRGameID"]].Text.Remove(0, GUID.Count());
                TexBox_Name.Text = SelectedNode.Nodes[GameMemoryMap["Name"]].Text.Remove(0, TheName.Count());
                TexBox_GamePath.Text = SelectedNode.Nodes[GameMemoryMap["Path"]].Text.Remove(0, Path.Count());
                TexBox_ImagePath.Text = SelectedNode.Nodes[GameMemoryMap["ImagePath"]].Text.Remove(0, ImagePath.Count());
                TexBox_PlayTime.Text = SelectedNode.Nodes[GameMemoryMap["PlayTime"]].Text.Remove(0, PlayTime.Count());
                TexBox_CostToPlay.Text = SelectedNode.Nodes[GameMemoryMap["Price"]].Text.Remove(0, CostToPlay.Count());
                TexBox_StartOptions.Text = SelectedNode.Nodes[GameMemoryMap["StartOptions"]].Text.Remove(0, StartOptions.Count());
            }
            if (TreVie_GamesView.SelectedNode.Level == 2)
            {
                TexBox_ExeGUID.Text = SelectedNode.Nodes[ExeMemoryMap["GUID"]].Text.Remove(0, GUID.Count());
                TexBox_ExePath.Text = SelectedNode.Nodes[ExeMemoryMap["Path"]].Text.Remove(0, Path.Count());
                TexBox_Delay.Text = SelectedNode.Nodes[ExeMemoryMap["Delay"]].Text.Remove(0, Delay.Count());
                TexBox_ExeOpt.Text = SelectedNode.Nodes[ExeMemoryMap["StartOptions"]].Text.Remove(0, StartOptions.Count());
                CheBox_IsDeletedOpExe.Checked = bool.Parse(SelectedNode.Nodes[ExeMemoryMap["IsDeleted"]].Text.Remove(0, IsDeleted.Count()));
            }
        }

        private void But_Save_Click(object sender, EventArgs e)
        {
            List<Games> ModelToSave = new List<Games>();
            Games GameModel = new Games();
            foreach (TreeNode Game in TreVie_GamesView.Nodes)
            {
                GameModel.GUID = Game.Nodes[GameMemoryMap["GUID"]].Text.Remove(0, GUID.Count());
                GameModel.MaxNumberShares = byte.Parse(Game.Nodes[GameMemoryMap["MaxNumberShares"]].Text.Remove(0, MaxNumberShares.Count()));
                GameModel.MinNumberShares = byte.Parse(Game.Nodes[GameMemoryMap["MinNumberShares"]].Text.Remove(0, MinNumberShares.Count()));
                GameModel.IsDeleted = bool.Parse(Game.Nodes[GameMemoryMap["IsDeleted"]].Text.Remove(0, IsDeleted.Count()));
                GameModel.IsActive = bool.Parse(Game.Nodes[GameMemoryMap["IsActive"]].Text.Remove(0, IsActive.Count()));
                GameModel.Description = Game.Nodes[GameMemoryMap["Description"]].Text.Remove(0, Description.Count());
                GameModel.Name = Game.Nodes[GameMemoryMap["Name"]].Text.Remove(0, TheName.Count());
                GameModel.Path = Game.Nodes[GameMemoryMap["Path"]].Text.Remove(0, Path.Count());
                GameModel.ImagePath = Game.Nodes[GameMemoryMap["ImagePath"]].Text.Remove(0, ImagePath.Count());
                GameModel.PlayTime = ulong.Parse(Game.Nodes[GameMemoryMap["PlayTime"]].Text.Remove(0, PlayTime.Count()));
                GameModel.CostToPlay = double.Parse(Game.Nodes[GameMemoryMap["CostToPlay"]].Text.Remove(0, CostToPlay.Count()));
                GameModel.StartOptions = Game.Nodes[GameMemoryMap["StartOptions"]].Text.Remove(0, StartOptions.Count());
                GameModel.OptionalExes = new List<OptionalExeView>();
                foreach (TreeNode OptionalExe in Game.Nodes[GameMemoryMap["ExeStarts"]].Nodes)
                {
                    OptionalExeView Exe = new OptionalExeView();
                    Exe.ID = OptionalExe.Nodes[ExeMemoryMap["GUID"]].Text.Remove(0, GUID.Count());
                    Exe.Path = OptionalExe.Nodes[ExeMemoryMap["Path"]].Text.Remove(0, Path.Count());
                    Exe.Delay = int.Parse(OptionalExe.Nodes[ExeMemoryMap["Delay"]].Text.Remove(0, Delay.Count()));
                    Exe.StartOptions = OptionalExe.Nodes[ExeMemoryMap["StartOptions"]].Text.Remove(0, StartOptions.Count());
                    Exe.IsDeleted = bool.Parse(Game.Nodes[ExeMemoryMap["IsDeleted"]].Text.Remove(0, IsDeleted.Count()));
                }
            }
            using (Stream Stream = new FileStream(ConfigLoc, FileMode.Create))
            using (StreamWriter sr = new StreamWriter(Stream))
            {
                sr.Write(JsonConvert.SerializeObject(GameModel, Formatting.Indented));
                sr.Close();
            }
            Close();
        }

        private void But_AddExeToSelectedGame_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null && TreVie_GamesView.SelectedNode.Level == 0)
            {
                int number = TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes.Count;
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes.Add(OptionalAdditionalExeStartsIndividual + number);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].Nodes.Add(GUID + TexBox_ExeGUID.Text);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].Nodes.Add(Path + TexBox_ExePath.Text);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].Nodes.Add(Delay + TexBox_Delay.Text);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].Nodes.Add(StartOptions + TexBox_ExeOpt.Text);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].Nodes.Add(IsDeleted + CheBox_IsDeletedOpExe.Checked);
                TreVie_GamesView.SelectedNode.Nodes[GameMemoryMap["ExeStarts"]].Nodes[number].ExpandAll();
                return;
            }
            MessageBox.Show("Please select a game to add to.");

        }

        private void But_AddGame_Click(object sender, EventArgs e)
        {
            string Name = TexBox_Name.Text + ":" + TexBox_GUID.Text;
            int Number = TreVie_GamesView.Nodes.Count;
            for (int i =0; i< TreVie_GamesView.Nodes.Count; i++)
            {
                if(TreVie_GamesView.Nodes[i].Text == Name)
                {
                    MessageBox.Show("Opps we already have that ID");
                    return;
                }
            }
            TreVie_GamesView.Nodes.Add(Name);
            //"VRGameID": "1316E2E1-E19B-4E49-BDF1-CA4A24BF609B",
            TreVie_GamesView.Nodes[Number].Nodes.Add(GUID + TexBox_GUID.Text);
            //"MinNumberShares": null,
            TreVie_GamesView.Nodes[Number].Nodes.Add(MinNumberShares + TexBox_MinNumberOfShares.Text);
            //"MaxNumberShares": null,
            TreVie_GamesView.Nodes[Number].Nodes.Add(MaxNumberShares + TexBox_MaxNumberOfShares.Text);
            //"Price": "25",
            TreVie_GamesView.Nodes[Number].Nodes.Add(CostToPlay + TexBox_CostToPlay.Text);
            //"IsActive": true,
            TreVie_GamesView.Nodes[Number].Nodes.Add(IsActive + CheBox_IsActive.Checked);
            //"IsDeleted": false,
            TreVie_GamesView.Nodes[Number].Nodes.Add(IsDeleted + CheBox_IsDeleted.Checked);
            //"CreatedBy": null,
            TreVie_GamesView.Nodes[Number].Nodes.Add(CreatedBy + "Null");
            //"CreatedAt": "0001-01-01T00:00:00",
            TreVie_GamesView.Nodes[Number].Nodes.Add(CreatedAt + "Null");
            //"UpdatedBy": null,
            TreVie_GamesView.Nodes[Number].Nodes.Add(UpdatedBy + "Null");
            //"UpdatedAt": null,
            TreVie_GamesView.Nodes[Number].Nodes.Add(UpdatedAt + "Null");
            //"Name": "Phantom Breaker: Battle Grounds",  
            TreVie_GamesView.Nodes[Number].Nodes.Add(TheName + TexBox_Name.Text);
            //"StartOptions": "",
            TreVie_GamesView.Nodes[Number].Nodes.Add(StartOptions + TexBox_StartOptions.Text);
            //"ImagePath": "PutPathToImageHere",
            TreVie_GamesView.Nodes[Number].Nodes.Add(ImagePath + TexBox_ImagePath.Text);
            //"Path": "I:\\Games\\Steam\\steamapps\\common\\Phantom Breaker Battle Grounds\\bin\\pbbg_win32.exe",  
            TreVie_GamesView.Nodes[Number].Nodes.Add(Path + TexBox_GamePath.Text);
            //"PlayTime": 0,
            TreVie_GamesView.Nodes[Number].Nodes.Add(PlayTime + TexBox_PlayTime.Text);
            //"Description": "This is a Indie title from japan", 
            TreVie_GamesView.Nodes[Number].Nodes.Add(Description + RicTexBox_Description.Text);
            TreVie_GamesView.Nodes[Number].Nodes.Add(OptionalAdditionalExeStarts);
            TreVie_GamesView.Nodes[Number].ExpandAll();
        }

        private void But_DeleteSelected_Click(object sender, EventArgs e)
        {
            TreVie_GamesView.SelectedNode?.Remove();
        }

        private void But_ReplaceSelectOptionalExe_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null && TreVie_GamesView.SelectedNode.Level == 2)
            {
                SelectedNode.Nodes[ExeMemoryMap["GUID"]].Text = GUID + TexBox_ExeGUID.Text;
                SelectedNode.Nodes[ExeMemoryMap["Path"]].Text = Path + TexBox_ExePath.Text;
                SelectedNode.Nodes[ExeMemoryMap["Delay"]].Text = Delay + TexBox_Delay.Text;
                SelectedNode.Nodes[ExeMemoryMap["StartOptions"]].Text = StartOptions + TexBox_ExeOpt.Text;
            }
        }

        private void But_ReplaceSelectGame_Click(object sender, EventArgs e)
        {
            string Name = TexBox_Name.Text + ":" + TexBox_GUID.Text;
            int Number = TreVie_GamesView.Nodes.Count;
            for (int i = 0; i < TreVie_GamesView.Nodes.Count; i++)
            {
                if (TreVie_GamesView.Nodes[i].Text == Name && SelectedNode.Text != Name)
                {
                    MessageBox.Show("Opps we already have that ID");
                    return;
                }
            }
            if (SelectedNode != null && TreVie_GamesView.SelectedNode.Level == 0)
            {
                SelectedNode.Nodes[GameMemoryMap["MaxNumberShares"]].Text = MaxNumberShares + TexBox_MaxNumberOfShares.Text;
                SelectedNode.Nodes[GameMemoryMap["MinNumberShares"]].Text = MinNumberShares + TexBox_MinNumberOfShares.Text;
                SelectedNode.Nodes[GameMemoryMap["IsDeleted"]].Text = IsDeleted + CheBox_IsDeleted.Checked.ToString();
                SelectedNode.Nodes[GameMemoryMap["IsActive"]].Text = IsActive + CheBox_IsActive.Checked.ToString();
                SelectedNode.Nodes[GameMemoryMap["Description"]].Text = Description + RicTexBox_Description.Text;


                SelectedNode.Nodes[GameMemoryMap["VRGameID"]].Text = GUID + TexBox_GUID.Text;
                SelectedNode.Nodes[GameMemoryMap["Name"]].Text = TheName + TexBox_Name.Text;
                SelectedNode.Nodes[GameMemoryMap["Path"]].Text = Path + TexBox_GamePath.Text;
                SelectedNode.Nodes[GameMemoryMap["ImagePath"]].Text = ImagePath + TexBox_ImagePath.Text;
                SelectedNode.Nodes[GameMemoryMap["PlayTime"]].Text = PlayTime + TexBox_PlayTime.Text;
                SelectedNode.Nodes[GameMemoryMap["Price"]].Text = CostToPlay + TexBox_CostToPlay.Text;
                SelectedNode.Nodes[GameMemoryMap["StartOptions"]].Text = StartOptions + TexBox_StartOptions.Text;
            }
        }

        private void TexBox_PlayTime_TextChanged(object sender, EventArgs e)
        {
            ulong Number = 0;
            bool IsNumber = ulong.TryParse(TexBox_PlayTime.Text, out Number);
            if (IsNumber)
                LastPlayTime = Number;
            else
            {
                MessageBox.Show("Sorry, but that is not a valid delay. It must be a whole number");
                TexBox_PlayTime.Text = LastPlayTime.ToString();
                TexBox_PlayTime.SelectionStart = TexBox_PlayTime.Text.Length;
                TexBox_PlayTime.SelectionLength = 0;
            }
        }

        private void TexBox_CostToPlay_TextChanged(object sender, EventArgs e)
        {
            double Number = 0;
            bool IsNumber = double.TryParse(TexBox_CostToPlay.Text, out Number);
            if (IsNumber)
                LastCostToPlay = Number;
            else
            {
                MessageBox.Show("Sorry, but that is not a valid delay. It must be a number");
                TexBox_CostToPlay.Text = LastCostToPlay.ToString();
                TexBox_CostToPlay.SelectionStart = TexBox_CostToPlay.Text.Length;
                TexBox_CostToPlay.SelectionLength = 0;
            }
        }

        private void TexBox_Delay_TextChanged(object sender, EventArgs e)
        {
            int Number = 0;
            bool IsNumber = int.TryParse(TexBox_Delay.Text, out Number);
            if (IsNumber)
                LastDelay = Number;
            else
            {
                MessageBox.Show("Sorry, but that is not a valid delay. It must be a whole number");
                TexBox_Delay.Text = LastDelay.ToString();
                TexBox_Delay.SelectionStart = TexBox_Delay.Text.Length;
                TexBox_Delay.SelectionLength = 0;
            }
        }

        static void ChangeBackGroundRecurive(TreeNode Node, Color Color)
        {
            Node.BackColor = Color;
            foreach (TreeNode Nodes in Node.Nodes)
            {
                ChangeBackGroundRecurive(Nodes, Color);
            }
        }

        private void TexBox_MaxNumberOfShares_TextChanged(object sender, EventArgs e)
        {
            byte Number = 0;
            bool IsNumber = byte.TryParse(TexBox_MaxNumberOfShares.Text, out Number);
            if (IsNumber && Number >= LastMinNumberOfShares)
                LastMaxNumberOfShares = Number;
            else
            {
                MessageBox.Show("Sorry, but that is not a valid number max of shares. It must be a whole number and larger or equal to the min");
                TexBox_MaxNumberOfShares.Text = LastMaxNumberOfShares.ToString();
                TexBox_MaxNumberOfShares.SelectionStart = TexBox_MaxNumberOfShares.Text.Length;
                TexBox_MaxNumberOfShares.SelectionLength = 0;
            }
        }

        private void TexBox_MinNumberOfShares_TextChanged(object sender, EventArgs e)
        {
            byte Number = 0;
            bool IsNumber = byte.TryParse(TexBox_MinNumberOfShares.Text, out Number);
            if (IsNumber && Number <= LastMaxNumberOfShares)
                LastMinNumberOfShares = Number;
            else
            {
                MessageBox.Show("Sorry, but that is not a valid number min of shares. It must be a whole number and smaller or equal to the max");
                TexBox_MinNumberOfShares.Text = LastMinNumberOfShares.ToString();
                TexBox_MinNumberOfShares.SelectionStart = TexBox_MinNumberOfShares.Text.Length;
                TexBox_MinNumberOfShares.SelectionLength = 0;
            }
        }
    }
}
