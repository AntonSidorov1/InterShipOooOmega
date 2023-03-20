namespace CatsShop.Classes.Position
{
    /// <summary>
    /// Данные позиции котика
    /// </summary>
    public class PozitionDatas
    {
        public PozitionDatas()
        {
            GetPosition();
        }

        public virtual void GetPosition()
        {

        }

        public virtual void SetChangePosition() { }



        decimal cost = 0;
        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Cost
        {
            get => cost; set
            {
                cost = value;
                SetChangePosition();
            }
        }

        int catID = 0;
        /// <summary>
        /// ID котика
        /// </summary>
        public int CatID
        {
            get => catID;
            set
            {
                catID = value;
                SetChangePosition();
            }
        }

        public PozitionDatas Copy()
        {
            PozitionDatas position = new PozitionDatas();
            position.Cost = Cost;
            position.catID = CatID;
            return position;
        }

        public Pozition CopyWithID(int id) => new Pozition(id, this);

        public PozitionWithDates PositionWithDates(int id) => new PozitionWithDates(CopyWithID(id));
    }
}
