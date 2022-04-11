using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
 public class PopupMessage : MonoBehaviour {
 
     public GameObject popUpBox;
     public Animator animator;
     public TMP_Text popUpText;
 
     // Use this for initialization
     void Start () {

         popUpBox.SetActive(false);
 
     }
     
     // Update is called once per frame
     void Update () {
     }
    
    public void popUp() {
        popUpBox.SetActive(true);
        // popUpText.text = text;
        // animator.setTrigger("pop");
    }

    public void closePopUp() {
        popUpBox.SetActive(false);
    }
//      public void Open(string message){
//          ui.SetActive (!ui.activeSelf);
 
//          if (ui.activeSelf) {
//              if(!string.IsNullOrEmpty(inventoryStuffName)){
//                 //  var texture = TakeInvenotryCollecition (inventoryStuffName);
//                  RawImage rawImage = ui.gameObject.GetComponentInChildren<RawImage>();
//                 //  rawImage.texture = texture;
//              }
//              if (!string.IsNullOrEmpty (message)) {
//                  Text textObject = ui.gameObject.GetComponentInChildren<Text> ();
//                  textObject.text = message;
//              }
//              Time.timeScale = 0f;
//          } 
//      }
//      public void Close(){
//          ui.SetActive (!ui.activeSelf);
//          if (!ui.activeSelf) {
//              Time.timeScale = 1f;
//          } 
//      }
// //  //You need to have Folder Resources/InvenotryItems
// //      public Texture TakeInventoryCollection(string LoadCollectionsToInventory)
// //      {
// //          Texture loadedGO = Resources.Load("InventoryItems/"+LoadCollectionsToInventory, typeof(Texture)) as Texture;
// //          return loadedGO;
// //      }
 }