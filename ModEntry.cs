using StardewModdingAPI;

internal class ModEntry : Mod
{
    public static bool IsAndroid => Constants.TargetPlatform == GamePlatform.Android;
    public override void Entry(IModHelper helper)
    {
        if (IsAndroid)
        {
            ThaiFontFixAndroid.Init();
        }
        else
        {
            ThaiFontFixPC.Init();
        }
    }
}
