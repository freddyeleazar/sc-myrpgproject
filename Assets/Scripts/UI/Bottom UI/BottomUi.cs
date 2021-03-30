using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class BottomUi : MonoBehaviour
{
    public Image background_;
    public BottomUiEmptyContainer_ leftContainer_;
    public BottomUiEmptyContainer_ rightContainer_;

    public Player_ player;
    public Base_ currentSelectedObject;

    public void Start()
    {
        Instantiate(player.graphics_.ui.bottomUI, leftContainer_.transform);
    }

    public void LoadSelectedObjectUI()
    {
        ClearRightContainer();
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null)//Si se hizo click sobre algún objeto
        {
            if (TryParseToBase(selectedObject, out Base_ parsedGameObject))//Si el objeto es mostrable por la bottom UI
            {
                if (!IsAlreadyShown(parsedGameObject))//Si el objeto aún no ha sido cargado en la bottom UI
                {
                    ClearRightContainer();//Limpiar el contenedor derecho de la bottom UI
                    currentSelectedObject = parsedGameObject;//Asignarlo como el nuevo objeto seleccionado
                    Instantiate(currentSelectedObject.graphics_.ui.bottomUI, rightContainer_.transform);
                }
            }
        }
        else
        {
            ClearRightContainer();
        }
    }

    public void LoadUI(Base_ selectedObject)
    {
        if (!IsAlreadyShown(selectedObject))//Si el objeto no está siendo mostrado aún en la bottom UI...
        {
            ClearRightContainer();
            currentSelectedObject = selectedObject;
            Instantiate(currentSelectedObject.graphics_.ui.bottomUI, rightContainer_.transform);
        }
    }

    public void LoadUI(Ground_ selectedTileGround)
    {
        ClearRightContainer();
        currentSelectedObject = null;
        Instantiate(selectedTileGround.graphics_.ui_.bottomUi, rightContainer_.transform);
    }

    public bool TryParseToBase(GameObject gameObjectToParse, out Base_ parsedGameObject)
    {
        if(gameObjectToParse.GetComponentInParent<Base_>() != null)
        {
            parsedGameObject = gameObjectToParse.GetComponentInParent<Base_>();
            return true;
        }
        else
        {
            parsedGameObject = null;
            return false;
        }
    }

    public bool IsAlreadyShown(Base_ selectedObject)
    {
        bool isAlreadyShown = false;
        if(selectedObject == currentSelectedObject)
        {
            isAlreadyShown = true;
        }
        return isAlreadyShown;
    }

    public void ClearRightContainer()
    {
        if(rightContainer_.GetComponentInChildren<BaseBottomUi_>() != null)
        {
            currentSelectedObject = null;
            Destroy(rightContainer_.GetComponentInChildren<BaseBottomUi_>().gameObject);
        }
    }
}
