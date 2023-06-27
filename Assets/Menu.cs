using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
	[Header("References")]
	[SerializeField] TextMeshProUGUI currencyUI;

	private bool isMenuOpen = true;
	private void Start()
	{
	}
	private void OnGUI()
	{
		currencyUI.text = LevelManager.Instance.currency.ToString() +"$";
	}
	public void SetSelected(){

	}
}
