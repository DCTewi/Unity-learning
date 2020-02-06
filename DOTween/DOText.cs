using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOText : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void ClearText()
    {
        if (text != null)
        {
            text.text = "";
        }
    }

    private void Start()
    {
        text.DOText("你好，我是冻葱Tewi", 2).SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                ClearText();
                text.DOText("很高兴见到你，憨批.", 2).SetEase(Ease.Linear);
            });
    }

}
