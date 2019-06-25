using System;
using System.Drawing;
using System.Collections.Generic;

namespace SharpSnake.Display
{
    /// <summary>
    /// Represents a color palette that holds available drawing colors.
    /// </summary>
    public class Palette
    {
        private readonly Color[] Colors;

        private static readonly Dictionary<PaletteId, Palette> Palettes;

        /// <summary>
        /// Initialize predefined palettes.
        /// </summary>
        static Palette()
        {
            Palettes = new Dictionary<PaletteId, Palette>();

            Palettes.Add(PaletteId.Default, new Palette(new Color[] {
                Color.FromArgb(22, 26, 27),
                Color.FromArgb(236, 240, 241),
                Color.FromArgb(230, 126, 34),
                Color.FromArgb(149, 165, 166),
                Color.FromArgb(46, 204, 113),
                Color.FromArgb(231, 76, 60),
                Color.FromArgb(64, 58, 48),
                Color.FromArgb(130, 90, 44)
            }));

            Palettes.Add(PaletteId.Monochrome, new Palette(new Color[] {
                Color.FromArgb(22, 22, 22),
                Color.FromArgb(240, 240, 240),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(160, 160, 160),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(50, 50, 50),
                Color.FromArgb(120, 120, 120)
            }));
        }

        /// <summary>
        /// Initialize a new palette with specified colors.
        /// Please note that the number and order of colors should match that in the <see cref="PaletteColor"/> enum.
        /// </summary>
        /// <param name="colors">Array of colors</param>
        public Palette(Color[] colors)
        {
            Colors = colors;
        }

        /// <summary>
        /// Get a color from the palette.
        /// </summary>
        /// <param name="color">The color's ID</param>
        /// <returns>The color</returns>
        public Color GetColor(PaletteColor color)
        {
            return Colors[Convert.ToInt32(color)];
        }

        /// <summary>
        /// Get a palette by its ID.
        /// </summary>
        /// <param name="paletteId">Palette ID</param>
        /// <returns>The requested palette</returns>
        public static Palette GetPalette(PaletteId paletteId)
        {
            return Palettes[paletteId];
        }
    }
}
