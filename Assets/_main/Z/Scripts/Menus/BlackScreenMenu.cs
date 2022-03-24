using UnityEngine;
using UnityEngine.EventSystems;

public class BlackScreenMenu : BaseMenu {
    public override LTDescr Open(EventSystem eventSystem) {

        gameObject.SetActive(true);

        SkipTween();
        
        LTDescrList.Add(LeanTween.alphaCanvas(canvasGroup, 1.25f, 1.5f));

        return LTDescrList[0];
    }

    public override LTDescr Close() {
        SkipTween();

        if (canvasGroup)    //HACK This should not stay like this, but canvasGroup is being deleted somewhere and is causing problems on player death
        {
            LTDescrList.Add(LeanTween.alphaCanvas(canvasGroup, 0, 1.5f));
            return LTDescrList[0].setOnComplete(() => gameObject.SetActive(false));
        }
        else
        {
            return null;
        }
    }
}
