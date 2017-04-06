using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using SoVPG.Properties;

namespace SoVPG
{
    public partial class PortraitGenerator : Form
    {
        private string CHARACTER;
        private string STYLE_STR;
        private int STYLE;
        private string EMOTION;
        private bool SWEAT;
        private bool BLUSH;

        private bool loaded = false;

        private FaceDataEntry[] FaceData;

        private List<string> Characters;
        private Dictionary<string, string> DisplayNames;
        private Dictionary<string, FaceDataEntry[]> Char2FaceData;

        public PortraitGenerator()
        {
            InitializeComponent();

            LoadResources();
            loaded = true;
            CB_Character.SelectedValue = Characters[0];
            CB_Character.SelectedValue = Characters[1];
            CB_Character.SelectedValue = Characters[0];
        }

        public void LoadResources()
        {
            var fd = Resources.FaceData;
            var entries = Resources.Entries.Split(new [] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('\t')).ToArray();
            var mpids = Resources.MPIDs.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Split('\t')).ToArray();
            Debug.Assert(fd.Length == entries.Length * 0x58);

            var MPIDDict = new Dictionary<string, string>();
            foreach (var mpid in mpids)
            {
                MPIDDict[mpid[0]] = mpid[1];
            }

            var ResourceSet = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, createIfNotExists: true, tryParents: true).Cast<DictionaryEntry>().Select(r => r.Key.ToString()).ToArray();

