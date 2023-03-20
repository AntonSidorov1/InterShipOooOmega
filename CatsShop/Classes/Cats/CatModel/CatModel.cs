namespace CatsShop.Classes.Cats.CatModel
{
    /// <summary>
    /// Модель котика
    /// </summary>
    public class CatModel : CatModelDatas
    {
        private int id = 0;

        /// <summary>
        /// ID модели
        /// </summary>
        public int ID
        {
            get => id;
            set => id = value;
        }


        public CatModel CopyWithID() => CopyWithID(this.ID);

        public CatModelFullDatas GetFullDatas() => GetFullDatasWithID(ID);
    }
}