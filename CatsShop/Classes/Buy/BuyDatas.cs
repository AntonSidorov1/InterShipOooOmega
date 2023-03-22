namespace CatsShop.Classes.Buy
{
    /// <summary>
    /// Покупка
    /// </summary>
    public class BuyDatas
    {
        int positionID = 0;

        /// <summary>
        /// ID купленной позиции
        /// </summary>
        public int PozitionID
        {
            get { return positionID; }
            set { positionID = value; }
        }
    }
}
