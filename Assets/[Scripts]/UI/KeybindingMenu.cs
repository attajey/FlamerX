/*  Filename:           IDamageable.cs
 *  Author:             Kevin Curiel Justo, Yuk Yee Wong
 *  Last Update:        December 7, 2022
 *  Description:        Keybinding screen
 *  Revision History:   December 2, 2022 (Kevin Curiel Justo): Initial script.
 *                      December 4, 2022 (Kevin Curiel Justo): Updated the UI text and input action
 *                      December 7, 2022 (Yuk Yee Wong): Replaced TMPro to Text, replaced the rebinds string with a GetRebindData function
 */

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/// <summary>
/// Keybinding screen
/// </summary>
public class KeybindingMenu : MonoBehaviour
{
    [Header("Actions")]
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private InputActionReference moveAction = null;
    [SerializeField] private InputActionReference fireAction = null;

    [Header("UI")]
    [SerializeField] private Text bindingDisplayTextLeft = null;
    [SerializeField] private Text bindingDisplayTextRight = null;
    [SerializeField] private Text bindingDisplayTextJump = null;
    [SerializeField] private Text bindingDisplayTextFire = null;

    private TempPlayerController playerController = null;
    private InputActionRebindingExtensions.RebindingOperation rebindOperation;

    private void Start()
    {        
        string rebinds = GetRebindData();

        if (string.IsNullOrEmpty(rebinds)){ return; }

        playerController = FindObjectOfType<TempPlayerController>();
        playerController.playerInput.actions.LoadBindingOverridesFromJson(rebinds);

        int JumpBindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);
        int FireBindingIndex = fireAction.action.GetBindingIndexForControl(fireAction.action.controls[0]);

        bindingDisplayTextJump.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[JumpBindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextFire.text = InputControlPath.ToHumanReadableString(
            fireAction.action.bindings[FireBindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextLeft.text = InputControlPath.ToHumanReadableString(
            moveAction.action.bindings[2].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextRight.text = InputControlPath.ToHumanReadableString(
            moveAction.action.bindings[4].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public static string GetRebindData()
    {
        return PlayerPrefs.GetString("rebinds");
    }

    
    public void Save()
    {
        string rebinds = playerController.playerInput.actions.SaveBindingOverridesAsJson();

        PlayerPrefs.SetString("rebinds", rebinds);
    }

    
    public void StartLeftKeybinding()
    {
        playerController.playerInput.SwitchCurrentActionMap("UI");
        
        rebindOperation = moveAction.action.PerformInteractiveRebinding(2)
                                            .WithControlsExcluding("mouse")
                                            .OnMatchWaitForAnother(0.01f)
                                            .OnComplete(operation => RebindComplete())
                                            .Start(); 
          
    }
    public void StartRightKeybinding()
    {
        playerController.playerInput.SwitchCurrentActionMap("UI");

        rebindOperation = moveAction.action.PerformInteractiveRebinding(4)
                                            .WithControlsExcluding("mouse")
                                            .OnMatchWaitForAnother(0.01f)
                                            .OnComplete(operation => RebindComplete())
                                            .Start();
    }

    public void StartJumpKeybinding()
    {        
        playerController.playerInput.SwitchCurrentActionMap("UI");

        rebindOperation = jumpAction.action.PerformInteractiveRebinding()
                                            .WithControlsExcluding("mouse")
                                            .OnMatchWaitForAnother(0.01f)
                                            .OnComplete(operation => RebindComplete())
                                            .Start();
        
    }
    public void StartFireKeybinding()
    {
        playerController.playerInput.SwitchCurrentActionMap("UI");

        rebindOperation = fireAction.action.PerformInteractiveRebinding()
                                            .WithControlsExcluding("mouse")
                                            .OnMatchWaitForAnother(0.01f)
                                            .OnComplete(operation => RebindComplete())
                                            .Start();
    }

    private void RebindComplete()
    {        
        int JumpBindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);
        int FireBindingIndex = fireAction.action.GetBindingIndexForControl(fireAction.action.controls[0]);

        bindingDisplayTextJump.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[JumpBindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextFire.text = InputControlPath.ToHumanReadableString(
            fireAction.action.bindings[FireBindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextLeft.text = InputControlPath.ToHumanReadableString(
            moveAction.action.bindings[2].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        bindingDisplayTextRight.text = InputControlPath.ToHumanReadableString(
            moveAction.action.bindings[4].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        rebindOperation.Dispose();
        playerController.playerInput.SwitchCurrentActionMap("Player");
    }

}
