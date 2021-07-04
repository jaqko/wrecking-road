using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;

public class DeezNuts : EditorWindow
{
    [MenuItem("Example/Load Textures To Folder")]
    public static void Apply()
    {
        string path = EditorUtility.OpenFolderPanel("select files", "mods", "mods");
        string[] files = Directory.GetFiles(path);
        foreach (string file in files)
            if (file.EndsWith(".cs"))
                File.Copy(file, EditorSceneManager.sceneCount.ToString());
    }
}
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        SceneManager.LoadScene("SelectCar");
        SceneManager.UnloadSceneAsync("MainMenu");
    }
    public void Mods()
    {
        DeezNuts.Apply();
    }

}
