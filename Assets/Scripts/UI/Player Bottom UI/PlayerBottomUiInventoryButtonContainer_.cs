using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBottomUiInventoryButtonContainer_ : MonoBehaviour
{
    public Image background_;
    public Button inventoryButton;
    public Text inventoryText;
    public Image iconImage;

    public PlayerUi_ playerUI;

    private void Start()
    {
        playerUI = FindObjectOfType<PlayerUi_>();
        inventoryButton.onClick.AddListener(SwitchInventoryUI);
    }

    public void SwitchInventoryUI()
    {
        GameObject inventoryUI = playerUI.inventoryUi;
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
        }
        else
        {
            playerUI.inventoryUi.SetActive(true);
        }
    }
}
