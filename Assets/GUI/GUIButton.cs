using UnityEngine;
using System.Collections;

public class GUIButton : MonoBehaviour {
	public Texture2D disabled;
	public Texture2D hover;
	public Texture2D active;
	public Texture2D enabled;
	
	public enum ButtonType { talk, take, observe, use, open, close, combine, enter };
	public ButtonType type = ButtonType.talk;
	
	private bool showActiveTextureLock = false;
	private bool buttonEnabled = false;
	private SelectableObject currentObject = null;
	
	void Start () {
		gameObject.renderer.material.SetTexture("_MainTex", disabled);		
	}
	
	void OnMouseOver() {
		if(buttonEnabled && !showActiveTextureLock) {
			gameObject.renderer.material.SetTexture("_MainTex", hover);		
		}
	}
	
	void OnMouseExit() {
		if(buttonEnabled) {
			gameObject.renderer.material.SetTexture("_MainTex", enabled);		
		}		
	}
	
	void OnMouseDown() {
		currentObject.Unselect();
		
		if(buttonEnabled) {
			StartCoroutine("BrieflyShowActiveTexture");
			switch(type) {
					case ButtonType.talk:
						if(currentObject.talk) {
							currentObject.Talk();
						}
						break;
					case ButtonType.take:
						if(currentObject.take) {
							currentObject.Take();
						}
						break;
					case ButtonType.open:
						if(currentObject.open) {
							currentObject.Open();
						}
						break;
					case ButtonType.close:
						if(currentObject.close) {
							currentObject.Close();
						}
						break;
					case ButtonType.enter:
						if(currentObject.enter) {
							currentObject.Enter();
						}
						break;
					case ButtonType.combine:
						if(currentObject.combine) {
						}
						break;
					case ButtonType.observe:
						if(currentObject.observe) {
							currentObject.Observe();
						}
						break;
					case ButtonType.use:
						if(currentObject.use) {
							currentObject.Use();
						}
						break;
					default:
						Debug.Log("Button type has not been set correctly OR button type is not recognized.");
						break;
					}
		}
	}
	
	private IEnumerator BrieflyShowActiveTexture() {
		showActiveTextureLock = true;
		gameObject.renderer.material.SetTexture("_MainTex", active);
		yield return new WaitForSeconds(0.25f);
		gameObject.renderer.material.SetTexture("_MainTex", enabled);
		showActiveTextureLock = false;
	}
	
	public void SetCurrentObject(SelectableObject obj) {
		currentObject = obj;
		
		if(currentObject == null) {
			buttonEnabled = false;
			gameObject.renderer.material.SetTexture("_MainTex", disabled);		
			return;
		}
		
		switch(type) {
		case ButtonType.talk:
			if(currentObject.talk) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.take:
			if(currentObject.take) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.open:
			if(currentObject.open) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.close:
			if(currentObject.close) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.enter:
			if(currentObject.enter) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.combine:
			if(currentObject.combine) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.observe:
			if(currentObject.observe) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		case ButtonType.use:
			if(currentObject.use) {
				buttonEnabled=true; 
				gameObject.renderer.material.SetTexture("_MainTex", enabled);
			}
			break;
		default:
			Debug.Log("Button type has not been set correctly OR button type is not recognized.");
			break;
		}
	}
	
	
	
	
}
