using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Used as a class to store all data that is carried between scenes
public class SingletonSolution : MonoBehaviour {

    private static SingletonSolution instance = null;
    public static SingletonSolution Instance {
        get { return instance; }
    }
	
	private List<SelectableObject> globalItems = new List<SelectableObject>();

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
	
	void OnLevelWasLoaded() {
		CharController safariGirl = Camera.main.gameObject.transform.parent.gameObject.GetComponent(typeof(CharController)) as CharController;
		PlayerInventory inv = safariGirl.GetComponent(typeof(PlayerInventory)) as PlayerInventory;
		
		foreach(SelectableObject so in globalItems) {
			inv.AddItemOnLoad(so);
		}
	}
	
    // any other methods you need
	public void AddToGlobalInventory(SelectableObject item) {
		globalItems.Add(item);
	}
}

