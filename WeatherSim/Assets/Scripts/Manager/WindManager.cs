using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindManager {
    public enum Direction {LEFT, RIGHT,DEFAULT};
    private Direction direction;
    private float Scale;
    public static WindManager Instance {
        get {
            if (WindManager.instance == null)
            {
                instance = new WindManager();
            }
            return instance;
        }
    }
    private static WindManager instance;

    private WindManager() {
        this.direction = Direction.DEFAULT;
        this.Scale = 0;
    }
    public Direction GetDirection()
    {
        return this.direction;
    }
    public float GetScale()
    {
        return this.Scale;
    }
    public void SetWind(Direction direction, float Scale) {
        this.Scale = Scale;
        this.direction = direction;
    }
}
