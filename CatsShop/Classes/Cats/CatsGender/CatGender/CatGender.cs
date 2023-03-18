namespace CatsShop.Classes.Cats.CatsGender.CatGender
{
    /// <summary>
    /// Пол котика
    /// </summary>
    public class CatGender
    {
        private int id = 0;

        /// <summary>
        /// ID пола
        /// </summary>
        public int ID
        {
            get => id;
            set => id = value;
        }

        private string name = "";

        /// <summary>
        /// Название пола
        /// </summary>
        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}