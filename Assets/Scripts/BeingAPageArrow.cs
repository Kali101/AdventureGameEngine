using UnityEngine;
using System.Collections;

public class BeingAPageArrow : MonoBehaviour {
	public enum ArrowDirection { right, left };
	public ArrowDirection dir;
	public Texture2D normal;
	public Texture2D hover;
	public Texture2D disabled;
	
	private bool buttonEnabled = false;
	public bool ButtonEnabled {
		get { return buttonEnabled; }
		set { buttonEnabled = value; SetArrowTextures(); }
	}
	
	public CharController safariGirl;
	
	void SetArrowTextures() {
		if(!buttonEnabled) { 
			renderer.material.SetTexture("_MainTex", disabled); 
		} else { 
			renderer.material.SetTexture("_MainTex", normal); 
		}
	}
	
	void OnMouseOver() {
		if(buttonEnabled) {
			renderer.material.SetTexture("_MainTex", hover);
		}
	}
	
	void OnMouseExit() {
		if(buttonEnabled) {		
			renderer.material.SetTexture("_MainTex", normal);
		}
	}
	
	void OnMouseDown() {
		PlayerInventory pi = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
		if(dir == ArrowDirection.left) { 
			pi.GoToPreviousPage();
		} else if (dir == ArrowDirection.right) {
			pi.GoToNextPage();
		}
	}
}


