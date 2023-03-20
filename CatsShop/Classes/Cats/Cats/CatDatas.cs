namespace CatsShop.Classes.Cats.Cats
{
    /// <summary>
    /// Данные котика
    /// </summary>
    public class CatDatas
    {

        int age = 0;
        /// <summary>
        /// Возраст котика
        /// </summary>
        public int Age
        {
            get => age; set => age = value;
        }

        int modelID = 0;
        /// <summary>
        /// ID медели котика
        /// </summary>
        public int ModelID
        {
            get => modelID; set => modelID = value;
        }

        public CatDatas Copy()
        {
            CatDatas cat = new CatDatas();
            cat.Age = age;
            cat.ModelID = modelID;
            return cat;
        }

        public Cat CopyWithID(int id)
        {
            Cat cat = new Cat(this);
            cat.ID = id;
            return cat;
        }

    }
}
