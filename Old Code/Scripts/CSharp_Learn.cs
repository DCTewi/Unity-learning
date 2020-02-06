using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharp_Learn : MonoBehaviour
{
    /* To be shown in inspector */
    //Enums
    public enum Direction
    {
        Left, Right, Up, Down
    };
    public Direction moveDir;

    //Hide public elements
    [HideInInspector]
    public Direction hideDir;

    //Set a range in inspector
    [Range (0.0f, 10.0f)]
    public float speed = 0.0f;

    //Arrays 
    public int[] arr = { 1, 2 };
    public GameObject[] gos = new GameObject[3];
    public string[] names = { "DCTewi", "dxc", "Inaba Tewi" };
    //Multiple-dimensional Array cannot be shown in the inspector
    public int[,] arr2 = new int[3, 4];

    /* Coroutines */
    //Coroutines only can be called by StartCoroutine();
    IEnumerator WaitAndPrint()
    {
        Debug.Log("Start Waiting");
        ///WaitForSeconds() can be affected by Time.timeScale
        yield return new WaitForSeconds(5);
        Debug.Log("Waited " + Time.time + "s");
    }

    IEnumerator Yields(int sth)
    {
        yield return null; //Next Update();
        yield return new WaitForFixedUpdate(); //Next FixedUpdate();
        yield return new WaitForSeconds(1); //After 1s
        yield return new WaitForEndOfFrame(); //After frame rendered
        yield return new WWW("https://somewebsite/download.zip"); //After download
        yield return StartCoroutine(WaitAndPrint()); //After other coroutines end
    }
    //StopCoroutine() can only stop the coroutines which are called by name, such as StartCoroutine("Yields", sth);

    private void Traversing()
    {
        //Ver 1
        for (int i = 0; i < arr.Length; i++)
        {
            //DoSth();
        }

        foreach (var i in arr)
        {
            //DoSth();
        }
    }

    private void Start()
    {
        StartCoroutine( WaitAndPrint() );
        print("Print Test");
        Debug.LogError("LogError Test");
        Debug.LogWarning("LogWarning Test");
    }

    private void Update()
    {
        
    }
}
