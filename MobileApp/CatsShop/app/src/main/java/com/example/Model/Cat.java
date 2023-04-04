package com.example.Model;

import com.example.Configuration.FormatClass;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;

public class Cat {

    public Cat()
    {

    }

    public Cat(String json)
    {
        this();
        SetJson(json);
    }

    public static Cat GetFromJson(String json)
    {
        return new Cat(json);
    }

    public int ID = 0;
    public String Gender = "";
    public String Color = "";
    public String Species = "";
    public String DateAdded = "";
    public String DateUpdated = "";
    public double Price = 0;

    public String GetPrice()
    {
        return String.format("%.2f р", Price);
    }

    public int Age = 0;

    public String GetAge()
    {
        int count = Age % 100;
        if(count > 4 && count < 21)
        {
            return Age + " лет";
        }
        count %= 10;
        if(count == 1)
        {
            return Age + " год";
        }
        else if(count > 1 && count < 5)
        {
            return Age + " года";
        }
        else
        {
            return Age + " лет";
        }
    }

    public void DatesChangeFormat()
    {
        DateAdded = FormatClass.FormatDate(DateAdded);
        DateUpdated = FormatClass.FormatDate(DateUpdated);
    }


    public String Datas()
    {
        String Color = this.Color, Species = this.Species;
        int color = Math.min(15, Color.length());
        int species = Math.min(15, Species.length());
        Color = Color.substring(0, color);
        Species = Species.substring(0, species);
        if(color < this.Color.length())
        {
            Color += "...";
        }
        if(species < this.Species.length())
        {
            Species += "...";
        }

        return String.valueOf(ID) + ") ==================== \n" +
                "Цвет - " + Color + "\n" +
                "Порода - " + Species + "\n" +
                "Пол - " + Gender + " \n " +
                "Возраст - " + GetAge() + " \n" +
                String.format("Цена - %.2f р", Price);
    }

    @Override
    public String toString() {
        return Datas();
    }

    public String GetJsonWithOutID()
    {
        return GetJson(false);
    }

    public String GetJsonWithID()
    {
        return GetJson(true);
    }

    public String GetJson(boolean needID)
    {
        String result = "";
        try {
            JSONObject object = new JSONObject();
            if (needID) {

                try {
                    object.put("id", ID);
                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
            try {
                object.put("age", Age);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            try {
                object.put("color", Color);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            try {
                object.put("gender", Gender);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            try {
                object.put("species", Species);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            try {
                object.put("price", Price);
            } catch (JSONException e) {
                e.printStackTrace();
            }
            result = object.toString();
        }
        catch (Exception e)
        {

        }
        return result;
    }

    public String GetJsonWithDates(boolean needID)
    {
        String result = GetJson(needID);

        try {
            JSONObject object = new JSONObject(result);
            try
            {
                object.put("dateAdded", DateAdded);
            }
            catch (Exception e)
            {

            }
            try
            {
                object.put("dateUpdated", DateUpdated);
            }
            catch (Exception e)
            {

            }
            result = object.toString();
        } catch (JSONException e) {
            e.printStackTrace();
        }

        return result;
    }


    public void SetJson(String json)
    {
        try {
            JSONObject object = new JSONObject(json);
            Age = object.getInt("age");
            Color = object.getString("color");
            Species = object.getString("species");
            Gender = object.getString("gender");
            Price = object.getDouble("price");
            try
            {
                ID = object.getInt("id");
            }
            catch (Exception e)
            {

            }
            try
            {
                DateAdded = object.getString("dateAdded");
            }
            catch (Exception e)
            {

            }
            try
            {
                DateUpdated = object.getString("dateUpdated");
            }
            catch (Exception e)
            {

            }
            try
            {
                DatesChangeFormat();
            }
            catch (Exception e)
            {

            }
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    public Cat CopyByJson()
    {
        String json = GetJsonWithDates(true);
        return GetFromJson(json);
    }

    public Gender GetGender(GendersList genders)
    {
        return genders.get(Gender);
    }

    public void SetGender(Gender gender)
    {
        Gender = gender.Name;
    }

    public void SetGenderByIndex(GendersList genders, int index)
    {
        SetGender(genders.get(index));
    }

    public void SetGenderByID(GendersList genders, int genderID)
    {
        SetGender(genders.get(genderID, true));
    }
}
