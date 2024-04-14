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
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString))));
        }
        {
            var method = spriteBatch.GetMethod(DrawStringName,
                [typeof(SpriteFont), typeof(string), typeof(Vector2), typeof(Color), typeof(float),
            typeof(Vector2), typeof(Vector2), typeof(SpriteEffects), typeof(float)]);
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString))));
        }
        {
            Type[] allParam = [typeof(SpriteFont), typeof(StringBuilder), typeof(Vector2), typeof(Color)];
            var method = spriteBatch.GetMethod(DrawStringName, allParam);
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawString))));
        }

        //StardewValley
        //Utility
        {
            var method = typeof(StardewValley.Utility).GetMethod("drawMultiLineTextWithShadow");
            harmony.Patch(method, new(GetMethod(nameof(PrefixDrawMultiLineTextWithShadow))));
        }
        //SpriteText
        {
            var method = typeof(SpriteText).GetMethod(nameof(SpriteText.drawString));
            var methodReplacement = GetMethod(nameof(PrefixSpriteTextDrawString));
            harmony.Patch(method, new(methodReplacement));
        }

    }

    static void PrefixDrawString(SpriteFont spriteFont, ref string text, Vector2 position, Color color)
    {
        text = "DS1:" + FontFixTool.Fix(text);
    }

    static void PrefixDrawString(SpriteFont spriteFont, ref string text, Vector2 position, Color color,
        float rotation, Vector2 origin, Vector2 scale,
        SpriteEffects effects, float layerDepth)
    {
        text = "DS2:" + FontFixTool.Fix(text);
    }

    static StringBuilder lastDrawStringBuilder = null;
    static void PrefixDrawString(SpriteFont spriteFont, StringBuilder text, Vector2 position, Color color)
    {
        //cache string
        if (lastDrawStringBuilder != text)
        {
            var allText = text.ToString();
            text.Clear();
            text.Append($"DS3:{FontFixTool.Fix(allText)}");
            lastDrawStringBuilder = text;
        }
    }
    static void PrefixDrawMultiLineTextWithShadow(SpriteBatch b, ref string text, SpriteFont font, Vector2 position,
        int width, int height, Color col, bool centreY = true, bool actuallyDrawIt = true,
        bool drawShadows = true, bool centerX = true, bool bold = false,
        bool close = false, float scale = 1f)
    {
        text = "DMLWSD:" + FontFixTool.Fix(text);
    }

    static void PrefixSpriteTextDrawString(SpriteBatch b, ref string s, int x, int y, int characterPosition = 999999,
        int width = -1, int height = 999999, float alpha = 1f, float layerDepth = 0.88f,
        bool junimoText = false, int drawBGScroll = -1, string placeHolderScrollWidthText = "",
        int color = -1, ScrollTextAlignment scroll_text_alignment = ScrollTextAlignment.Left)
    {
        s = "STDS:" + FontFixTool.Fix(s); ;
    }
}
