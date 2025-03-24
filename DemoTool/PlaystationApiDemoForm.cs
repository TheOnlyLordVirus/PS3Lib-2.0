using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using PS3Lib2;
using PS3Lib2.Capi;
using PS3Lib2.Tmapi;
using PS3Lib2.PS3Mapi;
using PS3Lib2.Cheats;
using PS3Lib2.Extentions;

#nullable enable

namespace DemoTool;

public sealed partial class PlaystationApiDemoForm : Form
{
    public PlaystationApiDemoForm() => InitializeComponent();

    #region Cheese

    private IPlaystationApi? currentApi = null;
    private IEnumerable<IGameCheat>? minecraftCheats = null;
    private IEnumerable<string>? supportedMethods = null;

    // Load from a json? Sure why not.
    // This is just an example tho.
    private const string _godModeString = "God Mode";
    private const uint _godModeAddress = 0x004B2021;
    private const byte _godModeEnable = 0x80;
    private const byte _godModeDisable = 0x20;

    private const string _superJumpButtonString = "Super Jump";
    private const string _superJumpString = "Super Jump & No Fall Damage";
    private const uint _superJumpAddress = 0x003AA77C;
    private const byte _superJumpEnable = 0x3F;
    private const byte _superJumpDisable = 0x3E;

    private const string _fallDamageString = "No Fall Damage";
    private const uint _fallDamageAddress = 0x003A409C;
    private const byte _fallDamageEnable = 0x40;
    private const byte _fallDamageDisable = 0x41;

    #endregion

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
            if (currentApi?.GetType() == newApi.GetType())
                return;

            currentApi?.Dispose();
            currentApi = newApi;

            supportedMethods = currentApi.GetSupportedMethods();

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

            var connectDialog = new ConnectDialog(currentApi);

            if (connectDialog.ShowDialog() is DialogResult.Cancel)
            {
                connectDialog?.Dispose();
                throw new Exception("Failed to connect!");
            }

            connectDialog?.Dispose();

            if (supportedMethods.Contains("RingBuzzer"))
                currentApi.RingBuzzer();

            if (supportedMethods.Contains("VshNotify"))
                currentApi.VshNotify("Demo tool connected to playstation 3!");

