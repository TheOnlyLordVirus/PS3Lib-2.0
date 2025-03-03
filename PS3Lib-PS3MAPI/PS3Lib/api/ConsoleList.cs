using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PS3Lib
{

    /// <summary>
    /// CCAPI Console list converted to WinForms designer for easy UI editing and 4k support.
    /// - Lord Virus 9/23/2022
    /// </summary>
    public partial class ConsoleList : Form
    {
        private PS3API Api;
        private List<CCAPI.ConsoleInfo> data;
        private bool? result = null;
        private int tNum = -1;

        /// <summary>
        /// Pass the current API object.
        /// </summary>
        /// <param name="f">Current PS3API instance</param>
        public ConsoleList(PS3API f)
        {
            InitializeComponent();

            // Change the text to match the current computers language.
            // This isn't really needed, just nice to have because alot of modders don't speak / read english.
            // If you truely intend to use this, it should be implemented throughout the rest of your tool.
            btnRefresh.Text = strTraduction("btnRefresh");
            btnConnect.Text = strTraduction("btnConnect");
            lblInfo.Text = strTraduction("selectGrid");
            this.Text = strTraduction("formTitle");

            Api = f;
            data = Api.CCAPI.GetConsoleList();

            ImageList imgL = new ImageList();
            imgL.Images.Add(BytesToIcon(ps3icon));
            listView.SmallImageList = imgL;
            int sizeData = data.Count();

            for (int i = 0; i < sizeData; i++)
            {
                ListViewItem item = new ListViewItem(" " + data[i].Name + " - " + data[i].Ip);
                item.ImageIndex = 0;
                listView.Items.Add(item);
            }

            // If there are more than 0 targets we show the form
            // Else we inform the user to create a console.
            if (sizeData > 0)
                this.ShowDialog();
            else
            {
                result = false;
                this.Close();
                MessageBox.Show(strTraduction("noConsole"), strTraduction("noConsoleTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// When we select a new console.
        /// </summary>
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView.FocusedItem != null)
            {
                tNum = listView.FocusedItem.Index;
                btnConnect.Enabled = true;
                string Name, Ip = "?";

                if (data[tNum].Name.Length > 18)
                    Name = data[tNum].Name.Substring(0, 17) + "...";
                else Name = data[tNum].Name;

                if (data[tNum].Ip.Length > 16)
                    Ip = data[tNum].Name.Substring(0, 16) + "...";
                else Ip = data[tNum].Ip;

                lblInfo.Text = strTraduction("selectedLbl") + " " + Name + " / " + Ip;
            }
        }

        /// <summary>
        /// Connect...
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (tNum > -1)
            {
                if (Api.ConnectTarget(data[tNum].Ip))
                {
                    Api.setTargetName(data[tNum].Name);
                    result = true;
                }
                else result = false;


                this.Close();
            }
            else
                MessageBox.Show(strTraduction("errorSelect"), strTraduction("errorSelectTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Attach...
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tNum = -1;
            listView.Clear();
            lblInfo.Text = strTraduction("selectGrid");
            btnConnect.Enabled = false;
            data = Api.CCAPI.GetConsoleList();
            int sizeD = data.Count();
            for (int i = 0; i < sizeD; i++)
            {
                ListViewItem item = new ListViewItem(" " + data[i].Name + " - " + data[i].Ip);
                item.ImageIndex = 0;
                listView.Items.Add(item);
            }
        }

        #region CCAPI Language

        /// <summary>
        /// This stuff was made by FMT / imscx for multi-language support. Feel free to use or remove this as you wish.
        /// </summary>
        public enum Lang
        {
            Null,
            French,
            English,
            German
        }

        private class SetLang
        {
            public static Lang defaultLang = Lang.Null;
        }

        public void SetFormLang(Lang Language)
        {
            SetLang.defaultLang = Language;
        }

        private Lang getSysLanguage()
        {
            if (SetLang.defaultLang == Lang.Null)
            {
                if (CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName.StartsWith("FRA"))
                    return Lang.French;
                else if (CultureInfo.CurrentCulture.ThreeLetterWindowsLanguageName.StartsWith("GER"))
                    return Lang.German;
                return Lang.English;
            }
            else return SetLang.defaultLang;
        }

        public string strTraduction(string keyword)
        {
            Lang lang = getSysLanguage();
            if (lang == Lang.French)
            {
                switch (keyword)
                {
                    case "btnConnect": return "Connexion";
                    case "btnRefresh": return "Rafraîchir";
                    case "errorSelect": return "Vous devez d'abord sélectionner une console.";
                    case "errorSelectTitle": return "Sélectionnez une console.";
                    case "selectGrid": return "Sélectionnez une console dans la grille.";
                    case "selectedLbl": return "Sélection :";
                    case "formTitle": return "Choisissez une console...";
                    case "noConsole": return "Aucune console disponible, démarrez CCAPI Manager (v2.60+) et ajoutez une nouvelle console.";
                    case "noConsoleTitle": return "Aucune console disponible.";
                }
            }
            else if (lang == Lang.German)
            {
                switch (keyword)
                {
                    case "btnConnect": return "Verbinde";
                    case "btnRefresh": return "Wiederholen";
                    case "errorSelect": return "Du musst zuerst eine Konsole auswählen.";
                    case "errorSelectTitle": return "Wähle eine Konsole.";
                    case "selectGrid": return "Wähle eine Konsole innerhalb dieses Gitters.";
                    case "selectedLbl": return "Ausgewählt :";
                    case "formTitle": return "Wähle eine Konsole...";
                    case "noConsole": return "Keine Konsolen verfügbar - starte CCAPI Manager (v2.60+) und füge eine neue Konsole hinzu.";
                    case "noConsoleTitle": return "Keine Konsolen verfügbar.";
                }
            }
            else
            {
                switch (keyword)
                {
                    case "btnConnect": return "Connection";
                    case "btnRefresh": return "Refresh";
                    case "errorSelect": return "You need to select a console first.";
                    case "errorSelectTitle": return "Select a console.";
                    case "selectGrid": return "Select a console within this grid.";
                    case "selectedLbl": return "Selected :";
                    case "formTitle": return "Select a console...";
                    case "noConsole": return "None consoles available, run CCAPI Manager (v2.60+) and add a new console.";
                    case "noConsoleTitle": return "None console available.";
                }
            }
            return "?";
        }

        #endregion

        /// <summary>
        /// We wait for the result to return true or false, while its null we wait in PS3API / CCAPI.
        /// </summary>
        public bool? Result
        {
            get { return result; }
        }

        /// <summary>
        /// Set the result to false when form closed.
        /// </summary>
        private void ConsoleList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(result is null)
                result = false;
        }
    }
}
