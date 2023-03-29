namespace CatsShop
{
    public class Cat
    {
        int age = 10;
        public virtual int Age
        {
            get => age; set => age = value;
        }

        string color = "Красный";

        public virtual string Color
        {
            get => color; set => color = value;
        }

        string species = "";

        public virtual string Species
        {
            get => species; set => species = value;
        }

        string gender = "ж";

        public virtual string Gender
        {
            get => gender; set => gender = value;
        }

        decimal price = 1000;

        public virtual decimal Price
        {
            get => price; set => price = value;
        }

        public Cat Copy()
        {
            return new Cat
            {
                Age = this.Age,
                Color = this.Color,
                Species = this.Species,
                Gender = this.Gender,
                Price = this.Price,
            };
        }

        public int GetGenderID() => GendersList.CreateGendersListFromDB().GetGenderFromName(Gender).ID;
    }
}
