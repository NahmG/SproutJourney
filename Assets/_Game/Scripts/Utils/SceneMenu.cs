using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneMenu : MonoBehaviour
{
    const string scenePath = "Assets/_Game/Scenes/";

    [MenuItem("Scenes/Game")]
    private static void OpenGameScene()
    {
        OpenScene(scenePath + "Game.unity");
    }

    [MenuItem("Scenes/Editor")]
    private static void OpenEditorScene()
    {
        OpenScene(scenePath + "Editor.unity");
    }

    private static void OpenScene(string path)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            EditorSceneManager.OpenScene(path);
    }
}
