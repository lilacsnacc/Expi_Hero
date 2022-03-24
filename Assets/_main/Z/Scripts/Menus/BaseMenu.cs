using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMenu : MonoBehaviour {
  protected List<LTDescr> LTDescrList = new List<LTDescr>();
  protected CanvasGroup canvasGroup;

  public bool escapeable = true;

  protected virtual void Awake() {
    if (canvasGroup) return;

    canvasGroup = GetComponent<CanvasGroup>();

    if (!canvasGroup)
      canvasGroup = gameObject.AddComponent(typeof(CanvasGroup)) as CanvasGroup;

    canvasGroup.alpha = 0;
  }

  public virtual LTDescr Open(EventSystem eventSystem) {
    SkipTween();
    gameObject.SetActive(true);

    LTDescrList.Add(LeanTween.alphaCanvas(canvasGroup, 1, 1));

    return LTDescrList[0];
  }
  public virtual LTDescr Close() {
    SkipTween();
    if(canvasGroup)  //HACK game crashes because canvasGroup is already destroyed but is trying to be accessed{
    {
        LTDescrList.Add(LeanTween.alphaCanvas(canvasGroup, 0, 1));
        return LTDescrList[0];
    }
    else
    {
        return null;
    }
  }

  public virtual void SkipTween() {
    LTDescrList.ForEach(lTDescr => lTDescr.passed = 9999);
    LTDescrList.Clear();
  }
}
