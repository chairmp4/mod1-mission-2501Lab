using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour{
   private int sleepCountdown = 4;
   private Rigidbody rigid;
   public GameObject   wallPrefab;
   public GameObject   slabPrefab;

   public GameObject   WinScreenPrefab;

   void Awake(){
    rigid = GetComponent<Rigidbody>();
   }

   void FixedUpdate(){
    if (sleepCountdown > 0){
        rigid.Sleep();
        sleepCountdown--;
    }

    // placeholder for win screen
    // if (wallPrefab; slabPrefab;){
    //     Instantiate(WinScreenPrefab);
    // }
    }
}
