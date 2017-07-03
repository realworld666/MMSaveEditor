// Decompiled with JetBrains decompiler
// Type: UIPreferencesKeyButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPreferencesKeyButton : MonoBehaviour
{
  public Preference.pName keyName = Preference.pName.Control_Pause;
  public UIPreferencesKeyButton.Group group;
  public Button button;
  public TextMeshProUGUI keyText;
  private KeyCode mKeyCode;
  private KeyCode mKeyModifierCode;

  public void SetButtonText(string inValue)
  {
    this.keyText.text = inValue;
  }

  public void SetMainKey(KeyCode inKeyCode)
  {
    this.mKeyCode = inKeyCode;
  }

  public void SetModifierKey(KeyCode inKeyCode)
  {
    this.mKeyModifierCode = inKeyCode;
  }

  public string GetButtonText()
  {
    return this.keyText.text;
  }

  public string GetMainKeyText()
  {
    return ((Enum) this.mKeyCode).ToString();
  }

  public string GetModifierKeyText()
  {
    return ((Enum) this.mKeyModifierCode).ToString();
  }

  public KeyCode GetMainKey()
  {
    return this.mKeyCode;
  }

  public KeyCode GetModifierKey()
  {
    return this.mKeyModifierCode;
  }

  public enum Group
  {
    FrontEnd,
    Race,
    Shared,
  }
}
