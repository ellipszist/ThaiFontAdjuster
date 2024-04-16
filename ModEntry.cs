using HarmonyLib;
using StardewModdingAPI;

internal class ModEntry : Mod
{
    public static bool IsAndroid => Constants.TargetPlatform == GamePlatform.Android;
    public override void Entry(IModHelper helper)
    {
        var harmony = new Harmony(ModManifest.UniqueID);
        int aa = 0;
        aa++;
        aa += 3;

        if (IsAndroid)
        {
            ThaiFontFixAndroid.Init(harmony);
        }
        else
        {
            ThaiFontFixPC.Init(harmony);
        }
    }
}
