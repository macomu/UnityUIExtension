using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>MultipleSpriteのプレハブを作成するエディタ拡張.</summary>
public class MultipleSpriteDataCreator : EditorWindow {
	
	/// <summary>ウィンドウ表示.</summary>
	[MenuItem("UI/MultipleSpriteDataCreator")]
	public static void ShowWindow () {
		var window = EditorWindow.GetWindow<MultipleSpriteDataCreator>();
		window.Show();
	}
	
	private Vector2 m_ScrollPosition = Vector2.zero; // スクロール位置.
	private Sprite m_Sprite;                         // 素となるプロジェクト内のSpriteオブジェクト.
	private Object[] m_SpriteObjects;                // m_Spriteに入っているSprite.
	private string[] m_SpriteNameLabels;             // 入っているSpriteの名前表示用文字列.
	private string m_SpritePath;                     // Spriteのパス.
	
	// GUI.
	private void OnGUI () {
		m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
		EditorGUILayout.BeginVertical();
		m_Sprite = EditorGUILayout.ObjectField("SpriteData", m_Sprite, typeof(Sprite), false) as Sprite;
		if (m_Sprite != null) {
			if (m_SpriteObjects == null) {
				// m_Sprite内に入っているSpriteを取得.
				m_SpritePath = AssetDatabase.GetAssetPath(m_Sprite);
				if (string.IsNullOrEmpty(m_SpritePath) == false) {
					m_SpriteObjects = AssetDatabase.LoadAllAssetRepresentationsAtPath(m_SpritePath);
					m_SpriteNameLabels = new string[m_SpriteObjects.Length];
					for (int index = 0; index < m_SpriteObjects.Length; ++ index) {
						m_SpriteNameLabels[index] = string.Format("{0, 3}", index + 1) + ":" + m_SpriteObjects[index].name;
					}
				}
			}
			
			if (GUILayout.Button("Create Prefab") == true) {
				CreateMultipleSpritePrefab();
			}
			
			if (m_SpriteObjects != null) {
				// m_Sprite内のSpriteを取得できたら表示.
				for(int index = 0; index < m_SpriteObjects.Length; ++index) {
					EditorGUILayout.LabelField(m_SpriteNameLabels[index]);
				}
			}
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}
	
	/// <summary>m_Spriteに入っているSpriteからMultipleSpriteのプレハブを作成.</summary>
	private void CreateMultipleSpritePrefab () {
		
		// Sprite配列の作成.
		var length = m_SpriteObjects.Length;
		var sprites = new Sprite[length];
		
		for (int index = 0; index < length; ++index) {
			sprites[index] = m_SpriteObjects[index] as Sprite;
		}
		
		// MultipleSpriteのプレハブ用オブジェクトを作成.
		var prefabObject = EditorUtility.CreateGameObjectWithHideFlags(m_Sprite.name, HideFlags.HideInHierarchy, typeof(MultipleSprite));
		// MultipleSpriteの設定.
		var multipleSprite = prefabObject.GetComponent<MultipleSprite>();
		multipleSprite.SetSprites(sprites);
		
		// 拡張子以外のパスを取得.
		string pathString = m_SpritePath.Substring(0, m_SpritePath.LastIndexOf('.'));
		
		// プレハブの作成をプレハブ用オブジェクトの破棄.
		PrefabUtility.CreatePrefab(pathString + ".prefab", prefabObject);
		Editor.DestroyImmediate(prefabObject);
	}
}
