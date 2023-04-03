package com.example.Configuration;

import android.view.View;

import java.text.SimpleDateFormat;
import java.util.Date;

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


    public static String FormatDate(String date)
    {
        Date time = new Date(Date.parse(date));
        return new SimpleDateFormat("dd.MM.yyyy hh:mm:ss").format(time);
    }

}
