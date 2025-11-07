using System;
using UnityEngine;

public class Juhyuck : MonoBehaviour   // 클래스 시작
{
    public float cooldownSeconds = 3600f;
    DateTime nextAvailableTime;
    const string NextTimeKey = "NextPixelAvailableTime";

    void Start()
    {
        string saved = PlayerPrefs.GetString(NextTimeKey, "");

        if (string.IsNullOrEmpty(saved))
        {
            nextAvailableTime = DateTime.MinValue;
        }
        else
        {
            nextAvailableTime = DateTime.Parse(
                saved,
                null,
                System.Globalization.DateTimeStyles.RoundtripKind
            );
        }
    }

    bool CanPlacePixel()
    {
        return DateTime.UtcNow >= nextAvailableTime;
    }

    // 다른 스크립트나 UI 버튼에서 호출할 수 있게 public 으로 변경
    public void TryPlacePixel(int x, int y)
    {
        if (!CanPlacePixel())
        {
            TimeSpan remain = nextAvailableTime - DateTime.UtcNow;
            Debug.Log($"아직 픽셀 못 찍어요. 남은 시간: {remain.Minutes}분 {remain.Seconds}초");
            return;
        }

        // 여기서 실제 보드에 픽셀 찍는 함수를 호출
        PlacePixelOnBoard(x, y);

        nextAvailableTime = DateTime.UtcNow.AddSeconds(cooldownSeconds);

        PlayerPrefs.SetString(NextTimeKey, nextAvailableTime.ToString("o"));
        PlayerPrefs.Save();
    }

    // 새로 추가한 함수: 실제로 (x, y)에 픽셀 찍는 부분
    void PlacePixelOnBoard(int x, int y)
    {
        Debug.Log($"({x}, {y}) 에 픽셀 찍음!");
    }
}