            FaceData = new FaceDataEntry[entries.Length];
            Characters = new List<string>();
            DisplayNames = new Dictionary<string, string>();
            Char2FaceData = new Dictionary<string, FaceDataEntry[]>();
            var cnt = 0;
            for (var i = 0; i < entries.Length; i++)
            {
                var data = new byte[0x58];
                Array.Copy(fd, i * data.Length, data, 0, data.Length);
                var entry = new FaceDataEntry
                {
                    FSID = entries[i][0],
                    MPID = entries[i][1],
                    Archive = entries[i][3],
                    Index = int.Parse(entries[i][4]),
                    Data = data,
                    Emotions = new List<string>(),
                    CanBlush = false,
                    CanSweat = false
                };

                Debug.Assert(entry.FSID.Split('_').Length == 3);
                entry.Style = entry.FSID.Split('_')[1];
                entry.Character = entry.FSID.Split('_')[2];

                // Don't generate debug entries
                if (entry.Character != "カゲマン" && entry.Archive.StartsWith("_カゲマン_"))
                {
                    continue;
                }

                bool add = false;

                foreach (var resource in ResourceSet)
                {
                    if (!resource.StartsWith(entry.Archive + "_"))
                        continue;
                    add = true;
                    var emo = resource.Substring(entry.Archive.Length + 1);
                    switch (emo)
                    {
                        case "汗":
                            entry.CanSweat = true;
                            break;
                        case "照":
                            entry.CanBlush = true;
                            break;
                        default:
                            entry.Emotions.Add(emo);
                            break;
                    }
                }

                if (add)
                {
                    FaceData[cnt++] = entry;
                    if (!Characters.Contains(entry.Character))
                    {
                        
                        Characters.Add(entry.Character);
                        if (MPIDDict.ContainsKey(entry.MPID))
                        {
                            var name = MPIDDict[entry.MPID];
                            var c = DisplayNames.Values.Count(x => new Regex(@" \(\d+\)").Replace(x, string.Empty) == MPIDDict[entry.MPID]);
                            if (c != 0)
                                name += $" ({c + 1})";
                            DisplayNames[entry.Character] = name;
                        }
                        else
                            DisplayNames[entry.Character] = entry.Character;
                    }

                    if (Char2FaceData.ContainsKey(entry.Character) && entry.Index >= Char2FaceData[entry.Character].Length)
                    {
                        var NewArr = new FaceDataEntry[entry.Index + 1];
                        for (var j = 0; j < Char2FaceData[entry.Character].Length; j++)
                        {
                            if (Char2FaceData[entry.Character][j] != null)
                                NewArr[j] = Char2FaceData[entry.Character][j];
                        }
                        Char2FaceData[entry.Character] = NewArr;
                    }
                    else
                    {
                        Char2FaceData[entry.Character] = new FaceDataEntry[entry.Index + 1];
                    }
                    Debug.Assert(Char2FaceData[entry.Character][entry.Index] == null);
                    Char2FaceData[entry.Character][entry.Index] = entry;

                }
            }
            Array.Resize(ref FaceData, cnt);
            Characters = Characters.OrderBy(c => DisplayNames[c]).ToList();
            var ncbis = new List<cbItem>();
            foreach (var character in Characters)
            {
                ncbis.Add(new cbItem { Text = DisplayNames[character], Value = character });
            }
            CB_Character.DisplayMember = CB_PortraitStyle.DisplayMember = CB_Emotion.DisplayMember = "Text";
            CB_Character.ValueMember = CB_PortraitStyle.ValueMember = CB_Emotion.ValueMember = "Value";
            CB_Character.DataSource = ncbis;
        }

        private void UpdateCharacter(object sender, EventArgs e)
        {
            if (!loaded)
                return;
            CHARACTER = CB_Character.SelectedValue.ToString();
            var OldStyle = (CB_PortraitStyle.SelectedItem as cbItem ?? new cbItem { Text = "", Value = -1 }).Text;
            var OldEmo = (CB_Emotion.SelectedItem as cbItem ?? new cbItem { Text = "", Value = "" }).Value.ToString();
            var StyleInd = -1;
            var ncbis = new List<cbItem>();
            for (var i = 0; i < Char2FaceData[CHARACTER].Length; i++)
            {
                var entry = Char2FaceData[CHARACTER][i];
                if (entry != null)
                {
                    ncbis.Add(new cbItem {Text = entry.Style, Value = i});
                    if (entry.Style == OldStyle) { StyleInd = i; }
                        
                }
            }
            CB_PortraitStyle.DataSource = ncbis;
            if (StyleInd != -1)
                CB_PortraitStyle.SelectedValue = StyleInd;
            if (CB_Emotion.Items.Cast<cbItem>().Any(cbi => cbi.Value.ToString() == OldEmo))
                CB_Emotion.SelectedValue = OldEmo;
            UpdateImage();
        }

        private void UpdateStyle(object sender, EventArgs e)
        {
            if (!loaded)
                return;
            STYLE = (int)CB_PortraitStyle.SelectedValue;
            STYLE_STR = ((cbItem) CB_PortraitStyle.SelectedItem).Text;
            var entry = Char2FaceData[CHARACTER][STYLE];
            CHK_SweatDrop.Enabled = entry.CanSweat;
            CHK_Blush.Enabled = entry.CanBlush;
            if (!entry.CanSweat)
                CHK_SweatDrop.Checked = false;
            if (!entry.CanBlush)
                CHK_Blush.Checked = false;
            var OldEmo = (CB_Emotion.SelectedItem as cbItem ?? new cbItem { Text = "", Value = "" }).Value.ToString();
            var ncbis = new List<cbItem>();
            foreach (var emo in entry.Emotions)
            {
                ncbis.Add(new cbItem { Text = emo, Value = emo });
            }
            CB_Emotion.DataSource = ncbis;
            if (CB_Emotion.Items.Cast<cbItem>().Any(cbi => cbi.Value.ToString() == OldEmo))
                CB_Emotion.SelectedValue = OldEmo;
            UpdateImage();
        }
        private void UpdateEmotion(object sender, EventArgs e)
        {
            if (!loaded)
                return;
            EMOTION = CB_Emotion.SelectedValue.ToString();
            UpdateImage();
        }

        private void UpdateBlush(object sender, EventArgs e)
        {
            BLUSH = CHK_Blush.Checked;
            UpdateImage();
        }

        private void UpdateSweat(object sender, EventArgs e)
        {
            SWEAT = CHK_SweatDrop.Checked;
            UpdateImage();
        }

        private void UpdateImage()
        {
            if (!loaded)
                return;
            var entry = Char2FaceData[CHARACTER][STYLE];
            var path = $"{entry.Archive}_{EMOTION}";
            Image C;
            C = (Resources.ResourceManager.GetObject(path) as Image) ?? new Bitmap(1, 1);
            using (var g = Graphics.FromImage(C))
            {
                if (BLUSH)
                {
                    var coord = new Point(0, 0);
                    switch (STYLE_STR)
                    {
                        case "BU":
                            coord = new Point(BitConverter.ToUInt16(entry.Data, 0x48), BitConverter.ToUInt16(entry.Data, 0x4A));
                            break;
                        case "TK":
                            coord = new Point(BitConverter.ToUInt16(entry.Data, 0x38), BitConverter.ToUInt16(entry.Data, 0x3A));
                            break;
                        default:
                            coord = new Point(0, 0);
                            break;
                    }
                    try
                    {
                        g.DrawImage(Resources.ResourceManager.GetObject($"{entry.Archive}_照") as Image, coord);
                    } catch (ArgumentNullException) { }
                    
                }
                if (SWEAT)
                {
                    var coord = new Point(0, 0);
                    switch (STYLE_STR)
                    {
                        case "BU":
                            coord = new Point(BitConverter.ToUInt16(entry.Data, 0x50), BitConverter.ToUInt16(entry.Data, 0x52));
                            break;
                        case "TK":
                            coord = new Point(BitConverter.ToUInt16(entry.Data, 0x40), BitConverter.ToUInt16(entry.Data, 0x42));
                            break;
                        default:
                            coord = new Point(0, 0);
                            break;
                    }
                    try
                    {
                        g.DrawImage(Resources.ResourceManager.GetObject($"{entry.Archive}_汗") as Image, coord);
                    }
                    catch (ArgumentNullException) { }
                }
            }
            PB_Portrait.Image = C;
        }

        private void SaveImage(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();
            var emotions = new List<string>(new[] {((cbItem) CB_Emotion.SelectedItem).Text});
            if (BLUSH)
                emotions.Add("照");
            if (SWEAT)
                emotions.Add("汗");
            var name = $"{((cbItem)CB_Character.SelectedItem).Text}_{((cbItem)CB_PortraitStyle.SelectedItem).Text.ToLower()}_{string.Join(",",emotions)}.png";
            sfd.FileName = name;
            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            PB_Portrait.Image.Save(sfd.FileName, ImageFormat.Png);
        }
    }

    public class FaceDataEntry
    {
        public string FSID;
        public string MPID;
        public string Character;
        public string Archive;
        public int Index;

        public byte[] Data;

        public string Style;

        public List<string> Emotions;
        public bool CanBlush;
        public bool CanSweat;
    }

    public class cbItem
    {
        public string Text { get; set; }
        public object Value { get; set; }
    }
}
