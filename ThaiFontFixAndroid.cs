using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.BellsAndWhistles;
using System.Reflection;
using System.Text;
using static StardewValley.BellsAndWhistles.SpriteText;

[HarmonyPatch]
internal static class ThaiFontFixAndroid
{
    public static void Init(Harmony harmony)
    {
        //Fix Bug!! PatchAll needed CallStackFrame >= 2
        //impact on build Release mode
        int fixbugLmao = 0;
        harmony.PatchAll();
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SpriteBatch), nameof(SpriteBatch.DrawString), [typeof(SpriteFont), typeof(string), typeof(Vector2), typeof(Color)])]
    static void DrawString(SpriteFont spriteFont, ref string text, Vector2 position, Color color)
    {
        text = FontFixTool.Fix(text);
#if DEBUG
        text = "DS1=" + text;
#endif
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SpriteBatch), nameof(SpriteBatch.DrawString),
        [typeof(SpriteFont), typeof(string), typeof(Vector2), typeof(Color), typeof(float), typeof(Vector2), typeof(Vector2),
        typeof(SpriteEffects), typeof(float)])]
    static void DrawString(SpriteFont spriteFont, ref string text, Vector2 position, Color color,
        float rotation, Vector2 origin, Vector2 scale,
        SpriteEffects effects, float layerDepth)
    {
        text = FontFixTool.Fix(text);
#if DEBUG
        text = "DS2=" + text;
#endif
    }

    static string _lastDrawString3_TextFontFix = "";
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SpriteBatch), nameof(SpriteBatch.DrawString), [typeof(SpriteFont), typeof(StringBuilder), typeof(Vector2), typeof(Color)])]
    static void DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
    {
        var allText = text.ToString();
        //Protect dont apply font fix & fotmat again!!
        if (allText == _lastDrawString3_TextFontFix)
            return;

        var textFontFix = FontFixTool.Fix(allText);
#if DEBUG
        textFontFix = "DS3=" + textFontFix;
#endif

        text.Clear();
        text.Append(textFontFix);
        _lastDrawString3_TextFontFix = textFontFix;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(SpriteText), nameof(SpriteText.drawString))]
    static void SpriteText_DrawString(SpriteBatch b, ref string s, int x, int y, int characterPosition = 999999,
        int width = -1, int height = 999999, float alpha = 1f, float layerDepth = 0.88f,
        bool junimoText = false, int drawBGScroll = -1, string placeHolderScrollWidthText = "",
        Color? color = null, ScrollTextAlignment scroll_text_alignment = ScrollTextAlignment.Left)
    {
        s = FontFixTool.Fix(s);
#if DEBUG
        s = "DS4=" + s;
#endif
    }



    static string _lastDrawString5_TextFontFix;
    [HarmonyPrefix]
    [HarmonyPatch(typeof(SpriteBatch))]
    [HarmonyPatch(nameof(SpriteBatch.DrawString))]
    [HarmonyPatch([typeof(SpriteFont), typeof(StringBuilder), typeof(Vector2), typeof(Color),
            typeof(float), typeof(Vector2), typeof(Vector2), typeof(SpriteEffects), typeof(float)])]
    static void SprtieBatch_DrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color,
            float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        var allText = text.ToString();
        //Protect dont apply font fix & fotmat again!!
        if (allText == _lastDrawString5_TextFontFix)
            return;

        var textFontFix = FontFixTool.Fix(allText);
#if DEBUG
        textFontFix = "DS5=" + textFontFix;
#endif

        text.Clear();
        text.Append(textFontFix);
        _lastDrawString5_TextFontFix = textFontFix;
    }
}
