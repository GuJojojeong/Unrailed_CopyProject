using System.Collections;
using UnityEngine;

public class BrakeCar : MonoBehaviour
{
    public Train train; // Train 클래스 참조

    public void ActivateBrake(float duration)
    {
        StartCoroutine(BrakeRoutine(duration));
    }

    private IEnumerator BrakeRoutine(float duration)
    {
        if (train != null)
        {
            float originalSpeed = train.TrainSpeed; // TrainSpeed 프로퍼티 사용
            train.TrainSpeed = originalSpeed / 2; // 속도를 절반으로 줄임
            yield return new WaitForSeconds(duration);
            train.TrainSpeed = originalSpeed; // 원래 속도로 복귀
        }
       
    }
}
