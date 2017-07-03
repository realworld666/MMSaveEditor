// Decompiled with JetBrains decompiler
// Type: UIMediaNewsArticle
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMediaNewsArticle : MonoBehaviour
{
  public List<TextMeshProUGUI> body = new List<TextMeshProUGUI>();
  public TextMeshProUGUI title;
  public UICharacterPortrait portrait;
  public TextMeshProUGUI writersText;
  private Person mJournalist;
  private int mBodyCount;

  public void GenerateNewsReportForSession()
  {
  }

  public void SendMediaStory()
  {
    Person person = Game.instance.dialogSystem.FindPerson("MediaPerson");
    this.mJournalist = person;
    DialogQuery inQuery = new DialogQuery();
    inQuery.AddCriteria("Source", "MediaStory");
    inQuery.AddCriteria("Type", "Start");
    DialogRule inRule = person.dialogQuery.ProcessQueryWithOwnCriteria(inQuery, false);
    if (inRule == null)
      return;
    this.writersText.text = person.contract.employeerName + "\n" + GameUtility.FormatDateTimeToShortDateString(Game.instance.time.now) + "\n" + person.name;
    this.title.text = Localisation.LocaliseID(inRule.localisationID, (GameObject) null);
    this.GetMediaBody(inRule);
  }

  private void GetMediaBody(DialogRule inRule)
  {
    if (inRule.triggerQuery == null)
      return;
    string who = inRule.triggerQuery.GetWho();
    Person person1 = Game.instance.dialogSystem.FindPerson(who);
    if (person1 != null)
    {
      if (who == "MediaPerson")
        person1 = this.mJournalist;
      DialogRule inRule1 = person1.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, false);
      if (inRule1 == null)
        return;
      this.SetBodyText(Localisation.LocaliseID(inRule1.localisationID, (GameObject) null));
      this.GetMediaBody(inRule1);
    }
    else
    {
      DialogRule dialogRule = App.instance.dialogRulesManager.ProcessQuery(inRule.triggerQuery, false);
      if (dialogRule == null)
        return;
      string mCriteriaInfo = dialogRule.who.mCriteriaInfo;
      Person person2 = Game.instance.dialogSystem.FindPerson(mCriteriaInfo);
      if (mCriteriaInfo == "MediaPerson")
        person2 = this.mJournalist;
      DialogRule inRule1 = person2.dialogQuery.ProcessQueryWithOwnCriteria(inRule.triggerQuery, false);
      if (inRule1 != null && person2 != null)
      {
        this.SetBodyText(Localisation.LocaliseID(inRule1.localisationID, (GameObject) null));
        this.GetMediaBody(inRule1);
      }
      else
        Debug.LogError((object) ("Not able to find a sender for rule ID: " + inRule1.localisationID), (Object) null);
    }
  }

  private void SetBodyText(string inText)
  {
    if (this.mBodyCount < this.body.Count - 1)
    {
      this.body[this.mBodyCount].text = inText;
      ++this.mBodyCount;
    }
    else
    {
      TextMeshProUGUI textMeshProUgui = this.body[this.mBodyCount];
      string str = textMeshProUgui.text + "\n" + inText;
      textMeshProUgui.text = str;
    }
  }
}
