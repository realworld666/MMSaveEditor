// Decompiled with JetBrains decompiler
// Type: MM2.UI.CalendarCustomisePausingItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MM2.UI
{
  internal class CalendarCustomisePausingItem : MonoBehaviour
  {
    private CalendarCustomisePausingItem.CategoryWrapper category = new CalendarCustomisePausingItem.CategoryWrapper() { category = CalendarEventCategory.None };
    [SerializeField]
    private CalendarEventTypeIconContainer iconContainer;
    [SerializeField]
    private TextMeshProUGUI descriptionText;
    [SerializeField]
    private Toggle toggleButton;

    public void Setup(CalendarEventCategory category_)
    {
      this.category.category = category_;
      if ((Object) this.iconContainer == (Object) null || (Object) this.descriptionText == (Object) null || (Object) this.toggleButton == (Object) null)
      {
        Debug.LogError((object) "CalendarCustomisePausingItem has some members not set; prefab has probably gone wrong. This instance will not be usable.", (Object) null);
      }
      else
      {
        this.iconContainer.SetIcon(CalendarEventTypeIconContainer.IconForEventCategory(this.category.category));
        this.descriptionText.text = this.category.category.ToString();
        this.toggleButton.isOn = (Game.instance.calendar.eventTypesToPauseOn & this.category.category) != CalendarEventCategory.None;
        this.toggleButton.onValueChanged.AddListener(new UnityAction<bool>(this.SetOption));
      }
    }

    private void SetOption(bool value)
    {
      if (value)
      {
        Game.instance.calendar.eventTypesToPauseOn = Game.instance.calendar.eventTypesToPauseOn | this.category.category;
      }
      else
      {
        scSoundManager.Instance.PlaySound(SoundID.Button_Select, 0.0f);
        Game.instance.calendar.eventTypesToPauseOn = Game.instance.calendar.eventTypesToPauseOn & ~this.category.category;
      }
    }

    private struct CategoryWrapper
    {
      public CalendarEventCategory category;
    }
  }
}
