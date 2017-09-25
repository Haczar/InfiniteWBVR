//
// Create Texture v1.0 by mgear @ UnityCoder.com
//
// Simple tool for creating plain texture assets
// email: support@unitycoder.com
// url: http://unitycoder.com
//

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

namespace unitycodercom
{

	public class CreateTexture : EditorWindow   
	{
		// texture size
	    public static int width = 64;
	    public static int height = 64;

		// color
		public static Color fillColor = Color.white;
		private static string[]  texTemplates= new [] {"Color Presets","White", "Black", "Clear:Opacity 0%", "Red", "Green","Blue","Yellow","Cyan","Magenta","Gray", "Brown"};

		// filltypes
		private string[]  fillTypes= new [] {"Solid","Gradient"};
		private int fillType=0;

		// gradients
		private string[]  gradientTypes= new [] {"Vertical","Horizontal"};
		private int gradientType=0;
		private Color gradientColorStart=Color.white;
		private Color gradientColorEnd=Color.clear;

		// TODO: [x] roundedEdges, rounding size, outline+outline width, texture import settings: [x] read enabled, sprite...


		// base filename (.png is added to it)
	    public static string textureName = "NewTexture1";
		
		// private vars
		private const string appName = "Create Texture";

		private string[]  texFormats= new [] {"Alpha8","RGB24","ARGB32","RGBA32"};
		private string[]  texFormatInfo= new [] {"Alpha-only texture format","A color texture format","Color with an alpha channel texture format","Color with an alpha channel texture format"};
		private TextureFormat[] texFormat = new [] {TextureFormat.Alpha8,TextureFormat.RGB24,TextureFormat.ARGB32,TextureFormat.RGBA32};
		private int textureIndex = 0;
		private static int texFormatIndex = 2;
		
		private static bool autoIncrement = false;
	    
		[MenuItem ("Assets/Create/"+appName+"..")]
	    static void Init () 
		{
	        CreateTexture window = (CreateTexture)EditorWindow.GetWindow (typeof(CreateTexture));
			window.titleContent = new GUIContent(appName);
			window.minSize = new Vector2(300,320);
	    }


	    
		// main loop
		void OnGUI () 
		{
			DrawResetButton();
			DrawNameGUI();
			DrawWidthAndHeightGUI();
			DrawFillTypeGUI();
			DrawColorGUI();
			DrawFormatGUI();
			
			if(GUILayout.Button (new GUIContent ("Create Texture", "Create Texture Now"), GUILayout.Height(40))) 
			{
				CreateTextureNow();
	        }
			
			autoIncrement = GUILayout.Toggle(autoIncrement, "Auto-increment counter");
			
		}



		// GUI PARTS

