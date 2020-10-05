using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    Repeater _hor = new Repeater("Horizontal");
    Repeater _ver = new Repeater("Vertical");

    public static event EventHandler<InfoEventArgs<Point>> moveEvent;
    public static event EventHandler<InfoEventArgs<int>> fireEvent;

    string[] _buttons = new string[] {"Fire1", "Fire2", "Fire3"};


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update () 
    {
        int x = _hor.Update();
        int y = _ver.Update();
        if (x != 0 || y != 0)
        {
            if (moveEvent != null)
            {
                moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
            }
        }
        for (int i = 0; i < 3; ++i)
        {
            if (Input.GetButtonUp(_buttons[i]))
            {
                if (fireEvent != null)
                fireEvent(this, new InfoEventArgs<int>(i));
            }
        }
    }

    class Repeater
    {
        const float threshold = 0.5f;
        const float rate = 0.25f;
        float _next;
        bool _hold;
        string _axis;
        public Repeater (string axisName)
        {
            _axis = axisName;
        }
        public int Update ()
        {
            int retValue = 0;
            int value = Mathf.RoundToInt( Input.GetAxisRaw(_axis) );
            if (value != 0)
            {
            if (Time.time > _next)
            {
                retValue = value;
                _next = Time.time + (_hold ? rate : threshold);
                _hold = true;
            }
            }
            else
            {
            _hold = false;
            _next = 0;
            }
            return retValue;
        }
    }
}
