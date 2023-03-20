namespace CatsShop.Classes.Cats.CatModel
{
    /// <summary>
    /// Модель котика (текстовые значения данных)
    /// </summary>
    public class CatModelDatasName
    {
        string gender = "";

        /// <summary>
        /// Пол котика
        /// </summary>
        public string Gender
        {
            get => gender; set => gender = value;
        }

        string color = "";

        /// <summary>
        /// Пол котика
        /// </summary>
        public string Color
        {
            get => color; set => color = value;
        }

        string species = "";

        /// <summary>
        /// Порода котика
        /// </summary>
        public string Species
        {
            get => species; set => species = value;
        }

        public CatModelFullDatas Copy()
        {
            CatModelFullDatas fullDatas = new CatModelFullDatas();
            fullDatas.Color = Color;
            fullDatas.Species = Species;
            fullDatas.Gender = Gender;
            return fullDatas;
        }

    }
}
