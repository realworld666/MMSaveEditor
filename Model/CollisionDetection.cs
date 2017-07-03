// Decompiled with JetBrains decompiler
// Type: CollisionDetection
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 0C496ACE-0409-4428-BF50-DF04E26AD4C0
// Assembly location: D:\Steam\steamapps\common\Motorsport Manager\MM_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

public class CollisionDetection
{
  public static CollisionResult Intersects(CollisionBounds inBounds1, CollisionBounds inBounds2, Vector2 inVelocity)
  {
    CollisionResult collisionResult = new CollisionResult();
    collisionResult.intersecting = true;
    collisionResult.willIntersect = true;
    int edgeCount1 = inBounds1.edgeCount;
    int edgeCount2 = inBounds2.edgeCount;
    float num1 = float.PositiveInfinity;
    Vector2 rhs = new Vector2();
    Vector2 one = Vector2.one;
    for (int inIndex = 0; inIndex < edgeCount1 + edgeCount2; ++inIndex)
    {
      Vector2 vector2_1 = inIndex >= edgeCount1 ? inBounds2.GetEdge(inIndex - edgeCount1) : inBounds1.GetEdge(inIndex);
      Vector2 vector2_2 = new Vector2(-vector2_1.y, vector2_1.x);
      vector2_2.Normalize();
      float inMin1 = 0.0f;
      float inMin2 = 0.0f;
      float inMax1 = 0.0f;
      float inMax2 = 0.0f;
      CollisionDetection.ProjectBounds(vector2_2, inBounds1, ref inMin1, ref inMax1);
      CollisionDetection.ProjectBounds(vector2_2, inBounds2, ref inMin2, ref inMax2);
      if ((double) CollisionDetection.IntervalDistance(inMin1, inMax1, inMin2, inMax2) > 0.0)
        collisionResult.intersecting = false;
      float num2 = Vector2.Dot(vector2_2, inVelocity);
      if ((double) num2 < 0.0)
        inMin1 += num2;
      else
        inMax1 += num2;
      float f = CollisionDetection.IntervalDistance(inMin1, inMax1, inMin2, inMax2);
      if ((double) f > 0.0)
        collisionResult.willIntersect = false;
      if (collisionResult.intersecting || collisionResult.willIntersect)
      {
        float num3 = Mathf.Abs(f);
        if ((double) num3 < (double) num1)
        {
          num1 = num3;
          rhs = vector2_2;
          if ((double) Vector2.Dot(inBounds1.GetPosition() - inBounds2.GetPosition(), rhs) < 0.0)
            rhs = -rhs;
        }
      }
      else
        break;
    }
    if (collisionResult.willIntersect)
      collisionResult.collisionResponse = rhs * num1;
    return collisionResult;
  }

  private static float IntervalDistance(float inMinA, float inMaxA, float inMinB, float inMaxB)
  {
    if ((double) inMinA < (double) inMinB)
      return inMinB - inMaxA;
    return inMinA - inMaxB;
  }

  private static void ProjectBounds(Vector2 inAxis, CollisionBounds inBounds, ref float inMin, ref float inMax)
  {
    float num1 = Vector2.Dot(inAxis, inBounds.GetPoint(0));
    inMin = num1;
    inMax = num1;
    for (int inIndex = 0; inIndex < inBounds.pointCount; ++inIndex)
    {
      float num2 = Vector2.Dot(inBounds.GetPoint(inIndex), inAxis);
      if ((double) num2 < (double) inMin)
        inMin = num2;
      else if ((double) num2 > (double) inMax)
        inMax = num2;
    }
  }
}
