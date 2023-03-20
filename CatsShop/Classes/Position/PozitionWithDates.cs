namespace CatsShop.Classes.Position
{
    /// <summary>
    /// Позиция котика с датами добавления и изменения
    /// </summary>
    public class PozitionWithDates : Pozition
    {
        public PozitionWithDates()
        {
        }

        public PozitionWithDates(PozitionDatas data) : base(data)
        {
        }

        public PozitionWithDates(int catID, decimal cost) : base(catID, cost)
        {
        }

        public PozitionWithDates(int id, PozitionDatas data) : base(id, data)
        {
        }

        public PozitionWithDates(Pozition position)
            : this(position.ID, position)
        {

        }


        DateTime dateAdded;
        /// <summary>
        /// Дата добавления позиции
        /// </summary>
        public DateTime DateAdded
        {
            get => dateAdded; set
            {
                dateAdded = value;
                SetChangePosition();
            }
        }

        DateTime dateOfChanged;
        /// <summary>
        /// Дата изменения позиции
        /// </summary>
        public DateTime DateOfChanged
        {
            get => dateOfChanged; set => dateOfChanged = value;
        }

        public override void GetPosition()
        {
            DateAdded = DateTime.Now;
        }

        public override void SetChangePosition()
        {
            DateOfChanged = DateTime.Now;
        }
    }
}
