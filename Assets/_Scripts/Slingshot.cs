using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slingshot : MonoBehaviour{

    public Text                scoreGT;

    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake(){
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
    }

    void Start()
    {
      // adds score into script and sets the text to zero
      GameObject scoreGO = GameObject.Find("ScoreCounter");    

      // Get the Text Component of that GameObject
      scoreGT = scoreGO.GetComponent<Text>();        
                             
      // Set the starting number of points to 0
      scoreGT.text = "0";
    }

    void OnMouseEnter(){
    //print("Slingshot:OnMouseEnter()");
    launchPoint.SetActive(true);
 }

 void OnMouseExit(){
    //print("Slingshot:OnMouseExit()");
    launchPoint.SetActive(false);
 }

 void OnMouseDown(){

    aimingMode = true;

    projectile = Instantiate(projectilePrefab) as GameObject;

    projectile.transform.position = launchPos;

    projectile.GetComponent<Rigidbody>().isKinematic = true;


   int score = int.Parse( scoreGT.text );

   score += 1; // previous block allows for int to be added toreturn

   // Convert the score back to a string and display it
   scoreGT.text = score.ToString();
   
   // Track the high score
   if (score > HighScore.score){
      HighScore.score = score;
   }

 }
 void Update(){

    if (!aimingMode) return;

    Vector3 mousePos2D = Input.mousePosition;
    mousePos2D.z = -Camera.main.transform.position.z;
    Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

    Vector3 mouseDelta = mousePos3D -launchPos;

    float maxMagnitude = this.GetComponent<SphereCollider>().radius;
    if (mouseDelta.magnitude > maxMagnitude){
        mouseDelta.Normalize();
        mouseDelta*= maxMagnitude;
    }

    Vector3 projPos = launchPos + mouseDelta;
    projectile.transform.position = projPos;

    if(Input.GetMouseButtonUp(0)){

        aimingMode = false;
        Rigidbody projRB = projectile.GetComponent<Rigidbody>();
        projRB.isKinematic = false;
        projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
        projRB.velocity = -mouseDelta * velocityMult;
        FollowCam.POI = projectile;
        Instantiate<GameObject>(projLinePrefab, projectile.transform);
        projectile = null;
    }
 }
}
