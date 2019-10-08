using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameInstancerNS;
using GameStartStopService.UtilitiesFolder;

namespace GameStartStopService
{
    public partial class GameSelectorEditor : Form
    {
        readonly ReadOnlyDictionary<string, int> GameMemoryMap = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>()
            {
                { "GUID" , 0 },
                { "Name" , 1 },
                { "Path" , 2 },
                { "ImagePath" , 3 },
                { "PlayTime" , 4 },
                { "CostToPlay" , 5 },
                { "StartOptions" , 6 },
                { "ExeStarts" , 7 },
            });

        readonly IDictionary<string, int> ExeMemoryMap = new ReadOnlyDictionary<string, int>(new Dictionary<string, int>()
            {
                { "Path" , 0 },
                { "Delay" , 1 },
                { "StartOptions" , 2 },
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

        Action<string> SetGame;

        TreeNode SelectedNode = null;

        internal GameSelectorEditor(IGameConfig Config, Action<string> SetGame)
        {
            InitializeComponent();
            int CurrentGameNode = 0;
            this.SetGame = SetGame;

            foreach (IGameModel Game in Config.Games)
            {
                int CurrentOptionalNode = 0;
                TreVie_GamesView.Nodes.Add(Game.Name+":"+Game.GUID);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(GUID + Game.GUID);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(TheName + Game.Name);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(Path + Game.Path);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(ImagePath + Game.ImagePath);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(PlayTime + Game.PlayTime.ToString());
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(CostToPlay + Game.CostToPlay.ToString());
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(StartOptions + Game.StartOptions);
                TreVie_GamesView.Nodes[CurrentGameNode].Nodes.Add(OptionalAdditionalExeStarts);
                foreach (IOptionalAddtionalExeStartsModel OptionalExe in Game.IOptionalAddtionalExeStarts)
                {
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes.Add(OptionalAdditionalExeStartsIndividual + CurrentOptionalNode);
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(Path + OptionalExe.Path);
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(Delay + OptionalExe.Delay.ToString());
                    TreVie_GamesView.Nodes[CurrentGameNode].Nodes[GameMemoryMap["ExeStarts"]].Nodes[CurrentOptionalNode].Nodes.Add(StartOptions + OptionalExe.StartOptions);

                    CurrentOptionalNode++;
                }
                TreVie_GamesView.ExpandAll();
                CurrentGameNode++;
            }
        }

        private void TreVie_GamesView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            while (TreVie_GamesView.SelectedNode.Level > 0)
                TreVie_GamesView.SelectedNode = TreVie_GamesView.SelectedNode.Parent;
            if (SelectedNode != null)
            {
                ChangeBackGroundRecurive(SelectedNode, Color.White);
            }
            SelectedNode = TreVie_GamesView.SelectedNode;
            ChangeBackGroundRecurive(SelectedNode, Color.LightBlue);
        }

        private void But_SelectGame_Click(object sender, EventArgs e)
        {
            SetGame.Invoke(SelectedNode.Nodes[GameMemoryMap["GUID"]].Text.Replace(GUID, ""));
            Close();
        }

        static void ChangeBackGroundRecurive(TreeNode Node, Color Color)
        {
            Node.BackColor = Color;
            foreach (TreeNode Nodes in Node.Nodes)
            {
                ChangeBackGroundRecurive(Nodes, Color);
            }
        }
    }
}
