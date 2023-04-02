package com.example.Model;

public class Cat {
    public int ID = 0;
    public String Gender = "";
    public String Color = "";
    public String Species = "";
    public String DateAdded = "";
    public String DateUpdated = "";
    public double Price = 0;
    public int Age = 0;

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

        return String.valueOf(ID) + " - " + Color + " - " + Species + " - " + Gender + " - " + String.format("0.00", Price);
    }

    @Override
    public String toString() {
        return Datas();
    }
}
