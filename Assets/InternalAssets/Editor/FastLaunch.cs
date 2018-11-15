using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class FastLaunch : MonoBehaviour
{
    [MenuItem("SoulEaterShortcuts/LaunchSplashScene %#t")]
    static void LaunchSplashScene()
    {
        EditorSceneManager.OpenScene("Assets/InternalAssets/Scenes/SplashScreenScene.unity");
        EditorApplication.ExecuteMenuItem("Edit/Play");
    }

    [MenuItem("SoulEaterShortcuts/OpenMainLevelScene %#i")]
    static void GetBackToInControlTestScene()
    {
        EditorSceneManager.OpenScene("Assets/InternalAssets/Scenes/Level1_Blender.unity");
    }
}
