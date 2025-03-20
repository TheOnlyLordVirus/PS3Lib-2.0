using System;
using System.Windows.Forms;
using System.Collections.Generic;

using PS3Lib2.Capi;
using PS3Lib2.Tmapi;
using PS3Lib2.PS3Mapi;
using PS3Lib2.Cheats;
using PS3Lib2.Interfaces;

#nullable enable

namespace DemoTool;

public sealed partial class PlaystationApiDemoForm : Form
{
    #region Cheese

    private IPlaystationApi? currentApi = null;
    private IEnumerable<IGameCheat>? minecraftCheats = null;

    private const string _godModeString = "God Mode";
    private const uint _godModeAddress = 0x004B2021;
    private const byte _godModeEnable = 0x80;
    private const byte _godModeDisable = 0x20;

    private const string _superJumpButtonString = "Super Jump";
    private const string _superJumpString = "Super Jump & No Fall Damage";
    private const uint _superJumpAddress = 0x003AA77C;
    private readonly byte _superJumpEnable = 0x3F;
    private readonly byte _superJumpDisable = 0x3E;

    private const string _fallDamageString = "No Fall Damage";
    private const uint _fallDamageAddress = 0x003A409C;
    private const byte _fallDamageEnable = 0x40;
    private const byte _fallDamageDisable = 0x41;

    #endregion

    public PlaystationApiDemoForm()
    {
        InitializeComponent();
    }

    #region Select Api

    private void CCAPRadioButton_CheckedChanged(object _, EventArgs __) =>
        Internal_InitApi(new CCAPI_Wrapper());

    private void TMAPIRadioButton_CheckedChanged(object _, EventArgs __) =>
        Internal_InitApi(new TMAPI_Wrapper());

    private void PS3mapiRadioButton_CheckedChanged(object _, EventArgs __) =>
        Internal_InitApi(new PS3MAPI_Wrapper());

    private void Internal_InitApi(IPlaystationApi newApi) =>
        Internal_DisplayExeceptions(() =>
        {
            currentApi?.Dispose();
            currentApi = newApi;

            // Init cheat with the new api instance.
            Internal_InitMinecraftCheats(currentApi);
        });

    #endregion

    #region Connect and Attach

    private void Connect_Button_Click(object _, EventArgs __) =>
        Internal_DisplayExeceptions(() =>
        {
            if (currentApi is null)
                throw new Exception("No API is selected. (currentApi is null)");

            var ConnectForm = new ConnectDialog(currentApi);

            ConnectForm.ShowDialog();
        });

