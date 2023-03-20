namespace CatsShop.Classes.Cats.Cats
{
    /// <summary>
    /// Котик
    /// </summary>
    public class Cat : CatDatas
    {
        public Cat() { }

        public Cat(CatDatas cat) 
        {
            Age = cat.Age;
            ModelID = cat.ModelID;
        }

        int id = 0;

        /// <summary>
        /// ID котика
        /// </summary>
        public int ID
        {
            get => id; set => id = value;
        }

        public Cat CopyAll() => CopyWithID(ID);

        
    }
}
