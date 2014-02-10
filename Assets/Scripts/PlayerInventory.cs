using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlayerInventory : MonoBehaviour {
	public GameObject[] inventorySlots;
	
	public BeingAPageArrow leftArrow;
	public BeingAPageArrow rightArrow;
	public Texture2D blankTex;
	private int page = 0;
	
	private List<SelectableObject> items = new List<SelectableObject>();
	
	public void HideInventory() {
		leftArrow.ButtonEnabled = false;
		rightArrow.ButtonEnabled = false;
		for(int i = 0; i < inventorySlots.Length; i++){
			inventorySlots[i].renderer.material.SetTexture("_MainTex", blankTex);
		}	
	}
	public void ShowInventory() {
		if(items.Count > 4) {
			leftArrow.ButtonEnabled = true;
			rightArrow.ButtonEnabled = true;
		}
		
		DisplayPage();
	}
	
	public void AddItemOnLoad(SelectableObject item) {
		item.take = false;
		items.Add(item);
		
		if(items.Count > 4) {
			leftArrow.ButtonEnabled = true;
			rightArrow.ButtonEnabled = true;
		}
		
		DisplayPage();	
	}
	
	public void AddItem(SelectableObject item) {
		item.take = false;
		//update item's GUI buttons
		CharController sg = gameObject.GetComponent(typeof(CharController)) as CharController;
		sg.SelectedObject = null;
		sg.SelectedObject = item;
		sg.HoverObject = item;
		
		items.Add(item);
		SingletonSolution.Instance.AddToGlobalInventory(item);

		if(items.Count > 4) {
			leftArrow.ButtonEnabled = true;
			rightArrow.ButtonEnabled = true;
		}
		
		DisplayPage();
	}
	
	public void RemoveItem(SelectableObject item) {
		items.Remove(item);
		
		if(items.Count <= 4) {
			leftArrow.ButtonEnabled = false;
			rightArrow.ButtonEnabled = false;
		}
		
		DisplayPage();
	}
	
	public void GoToPreviousPage() {
		page--;
		
		if(page < 0) {
			page = (Mathf.CeilToInt(items.Count / 4));
		}
		
		DisplayPage();
	}
	
	public void GoToNextPage() {
		page++;
		
		if(page > (Mathf.CeilToInt(items.Count / 4))) {
			page = 0;
		}
		
		DisplayPage();
	}
	
	private void DisplayPage() {		
		if( (items.Count % 4) == 0 || page != Mathf.CeilToInt(items.Count / 4)) {
			for(int i = 0; i < Mathf.Min(4, items.Count); i++) {
				inventorySlots[i].renderer.material.SetTexture("_MainTex", items[page*4 + i].thumbnail);
			}
		} else {
			for(int i = 0; i < 4; i++) {
				inventorySlots[i].renderer.material.SetTexture("_MainTex", blankTex);	
			}
			for(int i = 0; i < items.Count % 4; i++) {
				inventorySlots[i].renderer.material.SetTexture("_MainTex", items[page*4 + i].thumbnail);
			}
		}
	}
}
