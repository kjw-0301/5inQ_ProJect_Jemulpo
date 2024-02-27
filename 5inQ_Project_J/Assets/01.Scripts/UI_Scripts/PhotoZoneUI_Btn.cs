using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class PhotoZoneUI_Btn : MonoBehaviour
{
    [Header("촬영 모드")]
    [SerializeField] Text photoText;
    [SerializeField] Text videoText;

    [SerializeField] private GameObject CameraBtn; 
    [SerializeField] private GameObject RecordBtn; 
    [SerializeField] private GameObject RecordDoneBtn;
    [SerializeField] private GameObject SwipeAnime; //animator를 가져오기 위한 GameObject

    Animator animator;

    Color Highlightcolor = new Color(0, 0.6f, 1);
    Color Normalcolor = new Color(0.5f, 0.5f, 0.5f, 0.8f);

    void Awake()
    {
        animator = SwipeAnime.GetComponent<Animator>();
    }

    void Start()
    {
        HighlightPhotoText(); //사진 텍스트 하이라이트
        NormalVideoText();    //비디오 텍스트 노말
        CameraBtn.SetActive(true);
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

    }



    public void OnPhotoBtn()
    {
        if (CameraMode.isPhoto == false)//애니메이션 반복되지 않기 위한 조건
        {
            animator.SetTrigger("doRight"); //애니메이션 활성화
        }

        //버튼을 누르면 사진 모드가 켜지고 그에 맞는 텍스트 변경 
        CameraMode.isPhoto = true;
        CameraMode.isVideo = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;
        HighlightPhotoText();
        NormalVideoText();

        //사진 모드 버튼 활성화/ 비디오 모드 버튼 비활성화
        CameraBtn.SetActive(true);   
        RecordBtn.SetActive(false);
        RecordDoneBtn.SetActive(false);

    }

    public void OnVideoBtn()
    {
        if (CameraMode.isVideo == false)//애니메이션 반복되지 않기 위한 조건
        {
            animator.SetTrigger("doLeft"); //애니메이션 활성화
        }

        //버튼 누르면 비디오 모드가 켜지고 그에 맞는 텍스트 변경
        CameraMode.isVideo = true;
        CameraMode.isPhoto = false;
        CameraMode.isRecord = false;
        CameraMode.isRecordDone = false;

        HighlightVideoText();
        NormalPhotoText();

        //비디오 모드 버튼 활성화/ 사진 모드 버튼 비활성화
        CameraBtn.SetActive(false);
        RecordBtn.SetActive(true);
        RecordDoneBtn.SetActive(false);
    }

    public void OnGalleryBtn()
    {
        // READ_EXTERNAL_STORAGE 권한이 이미 부여되었는지 확인
        if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageRead))
        {
            // 권한이 없다면, 권한 요청
            UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.ExternalStorageRead);

            if (UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.ExternalStorageRead))
            {
                LoadGallery();
            }
        }
        else
        {
            LoadGallery();
        }
    }
    
    void LoadGallery()
    {
        AndroidJavaClass javaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject javaObject = javaClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = javaObject.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", "com.sec.android.gallery3d");
        javaObject.Call("startActivity", intent);
    }

    #region TextStyle
    //하이라이트될 때의 텍스트와 기본 상태의 텍스트를 Photo와 Video를 나누어 작성
    public void HighlightPhotoText()
    {
        photoText.fontStyle = FontStyle.Bold;
        photoText.DOColor(Highlightcolor, 0);
    }

    public void NormalPhotoText()
    {
        photoText.fontStyle = FontStyle.Normal;
        photoText.DOColor(Normalcolor, 0);
    }

    public void HighlightVideoText()
    {
        videoText.fontStyle = FontStyle.Bold;
        videoText.DOColor(Highlightcolor, 0);
    }

    public void NormalVideoText()
    {
        videoText.fontStyle = FontStyle.Normal;
        videoText.DOColor(Normalcolor, 0);
    }

    #endregion
}
