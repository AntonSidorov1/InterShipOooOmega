package com.example.Configuration;

import android.view.View;

public class FormatClass {

    public static int GetVisibleByBool(boolean visible)
    {
        if(visible)
            return View.VISIBLE;
        else
            return View.INVISIBLE;
    }

    public static int GetNoVisibleByBool(boolean noVisible)
    {
        return GetVisibleByBool(!noVisible);
    }
}
