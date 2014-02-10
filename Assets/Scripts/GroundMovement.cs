using UnityEngine;
using System.Collections;

public class GroundMovement : MonoBehaviour {
	private GameObject safariGirl;
	// left, right, front, back
	public Rect bounds;
	
	void Start() {
		safariGirl = Camera.main.gameObject.transform.parent.gameObject;
	}
	
	void OnMouseDown() {
		(safariGirl.GetComponent(typeof(CharController)) as CharController).SelectedObject = null;
		
		Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
		//Debug.DrawRay (r.origin, r.direction * 1000.0f, Color.yellow);
		RaycastHit rh;
		Physics.Raycast(r, out rh, Mathf.Infinity);
		
		if(rh.collider.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			CharController cc = (safariGirl.GetComponent(typeof(CharController)) as CharController);
			float newXLoc = rh.point.x;
			float newZLoc = rh.point.z;
			
			if(rh.point.x  < bounds.x) {
				newXLoc = bounds.x;
			} else if (rh.point.x > bounds.y) {
				newXLoc = bounds.y;
			}
			
			if(rh.point.z < bounds.width) {
				newZLoc = bounds.width;
			} else if(rh.point.z > bounds.height) {
				newZLoc = bounds.height;
			}
			
			if((bounds.x <= newXLoc && newXLoc <= bounds.y) && (bounds.width <= newZLoc && newZLoc <= bounds.height)) {
				cc.WalkTo(new Vector2(newXLoc, newZLoc));
			} 
		} 
	}
}
