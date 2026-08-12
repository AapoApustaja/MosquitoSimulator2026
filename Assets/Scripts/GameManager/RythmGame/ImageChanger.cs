using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
	// Raahaa Inspectoriin se UI-elementti, jonka kuva vaihtuu
	public Image muutettavaKuva;

	// Raahaa Inspectoriin se uusi kuva (Sprite)
	public Sprite uusiSprite;

	public void ChangePic()
	{
		
		muutettavaKuva.sprite = uusiSprite;
	}
}
