
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameSceneManager : GameSystem
{
    public bool IsMinigameLoaded { get; private set; }

    public override void Initialize()
    {
        //..
    }

    public void RequestSceneLoad(int index)
    {
        if (IsMinigameLoaded)
        {
            return;
        }

        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
        IsMinigameLoaded = true;

        PlayerInputLock.RegisterLock("minigame_" + index);
    }

    public void RequestSceneUnload(int index)
    {
        SceneManager.UnloadSceneAsync(index);
        IsMinigameLoaded = false;
        PlayerInputLock.ClearLock("minigame_" + index);
    }
}
