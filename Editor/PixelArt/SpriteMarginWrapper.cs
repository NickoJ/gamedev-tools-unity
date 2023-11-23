using System.IO;
using UnityEditor;
using UnityEngine;

using static NickoJ.GameDevTools.PixelArt.PixelArtDevTools;

namespace NickoJ.GameDevTools.PixelArt
{
    public static class SpriteMarginWrapper
    {
        private const string SpriteMarginWrapperMenu = PixelArtDevTools.PixelArtRootMenu + "/Sprite margin";

        [MenuItem(SpriteMarginWrapperMenu, true)]
        public static bool FixSpriteMarginValidation()
        {
            bool isValid = false;
            
            if (Selection.activeObject is Texture2D texture)
            {
                string path = AssetDatabase.GetAssetPath(texture);

                var importer = (TextureImporter)AssetImporter.GetAtPath(path);

                isValid = importer != null && 
                          importer.textureType is TextureImporterType.Sprite &&
                          importer.spriteImportMode == SpriteImportMode.Multiple;
            }
            
            return isValid;
        }
        
        [MenuItem(SpriteMarginWrapperMenu)]
        public static void FixSpriteMargin()
        {
            ExecuteSpriteMargin(Selection.activeObject as Texture2D);
        }
    }
}