using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class EditorStartup
{
    static EditorStartup()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene("Assets/Scenes/MainMenu.unity");
        }
    }
}