// Decompiled with JetBrains decompiler
// Type: UISponsorsPanel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

public class UISponsorsPanel : MonoBehaviour
{
  public List<SponsorSlot> slotsList = new List<SponsorSlot>();
  public List<SponsorshipDeal> sponsorTriggered = new List<SponsorshipDeal>();
  public List<UISponsorsPanel.SponsorData> sponsorDataList = new List<UISponsorsPanel.SponsorData>();
  private List<UISponsorsEntry> mEntries = new List<UISponsorsEntry>();
  public UIGridList sponsorsGrid;
  public UIGridList emptySponsorsGrid;
  public GameObject sponsorDivider;
  public SponsorsScreen screen;
  private Team mTeam;

  public void Setup()
  {
    this.mTeam = Game.instance.player.team;
    this.SetSponsorSlots();
  }

  private void SetSponsorSlots()
  {
    this.SetSponsorList();
    this.mEntries.Clear();
    this.sponsorDataList.Clear();
    this.sponsorTriggered.Clear();
    this.sponsorsGrid.DestroyListItems();
    this.emptySponsorsGrid.DestroyListItems();
    int count1 = this.slotsList.Count;
    for (int index = 0; index < count1; ++index)
    {
      SponsorSlot slots = this.slotsList[index];
      UISponsorsPanel.SponsorData sponsorData;
      if (!this.CheckSponsorData(slots.sponsorshipDeal))
      {
        sponsorData = new UISponsorsPanel.SponsorData();
        sponsorData.sponsorshipDeal = slots.sponsorshipDeal;
        this.sponsorDataList.Add(sponsorData);
      }
      else
        sponsorData = this.GetSponsorData(slots.sponsorshipDeal);
      sponsorData.slot.index = slots.slotNumber - 1;
      sponsorData.slot.number = slots.slotNumber;
    }
    for (int index = 0; index < count1; ++index)
    {
      SponsorSlot slots = this.slotsList[index];
      if (index == 3)
      {
        GameUtility.SetActive(this.sponsorDivider.gameObject, true);
        this.sponsorDivider.transform.SetAsLastSibling();
      }
      if (slots != null && slots.sponsorshipDeal != null)
      {
        if (!this.CheckTriggeredSponsorship(slots.sponsorshipDeal))
        {
          UISponsorsEntry listItem = this.sponsorsGrid.CreateListItem<UISponsorsEntry>();
          GameUtility.SetActive(listItem.gameObject, true);
          this.mEntries.Add(listItem);
          listItem.Setup(slots.sponsorshipDeal);
          this.sponsorTriggered.Add(slots.sponsorshipDeal);
        }
      }
      else
      {
        UISponsorsEmpty listItem = this.emptySponsorsGrid.CreateListItem<UISponsorsEmpty>();
        GameUtility.SetActive(listItem.gameObject, true);
        listItem.Setup(slots.slotNumber, slots.slotNumber - 1, this.screen);
      }
    }
    int count2 = this.mEntries.Count;
    for (int index = 0; index < count2; ++index)
      this.mEntries[index].SetSponsorSlots();
    GameUtility.SetActive(this.sponsorsGrid.itemPrefab, false);
    GameUtility.SetActive(this.emptySponsorsGrid.itemPrefab, false);
  }

  private void SetSponsorList()
  {
    this.slotsList.Clear();
    this.slotsList.Add(this.mTeam.sponsorController.slots[0]);
    this.slotsList.Add(this.mTeam.sponsorController.slots[2]);
    this.slotsList.Add(this.mTeam.sponsorController.slots[4]);
    this.slotsList.Add(this.mTeam.sponsorController.slots[1]);
    this.slotsList.Add(this.mTeam.sponsorController.slots[3]);
    this.slotsList.Add(this.mTeam.sponsorController.slots[5]);
  }

  public bool CheckSponsorData(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal != null)
    {
      for (int index = 0; index < this.sponsorDataList.Count; ++index)
      {
        if (this.sponsorDataList[index].sponsorshipDeal == inSponsorshipDeal)
          return true;
      }
    }
    return false;
  }

  public UISponsorsPanel.SponsorData GetSponsorData(SponsorshipDeal inSponsorshipDeal)
  {
    if (inSponsorshipDeal != null)
    {
      for (int index = 0; index < this.sponsorDataList.Count; ++index)
      {
        if (this.sponsorDataList[index].sponsorshipDeal == inSponsorshipDeal)
          return this.sponsorDataList[index];
      }
    }
    return (UISponsorsPanel.SponsorData) null;
  }

  public bool CheckTriggeredSponsorship(SponsorshipDeal inSponsorshipDeal)
  {
    for (int index = 0; index < this.sponsorTriggered.Count; ++index)
    {
      if (this.sponsorTriggered[index] == inSponsorshipDeal)
        return true;
    }
    return false;
  }

  public void CloseRollover()
  {
    UIManager.instance.dialogBoxManager.GetDialog<UISponsorsOverview>().Close();
  }

  public class SponsorData
  {
    public UISponsorsPanel.SponsorSlotData slot = new UISponsorsPanel.SponsorSlotData();
    public SponsorshipDeal sponsorshipDeal;
  }

  public class SponsorSlotData
  {
    public int index;
    public int number;
  }
}
