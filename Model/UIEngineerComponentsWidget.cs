// Decompiled with JetBrains decompiler
// Type: UIEngineerComponentsWidget
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIEngineerComponentsWidget : MonoBehaviour
{
  public List<UIComponentEntry> entries = new List<UIComponentEntry>();
  public UICharacterPortrait portrait;
  public UIAbilityStars stars;
  public TextMeshProUGUI engineerName;
  public GameObject noComponentsAvailableContainer;

  public void Setup()
  {
    Engineer personOnJob = (Engineer) Game.instance.player.team.contractManager.GetPersonOnJob(Contract.Job.EngineerLead);
    this.portrait.SetPortrait((Person) personOnJob);
    this.stars.SetAbilityStarsData((Person) personOnJob);
    this.engineerName.text = personOnJob.name;
    for (int index = 0; index < this.entries.Count; ++index)
      this.entries[index].Setup((CarPartComponent) null);
    for (int index = 0; index < personOnJob.availableComponents.Count; ++index)
    {
      CarPartComponent availableComponent = personOnJob.availableComponents[index];
      if (availableComponent.level - 1 >= 0 && availableComponent.level - 1 < this.entries.Count)
        this.entries[availableComponent.level - 1].Setup(availableComponent);
    }
    this.noComponentsAvailableContainer.SetActive(personOnJob.availableComponents.Count == 0);
  }
}
