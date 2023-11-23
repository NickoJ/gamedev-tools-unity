using System.IO;
using UnityEditor;
using UnityEngine;
using static NickoJ.GameDevTools.GameDevTools;

namespace NickoJ.GameDevTools.PixelArt
{
    public static class PixelArtDevTools
    {
        private const string PixelArtFolder = "PixelArt";

        private const string SpriteMarginScript = "SpriteMarginExecutor.py";
        
        public const string PixelArtRootMenu = AssetsRootMenu + "/Pixel art";

        public static void ExecuteSpriteMargin(Texture2D texture)
        {
            string texturePath = Path.Combine(Application.dataPath, "../", AssetDatabase.GetAssetPath(Selection.activeObject)); 

            ExecuteScript(PixelArtFolder, SpriteMarginScript, "fix_atlas_unity", texturePath);
            AssetDatabase.Refresh();
        }
    }
}