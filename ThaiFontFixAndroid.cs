using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.BellsAndWhistles;
using System.Reflection;
using System.Text;
using static StardewValley.BellsAndWhistles.SpriteText;

public static class ThaiFontFixAndroid
{
    //get method by name in current  type & check only static | private
    public static MethodInfo GetMethod(string newMethod)
    {
        var methods = typeof(ThaiFontFixAndroid).GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
        return methods.SingleOrDefault(m => m.Name == newMethod);
    }

    public static void Init()
    {
        var harmony = new Harmony(typeof(ThaiFontFixAndroid).FullName);
        var DrawStringName = nameof(SpriteBatch.DrawString);
        var spriteBatch = typeof(SpriteBatch);
        //XNA Microsoft
        //SpriteBatch
        {
            var method = spriteBatch.GetMethod(DrawStringName,
                [typeof(SpriteFont), typeof(string), typeof(Vector2), typeof(Color)]);
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString1))));
        }
        {
            var method = spriteBatch.GetMethod(DrawStringName,
                [typeof(SpriteFont), typeof(string), typeof(Vector2), typeof(Color), typeof(float),
                typeof(Vector2), typeof(Vector2), typeof(SpriteEffects), typeof(float)]);
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString2))));
        }
        {
            Type[] allParam = [typeof(SpriteFont), typeof(StringBuilder), typeof(Vector2), typeof(Color)];
            var method = spriteBatch.GetMethod(DrawStringName, allParam);
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString3))));
        }

        //StardewValley
        //SpriteText
        {
            var method = typeof(SpriteText).GetMethod(nameof(SpriteText.drawString));
            var methodReplacement = GetMethod(nameof(PrefixSpriteTextDrawString));
            harmony.Patch(method, new(methodReplacement));
        }

    }

    static void PrefixDrawString1(SpriteFont spriteFont, ref string text, Vector2 position, Color color)
    {
        text = "DS1:" + FontFixTool.Fix(text);
    }

    static void PrefixDrawString2(SpriteFont spriteFont, ref string text, Vector2 position, Color color,
        float rotation, Vector2 origin, Vector2 scale,
        SpriteEffects effects, float layerDepth)
    {
        text = "DS2:" + FontFixTool.Fix(text);
    }

    static string _lastDrawString3_TextFontFix = null;
    static void PrefixDrawString3(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
    {
        var sbToText = text.ToString();
        //Protect dont apply font fix again
        if (_lastDrawString3_TextFontFix == sbToText)
            return;

        text.Clear();
        var textFontFix = "DS3:" + FontFixTool.Fix(sbToText);
        text.Append(textFontFix);
        _lastDrawString3_TextFontFix = textFontFix;

        //error on android
        //text.Append($"DS3:{FontFixTool.Fix(allText)}");
    }

    static void PrefixSpriteTextDrawString(SpriteBatch b, ref string s, int x, int y, int characterPosition = 999999,
        int width = -1, int height = 999999, float alpha = 1f, float layerDepth = 0.88f,
        bool junimoText = false, int drawBGScroll = -1, string placeHolderScrollWidthText = "",
        int color = -1, ScrollTextAlignment scroll_text_alignment = ScrollTextAlignment.Left)
    {
        s = "DS4:" + FontFixTool.Fix(s); ;
    }
}
