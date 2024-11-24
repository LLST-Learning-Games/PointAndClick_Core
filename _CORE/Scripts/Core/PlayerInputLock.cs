using System.Collections.Generic;

public class PlayerInputLock
{
    private static HashSet<string> _uiStack = new();

    public static void RegisterLock(string uiId) => _uiStack.Add(uiId);
    public static void ClearLock(string uiId) => _uiStack.Remove(uiId);

    public static bool IsPlayerInputLocked => _uiStack.Count > 0;
}
