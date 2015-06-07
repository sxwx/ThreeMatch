namespace Assets.Scripts.GameLogic
{
  using UnityEngine;
  using UnityEngine.EventSystems;
  using UnityEngine.UI;

  public class DropHandler : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
  {
    public Image containerImage;
    public Image receivingImage;
	
    private Color normalColor;
    public Color highlightColor = Color.yellow;
	
    public void OnEnable ()
    {
      if (containerImage != null)
        normalColor = containerImage.color;
    }
	
    public void OnDrop(PointerEventData data)
    {
      containerImage.color = normalColor;
		
      if (receivingImage == null)
        return;
		
      var dropSprite = GetDropSprite (data);
      if (dropSprite != null)
      {
        var canFlip = FindObjectOfType<GridController>().CanFlip(this.gameObject, data.pointerDrag);
        if (canFlip)
        {
          data.pointerDrag.GetComponent<Image>().sprite = receivingImage.sprite;
          receivingImage.sprite = dropSprite;

//        data.pointerDrag.GetComponent<Image>().enabled = false;
        }      
      }			
    }

    public void OnPointerEnter(PointerEventData data)
    {
      if (containerImage == null)
        return;
		
      var dropSprite = GetDropSprite (data);
      if (dropSprite != null)
        containerImage.color = highlightColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
      if (containerImage == null)
        return;
		
      containerImage.color = normalColor;
    }
	
    private Sprite GetDropSprite(PointerEventData data)
    {
      var originalObj = data.pointerDrag;
      if (originalObj == null)
        return null;

      var srcImage = originalObj.GetComponent<Image>();
      if (srcImage == null)
        return null;
		
      return srcImage.sprite;
    }
  }
}