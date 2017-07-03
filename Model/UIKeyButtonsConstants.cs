// Decompiled with JetBrains decompiler
// Type: UIKeyButtonsConstants
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIKeyButtonsConstants
{
  public static readonly string[] KeyCodeEqualsLoc = new string[10]
  {
    "=",
    "=",
    "+",
    "+",
    "+",
    "=",
    "=",
    "ó",
    "=",
    "="
  };
  public static readonly string[] KeyCodeLeftBracketLoc = new string[10]
  {
    "[",
    ")",
    "'",
    "ß",
    "'",
    "´´",
    "[",
    "ő",
    "[",
    "х"
  };
  public static readonly string[] KeyCodeRightBracketLoc = new string[10]
  {
    "]",
    "¨",
    "ì",
    "´´",
    "¡",
    "[",
    "]",
    "ú",
    "]",
    "ъ"
  };
  public static readonly string[] KeyCodeSemicolonLoc = new string[10]
  {
    ";",
    "$",
    "è",
    "ü",
    "``",
    "ç",
    ";",
    "é",
    ";",
    "ж"
  };
  public static readonly string[] KeyCodeBackQuoteLoc = new string[10]
  {
    "'",
    "ù",
    "ò",
    "ö",
    "ñ",
    "'",
    "``",
    "ö",
    "`",
    "ё"
  };
  public static readonly string[] KeyCodeQuoteLoc = new string[10]
  {
    "#",
    "\x00B2",
    "à",
    "ä",
    "´´",
    "~~",
    "'",
    "á",
    "'",
    "э"
  };
  public static readonly string[] KeyCodeCommaLoc = new string[10]
  {
    ",",
    ",",
    ",",
    ",",
    ",",
    ",",
    ",",
    ",",
    ",",
    "б"
  };
  public static readonly string[] KeyCodePeriodLoc = new string[10]
  {
    ".",
    ";",
    ".",
    ".",
    ".",
    ".",
    ".",
    ".",
    ".",
    "ю"
  };
  public static readonly string[] KeyCodeSlashLoc = new string[10]
  {
    "/",
    ":",
    "ù",
    "#",
    "ç",
    ";",
    "/",
    "ü",
    "/",
    "."
  };
  public static readonly string[] KeyCodeBackslashLoc = new string[10]
  {
    "\\",
    "<",
    "\\",
    "<",
    "º",
    "\\",
    "\\",
    "í",
    "\\",
    "\\"
  };
  public static readonly string[] AlphaNumberLoc = new string[10]
  {
    "à",
    "&",
    "é",
    "\"",
    "'",
    "(",
    "-",
    "è",
    "_",
    "ç"
  };
  public static readonly KeyCode[] keyCodes = new KeyCode[96]
  {
    KeyCode.A,
    KeyCode.B,
    KeyCode.C,
    KeyCode.D,
    KeyCode.E,
    KeyCode.F,
    KeyCode.G,
    KeyCode.H,
    KeyCode.I,
    KeyCode.J,
    KeyCode.K,
    KeyCode.L,
    KeyCode.M,
    KeyCode.N,
    KeyCode.O,
    KeyCode.P,
    KeyCode.Q,
    KeyCode.R,
    KeyCode.S,
    KeyCode.T,
    KeyCode.U,
    KeyCode.V,
    KeyCode.W,
    KeyCode.X,
    KeyCode.Y,
    KeyCode.Z,
    KeyCode.Alpha0,
    KeyCode.Alpha1,
    KeyCode.Alpha2,
    KeyCode.Alpha3,
    KeyCode.Alpha4,
    KeyCode.Alpha5,
    KeyCode.Alpha6,
    KeyCode.Alpha7,
    KeyCode.Alpha8,
    KeyCode.Alpha9,
    KeyCode.Space,
    KeyCode.CapsLock,
    KeyCode.Return,
    KeyCode.LeftShift,
    KeyCode.RightShift,
    KeyCode.LeftControl,
    KeyCode.RightControl,
    KeyCode.LeftAlt,
    KeyCode.RightAlt,
    KeyCode.Tab,
    KeyCode.F1,
    KeyCode.F2,
    KeyCode.F3,
    KeyCode.F4,
    KeyCode.F5,
    KeyCode.F6,
    KeyCode.F7,
    KeyCode.F8,
    KeyCode.F9,
    KeyCode.F10,
    KeyCode.F11,
    KeyCode.F12,
    KeyCode.Keypad0,
    KeyCode.Keypad1,
    KeyCode.Keypad2,
    KeyCode.Keypad3,
    KeyCode.Keypad4,
    KeyCode.Keypad5,
    KeyCode.Keypad6,
    KeyCode.Keypad7,
    KeyCode.Keypad8,
    KeyCode.Keypad9,
    KeyCode.KeypadEnter,
    KeyCode.KeypadPlus,
    KeyCode.KeypadPeriod,
    KeyCode.KeypadMinus,
    KeyCode.KeypadMultiply,
    KeyCode.KeypadDivide,
    KeyCode.UpArrow,
    KeyCode.DownArrow,
    KeyCode.LeftArrow,
    KeyCode.RightArrow,
    KeyCode.Backspace,
    KeyCode.Minus,
    KeyCode.Equals,
    KeyCode.LeftBracket,
    KeyCode.RightBracket,
    KeyCode.Semicolon,
    KeyCode.BackQuote,
    KeyCode.Quote,
    KeyCode.Comma,
    KeyCode.Period,
    KeyCode.Slash,
    KeyCode.Backslash,
    KeyCode.Insert,
    KeyCode.Home,
    KeyCode.PageUp,
    KeyCode.PageDown,
    KeyCode.Delete,
    KeyCode.End
  };
  public static readonly KeyCode[] keyModifiers = new KeyCode[6]
  {
    KeyCode.LeftShift,
    KeyCode.RightShift,
    KeyCode.LeftControl,
    KeyCode.RightControl,
    KeyCode.LeftAlt,
    KeyCode.RightAlt
  };

  public static string getDisplayKeyCodesText(KeyCode key_code)
  {
    KeyCode keyCode = key_code;
    switch (keyCode)
    {
      case KeyCode.Space:
        return Localisation.LocaliseID("PSG_10004926", (GameObject) null);
      case KeyCode.Quote:
        return UIKeyButtonsConstants.KeyCodeQuoteLoc[Localisation.currentLanguageIndex];
      case KeyCode.Comma:
        return UIKeyButtonsConstants.KeyCodeCommaLoc[Localisation.currentLanguageIndex];
      case KeyCode.Minus:
        return "-";
      case KeyCode.Period:
        return UIKeyButtonsConstants.KeyCodePeriodLoc[Localisation.currentLanguageIndex];
      case KeyCode.Slash:
        return UIKeyButtonsConstants.KeyCodeSlashLoc[Localisation.currentLanguageIndex];
      case KeyCode.Alpha0:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[0];
        return "0";
      case KeyCode.Alpha1:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[1];
        return "1";
      case KeyCode.Alpha2:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[2];
        return "2";
      case KeyCode.Alpha3:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[3];
        return "3";
      case KeyCode.Alpha4:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[4];
        return "4";
      case KeyCode.Alpha5:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[5];
        return "5";
      case KeyCode.Alpha6:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[6];
        return "6";
      case KeyCode.Alpha7:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[7];
        return "7";
      case KeyCode.Alpha8:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[8];
        return "8";
      case KeyCode.Alpha9:
        if (Localisation.IsCurrentLanguageFrench())
          return UIKeyButtonsConstants.AlphaNumberLoc[9];
        return "9";
      case KeyCode.Semicolon:
        return UIKeyButtonsConstants.KeyCodeSemicolonLoc[Localisation.currentLanguageIndex];
      case KeyCode.Equals:
        return UIKeyButtonsConstants.KeyCodeEqualsLoc[Localisation.currentLanguageIndex];
      case KeyCode.LeftBracket:
        return UIKeyButtonsConstants.KeyCodeLeftBracketLoc[Localisation.currentLanguageIndex];
      case KeyCode.Backslash:
        return UIKeyButtonsConstants.KeyCodeBackslashLoc[Localisation.currentLanguageIndex];
      case KeyCode.RightBracket:
        return UIKeyButtonsConstants.KeyCodeRightBracketLoc[Localisation.currentLanguageIndex];
      case KeyCode.BackQuote:
        return UIKeyButtonsConstants.KeyCodeBackQuoteLoc[Localisation.currentLanguageIndex];
      case KeyCode.A:
        return Localisation.LocaliseID("PSG_10010892", (GameObject) null);
      case KeyCode.B:
        return Localisation.LocaliseID("PSG_10010893", (GameObject) null);
      case KeyCode.C:
        return Localisation.LocaliseID("PSG_10010894", (GameObject) null);
      case KeyCode.D:
        return Localisation.LocaliseID("PSG_10010895", (GameObject) null);
      case KeyCode.E:
        return Localisation.LocaliseID("PSG_10010896", (GameObject) null);
      case KeyCode.F:
        return Localisation.LocaliseID("PSG_10010897", (GameObject) null);
      case KeyCode.G:
        return Localisation.LocaliseID("PSG_10010898", (GameObject) null);
      case KeyCode.H:
        return Localisation.LocaliseID("PSG_10010899", (GameObject) null);
      case KeyCode.I:
        return Localisation.LocaliseID("PSG_10010900", (GameObject) null);
      case KeyCode.J:
        return Localisation.LocaliseID("PSG_10010901", (GameObject) null);
      case KeyCode.K:
        return Localisation.LocaliseID("PSG_10010902", (GameObject) null);
      case KeyCode.L:
        return Localisation.LocaliseID("PSG_10010903", (GameObject) null);
      case KeyCode.M:
        return Localisation.LocaliseID("PSG_10010904", (GameObject) null);
      case KeyCode.N:
        return Localisation.LocaliseID("PSG_10010905", (GameObject) null);
      case KeyCode.O:
        return Localisation.LocaliseID("PSG_10010906", (GameObject) null);
      case KeyCode.P:
        return Localisation.LocaliseID("PSG_10010907", (GameObject) null);
      case KeyCode.Q:
        return Localisation.LocaliseID("PSG_10010908", (GameObject) null);
      case KeyCode.R:
        return Localisation.LocaliseID("PSG_10010909", (GameObject) null);
      case KeyCode.S:
        return Localisation.LocaliseID("PSG_10010910", (GameObject) null);
      case KeyCode.T:
        return Localisation.LocaliseID("PSG_10010911", (GameObject) null);
      case KeyCode.U:
        return Localisation.LocaliseID("PSG_10010912", (GameObject) null);
      case KeyCode.V:
        return Localisation.LocaliseID("PSG_10010913", (GameObject) null);
      case KeyCode.W:
        return Localisation.LocaliseID("PSG_10010914", (GameObject) null);
      case KeyCode.X:
        return Localisation.LocaliseID("PSG_10010915", (GameObject) null);
      case KeyCode.Y:
        return Localisation.LocaliseID("PSG_10010916", (GameObject) null);
      case KeyCode.Z:
        return Localisation.LocaliseID("PSG_10010917", (GameObject) null);
      case KeyCode.Delete:
        return Localisation.LocaliseID("PSG_10011227", (GameObject) null);
      default:
        switch (keyCode - 256)
        {
          case KeyCode.None:
            return Localisation.LocaliseID("PSG_10010695", (GameObject) null);
          case (KeyCode) 1:
            return Localisation.LocaliseID("PSG_10010696", (GameObject) null);
          case (KeyCode) 2:
            return Localisation.LocaliseID("PSG_10010697", (GameObject) null);
          case (KeyCode) 3:
            return Localisation.LocaliseID("PSG_10010698", (GameObject) null);
          case (KeyCode) 4:
            return Localisation.LocaliseID("PSG_10010699", (GameObject) null);
          case (KeyCode) 5:
            return Localisation.LocaliseID("PSG_10010700", (GameObject) null);
          case (KeyCode) 6:
            return Localisation.LocaliseID("PSG_10010701", (GameObject) null);
          case (KeyCode) 7:
            return Localisation.LocaliseID("PSG_10010702", (GameObject) null);
          case KeyCode.Backspace:
            return Localisation.LocaliseID("PSG_10010703", (GameObject) null);
          case KeyCode.Tab:
            return Localisation.LocaliseID("PSG_10010704", (GameObject) null);
          case (KeyCode) 10:
            return Localisation.LocaliseID("PSG_10010707", (GameObject) null);
          case (KeyCode) 11:
            return Localisation.LocaliseID("PSG_10010710", (GameObject) null);
          case KeyCode.Clear:
            return Localisation.LocaliseID("PSG_10010709", (GameObject) null);
          case KeyCode.Return:
            return Localisation.LocaliseID("PSG_10010708", (GameObject) null);
          case (KeyCode) 14:
            return Localisation.LocaliseID("PSG_10010706", (GameObject) null);
          case (KeyCode) 15:
            return Localisation.LocaliseID("PSG_10010705", (GameObject) null);
          case (KeyCode) 17:
            return Localisation.LocaliseID("PSG_10004936", (GameObject) null);
          case (KeyCode) 18:
            return Localisation.LocaliseID("PSG_10004939", (GameObject) null);
          case KeyCode.Pause:
            return Localisation.LocaliseID("PSG_10004938", (GameObject) null);
          case (KeyCode) 20:
            return Localisation.LocaliseID("PSG_10004937", (GameObject) null);
          case (KeyCode) 21:
            return Localisation.LocaliseID("PSG_10011151", (GameObject) null);
          case (KeyCode) 22:
            return Localisation.LocaliseID("PSG_10011152", (GameObject) null);
          case (KeyCode) 23:
            return Localisation.LocaliseID("PSG_10011155", (GameObject) null);
          case (KeyCode) 24:
            return Localisation.LocaliseID("PSG_10011153", (GameObject) null);
          case (KeyCode) 25:
            return Localisation.LocaliseID("PSG_10011154", (GameObject) null);
          case (KeyCode) 26:
            return Localisation.LocaliseID("PSG_10010711", (GameObject) null);
          case KeyCode.Escape:
            return Localisation.LocaliseID("PSG_10010712", (GameObject) null);
          case (KeyCode) 28:
            return Localisation.LocaliseID("PSG_10010713", (GameObject) null);
          case (KeyCode) 29:
            return Localisation.LocaliseID("PSG_10010714", (GameObject) null);
          case (KeyCode) 30:
            return Localisation.LocaliseID("PSG_10010715", (GameObject) null);
          case (KeyCode) 31:
            return Localisation.LocaliseID("PSG_10010716", (GameObject) null);
          case KeyCode.Space:
            return Localisation.LocaliseID("PSG_10010717", (GameObject) null);
          case KeyCode.Exclaim:
            return Localisation.LocaliseID("PSG_10010718", (GameObject) null);
          case KeyCode.DoubleQuote:
            return Localisation.LocaliseID("PSG_10010719", (GameObject) null);
          case KeyCode.Hash:
            return Localisation.LocaliseID("PSG_10010720", (GameObject) null);
          case KeyCode.Dollar:
            return Localisation.LocaliseID("PSG_10010721", (GameObject) null);
          case (KeyCode) 37:
            return Localisation.LocaliseID("PSG_10010722", (GameObject) null);
          case KeyCode.Minus:
            return Localisation.LocaliseID("PSG_10004927", (GameObject) null);
          case KeyCode.Slash:
            return Localisation.LocaliseID("PSG_10004930", (GameObject) null);
          case KeyCode.Alpha0:
            return Localisation.LocaliseID("PSG_10004929", (GameObject) null);
          case KeyCode.Alpha1:
            return Localisation.LocaliseID("PSG_10004932", (GameObject) null);
          case KeyCode.Alpha2:
            return Localisation.LocaliseID("PSG_10004931", (GameObject) null);
          case KeyCode.Alpha3:
            return Localisation.LocaliseID("PSG_10004934", (GameObject) null);
          case KeyCode.Alpha4:
            return Localisation.LocaliseID("PSG_10004933", (GameObject) null);
          default:
            switch (keyCode - 8)
            {
              case KeyCode.None:
                return Localisation.LocaliseID("PSG_10011150", (GameObject) null);
              case (KeyCode) 1:
                return Localisation.LocaliseID("PSG_10004935", (GameObject) null);
              case (KeyCode) 5:
                return Localisation.LocaliseID("PSG_10010694", (GameObject) null);
              default:
                return string.Empty;
            }
        }
    }
  }

  public static bool IsModifierKeyCode(KeyCode key_code)
  {
    switch (key_code)
    {
      case KeyCode.RightShift:
      case KeyCode.LeftShift:
      case KeyCode.RightControl:
      case KeyCode.LeftControl:
      case KeyCode.RightAlt:
      case KeyCode.LeftAlt:
        return true;
      default:
        return false;
    }
  }
}
