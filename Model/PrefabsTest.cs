// Decompiled with JetBrains decompiler
// Type: PrefabsTest
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PrefabsTest : MonoBehaviour
{
  private Stopwatch stopwatch = new Stopwatch();
  private Dictionary<string, List<double>> results = new Dictionary<string, List<double>>();
  private List<string> testNames = new List<string>();
  public UIGridList gridTest;
  public GameObject instantiatingTest;
  public GameObject parentingTest;
  public GameObject activatingTest;
  public GameObject deactivatingTest;
  public GameObject reactivatingTest;
  public GameObject siblingOrderTest;
  private GameObject created;

  private void Start()
  {
    Profiler.maxNumberOfSamplesPerFrame = 8000000;
    for (int index = 0; index < 100; ++index)
    {
      this.ResetTests();
      this.GridTest();
      this.InstantiatingTest();
      this.DestroyTest();
      this.ReinstantiatingTest();
      this.DestroyImmediateTest();
      this.ResourcesInstantiatingTest();
      this.ParentingTest();
      this.ResetParentingTest();
      this.ActivatingTest();
      this.DeactivatingTest();
      this.ReactivatingTest();
      this.DoubleActivatingTest();
      this.DoubleDeactivatingTest();
      this.FirstSiblingOrderTest();
      this.GetSiblingOrderTest();
      this.LastSiblingOrderTest();
      this.SiblingOrderTest();
    }
    this.PostTestResults();
    UnityEngine.Debug.Break();
  }

  private void ResetTests()
  {
    if ((Object) this.created != (Object) null)
      Object.Destroy((Object) this.created);
    this.gridTest.DestroyListItems();
    this.parentingTest.transform.SetParent((Transform) null);
    this.activatingTest.SetActive(false);
    this.deactivatingTest.SetActive(true);
    this.siblingOrderTest.transform.SetAsLastSibling();
  }

  private void StartWatch(string inTestName)
  {
    this.stopwatch = new Stopwatch();
    this.stopwatch.Start();
  }

  private void StopWatch()
  {
    this.stopwatch.Stop();
  }

  private void GridTest()
  {
    this.StartWatch("UI Grid List Instantiating Test");
    this.gridTest.CreateListItem<UICharacterPortrait>();
    this.StopWatch();
    this.SetResult("UI Grid List Instantiating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void InstantiatingTest()
  {
    this.StartWatch("Instantiating GameObject Test");
    this.created = Object.Instantiate<GameObject>(this.instantiatingTest);
    this.StopWatch();
    this.SetResult("Instantiating GameObject Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void DestroyTest()
  {
    this.StartWatch("GameObject Destroy Test");
    Object.Destroy((Object) this.created);
    this.StopWatch();
    this.SetResult("GameObject Destroy Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ReinstantiatingTest()
  {
    this.StartWatch("Reinstantiating GameObject Test");
    this.created = Object.Instantiate<GameObject>(this.instantiatingTest);
    this.StopWatch();
    this.SetResult("Reinstantiating GameObject Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void DestroyImmediateTest()
  {
    this.StartWatch("GameObject Destroy Immediate Test");
    Object.DestroyImmediate((Object) this.created);
    this.StopWatch();
    this.SetResult("GameObject Destroy Immediate Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ResourcesInstantiatingTest()
  {
    this.StartWatch("Resources Instantiating GameObject Test");
    this.created = Object.Instantiate(Resources.Load("Prefabs/UI/Components/UIDriver", typeof (GameObject))) as GameObject;
    this.StopWatch();
    this.SetResult("Resources Instantiating GameObject Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ParentingTest()
  {
    this.StartWatch("Transform Parenting Test");
    this.parentingTest.transform.SetParent(this.transform);
    this.StopWatch();
    this.SetResult("Transform Parenting Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ResetParentingTest()
  {
    this.StartWatch("Null Parenting Test");
    this.parentingTest.transform.SetParent((Transform) null);
    this.StopWatch();
    this.SetResult("Null Parenting Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ActivatingTest()
  {
    this.StartWatch("GameObject Activating Test");
    this.activatingTest.SetActive(true);
    this.StopWatch();
    this.SetResult("GameObject Activating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void DeactivatingTest()
  {
    this.StartWatch("GameObject Deactivating Test");
    this.deactivatingTest.SetActive(false);
    this.StopWatch();
    this.SetResult("GameObject Deactivating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void ReactivatingTest()
  {
    this.StartWatch("GameObject Reactivating Test");
    this.reactivatingTest.SetActive(false);
    this.reactivatingTest.SetActive(true);
    this.StopWatch();
    this.SetResult("GameObject Reactivating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void DoubleActivatingTest()
  {
    this.StartWatch("GameObject Double Activating Test");
    this.activatingTest.SetActive(true);
    this.StopWatch();
    this.SetResult("GameObject Double Activating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void DoubleDeactivatingTest()
  {
    this.StartWatch("GameObject Double Deactivating Test");
    this.deactivatingTest.SetActive(false);
    this.StopWatch();
    this.SetResult("GameObject Double Deactivating Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void FirstSiblingOrderTest()
  {
    this.StartWatch("First Sibling Order Test");
    this.siblingOrderTest.transform.SetAsFirstSibling();
    this.StopWatch();
    this.SetResult("First Sibling Order Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void GetSiblingOrderTest()
  {
    this.StartWatch("Get Sibling Order Test");
    this.siblingOrderTest.transform.GetSiblingIndex();
    this.StopWatch();
    this.SetResult("Get Sibling Order Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void LastSiblingOrderTest()
  {
    this.StartWatch("Last Sibling Order Test");
    this.siblingOrderTest.transform.SetAsLastSibling();
    this.StopWatch();
    this.SetResult("Last Sibling Order Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void SiblingOrderTest()
  {
    this.StartWatch("Sibling Order Test");
    this.siblingOrderTest.transform.SetSiblingIndex(2);
    this.StopWatch();
    this.SetResult("Sibling Order Test", this.stopwatch.Elapsed.TotalMilliseconds);
  }

  private void SetResult(string inTestName, double inTotalElapse)
  {
    if (!this.results.ContainsKey(inTestName))
      this.results.Add(inTestName, new List<double>());
    this.results[inTestName].Add(inTotalElapse);
    if (this.testNames.Contains(inTestName))
      return;
    this.testNames.Add(inTestName);
  }

  private void PostTestResults()
  {
    int count1 = this.testNames.Count;
    for (int index1 = 0; index1 < count1; ++index1)
    {
      string testName = this.testNames[index1];
      int count2 = this.results[testName].Count;
      double num1 = 0.0;
      for (int index2 = 0; index2 < count2; ++index2)
        num1 += this.results[testName][index2];
      double num2 = num1 / (double) count2;
      UnityEngine.Debug.Log((object) (testName + " - " + (object) num2 + " ms"));
    }
  }
}
