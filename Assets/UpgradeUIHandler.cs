using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUIHandler : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler
{
    public bool mouse_over = false;

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouse_over = true;
		UIManager.Instance.SetHoveringState(true);
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		mouse_over = false;
		UIManager.Instance.SetHoveringState(false);
		gameObject.SetActive(false);
	}
}
