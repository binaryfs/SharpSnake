using System;
using System.Drawing;
using System.Collections.Generic;

namespace SharpSnake.Display
{
    public class Palette
    {
        private readonly Color[] Colors;

        private static readonly Dictionary<PaletteId, Palette> Palettes;

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

        public Palette(Color[] colors)
        {
            Colors = colors;
        }

        public Color GetColor(PaletteColor color)
        {
            return Colors[Convert.ToInt32(color)];
        }

        public static Palette GetPalette(PaletteId paletteId)
        {
            return Palettes[paletteId];
        }
    }
}
