// Decompiled with JetBrains decompiler
// Type: UIBaseSessionTopBarWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class UIBaseSessionTopBarWidget : MonoBehaviour
{
  protected KeyBinding.Name keybinding = KeyBinding.Name.WeatherConditions;
  public GameObject dropdown;
  protected bool useKeyBinding;

  public virtual bool isDropdownActive
  {
    get
    {
      if ((Object) this.dropdown != (Object) null)
        return this.dropdown.activeSelf;
      return false;
    }
  }

  public virtual void OnEnter()
  {
  }

  public virtual void CheckKeyBinding()
  {
    if (!Game.IsActive() || !this.useKeyBinding || (!((Object) this.dropdown != (Object) null) || !InputManager.instance.GetKeyUp(this.keybinding)) || (!(App.instance.gameStateManager.currentState is SessionState) && !(App.instance.gameStateManager.currentState is PreSessionHUBState) || !Game.instance.sessionManager.isCircuitActive))
      return;
    this.ToggleDropdown(!this.dropdown.activeSelf);
  }

  public virtual bool ShouldBeEnabled()
  {
    return true;
  }

  public virtual void ToggleDropdown(bool inValue)
  {
    if (inValue)
      this.OpenDropdown();
    else
      this.CloseDropdown();
  }

  public virtual void OpenDropdown()
  {
    if (!((Object) this.dropdown != (Object) null))
      return;
    GameUtility.SetActive(this.dropdown, true);
  }

  public virtual void CloseDropdown()
  {
    if (!((Object) this.dropdown != (Object) null))
      return;
    GameUtility.SetActive(this.dropdown, false);
  }
}
