namespace CatsShop.Classes.Position
{
    /// <summary>
    /// Позиция котика
    /// </summary>
    public class Pozition : PozitionDatas
    {
        int id = 0;
        /// <summary>
        /// ID позиции
        /// </summary>
        public int ID
        {
            get => id; set
            {
                id = value;
                SetChangePosition();
            }
        }

        public Pozition()
        { }

        public Pozition(int catID, decimal cost) : this()
        {
            CatID = catID;
            Cost = cost;
        }

        public Pozition(PozitionDatas data) : this(data.CatID, data.Cost) { }

        public Pozition(int id, PozitionDatas data):this(data)
        {
            ID = id;
        }

        public Pozition CopyWithID() => CopyWithID(this.ID);

        public PozitionWithDates PositionWithDates() => PositionWithDates(ID);

    }
}