            MessageBox.Show("Demo Tool has successfully connected to your playstation 3!");
        });

    private void Attach_Button_Click(object _, EventArgs __) =>
        Internal_ValidateApiAction(() =>
        {
            if (!currentApi!.AttachGameProcess())
                throw new Exception("Failed to AttachGameProcess!");

            MessageBox.Show("Sucessfully attached to the game process!", "Attached Dialog", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });

    #endregion

    #region Basic Read / Write Example

    private void Read_Button_Click(object _, EventArgs __) =>
        // Internal_ValidateApiAction will validate that we are not null and that we are connected.
        Internal_ValidateApiAction(() =>
        {
            // This is a basic read byte opperation, there are shorthands for reading & writing all primitive types.
            byte currentValue = currentApi!.ReadMemoryU8(_godModeAddress);

            bool valueToBool = currentValue is _godModeEnable;
            string curState = Internal_GetCheatString(_godModeString, valueToBool);

            MessageBox.Show(curState, "God Mode Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
        });

    // Simple Godmode Toggle Example using read / write memory.
    private void Write_Button_Click(object _, EventArgs __) =>
        Internal_ValidateApiAction(() =>
        {
            // Read byte.
            byte currentValue = currentApi!.ReadMemoryU8(_godModeAddress);

            currentApi!
                .WriteMemoryU8
                (
                    _godModeAddress,
                    currentValue is not _godModeEnable ?
                    _godModeEnable : _godModeDisable
                );
        });

    // I've had to do this alot. Believe me if you've got a bunch of cheats you don't want to manually enter all this shit in.
    // I hope this is easy to understand.
    private void IGameCheat_Simple_Example_Button_Click(object _, EventArgs __) =>
        Internal_ValidateApiAction(() =>
        {
            IGameCheat minecraftGodModeExample = 
                new PlaystationMemoryWriter
                (
                    currentApi!,
                    _godModeAddress, 
                    _godModeEnable, 
                    _godModeDisable
                );

            minecraftGodModeExample.Toggle();
        });

    #endregion

    #region IGameCheat Example

    private string Internal_GetCheatString(string messageString, bool enabled) =>
        string.Concat(messageString, " is ", Enabled ? "Enabled!" : "Disabled!");

    private string Internal_GetDialogString(string messageString) =>
        string.Concat(messageString, " Dialog.");

    private CheatActionHandler Internal_CreateCheatToggleMessageActionHandler(string cheatName)
        => new
        (
            () => Internal_HandleActivationNotification(cheatName, true), 
            () => Internal_HandleActivationNotification(cheatName, false)
        );

    private void Internal_HandleActivationNotification(string message, bool enabled)
    {
        MessageBox.Show
            (
                Internal_GetCheatString(message, enabled),
                Internal_GetDialogString(message),
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );

        Internal_ValidateApiAction(() =>
        {
            if (supportedMethods.Contains("RingBuzzer"))
                currentApi!
                    .RingBuzzer();

            if (supportedMethods.Contains("VshNotify"))
                currentApi!
                .VshNotify
                (
                    Internal_GetCheatString(message, enabled)
                );
        });
    }

    // More advanced implementation for IGameCheat.
    private void Internal_InitMinecraftCheats(IPlaystationApi currentApi)
    {
        Dictionary<Guid, string> minecraftCheatNames = [];

        // God Mode Cheat:
        // Read & Write bytes at the god mode address, We also display a message to the user on the ps3 and pc.
        IGameCheat[] godModeGameCheats =
        [
            new PlaystationMemoryWriter(currentApi, _godModeAddress, _godModeEnable, _godModeDisable),
            Internal_CreateCheatToggleMessageActionHandler(_godModeString)
        ];

        IGameCheat godModeGameCheatsGroup = new GameCheatGroup(currentApi, godModeGameCheats);
        minecraftCheatNames.Add(godModeGameCheatsGroup.Id, _godModeString); // Maps button names to cheat id.

        // No Fall Cheat:
        // No fall cheat is the same, but we keep a copy of no fall for super jump.
        PlaystationMemoryWriter noFallGameCheat = new (currentApi, _fallDamageAddress, _fallDamageEnable, _fallDamageDisable);
        IGameCheat[] noFallGameCheats =
        [
            noFallGameCheat,
            Internal_CreateCheatToggleMessageActionHandler(_fallDamageString)
        ];

        IGameCheat noFallGameCheatGroup = new GameCheatGroup(currentApi, noFallGameCheats);
        minecraftCheatNames.Add(noFallGameCheatGroup.Id, _fallDamageString);

        // Super Jump Cheat:
        // Superjump is the same, but we also enable / disable fall damage when we toggle the cheat.
        IGameCheat[] superJumpGameCheats =
        [
            new PlaystationMemoryWriter(currentApi, _superJumpAddress, _superJumpEnable, _superJumpDisable),
            noFallGameCheat,
            Internal_CreateCheatToggleMessageActionHandler(_superJumpString)
        ];

        IGameCheat superJumpGameCheatGroup = new GameCheatGroup(currentApi, superJumpGameCheats);
        minecraftCheatNames.Add(superJumpGameCheatGroup.Id, _superJumpButtonString);


        // Now each cheat has its own definition for how to execute the code.
        minecraftCheats = [godModeGameCheatsGroup, noFallGameCheatGroup, superJumpGameCheatGroup];


        // Add all the cheats to the layout as buttons that toggle the cheat OnClick
        cheatButtonFlowLayout.Controls.Clear();

        foreach (var cheat in minecraftCheats)
        {
            Button cheatButton = new ()
            {
                Width = 120,
                Height = 40,
                Text = minecraftCheatNames[cheat.Id]
            };
;
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
