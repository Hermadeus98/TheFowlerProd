using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace QRCode.Extensions
{
    public static class ColorExtension
    {
        public static Color GetRandomColor(this Color color, bool randomAlpha = false)
        {
            return new Color(
                r: Random.Range(0f, 1f),
                g: Random.Range(0f, 1f),
                b: Random.Range(0f, 1f),
                a: randomAlpha? Random.Range(0f, 1f) : 1f
                );
        }
        
        /// <summary>
        /// Get a color from the french pallet from https://flatuicolors.com/
        /// </summary>
        /// <param name="color"></param>
        /// <param name="colorName"></param>
        /// <returns></returns>
        public static Color GetColorFromFrenchPallet(this Color color, FrenchPallet colorName)
        {
            return ColorPallets.GetFrenchPalletColor(colorName);
        }

        /// <summary>
        /// Return Htlm code from a RGB color.
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ToHtlmColor(this Color color)
        {
            return "#" + ColorUtility.ToHtmlStringRGBA(color);
        }

        /// <summary>
        /// Return a RGB color from a htlm code;
        /// </summary>
        /// <param name="color"></param>
        /// <param name="htlmCode"></param>
        /// <returns></returns>
        public static Color ToColor(this Color color, string htlmCode)
        {
            ColorUtility.TryParseHtmlString(htlmCode, out var c);
            return c;
        }
    }

    public static class ColorPallets
    {
        /// <summary>
        /// Colors from https://flatuicolors.com/
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetFrenchPalletColor(FrenchPallet color) => color switch
        {
            FrenchPallet.TURQUOISE => new Color().ToColor("#1abc9c"),
            FrenchPallet.GREEN_SEA => new Color().ToColor("#16a085"),
            FrenchPallet.EMERALD => new Color().ToColor("#2ecc71"),
            FrenchPallet.NEPHRITIS => new Color().ToColor("#27ae60"),
            FrenchPallet.PETER_RIVER => new Color().ToColor("#3498db"),
            FrenchPallet.BELIZE_HOLE => new Color().ToColor("#2980b9"),
            FrenchPallet.AMETHYST => new Color().ToColor("#9b59b6"),
            FrenchPallet.WISTERIA => new Color().ToColor("#8e44ad"),
            FrenchPallet.WET_ASPHALT => new Color().ToColor("#34495e"),
            FrenchPallet.MIDNIGHT_BLUE => new Color().ToColor("#2c3e50"),
            FrenchPallet.SUN_FLOWER => new Color().ToColor("#f1c40f"),
            FrenchPallet.ORANGE => new Color().ToColor("#f39c12"),
            FrenchPallet.CARROT => new Color().ToColor("#e67e22"),
            FrenchPallet.PUMPKIN => new Color().ToColor("#d35400"),
            FrenchPallet.ALIZARIN => new Color().ToColor("#e74c3c"),
            FrenchPallet.POMEGRANATE => new Color().ToColor("#c0392b"),
            FrenchPallet.CLOUDS => new Color().ToColor("#ecf0f1"),
            FrenchPallet.SILDER => new Color().ToColor("#bdc3c7"),
            FrenchPallet.CONCRETE => new Color().ToColor("#95a5a6"),
            FrenchPallet.ASBESTOS => new Color().ToColor("#7f8c8d"),
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
    }
    public enum FrenchPallet
    {
        TURQUOISE,
        GREEN_SEA,
        EMERALD,
        NEPHRITIS,
        PETER_RIVER,
        BELIZE_HOLE,
        AMETHYST,
        WISTERIA,
        WET_ASPHALT,
        MIDNIGHT_BLUE,
        SUN_FLOWER,
        ORANGE,
        CARROT,
        PUMPKIN,
        ALIZARIN,
        POMEGRANATE,
        CLOUDS,
        SILDER,
        CONCRETE,
        ASBESTOS
    }
}

