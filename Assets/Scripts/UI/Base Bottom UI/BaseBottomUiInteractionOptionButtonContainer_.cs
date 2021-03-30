using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBottomUiInteractionOptionButtonContainer_ : MonoBehaviour
{
    public Image background_;
    public Button button;
    public Image buttonImage;
    public Image iconImage;

    protected PlayerBehaviour_ playerBehaviour_;

    public void Awake()
    {
        playerBehaviour_ = FindObjectOfType<PlayerBehaviour_>();
        switch (transform.parent.GetComponent<BaseBottomUiInteractionOptionContainer_>().interactionName)
        {
            case "Go":
                button.onClick.AddListener(playerBehaviour_.MoveToSelectedObject);
                break;
            case "Equip":
                button.onClick.AddListener(playerBehaviour_.EquipSelectedItem);
                break;
            case "Store":
                button.onClick.AddListener(playerBehaviour_.StoreSelectedItem);
                break;
            case "Drop":
                button.onClick.AddListener(playerBehaviour_.DropSelectedItem);
                break;
            case "Attack":
                button.onClick.AddListener(playerBehaviour_.AttackSelectedUnit);
                break;
            default:
                break;
        }
    }

}
