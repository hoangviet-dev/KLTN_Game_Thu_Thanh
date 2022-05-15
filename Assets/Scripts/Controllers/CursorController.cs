using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CursorController : MonoBehaviour
{

    public static CursorController instance;
    public Texture2D cursor;
    public ParticleSystem particleSystemClick;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cursor!=null)
        {
            ActivateCursor();
        }

    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    Vector3 worldPosition = ray.origin;
        //    worldPosition.z = particleSystemClick.transform.localPosition.z;
        //    if (particleSystemClick != null)
        //    {
        //        particleSystemClick.transform.position = worldPosition;
        //    }
        //}
    }

    public void ActivateCursor()
    {
        CursorMode mode = CursorMode.ForceSoftware;
        UnityEngine.Cursor.SetCursor(cursor, Vector2.zero, mode);
    }
}
