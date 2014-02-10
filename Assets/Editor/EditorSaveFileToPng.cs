// Opens a file selection dialog for a PNG file and saves a selected texture to the file.
using System.IO;
using UnityEditor;
using UnityEngine;

class EditorSaveFileToPng : MonoBehaviour {
	[MenuItem ("Assets/Save Texture to file")]
	static void Apply () {
	
		Texture2D texture = (Camera.main.GetComponent(typeof(SpriteSheetAnimator)) as SpriteSheetAnimator).packedAnimationSheet;
		if (texture == null)
		{
			EditorUtility.DisplayDialog("Select Texture", "You Must Select a Texture first!", "Ok");
			return;
		}
	
		var path = EditorUtility.SaveFilePanel("Save texture as PNG", "Assets/Resources", (Camera.main.GetComponent(typeof(SpriteSheetAnimator)) as SpriteSheetAnimator).packedAnimationSheetName + ".png", "png");
		if (path.Length != 0)
		{
			// Convert the texture to a format compatible with EncodeToPNG
			if ( texture.format != TextureFormat.ARGB32 && texture.format != TextureFormat.RGB24 ) {
				Texture2D newTexture = new Texture2D(texture.width, texture.height);
				newTexture.SetPixels(texture.GetPixels(0),0);
				texture = newTexture;
			}
			var pngData = texture.EncodeToPNG();
			if (pngData != null) {
				File.WriteAllBytes(path, pngData);
			}
		}
	}
}