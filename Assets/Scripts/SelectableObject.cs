using UnityEngine;
using System;
using System.Collections;

public class SelectableObject : MonoBehaviour {
	private CharController safariGirl;
	public string[] DescriptionLines; // might have to make a texture2d...
	public Texture2D thumbnail;
	public bool talk = true;
	public bool take = true;
	public bool observe = true;
	public bool use = true;
	public bool open = true;
	public bool close = true;
	public bool combine = true;
	public bool enter = true;
	
	private bool observeGUIOn = false;
	public TextMesh[] DescriptionGUILines;
	
	public Texture2D openTex;
	public Texture2D closedTex;
	public SelectableObject objectInside;
	private bool alreadyOpened = false;
	
	public string SceneToEnter = "";
	
	void Start() {
		safariGirl = Camera.main.gameObject.transform.parent.gameObject.GetComponent(typeof(CharController)) as CharController;
	}
	
	public void Unselect() {
		if(observeGUIOn == true) {
			for(int i = 0; i < DescriptionGUILines.Length; i++) {
				DescriptionGUILines[i].text = "";
			}
			observeGUIOn = false;
			PlayerInventory inv = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
			inv.ShowInventory();
		}
	}
	
	
	void OnMouseOver() {
		safariGirl.HoverObject = this;
	}
	
	void OnMouseExit() {
		if(safariGirl != null && safariGirl.HoverObject != null) {
			safariGirl.HoverObject = null;
		}
	}
	
	void OnMouseDown() {
		safariGirl.SelectedObject = this;
		
		int objectDistance = Convert.ToInt32((safariGirl.gameObject.collider.bounds.extents.x + gameObject.collider.bounds.extents.x) * 1.2f);
		if(safariGirl.gameObject.transform.position.x < gameObject.transform.position.x) {
			safariGirl.WalkTo(new Vector2(gameObject.transform.position.x - objectDistance, gameObject.transform.position.z));
		} else {
			safariGirl.WalkTo(new Vector2(gameObject.transform.position.x + objectDistance, gameObject.transform.position.z));
		}
	}
	
	public void Talk() {
		if(!talk) {
			Debug.LogError("Talk is not enabled for this object and was mistakenly triggered.");
		}
	}
	
	public void Observe() {
		if(!observe) {
			Debug.LogError("Observe is not enabled for this object and was mistakenly triggered.");
		}		
		observeGUIOn = true;
		PlayerInventory inv = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
		inv.HideInventory();
		
		for(int i = 0; i < DescriptionLines.Length; i++) {
			DescriptionGUILines[i].text = DescriptionLines[i];
		}
	}

	public void Enter() {
		if(!enter) {
			Debug.LogError("Enter is not enabled for this object and was mistakenly triggered.");
		} else if(SceneToEnter == "") {
			Debug.LogError("Error! Destination for object "+gameObject.name+" is not set.");
		}
		
		Application.LoadLevel(SceneToEnter);
		
	}
	
	public void Use() {
		if(!use) {
			Debug.LogError("Use is not enabled for this object and was mistakenly triggered.");
		} else {
			if(this.gameObject.name == "SkyIsRedSign") {
				Camera c = Camera.main.gameObject.GetComponent(typeof(Camera)) as Camera;
				c.backgroundColor = Color.red;
			}
		}
	}
	
	public void Take() {
		if(!take) {
			Debug.LogError("Take is not enabled for this object and was mistakenly triggered.");
		}
		PlayerInventory inv = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
		inv.AddItem(this);
		safariGirl.SelectedObject = this;
		this.gameObject.active = false;
	}
	
	public void Open() {
		if(!open) {
			Debug.LogError("Open is not enabled for this object and was mistakenly triggered.");
		}
		if(openTex != null) {
			gameObject.renderer.material.SetTexture("_MainTex", openTex);
		}
		if(!alreadyOpened) {
			PlayerInventory inv = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
			inv.AddItem(objectInside);
			safariGirl.SelectedObject = objectInside;
			//objectInside.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -100.0f));
			alreadyOpened = true;
		}
	}
	
	public void Close() {
		if(!close) {
			Debug.LogError("Close is not enabled for this object and was mistakenly triggered.");
		}
		if(closedTex != null) {
			gameObject.renderer.material.SetTexture("_MainTex", closedTex);
		}
	}
}
