using StardewModdingAPI;

namespace StardewValleyThai
{
    internal class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            ThaiFontFix.Init();
        }
    }
}
