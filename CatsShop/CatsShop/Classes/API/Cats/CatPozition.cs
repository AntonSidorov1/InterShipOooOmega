namespace CatsShop
{
    public class CatPozition : Cat
    {
        DateTime dateAdded = DateTime.Now;

        DateTime dateUpdated = DateTime.Now;

        int id = 0;

        public int ID
        {
            get => id; set => id = value;
        }


        public DateTime DateUpdated
        {
            get => dateUpdated;
            set
            {
                dateUpdated = value > DateAdded? value : DateAdded;
            }
        }


        public DateTime DateAdded
        {
            get => dateAdded;
            set
            {
                dateAdded = value < DateTime.Now ? value : DateTime.Now;
                if (dateUpdated < DateAdded)
                    DateUpdated = DateTime.Now;
            }
        }

        public override int Age
        {
            get => base.Age; 
            set
            {
                base.Age = value;
                RunChangeIfIsChanging();
            }
        }

        public override string Color
        {
            get => base.Color;
            set
            {
                base.Color = value;
                RunChangeIfIsChanging();
            }
        }

        public override string Species 
        { get => base.Species;
            set
            {
                base.Species = value;
                RunChangeIfIsChanging();
            }
        }

        public override string Gender
        {
            get => base.Gender; set
            {
                base.Gender = value;
                RunChangeIfIsChanging();
            }
        }

        public override decimal Price 
        { 
            get => base.Price;
            set
            {
                base.Price = value;
                RunChangeIfIsChanging();
            }
        }

        bool changing = true;

        public bool IsChanging() => changing;
        public void SetChanging(bool value = true) => changing = value;

        public void RunChange() => DateUpdated = DateTime.Now;

        public void RunChangeIfIsChanging()
        {
            if(IsChanging())
                DateUpdated = DateTime.Now;
        }
    }
}
