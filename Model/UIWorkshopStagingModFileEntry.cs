// Decompiled with JetBrains decompiler
// Type: UIWorkshopStagingModFileEntry
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using ModdingSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWorkshopStagingModFileEntry : MonoBehaviour
{
  public TextMeshProUGUI fileName;
  [SerializeField]
  private TextMeshProUGUI subHeaderFileName;
  [SerializeField]
  private Image fileIcon;
  private ModFileInfo mModFile;

  public void SetupEntryFileInfo(ModFileInfo inModFile)
  {
    this.mModFile = inModFile;
    this.fileName.text = this.mModFile.fileInfo.Name;
    this.SetupSubHeader();
    this.SetupFileIcon();
  }

  private void SetupSubHeader()
  {
    switch (this.mModFile.fileInfoType)
    {
      case ModFileInfo.ModFileInfoType.Logo:
        this.subHeaderFileName.text = Localisation.LocaliseID("PSG_10012018", (GameObject) null);
        break;
      case ModFileInfo.ModFileInfoType.Image:
        this.subHeaderFileName.text = Localisation.LocaliseID("PSG_10012019", (GameObject) null);
        break;
      case ModFileInfo.ModFileInfoType.Database:
        this.subHeaderFileName.text = Localisation.LocaliseID("PSG_10012020", (GameObject) null);
        break;
      case ModFileInfo.ModFileInfoType.Model:
        this.subHeaderFileName.text = Localisation.LocaliseID("PSG_10012021", (GameObject) null);
        break;
      case ModFileInfo.ModFileInfoType.Video:
        this.subHeaderFileName.text = Localisation.LocaliseID("PSG_10012022", (GameObject) null);
        break;
    }
  }

  private void SetupFileIcon()
  {
    switch (this.mModFile.fileInfoType)
    {
      case ModFileInfo.ModFileInfoType.Logo:
        this.fileIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "Workshop-IconLogos");
        break;
      case ModFileInfo.ModFileInfoType.Image:
        this.fileIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "Workshop-IconImages");
        break;
      case ModFileInfo.ModFileInfoType.Database:
        this.fileIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "Workshop-IconDatabase");
        break;
      case ModFileInfo.ModFileInfoType.Model:
        this.fileIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "Workshop-IconModels");
        break;
      case ModFileInfo.ModFileInfoType.Video:
        this.fileIcon.sprite = App.instance.atlasManager.GetSprite(AtlasManager.Atlas.Frontend1, "Workshop-IconVideos");
        break;
    }
  }
}
