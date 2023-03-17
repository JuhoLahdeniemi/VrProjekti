using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;
using TMPro;

public class CannonControl : MonoBehaviour
{
    public GameObject targetLocation;
    public GameObject targetLocation1;
    public GameObject targetLocation2;
    public GameObject targetLocation3; 
    public GameObject ammoSpawn;
    public GameObject ammo;
    public GameObject barrelRotator;
    public float force;
    public Vector3 gravity;
    private int angleMultiplier;
    public TextMeshProUGUI gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.LookAt(new Vector3(targetLocation.transform.position.x, gameObject.transform.position.y, targetLocation.transform.position.z));
    }

    public void Shoot()
    {
        
            StartCoroutine(ShootBalls());
        
       
    }

    IEnumerator ShootBalls()
    {

        gameOver.text = "";
        GameManager.manager.resetPoints();
        //Katsotaan mik‰ lentorata maaleille on asetettu
        if (GameManager.manager.getTargetArea() == 1)
        {
            targetLocation = targetLocation1;
            force = 20;
        }
        else if(GameManager.manager.getTargetArea() == 2)
        {
            targetLocation = targetLocation2;
            force = 28;
        }
        else if (GameManager.manager.getTargetArea() == 3)
        {
            targetLocation = targetLocation3;
            force = 35;
        }

        Debug.Log("Ammutaan ammus");
        Vector3[] direction = HitTargetBySpeed(ammoSpawn.transform.position, targetLocation.transform.position, gravity, force);

        if (gameObject.transform.position.z < targetLocation.transform.position.z)
        {
            angleMultiplier = -1;
        }
        else
        {
            angleMultiplier = 1;
        }

        // Ennen ensimm‰ist‰ ammusta lasketaan kulma, mihin piipun pit‰isi k‰‰nty‰. Ilmoitetaan se RotateGuniin ja odotetaan coroutinessa kunnes piippu on k‰‰ntynyt
        // ja vasta sitten ammutaan.
        //Debug.Log("Piipun tulisi k‰‰nty‰ kulmaan: " + Mathf.Atan(direction[0].y / direction[0].z) * Mathf.Rad2Deg * angleMultiplier);
        //barrelRotator.GetComponent<RotateBarrel>().xAngle = Mathf.Atan(direction[0].y / direction[0].z) * Mathf.Rad2Deg * angleMultiplier;

        //Odotetaan, ett k‰‰ntyminen p‰‰ttyy. K‰ytet‰‰n Coroutinen ominaisuuksia
        //yield return new WaitUntil(() => barrelRotator.GetComponent<RotateBarrel>().rotating == false);

        for (int i = 0; i < 10; i++)
        {
            
            // Eka ammus
            /*
            GameObject projectile = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddRelativeForce(direction[0], ForceMode.Impulse);

            yield return new WaitForSeconds(2);
            */
            Debug.Log("Piipun tulisi k‰‰nty‰ kulmaan: " + Mathf.Atan(direction[1].y / direction[1].z) * Mathf.Rad2Deg * angleMultiplier);
            barrelRotator.GetComponent<RotateBarrel>().xAngle = Mathf.Atan(direction[1].y / direction[1].z) * Mathf.Rad2Deg * angleMultiplier;

            //Odotetaan, ett k‰‰ntyminen p‰‰ttyy. K‰ytet‰‰n Coroutinen ominaisuuksia
            yield return new WaitUntil(() => barrelRotator.GetComponent<RotateBarrel>().rotating == false);

            // Toka ammus
            GameObject projectile2 = Instantiate(ammo, ammoSpawn.transform.position, Quaternion.identity);
            projectile2.GetComponent<Rigidbody>().AddRelativeForce(direction[1], ForceMode.Impulse);
        }
        yield return new WaitForSeconds(10);
        gameOver.text = "Game over." + "\n" + "Points: " + GameManager.manager.getPoints().ToString();
    }

    public Vector3[] HitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float launchSpeed)
    {
        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase, startPosition);
        float horizontalDistance = horizontal.magnitude;

        Vector3 vertical = GetVerticalVector(AtoB, gravityBase, startPosition);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

        float x2 = horizontalDistance * horizontalDistance;
        float v2 = launchSpeed * launchSpeed;
        float v4 = launchSpeed * launchSpeed * launchSpeed * launchSpeed;
        float gravMag = gravityBase.magnitude;

        // LAUNCHTEST
        // Jos launchtest on negatiivinen, niin ei ole mit‰‰n mahdollisuutta osua kohteeseen annetulla forcella.
        // Jos launchtest on positiivinen, osuminen on mahdollista ja voidaan laskea kaksi mahdollista kulmaa.

        float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance)));
        Debug.Log("LAUNCHTEST: " + launchTest);

        Vector3[] launch = new Vector3[2];

        if (launchTest < 0)
        {
            Debug.Log("Ei voida osua maaliin. Ammutaan kuitenkin 45 asteen kulmassa kaksi palloa.");
            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(45.0f * Mathf.Deg2Rad)) - gravityBase.normalized * launchSpeed * Mathf.Sin(45.0f * Mathf.Deg2Rad);


        }
        else
        {
            Debug.Log("Voidaan osua, lasketaan kulmat");
            float[] tanAngle = new float[2];
            tanAngle[0] = (v2 - Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);
            tanAngle[1] = (v2 + Mathf.Sqrt(v4 - gravMag * ((gravMag * x2) + (2 * verticalDistance * v2)))) / (gravMag * horizontalDistance);

            float[] finalAngle = new float[2];
            finalAngle[0] = Mathf.Atan(tanAngle[0]);
            finalAngle[1] = Mathf.Atan(tanAngle[1]);
            Debug.Log("Kulmat joihin ammutaan ovat: " + finalAngle[0] * Mathf.Rad2Deg + " ja " + finalAngle[1] * Mathf.Rad2Deg);

            launch[0] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[0])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[0]);
            launch[1] = (horizontal.normalized * launchSpeed * Mathf.Cos(finalAngle[1])) - gravityBase.normalized * launchSpeed * Mathf.Sin(finalAngle[1]);
        }

        return launch;
    }

    public Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(AtoB, perpendicular);
        Debug.DrawRay(startPosition, output, Color.green, 10f);
        return output;
    }

    public Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        Debug.DrawRay(startPosition, output, Color.cyan, 10f);


        return output;
    }
}

