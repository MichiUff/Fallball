using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkPanel : MonoBehaviour
{
    private Text textComponent;

	// Use this for initialization
	void Start ()
    {
        InkManager.UpdatedInk += InkManager_UpdatedInk;
        textComponent = transform.GetComponent<Text>();
        textComponent.text = InkManager.CurrentInk.ToString();
    }

    private void InkManager_UpdatedInk(int ink)
    {
        textComponent.text = ink.ToString();
    }
}
