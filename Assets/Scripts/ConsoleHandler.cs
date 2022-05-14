using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.Audio;
using TMPro;

public class ConsoleHandler : MonoBehaviour
{
    public TextMeshProUGUI[] ButtonsText = new TextMeshProUGUI[9];
    public Button[] Buttons = new Button[9];
    public TextMeshProUGUI PlayerInputText;
    public Color passwordCorrectButtonsColor;
    public Color passwordFailedButtonsColor;
    public GameObject antiTeleport;
    public GameObject particulesSystem;
    public AudioSource doorAudioSource;
    public Animator doorOpenAnimator;

    [SerializeField] private string consolePassword = "194";
    private string storageRoomPlayerInput = "";
    private bool passedStorageRoom = false;


    // Buttons turns red during 2 seconds
    // during this time, the buttons are not interactable
    private IEnumerator StorageRoomPasswordFail()
    {
        for (int actualButton = 0; actualButton < Buttons.Length; actualButton++)
        {
            ColorBlock cb = Buttons[actualButton].colors;

            cb.disabledColor = passwordFailedButtonsColor;

            Buttons[actualButton].colors = cb;

            Buttons[actualButton].interactable = false;
        }

        yield return new WaitForSeconds(2);

        // Reset player input string
        PlayerInputText.text = storageRoomPlayerInput = "";

        for (int actualButton = 0; actualButton < Buttons.Length; actualButton++)
        {
            Buttons[actualButton].interactable = true;
        }
    }

    private IEnumerator OpenDoorAfterSound()
    {
        yield return new WaitForSeconds(doorAudioSource.clip.length);

        doorOpenAnimator.SetTrigger("OpenDoor");
    }

    /// <summary>
    /// Handle storage room console input system.
    /// </summary>
    public void StorageRoomConsoleInput()
    {
        try
        {
            for (int i = 0; i < ButtonsText.Length; i++)
            {
                // Actual clicked button correspond to the button text
                if (EventSystem.current.currentSelectedGameObject.name == $"Button {ButtonsText[i].text}")
                {
                    PlayerInputText.text = storageRoomPlayerInput += ButtonsText[i].text;
                }


                // Password is correct
                if (storageRoomPlayerInput == consolePassword)
                {
                    for (int actualButton = 0; actualButton < Buttons.Length; actualButton++)
                    {
                        // buttons become green and are cannot interact with them anymore

                        ColorBlock cb = Buttons[actualButton].colors;

                        cb.disabledColor = passwordCorrectButtonsColor;
                        Buttons[actualButton].colors = cb;

                        Buttons[actualButton].interactable = false;

                        // Password of main room is correct
                        if (passedStorageRoom == true && storageRoomPlayerInput == "658")
                        {
                            // active particules
                            particulesSystem.SetActive(true);
                        }
                        // Password of Storage room is correct
                        if (passedStorageRoom == false)
                        {
                            // door decompression sound is played

                            doorAudioSource.PlayOneShot(doorAudioSource.clip);

                            // deactivate antiTeleport GameObject

                            antiTeleport.SetActive(false);

                            // player can now open the door

                            StartCoroutine(OpenDoorAfterSound());

                            passedStorageRoom = true;
                        }
                    }
                }
                // Password is not correct and player input length is >= of 3
                else if (storageRoomPlayerInput != consolePassword && storageRoomPlayerInput.Length >= 3)
                {
                    // Buttons becomes red during 2 seconds if the player failed the password
                    // during this time, the buttons are not interactables

                    StartCoroutine(StorageRoomPasswordFail());
                }
            }
        }
        catch (NullReferenceException)
        {
            // void
        }

    }

}
