using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject mCardSymbol;
    [SerializeField]
    private GameObject mCardBG;
    private int mCardRowID=0;
    private int mCardColID=0;
    private bool IsInputVaild=false;
    private bool IsCardMatched=false;
    private bool IsCardOpen=false;
    private bool IsVisible=false;
    private string mSpriteIDStr="";
     [SerializeField]
    private LevelManager LevelManagerCS;
    // Start is called before the first frame update
    public string GetSpriteName()
    {
        return mSpriteIDStr;
    }
    public void Init(int rowid,int colid,Sprite symbol,Color bg,bool show=false,bool inputvaild=false,bool cardmatched=false,bool cardopen=false,LevelManager tmpLevelManagerCS=null)
    {
        LevelManagerCS=tmpLevelManagerCS;
        mCardRowID=rowid;
        mCardColID=colid;
        mCardSymbol.GetComponent<SpriteRenderer>().sprite=symbol;
        mSpriteIDStr=symbol.name;
        mCardBG.GetComponent<SpriteRenderer>().color=bg;
        IsVisible=show;
        IsCardMatched=cardmatched;
        IsCardOpen=cardopen;
        if(!IsVisible){
        IsInputVaild=false;
        mCardSymbol.SetActive(false);
        mCardBG.SetActive(false);
        this.gameObject.SetActive(false);
        }
        else{
        IsInputVaild=true;
        }
       
    }
    void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
         RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
                if(hit.collider.CompareTag("Cards")&&!IsCardOpen&&IsInputVaild&&!IsCardMatched&&IsVisible
                &&!GameManager.Instance.isPopUpWindows)
                {
                    SoundManager.Instance.ButtonOnClick();
                    Debug.Log("Card Clicked");
                    if(IsVisible)
                    {
                      //  Debug.Log("Card Clicked");
                        IsInputVaild=false;
                        IsCardOpen=!IsCardOpen;
                        mCardBG.GetComponent<Animator>().Play("CardsAnim_open");
                        LevelManagerCS.InputValidCount(mSpriteIDStr,this);
                    }
                }
        }
    }
    public void CardClose(bool tmpisMatched){
         
          IsCardOpen=false;
           IsCardMatched=tmpisMatched;
            StartCoroutine(CardCloseAnim());
        
    }

   public IEnumerator CardCloseAnim()
   {
    yield return new WaitForSeconds(1.3f);
   if(IsCardMatched){
 mCardBG.GetComponent<Animator>().Play("CardsAnim_match");
          }
          else{
             mCardBG.GetComponent<Animator>().Play("CardsAnim_close");
             IsInputVaild=true;
          }
   }
}
