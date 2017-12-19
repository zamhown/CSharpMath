﻿using CSharpMath.FrontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMath.Display;
using System.Drawing;
using TGlyph = System.Char;
using TMathFont = CSharpMath.Display.MathFont<System.Char>;

namespace CSharpMath.Tests.FrontEnd {
  public class TestGlyphBoundsProvider : IGlyphBoundsProvider<MathFont<TGlyph>, TGlyph> {
    private const float WidthPerCharacterPerFontSize = 0.5f; // "m" and "M" get double width.
    private const float AscentPerFontSize = 0.7f;
    private const float DescentPerFontSize = 0.2f; // all constants were chosen to bear some resemblance to a real font.

    private int GetEffectiveLength(TGlyph[] glyphs) {
      string glyphString = new string(glyphs);
      int length = glyphs.Length;
      int extraLength = glyphString.ToLower().ToArray().Count(x => x == 'm');
      int effectiveLength = length + extraLength;
      return effectiveLength;
    }

    public RectangleF GetCombinedBoundingRectForGlyphs(MathFont<char> font, TGlyph[] glyphs) {
      int effectiveLength = GetEffectiveLength(glyphs);
      float width = font.PointSize * effectiveLength * WidthPerCharacterPerFontSize;
      float ascent = font.PointSize * AscentPerFontSize;
      float descent = font.PointSize * DescentPerFontSize;
      //  The y axis is NOT inverted. So our y coordinate is minus the descent, i.e. the rect bottom is the descent below the axis.
      return new RectangleF(0, -descent, width, ascent + descent);
    }

    public RectangleF[] GetBoundingRectsForGlyphs(TMathFont font, TGlyph[] glyphs, int nVariants) {
      RectangleF[] r = new RectangleF[nVariants];
      for (int i = 0; i < glyphs.Length; i++) {
        var glyph = glyphs[i];
        TGlyph[] singleGlyph = { glyph };
        int effectiveLength = GetEffectiveLength(singleGlyph);
        float width = font.PointSize * effectiveLength * WidthPerCharacterPerFontSize;
        float ascent = font.PointSize * AscentPerFontSize;
        float descent = font.PointSize * DescentPerFontSize;
        //  The y axis is NOT inverted. So our y coordinate is minus the descent, i.e. the rect bottom is the descent below the axis.
        r[i] = new RectangleF(0, -descent, width, ascent + descent);
      }
      return r;
    }

    public float GetAdvancesForGlyphs(MathFont<TGlyph> font, TGlyph[] glyphs) {
      int effectiveLength = GetEffectiveLength(glyphs);
      float width = font.PointSize * effectiveLength * WidthPerCharacterPerFontSize;
      return width;
    }
  }
}
