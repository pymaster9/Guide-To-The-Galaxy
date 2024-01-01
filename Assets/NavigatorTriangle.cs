using UnityEngine;
using UnityEngine.UI;

public class NavigatorTriangle : MonoBehaviour
{
    public Vector3 pointtowards;
    public Vector3 lookingat;
    public Image left;
    public Image right;
    public Image up;
    public Image down;
    public float clearence;
    public static NavigatorTriangle main;
    public float advancerange;
    public bool onnavinstruction;
    private void Start()
    {
        main = this;
    }
    private void Update()
    {
        if (onnavinstruction)
        {
            Vector3 ptl = Camera.main.transform.InverseTransformPoint(pointtowards);
            ptl = ptl.normalized * 10;
            print(ptl);

                //LeftImage
                left.gameObject.SetActive(ptl.x < -clearence);

                //RightImage
                right.gameObject.SetActive(ptl.x > clearence);

                //UpImage
                up.gameObject.SetActive(ptl.y < -clearence);

                //DownImage
                down.gameObject.SetActive(ptl.y > clearence);

                if (Mathf.Abs(ptl.x) <= clearence && Mathf.Abs(ptl.y) <= clearence)
                {
                    print("HERE");
                    TourGuide.main.RunStep();
                    onnavinstruction = false;
                }
        }
        else
        {
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            up.gameObject.SetActive(false);
            down.gameObject.SetActive(false);
        }
    }
}
