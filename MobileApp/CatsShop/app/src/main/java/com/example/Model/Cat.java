package com.example.Model;

import com.example.Configuration.FormatClass;

import java.util.Date;

public class Cat {
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
}
