using System.Collections;
using System; 
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro; 
public class AnimationController : MonoBehaviour
{
    public enum AnimationType
    {
        PunchScale,
        MoveY
    }

    public static AnimationController Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Taro gameobject yang mau dianimate sebagai parameter, trus jalanin. 
    //Pastiin kalo mau animate terus objectnya dihilangkan / mau pindah scene, jalanin func animate baru 
    //jalanin fungsi pindah/hilang pake invoke
    public static void AnimateButtonPush(GameObject obj)
    { //animate squish seperti pencet tombol
        obj.transform.DORewind();

        float vectorX = obj.transform.localScale.x * -0.25f;
        float vectorY = obj.transform.localScale.y * -0.25f;
        float vectorZ = obj.transform.localScale.z * -0.25f;

        obj.transform.DOPunchScale(new Vector3(vectorX, vectorY, vectorZ), 0.2f, 1, 1);
    }

    public void AnimateButtonPush(GameObject obj, Action act)
    {
        obj.transform.DORewind();

        float vectorX = obj.transform.localScale.x * -0.1f;
        float vectorY = obj.transform.localScale.y * -0.1f;
        float vectorZ = obj.transform.localScale.z * -0.1f;

        obj.transform.DOPunchScale(new Vector3(vectorX, vectorY, vectorZ), 0.1f, 1, 1)
            .OnComplete(() => act());
    }

    public static void AnimatePopUp(GameObject obj)
    { //animate expand kebalikan dari pencet tombol
        obj.transform.DORewind();

        float vectorX = obj.transform.localScale.x * 0.25f;
        float vectorY = obj.transform.localScale.y * 0.25f;
        float vectorZ = obj.transform.localScale.z * 0.25f;

        obj.transform.DOPunchScale(new Vector3(vectorX, vectorY, vectorZ), 0.3f, 1);
    }
    public static void AnimatePopUpDisappear(GameObject obj)
    { //animate shrinking menjadi super kecil (belom menghilang)
        obj.transform.DORewind();

        float vectorX = obj.transform.localScale.x * -0.99f;
        float vectorY = obj.transform.localScale.y * -0.99f;
        float vectorZ = obj.transform.localScale.z * -0.99f;

        obj.transform.DOPunchScale(new Vector3(vectorX, vectorY, vectorZ), 0.6f, 1);
    }


    public static void AnimateButtonJump(GameObject obj)
    {
        obj.transform.DOPunchPosition(new Vector3(0, 5f), 1f, 0, 1f).SetLoops(-1);
    }

    // Animate group of objects based on the given parameter (duration & animationType)
    public IEnumerator AnimateObjects(GameObject[] objects, float duration, AnimationType type, float target, float from)
    {
        foreach (var obj in objects)
        {
            // Set parent to active if it's inactive
            if (obj.transform.parent != null && !obj.transform.parent.gameObject.activeSelf)
            {
                var parentObj = obj.transform.parent.gameObject;
                parentObj.SetActive(true);
            }

            obj.SetActive(true);

            switch (type)
            {
                case AnimationType.MoveY:
                    MoveY(obj, target, from);
                    break;
                case AnimationType.PunchScale:
                    PunchScale(obj);
                    break;
            }
            yield return new WaitForSeconds(duration);
        }
    }

    public IEnumerator AnimateObjects(List<GameObject> objects, float duration, AnimationType type, float target, float from)
    {
        foreach (var obj in objects)
        {
            // Set parent to active if it's inactive
            if (obj.transform.parent != null && !obj.transform.parent.gameObject.activeSelf)
            {
                var parentObj = obj.transform.parent.gameObject;
                parentObj.SetActive(true);
            }

            obj.SetActive(true);

            switch (type)
            {
                case AnimationType.MoveY:
                    MoveY(obj, target, from);
                    break;
                case AnimationType.PunchScale:
                    PunchScale(obj);
                    break;
            }
            yield return new WaitForSeconds(duration);

        }
    }

    public void PunchScale(GameObject obj)
    {
        obj.transform.DORewind();
        obj.transform.DOPunchScale(new Vector3(0.25f, 0.25f, 0.25f), 0.2f, 1, 1);
    }

    public void LocalMoveY(GameObject obj, float move, float duration)
    {
        obj.transform.DOLocalMoveY(move, duration);
    }

    public void MoveY(GameObject obj, float target, float from, bool loop = false)
    {
        var post = obj.transform.position;
        obj.SetActive(true);
        if (loop)
            obj.transform.DOMoveY(post.y + target, 0.75f).SetEase(Ease.InOutQuad).From(post.y + from).SetLoops(-1, LoopType.Yoyo);

        else
            obj.transform.DOMoveY(post.y + target, 0.75f).SetEase(Ease.InOutQuad).From(post.y + from);
    }

    public void MoveX(GameObject obj, float target, float from, bool loop = false)
    {
        var post = obj.transform.position;
        obj.SetActive(true);
        if (loop)
            obj.transform.DOMoveX(post.x + target, 0.75f).SetEase(Ease.InOutQuad).From(post.x + from).SetLoops(-1, LoopType.Yoyo);

        else
            obj.transform.DOMoveX(post.x + target, 0.75f).SetEase(Ease.InOutQuad).From(post.x + from);
    }

    public void MoveCameraX(Camera obj, float target, float from, bool loop = false)
    {// used to move the camera along the x axis with duration 0.75 seconds
        var post = obj.transform.position;
        //obj.SetActive(true);
        if (loop)
            obj.transform.DOMoveX(target, 0.75f).SetEase(Ease.InOutQuad).From(from).SetLoops(-1, LoopType.Yoyo);

        else
            obj.transform.DOMoveX(target, 0.75f).SetEase(Ease.InOutQuad).From(from);
    }
}
