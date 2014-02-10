using UnityEngine;
using System.Collections;

public class CharController : MonoBehaviour {
	private float speed = 20.0f;
	private float movePerFrame = 0.0f;
	private Vector2 destination;
	public StatusSelectBox statusSelectBox;
	
	private SelectableObject selectedObject;
	public SelectableObject SelectedObject {
		get { return selectedObject; }
		set { 
			if(selectedObject) {
				SelectableObject prev = selectedObject;
				prev.Unselect();
			}
			selectedObject = value; 
			SetGUIButtons();
			SetStatusGUI(); 
		}
	}
	private SelectableObject hoverObject;
	public SelectableObject HoverObject {
		get { return hoverObject; }
		set { hoverObject = value; SetGUIButtons(); SetStatusGUI(); }
	}
	
	private void SetGUIButtons() {
		foreach(GUIButton b in Camera.main.GetComponentsInChildren(typeof(GUIButton))) {
			b.SetCurrentObject(null);
			b.SetCurrentObject(selectedObject);
		}
	}
	
	private void SetStatusGUI() {
		if (hoverObject && hoverObject != selectedObject) {
			statusSelectBox.Text = "HOVER";
            statusSelectBox.Image = hoverObject.thumbnail;
		} else if(selectedObject) {
            statusSelectBox.Image = selectedObject.thumbnail;
			statusSelectBox.Text = "SELECTED";
		} else {
            statusSelectBox.Text = "";
            statusSelectBox.Image = null;
		}
	}
	
	// positions are represented as x,y on the ground plane
	public void WalkTo(Vector2 dest) {
		destination = dest;
		float dist = Vector2.Distance(new Vector2(this.transform.position.x, this.transform.position.z), destination);
		movePerFrame = (Time.deltaTime * speed);
	}
	
	void Update() {
		if(this.transform.position.x != destination.x || this.transform.position.z != destination.y) {
			this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(destination.x, this.transform.position.y, destination.y), movePerFrame);
		} else {
			movePerFrame = 0.0f;
		}
	}
}
