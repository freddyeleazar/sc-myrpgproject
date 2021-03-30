using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome
{
    public BiomeType biomeType;

    public float optimalTemperature;
    public Vector2 temperatureLimits;
    private Vector2 temperatureDeviationLimits;

    public float optimalMoisture;
    public Vector2 moistureLimits;
    private Vector2 moistureDeviationLimits;

    public int treeDensity;

    public Biome(BiomeType biomeType)
    {
        this.biomeType = biomeType;
        SetStats();
    }

    public void SetStats()
    {
        switch (biomeType)
        {
            case BiomeType.Tundra:
                optimalTemperature = 0;
                temperatureLimits = new Vector2(-50, 5);
                optimalMoisture = 50;
                moistureLimits = new Vector2(33, 66);
                treeDensity = 5;
                break;
            case BiomeType.Taiga:
                optimalTemperature = 10;
                temperatureLimits = new Vector2(5, 15);
                optimalMoisture = 50;
                moistureLimits = new Vector2(33, 66);
                treeDensity = 25;
                break;
            case BiomeType.TemperateForest:
                optimalTemperature = 20;
                temperatureLimits = new Vector2(-15, 25);
                optimalMoisture = 66 + 16.5f;
                moistureLimits = new Vector2(66, 100);
                treeDensity = 50;
                break;
            case BiomeType.TemperateGrassland:
                optimalTemperature = 20;
                temperatureLimits = new Vector2(15, 25);
                optimalMoisture = 33 + 16.5f;
                moistureLimits = new Vector2(33, 66);
                treeDensity = 15;
                break;
            case BiomeType.Shrubland:
                optimalTemperature = 20;
                temperatureLimits = new Vector2(-15, 15);
                optimalMoisture = 0 + 16.5f;
                moistureLimits = new Vector2(0, 33);
                treeDensity = 0;
                break;
            case BiomeType.XericShrubland:
                optimalTemperature = 20;
                temperatureLimits = new Vector2(15, 25);
                optimalMoisture = 0 + 16.5f;
                moistureLimits = new Vector2(0, 33);
                treeDensity = 0;
                break;
            case BiomeType.RainForest:
                optimalTemperature = 30;
                temperatureLimits = new Vector2(25, 50);
                optimalMoisture = 66 + 16.5f;
                moistureLimits = new Vector2(66, 100);
                treeDensity = 75;
                break;
            case BiomeType.Savanna:
                optimalTemperature = 30;
                temperatureLimits = new Vector2(25, 50);
                optimalMoisture = 33 + 16.5f;
                moistureLimits = new Vector2(33, 66);
                treeDensity = 8;
                break;
            case BiomeType.Desert:
                optimalTemperature = 30;
                temperatureLimits = new Vector2(25, 50);
                optimalMoisture = 0+16.5f;
                moistureLimits = new Vector2(0, 33);
                treeDensity = 5;
                break;
            default:
                break;
        }
        SetTemperatureDeviationLimits();
        SetMoistureDeviationLimits();
    }
    private void SetTemperatureDeviationLimits()
    {
        temperatureDeviationLimits = new Vector2(optimalTemperature - temperatureLimits.x, temperatureLimits.y - optimalTemperature);
    }

    private void SetMoistureDeviationLimits()
    {
        moistureDeviationLimits = new Vector2(optimalMoisture - moistureLimits.x, moistureLimits.y - optimalMoisture);
    }

    public bool IsSupported(float temperature, float moisture)
    {
        bool isSupported = false;
        if (temperature >= temperatureLimits.x && temperature <= temperatureLimits.y)
        {
            if (moisture >= moistureLimits.x && moisture <= moistureLimits.y)
            {
                isSupported = true;
            }
        }
        return isSupported;
    }

    public float GetTemperatureDeviation(float temperature)
    {
        float temperatureDeviation = optimalTemperature - temperature;
        if (temperature > optimalTemperature) temperatureDeviation *= -1;
        return temperatureDeviation;
    }

    public float GetMoistureDeviation(float moisture)
    {
        float moistureDeviation = optimalMoisture - moisture;
        if (moisture > optimalMoisture) moistureDeviation *= -1;
        return moistureDeviation;
    }

    private float GetTemperatureChances(float temperature)
    {
        float fTemperatureChances;
        if (temperature > optimalTemperature)
        {
            fTemperatureChances = GetTemperatureDeviation(temperature) / temperatureDeviationLimits.y;
        }
        else if (temperature == optimalTemperature)
        {
            fTemperatureChances = 0;
        }
        else
        {
            fTemperatureChances = GetTemperatureDeviation(temperature) / temperatureDeviationLimits.x;
        }
        return 1 - fTemperatureChances;
    }

    private float GetMoistureChances(float moisture)
    {
        float fMoistureChances;
        if (moisture > optimalMoisture)
        {
            fMoistureChances = GetMoistureDeviation(moisture) / moistureDeviationLimits.y;
        }
        else if (moisture == optimalMoisture)
        {
            fMoistureChances = 0;
        }
        else
        {
            fMoistureChances = GetMoistureDeviation(moisture) / moistureDeviationLimits.x;
        }
        return 1 - fMoistureChances;
    }

    public int GetGenerationChances(float temperature, float moisture)
    {
        float fGenerationChances = GetTemperatureChances(temperature) / 2 + GetMoistureChances(moisture) / 2;
        int generationChances = Mathf.FloorToInt(fGenerationChances * 100);
        return generationChances;
    }
}

public enum BiomeType
{
    Tundra,
    Taiga,
    TemperateForest,
    TemperateGrassland,
    Shrubland,
    XericShrubland,
    RainForest,
    Savanna,
    Desert
}