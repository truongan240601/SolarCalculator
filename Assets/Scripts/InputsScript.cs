using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Innovative.SolarCalculator;

public class InputsScript : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] TMP_InputField latitudeInput;
    [SerializeField] TMP_InputField longitudeInput;
    [SerializeField] TMP_InputField dayInput;
    [SerializeField] TMP_InputField monthInput;
    [SerializeField] TMP_InputField timeInput;
    [SerializeField] Slider cameraSlide;

    [Header("Outputs")]
    [SerializeField] Transform sun;
    [SerializeField] Transform cam;
    [SerializeField] TextMeshProUGUI statsTMP;

    float latitude = 0;
    float longitude = 0;
    public DateTime dateTime;


    // Start is called before the first frame update
    void Awake()
    {
        latitudeInput.text = "0";
        longitudeInput.text = "0";
        dayInput.text = "1";
        monthInput.text = "1";
        timeInput.text = "09:00";

        latitudeInput.onEndEdit.AddListener(LatitudeOnValueChanged);
        longitudeInput.onEndEdit.AddListener(LongitudeOnValueChanged);
        dayInput.onEndEdit.AddListener(DayOnValueChanged);
        monthInput.onEndEdit.AddListener(MonthOnValueChanged);
        timeInput.onEndEdit.AddListener(TimeOnEndEdit);
        cameraSlide.onValueChanged.AddListener(CameraOnValueChanged);


        dateTime = new DateTime(2001, 1, 1, 9, 0, 0, DateTimeKind.Unspecified);
        //dateTime.AddDays(1);
        //dateTime.AddMonths(1);
        //dateTime.AddHours(0);

        SolarCal();
    }

    void LatitudeOnValueChanged(string value)
    {
        float lat = int.Parse(value);
        lat = Mathf.Clamp(lat, -90, 90);
        latitudeInput.text = lat.ToString();
        latitude = lat;
        SolarCal();
    }
    void LongitudeOnValueChanged(string value)
    {
        float lon = int.Parse(value);
        lon = Mathf.Clamp(lon, -180, 180);
        longitudeInput.text = lon.ToString();
        longitude = lon;
        SolarCal();
    }
    void DayOnValueChanged(string value)
    {
        int day = int.Parse(value);

        day = Mathf.Clamp(day, 1, DateTime.DaysInMonth(2001, dateTime.Month));
        dayInput.text = day.ToString();
        dateTime = new DateTime(dateTime.Year, dateTime.Month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Unspecified);
        SolarCal();
    }
    void MonthOnValueChanged(string value)
    {
        int month = int.Parse(value);
        int day = dateTime.Day;
        //format month
        month = Mathf.Clamp(month, 1, 12);
        monthInput.text = month.ToString();
        //format day of month
        day = Mathf.Clamp(day, 1, DateTime.DaysInMonth(2001, month));
        dayInput.text = day.ToString();
        //set datetime
        dateTime = new DateTime(dateTime.Year, month, day, dateTime.Hour, dateTime.Minute, dateTime.Second, DateTimeKind.Unspecified);
        SolarCal();
    }

    void CameraOnValueChanged(float value)
    {
        cam.rotation = Quaternion.Euler(0, value, 0);
    }

    void TimeOnEndEdit(string value)
    {
        string[] parts = value.Split(':');
        int hour = 0;
        int minute = 0;
        if (parts.Length == 2)
        {
            hour = int.Parse(parts[0]);
            minute = int.Parse(parts[1]);
        }
        else if (parts.Length == 1)
        {
            hour = int.Parse(parts[0]);
            minute = 0;
        }

        hour = Mathf.Clamp(hour, 0, 23);
        minute = Mathf.Clamp(minute, 0, 59);
        timeInput.text = string.Format("{0:00}:{1:00}", hour, minute);

        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, dateTime.Second, DateTimeKind.Unspecified);
        SolarCal();
    }



    static int DetermineTimeZone(double longitude)
    {
        if (longitude < 0)
            return (int)((longitude - 7.5) / 15);
        return (int)((longitude + 7.5) / 15);
    }

    public void SolarCal()
    {
        //TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        SolarTimes solarTimes = new SolarTimes(dateTime, DetermineTimeZone(longitude), latitude, longitude);

        string stats = string.Format("Mặt trời mọc: {0} <br>Mặt trời lặng: {1} <br>Az/El(in °): {2}°/{3}° ", solarTimes.Sunrise.ToString("HH:mm:ss"), solarTimes.Sunset.ToString("HH:mm:ss"), solarTimes.SolarAzimuth.Degrees, solarTimes.SolarElevation.Degrees);
        statsTMP.text = stats;

        Debug.Log("datetime: " + dateTime);
        Debug.Log("time zone: " + solarTimes.TimeZoneOffset);
        Debug.Log(solarTimes.SolarElevation.Degrees + "-----------" + solarTimes.SolarAzimuth.Degrees);
        sun.rotation = Quaternion.Euler(solarTimes.SolarElevation.Degrees, solarTimes.SolarAzimuth.Degrees, 0);

    }

    public void ExitClick()
    {
        Application.Quit();
    }


}