		void DrawResetButton()
		{
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			GUILayout.Label ("Texture Settings", EditorStyles.boldLabel);
			if(GUILayout.Button (new GUIContent ("[Reset]", "Reset settings"), GUILayout.Width(62))) 
			{
				// reset values to default
				textureName = "NewTexture1";
				width = 64;
				height= 64;
				fillColor = Color.white;
				gradientColorStart = Color.white;
				gradientColorEnd = Color.clear;
				fillType = 0;
				gradientType = 0;
				texFormatIndex = 2;
				textureIndex = 0;
				autoIncrement = false;
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space();
		}

		void DrawFillTypeGUI()
		{
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			GUILayout.Label ("Fill Type : ");
			fillType = EditorGUILayout.Popup (fillType, fillTypes);
			switch (fillType) 
			{
			case 0:	// solid
				break;
			case 1: // gradient
				break;
			default: break;
			}

			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();

		}

		void DrawColorGUI()
		{

			switch (fillType)
			{
				case 0: DrawSolidColorGUI(); break;
				case 1: DrawGradientColorGUI(); break;
				default: break;
			}

		}

		void DrawSolidColorGUI()
		{
			// TODO: more colors http://en.wikipedia.org/wiki/Web_colors
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			fillColor = EditorGUILayout.ColorField ("Texture Color : ", fillColor);
			textureIndex = EditorGUILayout.Popup (textureIndex, texTemplates);
			switch (textureIndex) 
			{
			case 0:	// none
				break;
			case 1:	// white
				fillColor = Color.white;
				break;
			case 2:	// black
				fillColor = Color.black;
				break;
			case 3:	// black transparent
				fillColor = new Color (0, 0, 0, 0);
				break;
			case 4:	// red
				fillColor = Color.red;
				break;
			case 5:	// green
				fillColor = Color.green;
				break;
			case 6:	// blue
				fillColor = Color.blue;
				break;
			case 7:	// yellow
				fillColor = Color.yellow;
				break;
			case 8:	// cyan
				fillColor = Color.cyan;
				break;
			case 9:	// magenta
				fillColor = Color.magenta;
				break;
			case 10: // gray
				fillColor = Color.gray;
				break;
			case 11: // brown (SaddleBrown)
				fillColor = new Color32(139,69,19,255);
				break;
			default:
				Debug.LogError ("CreateTexturePlugin: Unknown textureIndex returned : " + textureIndex);
				break;
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();

		}

		void DrawGradientColorGUI()
		{
			// TODO: basic gradients: vertical (top, bottom colors), horizontal
			GUILayout.BeginVertical ("", GUIStyle.none);

			GUILayout.BeginHorizontal ("", GUIStyle.none);
			GUILayout.Label ("Gradient : ");
			gradientType = EditorGUILayout.Popup (gradientType, gradientTypes);
			switch (gradientType) 
			{
			case 0:	// vertical
				break;
			case 1: // horizontal
				break;
			default: break;
			}
			
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();

			gradientColorStart = EditorGUILayout.ColorField ("From:", gradientColorStart);
			gradientColorEnd = EditorGUILayout.ColorField ("To:", gradientColorEnd);
			EditorGUILayout.EndVertical();
			EditorGUILayout.Space ();

		}

		void DrawNoiseColorGUI()
		{
			// TODO: random noise
		}


		void DrawNameGUI ()
		{
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			textureName = EditorGUILayout.TextField ("Name :", textureName);
			if (GUILayout.Button (new GUIContent ("+", "Increment counter"), GUILayout.Width (20))) 
			{
				textureName = incrementNameCounter (textureName);
			}
			EditorGUILayout.EndHorizontal ();
		}

		static void DrawWidthAndHeightGUI ()
		{
			// ** Width GUI **
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			width = Mathf.Clamp (EditorGUILayout.IntField ("Width :", width), 1, 4096);
			if (GUILayout.Button (new GUIContent ("<", "Previous Power of Two"), GUILayout.Width (20))) {
				width = Mathf.NextPowerOfTwo (width) / 2;
			}
			if (GUILayout.Button (new GUIContent ("PoT", "Nearest Power of Two"), GUILayout.Width (40))) {
				width = Mathf.ClosestPowerOfTwo (width);
			}
			if (GUILayout.Button (new GUIContent (">", "Next Power of Two"), GUILayout.Width (20))) {
				width = Mathf.NextPowerOfTwo (width + 1);
			}
			EditorGUILayout.EndHorizontal ();
			// ** Height GUI ** 
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			height = Mathf.Clamp (EditorGUILayout.IntField ("Height :", height), 1, 4096);
			if (GUILayout.Button (new GUIContent ("<", "Previous Power of Two"), GUILayout.Width (20))) {
				height = Mathf.NextPowerOfTwo (height) / 2;
			}
			if (GUILayout.Button (new GUIContent ("PoT", "Nearest Power of Two"), GUILayout.Width (40))) {
				height = Mathf.ClosestPowerOfTwo (height);
			}
			if (GUILayout.Button (new GUIContent (">", "Next Power of Two"), GUILayout.Width (20))) {
				height = Mathf.NextPowerOfTwo (height + 1);
			}
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.Space ();

		}

		void DrawFormatGUI ()
		{
			// ** Format GUI **
			EditorGUILayout.Space ();
			GUILayout.BeginHorizontal ("", GUIStyle.none);
			EditorGUILayout.LabelField (new GUIContent ("Format : ", "Texture format"), GUILayout.Width (65));
			texFormatIndex = EditorGUILayout.Popup (texFormatIndex, texFormats);
			EditorGUILayout.EndHorizontal ();
			EditorGUILayout.LabelField ("\"" + texFormatInfo [texFormatIndex] + "\"", EditorStyles.miniLabel);
			EditorGUILayout.Space();
		}

		void CreateTextureNow()
		{
			// create texture
			Texture2D tex = new Texture2D (width, height, texFormat [texFormatIndex], false);
			// progress bar
			float progressStatus = 0.0f;
			float progressStep = 1.0f / height;
			EditorUtility.DisplayCancelableProgressBar ("Creating texture..", "Initializing", progressStatus);



			// fill texture with selected color
			for (int y = 0; y < height; y++) 
			{
				for (int x = 0; x < width; x++) 
				{
					Color c = fillColor;

					switch (fillType)
					{
					case 0: // solid
						break;
					case 1: // gradient

						if (gradientType==0) // vertical
						{
							float t = 1-y/(float)height;
							c = Color.Lerp(gradientColorStart,gradientColorEnd,t);
						}
						else if (gradientType==1) // horizontal
						{
							float t = x/(float)width;
							c = Color.Lerp(gradientColorStart,gradientColorEnd,t);
						}

						break;
					}

					tex.SetPixel (x, y, c);
				}
				progressStatus += progressStep;
				if (EditorUtility.DisplayCancelableProgressBar ("Creating texture..", "Painting..", progressStatus)) {
					Debug.Log ("CreateTexturePlugin: Canceled by the user");
					UnityEngine.Object.DestroyImmediate (tex);
					EditorUtility.ClearProgressBar ();
					return;
				}
			}

			tex.Apply();
			if (EditorUtility.DisplayCancelableProgressBar ("Creating texture..", "Saving file..", progressStatus)) 
			{
				Debug.Log ("CreateTexturePlugin: Canceled by the user");
				UnityEngine.Object.DestroyImmediate (tex);
				EditorUtility.ClearProgressBar ();
				return;
			}
			// save file
			if (textureName.Length < 1)	textureName = "NewTexture";
			var bytes = tex.EncodeToPNG();
			File.WriteAllBytes (Application.dataPath + "/" + textureName + ".png", bytes);

			EditorUtility.ClearProgressBar ();
			AssetDatabase.Refresh (ImportAssetOptions.ForceUpdate);
			UnityEngine.Object.DestroyImmediate (tex);

			if (autoIncrement)	textureName = incrementNameCounter (textureName);
		}

		// --- helper functions ---
		
		// finds number value from string and increments it by 1
		string incrementNameCounter(string currentName)
		{
			string nameCounter = Regex.Match(currentName, @"\d+").Value;
			if (nameCounter == "") nameCounter = "0";
			return currentName.Replace(nameCounter.ToString(),"") + (int.Parse(nameCounter)+1).ToString();
		}



	} // class

} // namespace