    private void Attach_Button_Click(object _, EventArgs __) => 
        Internal_DisplayExeceptions(() =>
        {
            if (!currentApi!.AttachGameProcess())
                throw new Exception("Failed to AttachGameProcess!");

            MessageBox.Show("Sucessfully attached to the game process!", "Attached Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });

    #endregion

    #region Basic Read / Write

    private void Read_Button_Click(object _, EventArgs __) =>
        Internal_ValidateApiAction(() =>
        {
            byte currentValue = currentApi!.ReadMemoryU8(_godModeAddress);
            bool valueToBool = currentValue is _godModeEnable;
            string curState = Internal_GetCheatString(_godModeString, valueToBool);

            MessageBox.Show(curState, "God Mode Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });

    private void Write_Button_Click(object _, EventArgs __) =>
        Internal_ValidateApiAction(() =>
        {
            // Simple Godmode Toggle Example.
            byte currentValue = currentApi!.ReadMemoryU8(_godModeAddress);

            currentApi
                .WriteMemoryU8
                (
                    _godModeAddress,
                    currentValue is not _godModeEnable ?
                    _godModeEnable : _godModeDisable
                );
        });

    #endregion

    #region IGameCheat Example


    private string Internal_GetCheatString(string messageString, bool enabled) =>
        string.Concat(messageString, " is ", Enabled ? "Enabled!" : "Disabled!");

    private string Internal_GetDialogString(string messageString) =>
        string.Concat(messageString, " Dialog.");

    private CheatActionHandler Internal_GetCheatActivationDialog(string cheatDialogMessage) => new 
        (
            () => MessageBox.Show
            (
                Internal_GetCheatString(cheatDialogMessage, true),
                Internal_GetDialogString(cheatDialogMessage),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            ),

            () => MessageBox.Show
            (
                Internal_GetCheatString(cheatDialogMessage, false),
                Internal_GetDialogString(cheatDialogMessage),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            )
        );

    private void Internal_InitMinecraftCheats(IPlaystationApi currentApi)
    {
        Dictionary<Guid, string> minecraftCheatNames = [];


        // God Mode Cheat:
        // Read & Write bytes at the god mode address, then display a message box for enable / disable.
        IGameCheat[] godmodeGameCheats =
            [
                new PlaystationMemoryWriter(currentApi, _godModeAddress, _godModeEnable, _godModeDisable),
                Internal_GetCheatActivationDialog(_godModeString)
            ];

        IGameCheat godModeGameCheatGroup = new GameCheatGroup(currentApi, godmodeGameCheats);
        minecraftCheatNames.Add(godModeGameCheatGroup.Id, _godModeString); // Maps button names to cheat id.


        // No Fall Cheat:
        // No fall cheat is the same, but we keep a copy of no fall for super jump.
        IGameCheat noFallGameCheat = new PlaystationMemoryWriter(currentApi, _fallDamageAddress, _fallDamageEnable, _fallDamageDisable);

        IGameCheat[] noFallGameCheats =
             [
                noFallGameCheat,
                Internal_GetCheatActivationDialog(_fallDamageString)
             ];

        IGameCheat noFallGameCheatGroup = new GameCheatGroup(currentApi, noFallGameCheats);
        minecraftCheatNames.Add(noFallGameCheatGroup.Id, _fallDamageString);


        // Super Jump Cheat:
        // Superjump is the same, but we also enable / disable fall damage when we toggle the cheat.
        IGameCheat[] SuperJumpGameCheats =
            [
                new PlaystationMemoryWriter(currentApi, _superJumpAddress, _superJumpEnable, _superJumpDisable),
                noFallGameCheat,
                Internal_GetCheatActivationDialog(_superJumpString)
            ];

        IGameCheat superJumpGameCheatGroup = new GameCheatGroup(currentApi, SuperJumpGameCheats);
        minecraftCheatNames.Add(superJumpGameCheatGroup.Id, _superJumpButtonString);


        // Now each cheat has its own definition for how to execute the code.
        minecraftCheats = [godModeGameCheatGroup, noFallGameCheatGroup, superJumpGameCheatGroup];


        // Add all the cheats to the layout as buttons that toggle the cheat OnClick
        cheatButtonFlowLayout.Controls.Clear();

        foreach (var cheat in minecraftCheats)
        {
            Button cheatButton = new ();
            cheatButton.Width = 120;
            cheatButton.Height = 40;
            cheatButton.Text = minecraftCheatNames[cheat.Id];

            cheatButton.Click +=
                (s, e) => Internal_ValidateApiAction
                    (
                        () => cheat.Toggle()
                    );

            cheatButtonFlowLayout.Controls.Add(cheatButton);
        }
    }

    #endregion

    #region Exeception Handling

    private void Internal_ValidateApiAction(Action throwableAction) =>
        Internal_DisplayExeceptions(() =>
        {
            if (currentApi is null)
                throw new Exception("No API is selected. (currentApi is null)");

            if (!currentApi.IsConnected)
                throw new Exception("Must be connected to Playstation 3!");

            throwableAction.Invoke();
        });

    private void Internal_DisplayExeceptions(Action throwableAction)
    {
        try
        {
            throwableAction.Invoke();
        }

        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    #endregion
}
