using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageDetection : MonoBehaviour
{
    ARTrackedImageManager imageManager;

    void Awake()
    {
        imageManager = GetComponent<ARTrackedImageManager>();
        imageManager.trackedImagesChanged += OnImageTrackedEvent;       
    }

    private void OnImageTrackedEvent(ARTrackedImagesChangedEventArgs args)
    {
        foreach (ARTrackedImage trackedImage in args.added)
        {          
            //Reference Image Library로 접근
            string imageName = trackedImage.referenceImage.name;

            //빈 오브젝트에 Resources에서 이름이 같은 모델 불러와 저장
            GameObject prefab = Resources.Load<GameObject>($"AR_Model/{imageName}");

            if (prefab != null)
            {
                GameObject obj = Instantiate(prefab); //prefab생성
                obj.transform.SetParent(trackedImage.transform); //이미지의 자식으로 들어간다
            }
        }

        foreach (ARTrackedImage trackedImage in args.updated)
        {
            if (trackedImage.trackingState == TrackingState.None) //이미지가 더 이상 추적되지 않는 경우
            {
                if (trackedImage.transform.childCount > 0) //자식 오브젝트가 존재하면
                {
                    Destroy(trackedImage.transform.GetChild(0));//파괴
                }
            }
            else//이미지가 추저되는 경우
            {
                if (trackedImage.transform.childCount > 0)//자식 오브젝트가 존재하면
                {
                    //생성 위치, 회전값
                    trackedImage.transform.GetChild(0).position = trackedImage.transform.position - (Vector3.up * 0.1f);
                    trackedImage.transform.GetChild(0).rotation = trackedImage.transform.rotation;
                }
            }
        }
    }

}
