using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogBox : MonoBehaviour
{
    public GameObject DialogBoxObject;

    public void DisableDialogBox()
    {
        DialogBoxObject.SetActive(false);
    }
}
